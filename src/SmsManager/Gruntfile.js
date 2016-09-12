module.exports = function (grunt) {

    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-concat');

    grunt.initConfig({
        uglify: {
            my_target: {
                files: {
                    '../InterviewProjects.Shell/wwwroot/sms-manager/js/smsManagerApp.js': ['wwwroot/sms-manager/js/smsManagerApp.js', 'wwwroot/sms-manager/js/**/*.js', '!wwwroot/sms-manager/js/tests/*']
                }
            }
        },
        copy: {
            views: {
                files: [{
                    expand: true,
                    flatten: true,
                    src: ['wwwroot/sms-manager/views/*'],
                    dest: '../InterviewProjects.Shell/wwwroot/sms-manager/views',
                    filter: 'isFile'
                }, {
                    src: ['wwwroot/sms-manager/home.html'],
                    dest: '../InterviewProjects.Shell/wwwroot/sms-manager/home.html',
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