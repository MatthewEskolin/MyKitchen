const path = require('path');
const { debug } = require('util');

module.exports = {
    entry: './wwwroot/js/site.js', // Specify the entry point for your application
    mode:'development',
    output: {
        filename: 'bundle.js', // Name of the bundled output file
        path: path.resolve(__dirname, 'wwwroot/js'), // Output directory
    },
    module: {
        rules: [
            {
                test: /\.js$/, // Apply the following rules to .js files
                exclude: /node_modules/, // Exclude node_modules
                use: {
                    loader: 'babel-loader', // Use Babel for transpilation
                },
            },
            {
                test: /\.css$/, // Apply the following rules to .css files
                use: ["style-loader","css-loader"]
            }
        ],
    },
    // Add any additional plugins and configuration options as needed
};
