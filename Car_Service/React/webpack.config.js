module.exports = {
    module: {
        loaders: [{
            test: /\.jsx?$/,
            exclude: /(node_modules)/,
            loader: 'babel-loader',
            query: {
                presets: ['es2015', 'react']
            }
        },
        {
            test: /\.css$/,
            use: [
                { loader: "style-loader" },
                { loader: "css-loader" }
            ]
        }]

    },
    entry: {
        js: ['babel-polyfill', './src/index.js']
    },
    output: {
        path: __dirname + './../App_Data/js',
        filename: 'bundle.js'
    },
    watch: true
};