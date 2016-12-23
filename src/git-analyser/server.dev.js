/* eslint no-console: 0 */

const path = require('path');
const express = require('express');
const bodyParser = require('body-parser');
const webpack = require('webpack');
const webpackDevMiddleware = require('webpack-dev-middleware');
const webpackHotMiddleware = require('webpack-hot-middleware');
const proxy = require('http-proxy-middleware');

const devBuildConfig = require('./webpack.dev.config');

const PORT = 8080;

const server = express();
const compiler = webpack(devBuildConfig);

// proxy middleware options
var options = {
    target: 'http://localhost:1234/', // target host
    changeOrigin: true,               // needed for virtual hosted sites
    ws: true,                         // proxy websockets
    logLevel: 'debug',
    //pathRewrite: {
    //    '^/old/api': '/new/api',     // rewrite path
    //    '^/remove/api': '/api'       // remove path
    //},
    //router: {
        // when request.headers.host == 'dev.localhost:3000',
        // override target 'http://www.example.org' to 'http://localhost:8000'
    //    'dev.localhost:3000': 'http://localhost:8000'
    //}
};

server.use(webpackDevMiddleware(compiler, {
  publicPath: devBuildConfig.output.publicPath,
  hot: true,
  historyApiFallback: true,
  stats: {
    colors: true,
    hash: false,
    version: false,
    chunks: false,
    children: false,
  }
}));

server.use(webpackHotMiddleware(compiler));

// create the proxy (without context)
var exampleProxy = proxy(options);
server.use('/', exampleProxy);

server.use(bodyParser.json());
server.use(bodyParser.urlencoded({ extended: true }));

server.listen(PORT, 'localhost', err => {
  if (err) console.log(`=> OMG!!! 🙀 ${err}`);
  console.log(`=> 🔥  Webpack dev server is running on port ${PORT}`);
});
