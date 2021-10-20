const Koa = require('koa');
const cors = require('@koa/cors');
const logger = require('koa-logger');
const koaBody = require('koa-body');
const serve = require('koa-static');
const router = require('./route');
const mongoose = require('mongoose');
const path = require('path');
const PORT = process.env.PORT || '4000';

mongoose.set('debug', true);
mongoose.connect('mongodb://localhost:27017/test', {useNewUrlParser: true, useUnifiedTopology: true});
const db = mongoose.connection;
db.on('error', console.error.bind(console, 'Connection error:'));
db.once('open', function() {
  app.listen(PORT, "0.0.0.0");
  console.log(`Listening on port: ${PORT}`);
});

const app = new Koa();
app.use(cors());
app.use(koaBody({multipart: true, includeUnparsed: true, textLimit: '10mb'}));
app.use(logger());
app.use(async (ctx, next) => {
  try {
    await next();
  } catch (err) {
    // will only respond with JSON
    ctx.status = err.statusCode || err.status || 500;
    ctx.body = {
      msg: err.message,
      trace: err.stack,
    };
  }
});
app
    .use(router.routes())
    .use(router.allowedMethods());
// statics contains gifs and icons.
app.use(serve(path.join(__dirname, 'statics')));
// Link to frontend, build contains frontend codes
app.use(serve(path.join(__dirname, 'build')));