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
                files: {
                    '../InterviewProjects.Shell/wwwroot/transcipher/js/transcipherApp.js': ['wwwroot/transcipher/js/transcipherApp.js', 'wwwroot/transcipher/js/**/*.js', '!wwwroot/transcipher/js/tests/*']
                }
            }
        },
        copy: {
            views: {
                files: [{
                    expand: true,
                    flatten: true,
                    src: ['wwwroot/transcipher/views/*'],
                    dest: '../InterviewProjects.Shell/wwwroot/transcipher/views',
                    filter: 'isFile'
                }, {
                    src: ['wwwroot/transcipher/home.html'],
                    dest: '../InterviewProjects.Shell/wwwroot/transcipher/home.html',
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