const IllusionModel = require('./models/illusion');
const _ = require('lodash');
const parse = require('csv-parse/lib/sync');
const TagModels = {
  elements: require('./models/element'),
  effects: require('./models/effect'),
};
const AllowedTagType = ['elements', 'effects'];

const getPopulatedTags = async (type='', getTopTagsOnly=false) => {
  const subTagName = `sub${type}`;
  const selectedColumns = `name _id ${subTagName} level iconURL`;
  return await TagModels[type].model
      .find( getTopTagsOnly?{level: 0}:{}, selectedColumns)
      .populate({
        path: subTagName,
        populate: {
          path: subTagName,
          select: selectedColumns,
        },
        select: selectedColumns,
      })
      .exec();
};
const getTagIDFinder = (typeStr, allCandidateArray) => {
  const subTagCol = `sub${typeStr}`;
  const allCandidates = allCandidateArray;
  return (unsplitedTagArray) => {
    const splitedTagArray = unsplitedTagArray.split('|');
    return _.uniq(splitedTagArray.map((splitedTag) => {
      const result = [];
      const levelTags = splitedTag.split('/');
      let curNode = allCandidates.find(
          (c)=> {
            return c.name === levelTags[0];
          },
      );
      for (let i=0; i<levelTags.length-1; i++) {
        if (curNode[subTagCol] !== undefined && curNode[subTagCol].length>0) {
          result.push(curNode._id);
          const candidates = allCandidates.filter(
              (c) => {
                return curNode[subTagCol].map((x) =>x._id).includes(c._id);
              },
          );
          curNode = candidates.find((c)=> c.name === levelTags[i+1]);
        }
      };
      if (curNode==undefined) {
        throw new Error(`A tag in ${splitedTag} not exists`);
      } else {
        result.push(curNode._id);
      };
      return result;
    }).reduce((acc, cur)=>acc.concat(...cur), []));
  };
};

module.exports = {
  // Controllers
  getAllTags:
    async (ctx, next) => {
      let res;
      const type = ctx.params.type;
      if (AllowedTagType.find((x) => x === type) === undefined) {
        throw new EvalError('Invalid Tag Type');
      }
      if (ctx.query.populate === 'true') {
        res = await getPopulatedTags(type, getTopTagsOnly=true);
      } else {
        res = await TagModels[type].model.find({}, 'name _id iconURL').exec();
      }
      return res;
    },
  searchByTagIDs: async (ctx, next) => {
    const type = ctx.params.type;
    if (AllowedTagType.find((x) => x === type) === undefined) {
      throw new EvalError('Invalid Tag Type');
    }
    const {
      tags: unsplitedTagArray,
    } = ctx.request.body;
    const splitedTagArray = unsplitedTagArray.map((row) => row.split('&'));
    const targetTags = [];

    while (splitedTagArray.length > 0) {
      const tags = splitedTagArray[0];
      if (tags[tags.length-1] === '') {
        tags.pop();
      }
      if (tags.length > 0) {
        targetTags.push(...tags);
      }
      splitedTagArray.shift();
    }
    const output = await IllusionModel.model
        .find({[type]: {$all: targetTags}}).exec();
    return Object.keys(output).map((key) => output[key]._id);
  },
  getAllEntry: async (ctx, next) => {
    if (ctx.query.extend === 'true') {
      return await IllusionModel.model.find()
          .populate('effects', 'name _id iconURL')
          .populate('elements', 'name _id iconURL')
          .exec();
    } else {
      return await IllusionModel.model.find({}, '_id title').exec();
    }
  },
  getEntryById: async (ctx, next) => {
    const res = await IllusionModel.model
        .findOne({_id: ctx.params.id})
        .populate('effects', 'name _id iconURL')
        .populate('elements', 'name _id iconURL')
        .exec();
    res.gifFileName = `/gifs/${res.gifFileName}`;
    return res;
  },
  insertNewEntry: async (ctx, next) => {
    const illusion = new IllusionModel.model();
    const {
      title: title,
      gifFileName: gifFileName,
      refURL: refURL,
      summary: summary,
      content: content,
      elements: unsplitedElementArray,
      effects: unsplitedEffectArray,
    } = ctx.request.body;
    const attrArrays = {
      elementsArray: unsplitedElementArray.map((line) => line.split('&')),
      effectsArray: unsplitedEffectArray.map((line) => line.split('&')),
    };
    console.log(attrArrays);

    illusion.title = title;
    illusion.gifFileName = gifFileName;
    illusion.refURL = refURL;
    illusion.summary = summary;
    illusion.content = content;

    await illusion.assignAttributes(attrArrays);
  },
  batchImport: async (ctx, next) => {
    const entryArray = parse(ctx.request.body, {
      delimiter: '\t',
      columns: true,
    });
    const allElements = await getPopulatedTags('elements');
    const allEffects = await getPopulatedTags('effects');
    const results = entryArray.map((entry) => {
      const newIllusion = {};
      const effectFinder = getTagIDFinder('effects', allEffects);
      const elementFinder = getTagIDFinder('elements', allElements);

      newIllusion.title = entry.title;
      newIllusion.gifFileName = entry['GIF檔名'];
      newIllusion.refURL = entry['URLs'];
      newIllusion.summary = entry.summary;
      try {
        newIllusion.effects = effectFinder(entry.effect);
        newIllusion.elements = elementFinder(entry.element);
        return newIllusion;
      } catch (e) {
        console.log(e);
        delete newIllusion;
        return {};
      };
    });
    return await IllusionModel.model.insertMany(results);
  },
};
