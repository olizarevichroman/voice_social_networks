var webpack = require('webpack');

var MiniCssExtractPlugin = require('mini-css-extract-plugin');
var WebpackNotifierPlugin = require('webpack-notifier');

module.exports = {
    context: __dirname,
    devtool: "source-map",
    entry: "./src/index.js",
    output: {
      path: __dirname + "/content",
      filename: "bundle.js"
    },
    plugins: [
        new MiniCssExtractPlugin({ filename: 'bundle.css'}),
        new WebpackNotifierPlugin()
    ],
    module: {
        rules: [
            {
                test: /\.css$/,
                exclude: /skins|inspinia/,
                use: [
                    MiniCssExtractPlugin.loader,
                    'css-loader'
                ]
            },
            {
                test: /\.jsx?$|\.js?$/,
                exclude: /node_modules/,
                loader: 'babel-loader'
            }
        ]
    }
  }