const Router = require('@koa/router');
const queryFunctions = require('./controller');
const router = new Router();
const send = require('koa-send');
const path = require('path');


router
    .get('/', async (ctx, next) => {
      await send(ctx,  path.join('build', 'index.html'), {root: __dirname});
    })
    .get('/.well-known/pki-validation/4AE0453C0C9462375FBF3419A2913B57.txt', async (ctx, next) => {
      // verify domain
      await send(ctx, path.join('statics', '4AE0453C0C9462375FBF3419A2913B57.txt'), {root: __dirname});
    })
    .get('/favicon.ico', async (ctx, next) => {
      await send(ctx,  path.join('build', 'favicon32.ico'), {root: __dirname});
    })
    .get('/tags/:type', async (ctx, next) => {
      return ctx.body = await queryFunctions
          .getAllTags(ctx, next);
    })

    .get('/illusions', async (ctx, next) =>
      ctx.body = await queryFunctions.getAllEntry(ctx, next),
    )
    .get('/illusions/:id', async (ctx, next) =>
      ctx.body = await queryFunctions.getEntryById(ctx, next),
    )
    .post('/illusions/', async (ctx, next) => {
      await queryFunctions.insertNewEntry(ctx, next);
      return ctx.body = 'Done';
    })
    .post('/illusions/search/:type', async (ctx, next) => {
      return ctx.body = await queryFunctions
          .searchByTagIDs(ctx, next);
    })
    .post('/import', async (ctx, next) => {
      return ctx.body = await queryFunctions.batchImport(ctx, next);
    });

module.exports = router;
