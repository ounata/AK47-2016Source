module.exports = function (grunt) {
    "use strict";

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        concat: {
            ppts_startup: {
                src: ['src/startup/mcs.js',
            		  'src/config/ppts.config.js'],
                dest: 'build/ppts.startup.js'
            },
            ppts_global: {
                src: ['src/global/ppts.head.js',
                      'src/global/ppts.controller.js',
                      'src/global/ppts.route.js',
                      'src/global/ppts.enum.js',
                      'src/global/ppts.filter.js',
                      'src/global/ppts.service.js',
                      'src/global/ppts.directive.js',
                      'src/global/ppts.interceptor.js',
                      'src/global/ppts.tail.js'],
                dest: 'build/ppts.global.js'
            },
            component_startup: {
                src: ['src/startup/mcs.js',
            		  'samples/common/config/ppts.config.js'],
                dest: 'samples/build/ppts.startup.js'
            },
            component_global: {
                src: ['samples/common/global/ppts.head.js',
                      'samples/common/global/ppts.controller.js',
                      'samples/common/global/ppts.route.js',
                      'samples/common/global/ppts.service.js',
                      'src/global/ppts.directive.js',
                      'src/global/ppts.interceptor.js',
                      'src/global/ppts.tail.js'],
                dest: 'samples/build/ppts.global.js'
            }
        },
        uglify: {
            buildStartup: {
                src: ['build/ppts.startup.js'],
                dest: 'build/ppts.startup.min.js',
            },
            buildRequire: {
                src: ['src/config/require.config.js'],
                dest: 'build/require.config.min.js',
            },
            buildGlobal: {
                src: ['build/ppts.global.js'],
                dest: 'build/ppts.global.min.js',
            },
        }
    });

    // Load the plugin that provides.
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    // Default task(s).
    grunt.registerTask('default', ['concat', 'uglify']);
};