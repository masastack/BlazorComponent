var gulp = require('gulp');

var browserify = require('browserify');
var source = require('vinyl-source-stream');
var tsify = require('tsify');
var uglify = require('gulp-uglify');
var sourcemaps = require('gulp-sourcemaps');
var buffer = require('vinyl-buffer');

gulp.task('interop', function () {
    return browserify({
        basedir: '.',
        debug: true,
        entries: ['./main.ts'],
        cache: {},
        packageCache: {},
    })
        .plugin(tsify)
        .transform('babelify', {
            presets: ['es2015'],
            extensions: ['.ts']
        })
        .bundle()
        .pipe(source('blazor-component.js'))
        .pipe(buffer())
        .pipe(sourcemaps.init({ loadMaps: true }))
        .pipe(uglify())
        .pipe(sourcemaps.write('./'))
        .pipe(gulp.dest('../BlazorComponent/wwwroot/js'));
});

gulp.task('input', function () {
    return browserify()
        .add('./src/input.ts')
        .plugin(tsify)
        // .transform('babelify', {
        //     presets: ['es2015'],
        //     extensions: ['.ts']
        // })
        .bundle()
        .pipe(source('input.js'))
        .pipe(buffer())
        // .pipe(sourcemaps.init({ loadMaps: true }))
        // .pipe(uglify())
        // .pipe(sourcemaps.write('./'))
        .pipe(gulp.dest('../BlazorComponent/wwwroot/js'));
})

gulp.task('default', gulp.parallel('input'), function () { });