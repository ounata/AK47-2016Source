module.exports = function (grunt) {
    "use strict";

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        concat: {
            startup: {
                src: ['src/startup/mcs.js',
            		  'src/config/ppts.config.js'],
                dest: 'build/ppts.startup.js'
            },
            global: {
                src: ['src/global/ppts.head.js',
                      'src/global/ppts.controller.js',
                      'src/global/ppts.route.js',
                      'src/global/ppts.filter.js',
                      'src/global/ppts.service.js',
                      'src/global/ppts.directive.js',
                      'src/global/ppts.interceptor.js',
                      'src/global/ppts.tail.js'],
                dest: 'build/ppts.global.js'
            }
        },
        uglify: {
            buildStartup: {
                src: ['build/ppts.startup.js'],
                dest: 'build/ppts.startup.min.js',
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