(function () {
    var framework = ppts.config.componentBaseUrl;
    require.config({
        baseUrl: '.',
        paths: {
            jquery: framework + 'libs/jquery-2.2.1/dist/jquery.min',
            bootstrap: framework + 'libs/bootstrap-3.3.5/js/bootstrap.min',
            datePicker: framework + 'libs/date-time-3.0.0/bootstrap-datepicker.min',
            timePicker: framework + 'libs/date-time-3.0.0/bootstrap-timepicker.min',
            dateRangePicker: framework + 'libs/date-time-3.0.0/daterangepicker.min',
            dateTimePicker: framework + 'libs/date-time-3.0.0/bootstrap-datetimepicker.min',
            dateLocale: framework + 'libs/date-time-3.0.0/locales/bootstrap-datepicker.zh-CN',
            ztree: framework + 'libs/zTree-3.5.22/js/jquery.ztree.all.min',
            autocomplete: framework + 'libs/ng-tags-input-3.0.0/ng-tags-input',
            moment: framework + 'libs/date-time-3.0.0/moment.min',
            momentLocale: framework + 'libs/fullcalendar-2.6.1/moment-with-locales',
            fullCalendar: framework + 'libs/fullcalendar-2.6.1/fullcalendar',
            fullCalendarLang: framework + 'libs/fullcalendar-2.6.1/lang-all',
            ace: framework + 'libs/ace-1.2.3/ace.min',
            aceExtra: framework + 'libs/ace-1.2.3/ace-extra.min',
            angular: framework + 'libs/angular-1.5.0/angular',
            ngLocale: framework + 'libs/angular-1.5.0/angular-locale_zh-cn',
            uiRouter: framework + 'libs/angular-ui-router-0.2.18/release/angular-ui-router.min',
            ngSanitize: framework + 'libs/angular-sanitize-1.4.6/angular-sanitize.min',
            ngResource: framework + 'libs/angular-resource-1.5.0/angular-resource',
            ngBreadcrumb: framework + 'libs/angular-breadcrumb-0.4.1/dist/angular-breadcrumb.min',
            ngCalendar: framework + 'libs/angular-ui-calendar-0.9.0.b1/src/calendar',
            blockUI: framework + 'libs/angular-block-ui-0.2.2/dist/angular-block-ui.min',
            uiBootstrapTpls: framework + 'libs/ui-bootstrap-1.1.0/ui-bootstrap-tpls-1.1.0.min',
            mcsComponent: framework + 'libs/mcs-jslib-1.0.0/component/mcs.component',
            uiSelect: framework + 'libs/angular-ui-select-0.13.2/dist/select.min',
            dialogs: framework + 'libs/angular-dialog-service-5.3.0/dist/dialogs.min',
            ppts: 'app/ppts'
        },

        shim: {
            jquery: {
                exports: 'jquery'
            },
            bootstrap: {
                exports: 'bootstrap',
                deps: ['jquery']
            },
            datePicker: {
                exports: 'datePicker',
                deps: ['jquery', 'bootstrap']
            },
            timePicker: {
                exports: 'timePicker',
                deps: ['jquery', 'bootstrap', 'moment']
            },
            dateRangePicker: {
                exports: 'dateRangePicker',
                deps: ['jquery', 'bootstrap']
            },
            dateTimePicker: {
                exports: 'dateTimePicker',
                deps: ['jquery', 'bootstrap', 'moment']
            },
            dateLocale: {
                exports: 'dateLocale',
                deps: ['datePicker', 'dateTimePicker']
            },
            ztree: {
                exports: 'ztree',
                deps: ['jquery']
            },
            autocomplete: {
                exports: 'autocomplete',
                deps: ['jquery', 'angular']
            },
            momentLocale: {
                exports: 'momentLocale',
                deps: ['jquery']
            },
            fullCalendar: {
                exports: 'fullCalendar',
                deps: ['momentLocale']
            },
            fullCalendarLang: {
                exports: 'fullCalendarLang',
                deps: ['fullCalendar']
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
            ngLocale: {
                exports: 'ngLocale',
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
            ngBreadcrumb: {
                exports: 'ngBreadcrumb',
                deps: ['angular']
            },
            ngCalendar: {
                exports: 'ngCalendar',
                deps: ['angular', 'moment']
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
                deps: ['angular', 'uiBootstrapTpls']
            },
            mcsComponent: {
                exports: 'mcsComponent',
                deps: ['angular']
            }
        },
        callback: function () {
            require([
                'jquery',
                'bootstrap',
                'moment',
                'datePicker',
                'timePicker',
                'dateRangePicker',
                'dateTimePicker',
                'dateLocale',
                'ztree',
                'autocomplete',
                'ace',
                'aceExtra',
                'angular',
                'blockUI',
                'ngLocale',
                'uiRouter',
                'ngSanitize',
                'ngResource',
                'ngBreadcrumb',
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