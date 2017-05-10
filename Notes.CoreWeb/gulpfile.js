var gulp = require('gulp');
var concatCss = require('gulp-concat-css');
var libs = './wwwroot/libs/';
var app = './wwwroot/app/';
var styles = './wwwroot/styles/';
var typings = './typings/';
var gnf = require('gulp-npm-files');

gulp.task('default', function () {
    // place code for your default task here
});

gulp.task('restore:core-js', function () {
    gulp.src([
        'node_modules/core-js/client/*.js'
    ]).pipe(gulp.dest(libs + 'core-js'));
});
gulp.task('restore:zone.js', function () {
    gulp.src([
        'node_modules/zone.js/dist/*.js'
    ]).pipe(gulp.dest(libs + 'zone.js'));
});
gulp.task('restore:reflect-metadata', function () {
    gulp.src([
        'node_modules/reflect-metadata/reflect.js'
    ]).pipe(gulp.dest(libs + 'reflect-metadata'));
});
gulp.task('restore:systemjs', function () {
    gulp.src([
        'node_modules/systemjs/dist/*.js'
    ]).pipe(gulp.dest(libs + 'systemjs'));
});
gulp.task('restore:rxjs', function () {
    gulp.src([
        'node_modules/rxjs/**/*.js'
    ]).pipe(gulp.dest(libs + 'rxjs'));
});


gulp.task('restore:angular', function () {
    gulp.src([
        'node_modules/@angular/**/*.js'
    ]).pipe(gulp.dest(libs + '@angular'));
});

gulp.task('restore:angularUI', function () {
    gulp.src([
        'node_modules/@ng-bootstrap/**/*.js'
    ]).pipe(gulp.dest(libs + '@ng-bootstrap'));
});

gulp.task('restore:bootstrap', function () {
    gulp.src([
        'node_modules/bootstrap/dist/**/*.*'
    ]).pipe(gulp.dest(libs + 'bootstrap'));
});

gulp.task('restore:oidc-client', function () {
    gulp.src([
        'node_modules/oidc-client/dist/**/*.*'
    ]).pipe(gulp.dest(libs + 'oidc-client'));
});

gulp.task('restore-typings:oidc-client', function () {
    gulp.src([
        'node_modules/oidc-client/*.d.ts'
    ]).pipe(gulp.dest(typings + 'oidc-client'));
});




gulp.task('compile:css', function () {
    return gulp.src(libs + '**/*.css')
      .pipe(concatCss('bundle.css'))
      .pipe(gulp.dest(styles));
});
gulp.task('compile:appcss', function () {
    return gulp.src(app + '**/*.css')
      .pipe(concatCss('app.css'))
      .pipe(gulp.dest(styles));
});

// Copy dependencies to build/node_modules/ 
gulp.task('copyNpmDependenciesOnly', function () {
    gulp.src(gnf(), { base: './' }).pipe(gulp.dest('./build'));
});

gulp.task('restore', [
    'restore:core-js',
    'restore:zone.js',
    'restore:reflect-metadata',
    'restore:systemjs',
    'restore:rxjs',
    'restore:angular',
    'restore:bootstrap',
    'restore:oidc-client',
    'restore-typings:oidc-client',
    'restore:angularUI'
]);