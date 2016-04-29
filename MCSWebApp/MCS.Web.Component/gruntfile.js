module.exports = function(grunt) {
    grunt.initConfig({

        concat: {
			global: {
				src: ['src/mcs/global/mcs.js',
					  'src/mcs/global/mcs.global.js',
				      'src/mcs/util/mcs.util.js',
                      'src/mcs/util/mcs.date.js',
				      'src/mcs/util/mcs.browser.js'],
                dest: 'libs/mcs-jslib-1.0.0/global/mcs.js'
			},
			
            component: {
                src: ['src/mcs/angular/mcs.bootstrap.js',
					  'src/mcs/angular/mcs.filter.js',
					  'src/mcs-*/**/*.js'],
                dest: 'libs/mcs-jslib-1.0.0/component/mcs.component.js'
            },

            css: {
                src: ['src/css/*.css'],
                dest: 'libs/mcs-jslib-1.0.0/component/mcs.component.css'
            }
        },
        uglify: {
			buildGlobal: {
				src: ['libs/mcs-jslib-1.0.0/global/mcs.js'],
				dest: 'libs/mcs-jslib-1.0.0/global/mcs.min.js'
			},
            buildComponent: {
                src: 'libs/mcs-jslib-1.0.0/component/mcs.component.js',
                dest: 'libs/mcs-jslib-1.0.0/component/mcs.component.min.js'
            }
        }
    });
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.registerTask('default', ['concat', 'uglify']);
}
