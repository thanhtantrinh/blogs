const path = require('path');
const CheckerPlugin = require('awesome-typescript-loader').CheckerPlugin;
const CleanWebpackPlugin = require('clean-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = (env) => {
    const isDevBuild = !(env && env.prod);
    const outputDir = (env && env.publishDir) ? env.publishDir : __dirname;
    //const imagePath = path.join(outputDir, 'wwwroot/assets/images');
    return [{
        
        //mode: isDevBuild ? 'development' : 'production',
        mode: 'development',
        devtool: 'source-map', //inline-source-map
        stats: { modules: false },
        entry: {
            'App': './ClientApp/App.tsx',
            //'Images': './ClientApp/assets/images/'
        },
        watchOptions: {
            ignored: /node_modules/
        },
        output: {
            filename: "dist/static/[name].js",
            path: path.join(outputDir, 'wwwroot'),
            publicPath: '/'
        },

        resolve: {
            // Add '.ts' and '.tsx' as resolvable extensions.
            extensions: [".ts", ".tsx", ".js", ".json"]
        },

        devServer: {
            hot: true
        },

        module: {
            rules: [
                // All files with a '.ts' or '.tsx' extension will be handled by 'awesome-typescript-loader'.
                {
                    test: /\.tsx?$/,
                    include: /ClientApp/,
                    loader: [
                        {
                            loader: 'awesome-typescript-loader',
                            options: {
                                useCache: true,
                                useBabel: true,
                                babelOptions: {
                                    babelrc: false,
                                    plugins: ['react-hot-loader/babel'],
                                }
                            }
                        }
                    ]
                },
                //{
                //    test: /\.css$/,
                //    //use: isDevBuild ? ['style-loader', 'css-loader'] : ExtractTextPlugin.extract({ use: 'css-loader?' }),
                //    //loader: ExtractTextPlugin.extract({ loader: 'css-loader?importLoaders=1',})
                //    use: [
                //        { loader: 'style-loader', options: { sourceMap: true } },
                //        { loader: 'css-loader', options: { sourceMap: true } },
                //        { loader: 'postcss-loader', options: { sourceMap: true } },
                //        { loader: 'sass-loader', options: { sourceMap: true } }
                //    ]
                //},
                //{
                //    test: /\.(scss)$/,
                //    //include: /ClientApp/,
                //    //use: isDevBuild ? ['style-loader', 'css-loader', "sass-loader"] : ExtractTextPlugin.extract({ use: 'sass-loader' })
                //    use: [{
                //        loader: 'file-loader',
                //        options: {
                //            name: '[name].css',
                //            outputPath: '/assets/css/',     
                //            outputStyle: 'compressed',
                //            sourceMap: true
                //        }
                //    },
                //    {
                //        loader: 'extract-loader', options: { sourceMap: true }
                //    },
                //    {
                //        loader: 'css-loader', options: { sourceMap: true }
                //    },
                //    {
                //        loader: 'postcss-loader', options: { sourceMap: true }
                //    },
                //    {
                //        loader: 'sass-loader',
                //        options: {
                //            sourceMap: true,                            
                //        }
                //    }]
                //},
                //{ // regular css files
                //    test: /\.css$/,
                //    loader: ExtractTextPlugin.extract({
                //        loader: 'css-loader?importLoaders=1',
                //    }),
                //},
                //{ // sass / scss loader for webpack
                //    test: /\.(scss)$/,
                //    loader: ExtractTextPlugin.extract({
                //        fallback: 'style-loader',
                //        use: [
                //            'css-loader',
                //            'resolve-url-loader',
                //            'sass-loader?sourceMap',
                //        ],
                //        publicPath: '/css/'
                //    })
                //},
                //{
                //    test: /\.css/,
                //    use: [
                //        { loader: 'style-loader', options: { sourceMap: true } },
                //        { loader: 'css-loader', options: { sourceMap: true } },
                //        { loader: 'postcss-loader', options: { sourceMap: true } },
                //        { loader: 'sass-loader', options: { sourceMap: true } }
                //    ]
                //},
                {
                    test: /\.scss$/,
                    use: [
                        { loader: "style-loader" },
                        { loader: "css-loader", options: { sourceMap: true }},
                        { loader: "sass-loader", options: { sourceMap: true }}                  
                    ]
                },
                {
                    test: /\.(jpg|png|gif|svg|pdf|ico)$/,
                    //use: 'url-loader?limit=25000'
                    use: [
                        {
                            loader: 'file-loader',
                            options: {
                                name: 'dist/assets/images/[name].[ext]',
                                //outputPath: '/assets/images/',
                                
                            },                            
                        },
                    ]                    
                },
                {
                    test: /.(ttf|otf|eot|svg|woff(2)?)(\?[a-z0-9]+)?$/,
                    //use: 'url-loader?limit=25000'
                    use: [
                        {
                            loader: 'file-loader',
                            options: {
                                name: '[name].[ext]',
                                outputPath: 'dist/assets/fonts/',
                                publicPath: 'dist/assets/fonts/'
                            },
                        },
                    ]
                },
                //{
                //    test: /\.(ttf|eot|woff|woff2)$/,
                //    use: {
                //        loader: "file-loader",
                //        options: {
                //            limit: 50000,
                //            name: "dist/assets/fonts/[name].[ext]",
                //            publicPath: "../", // Take the directory into account
                //        },
                //    },
                //},
            ]
        },

        plugins: [
            new CleanWebpackPlugin(path.join(outputDir, 'wwwroot', 'dist')),
            new CheckerPlugin(),
            //new ExtractTextPlugin({ // define where to save the file
            //    filename: 'dist/bundle.css',
            //    allChunks: true,
            //}),
        ]
    }];
};
