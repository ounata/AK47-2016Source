module.exports = function (grunt) {
    "use strict";

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        concat: {
            startup: {
                src: ['app/common/startup/mcs.js',
            		  'app/common/config/ppts.config.js'],
                dest: 'build/ppts.startup.js'
            },
            global: {
                src: ['app/common/global/ppts.head.js',
                      'app/common/global/ppts.controller.js',
                      'app/common/global/ppts.route.js',
                      'app/common/global/ppts.filter.js',
                      'app/common/global/ppts.service.js',
                      'app/common/global/ppts.directive.js',
                      'app/common/global/ppts.interceptor.js',
                      'app/common/global/ppts.tail.js'],
                dest: 'app/ppts.js'
            }
        },
        uglify: {
            buildStartup: {
                src: ['build/ppts.startup.js'],
                dest: 'build/ppts.startup.min.js',
            },
            buildGlobal: {
                src: ['app/ppts.js'],
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