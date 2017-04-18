(function (global) {
    System.config({
        paths: {
            // paths serve as alias
            'npm:': 'node_modules/',
            'app/*': 'app/*',
            'app':'app',
            '*': 'node_modules/*',
            
        },
       // packageConfigPaths: ['node_modules/*/package.json'],
        // map tells the System loader where to look for things
        map: {
            // our app is within the app folder
           // app: 'app', // 'dist',
            // angular bundles
            '@angular/core': 'npm:@angular/core/bundles/core.umd.js',
            '@angular/common': 'npm:@angular/common/bundles/common.umd.js',
            '@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js',
            '@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',
            '@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
            '@angular/http': 'npm:@angular/http/bundles/http.umd.js',
            '@angular/router': 'npm:@angular/router/bundles/router.umd.js',
            '@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',
            // other libraries
            'rxjs': 'npm:rxjs',
            'ng2-datetime-picker': 'npm:ng2-datetime-picker',
            '@ng-bootstrap/ng-bootstrap': 'npm:@ng-bootstrap/ng-bootstrap/bundles/ng-bootstrap.js',
            'angular-oauth2-oidc': 'npm:angular-oauth2-oidc/dist/index.js',
        },
        // packages tells the System loader how to load when no filename and/or no extension
        packages: {
            app: { main: './main.js', defaultExtension: 'js' },
            rxjs: { defaultExtension: 'js' },
            'angular-oauth2-oidc': {
                map: {
                    'oauth-service':'oauth-service.js',
                },
                defaultExtension: 'js'
            },
         //   'ng2-datetime-picker': { main: 'ng2-datetime-picker.umd.js', defaultExtension: 'js' }
        }
    });
})(this);