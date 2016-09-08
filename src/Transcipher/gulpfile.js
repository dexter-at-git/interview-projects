var gulp = require('gulp');
rimraf = require("rimraf");

var paths = {
    webroot: "./wwwroot/",
    bower: "./bower_components/",
    lib: "./wwwroot/lib/"
};

gulp.task("clean", function (cb) {
    rimraf(paths.lib, cb);
});

gulp.task("copy", ["clean"], function () {
    var bower = {
        "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
        "angular": "angular/angular*.{js,map}",
        "angular-ui-router": "angular-ui-router/release/*.js",
        "angular-busy": "angular-busy/dist/*.{js,css}",
        "font-awesome": "font-awesome/**/*.{css,ttf,svg,woff,eot}"
    }

    for (var destinationDir in bower) {
        console.log(paths.bower + bower[destinationDir]);
        gulp.src(paths.bower + bower[destinationDir])
          .pipe(gulp.dest(paths.lib + destinationDir));
    }
});