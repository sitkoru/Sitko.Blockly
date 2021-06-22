const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CssoWebpackPlugin = require('csso-webpack-plugin').default;
const TerserPlugin = require('terser-webpack-plugin');

var config = {
    entry: {
        index: path.resolve(__dirname, 'src', 'index.js'),
    },
    output: {
        filename: 'Sitko.Blockly.AntDesign.js',
        path: path.resolve(__dirname, '..', 'wwwroot'),
    },
    plugins: [
        new webpack.ProvidePlugin({}),
        new MiniCssExtractPlugin({
            filename: "Sitko.Blockly.AntDesign.css"
        }),
        new CssoWebpackPlugin()
    ],
    module: {
        rules: [
            {
                test: /\.(sa|sc)ss$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader,
                        options: {
                            publicPath: '/dist'
                        }
                    },
                    {
                        loader: 'css-loader', // translates CSS into CommonJS modules
                    }, {
                        loader: 'postcss-loader', // Run post css actions
                        options: {
                            postcssOptions: {
                                plugins: function () { // post css plugins, can be exported to postcss.config.js
                                    return [
                                        require('precss'),
                                        require('autoprefixer')
                                    ];
                                }
                            }
                        }
                    }, {
                        loader: 'sass-loader' // compiles Sass to CSS
                    }]
            },
            {
                test: /\.less$/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader,
                        options: {
                            publicPath: '/dist'
                        }
                    },
                    {
                        loader: 'css-loader', // translates CSS into CommonJS modules
                    }, {
                        loader: 'less-loader', // compiles Less to CSS
                        options: {
                            lessOptions: {
                                javascriptEnabled: true
                            }
                        }
                    }]
            },
            {
                test: /\.(eot|svg|ttf|woff|woff2)$/,
                loader: 'file-loader',
                options: {
                    name: '/fonts/[name].[ext]'
                }
            },
            {
                test: /\.(jpg|png|bmp|gif|ico|jpeg)$/,
                loader: 'file-loader',
                options: {
                    name: '/resources/[name].[ext]'
                }
            },
        ]
    }
};
module.exports = (env, argv) => {
    if (argv.mode === 'development') {
        config.devtool = 'source-map';
    }

    if (argv.mode === 'production') {
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

    return config;
};
