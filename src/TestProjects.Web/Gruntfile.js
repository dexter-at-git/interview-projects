module.exports = function (grunt) {

    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.initConfig({
        concat: {
            options: {
                separator: ';',
            },
            dist: {
                src: ['Scripts/testProjectsApp.js', 'Scripts/image-search/**/*.js'],
                dest: 'wwwroot/testProjectsApp.js',
            },
        },
        uglify: {
            my_target: {
                files: { 'wwwroot/testProjectsApp.js': ['Scripts/testProjectsApp.js','Scripts/image-search/**/*.js'] }
            }
        },
        copy: {
            main: {
                files: [{
                    expand: true,
                    flatten: true,
                    src: ['Scripts/image-search/**/*.js'],
                    dest: 'wwwroot/image-search/',
                    filter: 'isFile'
                }]
            }
        },
        watch: {
            scripts: {
                files: ['Scripts/**/*.js'],
                tasks: ['uglify']
            }
        }
    });

    grunt.registerTask('default', ['uglify', 'watch']);
};