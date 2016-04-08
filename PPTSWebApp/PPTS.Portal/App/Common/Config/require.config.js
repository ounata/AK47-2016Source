(function () {
    var framework = ppts.config.componentBaseUrl;
    require.config({
        baseUrl: '.',
        paths: {
            jquery: framework + 'libs/jquery-2.2.1/dist/jquery.min',
            //jqueryChosen: framework + 'libs/jquery-2.2.1/plugins/chosen/jquery.chosen.min',
            bootstrap: framework + 'libs/bootstrap-3.3.5/js/bootstrap.min',
            ace: framework + 'libs/ace-1.2.3/ace.min',
            aceExtra: framework + 'libs/ace-1.2.3/ace-extra.min',
            angular: framework + 'libs/angular-1.5.0/angular.min',
            uiRouter: framework + 'libs/angular-ui-router-0.2.18/release/angular-ui-router.min',
            ngSanitize: framework + 'libs/angular-sanitize-1.4.6/angular-sanitize.min',
            ngResource: framework + 'libs/angular-resource-1.5.0/angular-resource.min',
            blockUI: framework + 'libs/angular-block-ui-0.2.2/dist/angular-block-ui.min',
            uiBootstrap: framework + 'libs/ui-bootstrap-1.1.0/ui-bootstrap-1.1.0.min',
            uiBootstrapTpls: framework + 'libs/ui-bootstrap-1.1.0/ui-bootstrap-tpls-1.1.0.min',
            mcsComponent: framework + 'libs/mcs-jslib-1.0.0/component/mcs.component',
            uiSelect: framework + 'libs/angular-ui-select-0.13.2/dist/select.min',
            dialogs: framework + 'libs/angular-dialog-service/dist/dialogs.min',
            ppts: 'app/ppts'
        },
        shim: {
            jquery: {
                exports: 'jquery'
            },
            //jqueryChosen: {
            //    exports: 'jqueryChosen',
            //    deps: ['jquery']
            //},
            bootstrap: {
                exports: 'bootstrap',
                deps: ['jquery']
            },
            ace: {
                exports: 'ace',
                deps: ['jquery']
            },
            aceExtra: {
                exports: 'aceExtra',
                deps: ['ace']
            },
            angular: {
                exports: 'angular'
            },
            blockUI: {
                exports: 'blockUI',
                deps: ['angular']
            },
            ngResource: {
                exports: 'ngResource',
                deps: ['angular']
            },
            uiRouter: {
                exports: 'uiRouter',
                deps: ['angular']
            },
            ngSanitize: {
                exports: 'ngSanitize',
                deps: ['angular']
            },
            uiBootstrap: {
                exports: 'uiBootstrap',
                deps: ['angular']
            },
            uiBootstrapTpls: {
                exports: 'uiBootstrapTpls',
                deps: ['angular']
            },
            uiSelect: {
                exports: 'uiSelect',
                deps: ['angular']
            },
            dialogs: {
                exports: 'dialogs',
                deps: ['angular']
            },
            mcsComponent: {
                exports: 'mcsComponent',
                deps: ['angular']
            }
        },
        callback: function () {
            require([
                'jquery',
                //'jqueryChosen',
                'bootstrap',
                'ace',
                'aceExtra',
                'angular',
                'blockUI',
                'uiRouter',
                'ngSanitize',
                'ngResource',
                'uiBootstrap',
                'uiBootstrapTpls',
                'uiSelect',
                'mcsComponent',
                'dialogs',
                'ppts'
            ], function () {
                angular.element(document).ready(function () {
                    angular.bootstrap(document, ['ppts']);
                });
            });
        }
    });
})();