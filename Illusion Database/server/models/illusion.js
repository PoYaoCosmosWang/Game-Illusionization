const mongoose = require('mongoose');
const schemas = {
  elements: require('./element.js'),
  effects: require('./effect.js'),
};

const cheakAttrExists = async (illusionInstance, attrName, attrArray) => {
  for (let i=0; i<attrArray.length; i++) {
    const attrs = attrArray[i];
    console.log(`Attach ${attrName}: ${attrs}`);
    const foundAttrs = await Promise.all(attrs.map( async (id) => {
      const foundAttr = await schemas[attrName].model
          .findOne({_id: id}, '_id').exec();
      if (!foundAttr) {
        throw new ReferenceError(`Invalid ${attrName} ID: "${a}"`);
      }
      return foundAttr._id;
    }));
    console.log(foundAttrs);
    return;
  }
};

const illusionSchema = new mongoose.Schema({
  title: String,
  gifFileName: String,
  refURL: String,
  summary: String,
  update_at: {type: Date, default: Date.now},
  elements: [{type: mongoose.ObjectId, ref: 'Element'}],
  effects: [{type: mongoose.ObjectId, ref: 'Effect'}],
});
illusionSchema.methods.getContentString = function() {
  return this.content.toString('utf8');
};
illusionSchema.methods.getContentBlob = function() {
  return this.content;
};
illusionSchema.methods.assignAttributes = async function(
    {elementsArray, effectsArray},
) {
  await Promise.all([
    cheakAttrExists(this, 'elements', elementsArray),
    cheakAttrExists(this, 'effects', effectsArray),
  ]);
  this.elements = elementsArray.reduce((acc, cur) => acc.concat(cur), []);
  this.effects = effectsArray.reduce((acc, cur) => acc.concat(cur), []);
  return this.save();
};

module.exports = {
  model: mongoose.model('Illusion', illusionSchema),
  schema: illusionSchema,
};
