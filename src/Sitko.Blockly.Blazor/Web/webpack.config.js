const path = require('path');
const TerserPlugin = require('terser-webpack-plugin');
const dotenv = require('dotenv');
const webpack = require('webpack');

const config = {
  entry: {
    blocklyForms: path.resolve(__dirname, 'src', 'blocklyForms'),
    blocklyTwitch: path.resolve(__dirname, 'src', 'blocklyTwitch'),
    blocklyTwitter: path.resolve(__dirname, 'src', 'blocklyTwitter'),
  },
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
    ],
  },
  resolve: {
    extensions: ['.tsx', '.ts', '.js'],
  },
  output: {
    filename: '[name].js',
    path: path.resolve(__dirname, 'dist'),
  }
};
module.exports = (env, argv) => {
  if (argv.mode === 'development') {
    config.devtool = 'source-map';
  }

  if (argv.mode === 'production') {
    config.devtool = false;
    config.optimization = {
      minimize: true,
      minimizer: [
        new TerserPlugin({
          terserOptions: {
            compress: {
              drop_console: true, // will remove console.logs from your files
            },
          },
        }),
      ],
    }
  }

  config.plugins = [
    new webpack.DefinePlugin({
      'process.env': JSON.stringify(dotenv.config({path: `./.env.${argv.mode}`}).parsed)
    })
  ];

  return config;
};
