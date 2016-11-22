// Very similar to webpack.prod.config.js. Common parts could be extracted to a base config.
// See example at:
// https://github.com/shakacode/react-webpack-rails-tutorial/blob/master/client%2Fwebpack.client.base.config.js
const webpack = require('webpack');
const path = require('path');
const autoprefixer = require('autoprefixer');
const bootstrapEntryPoints = require('./webpack.bootstrap.config.js');

// eslint-disable-next-line no-console
console.log(`=> bootstrap-loader configuration: ${bootstrapEntryPoints.dev}`);

module.exports = {

    entry: [
      'webpack-hot-middleware/client',
      'tether',
      'font-awesome-loader',
      bootstrapEntryPoints.dev,
      './wwwroot/scripts/app',
    ],

    output: {
        path: path.join(__dirname, 'public', 'assets'),
        filename: 'app.js',
        publicPath: '/assets/',
    },

    devtool: '#source-map',

    resolve: { extensions: ['*', '.js'] },

    plugins: [
      new webpack.HotModuleReplacementPlugin(),
      new webpack.NoErrorsPlugin(),
      new webpack.ProvidePlugin({
          'window.Tether': 'tether',
      }),
      new webpack.LoaderOptionsPlugin({
          postcss: [autoprefixer],
      }),
      new webpack.ProvidePlugin({
          jQuery: 'jquery'
      })
    ],

    module: {
        loaders: [
          { test: /\.css$/, loaders: ['style-loader', 'css-loader', 'postcss-loader'] },
          { test: /\.scss$/, loaders: ['style-loader', 'css-loader', 'postcss-loader', 'sass-loader'] },
          {
              test: /\.woff2?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
              loader: 'url-loader?limit=10000',
          },
          {
              test: /\.(ttf|eot|svg)(\?[\s\S]+)?$/,
              loader: 'file-loader',
          },

          // Use one of these to serve jQuery for Bootstrap scripts:

          // Bootstrap 4
          { test: /bootstrap\/dist\/js\/umd\//, loader: 'imports?jQuery=jquery' },

          // Bootstrap 3
          { test: /bootstrap-sass\/assets\/javascripts\//, loader: 'imports?jQuery=jquery' },
        ],
    },

};