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
                // files: { 'wwwroot/image-search/js/imageSearchApp.js': ['Scripts/image-search/imageSearchApp.js', 'Scripts/image-search/**/*.js', '!Scripts/image-search/tests/*'] }
                files: {
                    '../InterviewProjects.Shell/wwwroot/image-search/js/imageSearchApp.js': ['wwwroot/image-search/js/imageSearchApp.js', 'wwwroot/image-search/js/**/*.js', '!wwwroot/image-search/js/tests/*']
                }
            }
        },
        copy: {
            css: {
                files: [{
                    expand: true,
                    flatten: true,
                    src: ['wwwroot/image-search/css/*.css'],
                    dest: '../InterviewProjects.Shell/wwwroot/image-search/css',
                    filter: 'isFile'
                }]
            },
            views: {
                files: [{
                expand: true,
                flatten: true,
                src: ['wwwroot/image-search/views/*'],
                dest: '../InterviewProjects.Shell/wwwroot/image-search/views',
                filter: 'isFile'
                }, {
                    src: ['wwwroot/image-search/home.html'],
                    dest: '../InterviewProjects.Shell/wwwroot/image-search/home.html',
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