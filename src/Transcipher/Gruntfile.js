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
                src: ['Scripts/transcipherApp.js', 'Scripts/transcipher/**/*.js'],
                dest: 'wwwroot/transcipherApp.js',
            },
        },
        uglify: {
            my_target: {
                files: { 'wwwroot/transcipher/js/transcipherApp.js': ['Scripts/transcipher/transcipherApp.js', 'Scripts/transcipher/**/*.js', '!Scripts/transcipher/tests/*'] }
            }
        },
        copy: {
            main: {
                files: [{
                    expand: true,
                    flatten: true,
                    src: ['Scripts/transcipher/**/*.js'],
                    dest: 'wwwroot/transcipher/',
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