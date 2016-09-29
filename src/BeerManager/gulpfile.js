var gulp = require('gulp');
rimraf = require("rimraf");

var paths = {
    webroot: "./wwwroot/",
    npm: "./node_modules/",
    lib: "./wwwroot/lib/"
};

gulp.task("clean", function (cb) {
    rimraf(paths.lib, cb);
});

gulp.task("copy", ["clean"], function () {
    var packages = {
        "bootstrap": "bootstrap/dist/**/*.{js,map,css,ttf,svg,woff,eot}",
        "angular": "angular/angular*.{js,map}",
        "angular-ui-router": "angular-ui-router/release/*.js",
        "angular-busy": "angular-busy/dist/*.{js,css}",
        //"jasmine": "jasmine-core/lib/jasmine-core.js",
        //"angular-mocks": "angular-mocks/angular-mocks.js"
    }

    for (var src in packages) {
        if (!packages.hasOwnProperty(src))
            continue;
        gulp.src(paths.npm + packages[src])
          .pipe(gulp.dest(paths.lib + src));
    }
});
