(function () {
    require.config({
        baseUrl: '.',
        paths: {
            jquery: ppts.config.mcsComponentBaseUrl + 'libs/jquery-2.2.1/dist/jquery.min',
            bootstrap: ppts.config.mcsComponentBaseUrl + 'libs/bootstrap-3.3.5/js/bootstrap.min',
            datePicker: ppts.config.mcsComponentBaseUrl + 'libs/date-time-3.0.0/bootstrap-datepicker.min',
            timePicker: ppts.config.mcsComponentBaseUrl + 'libs/date-time-3.0.0/bootstrap-timepicker.min',
            dateRangePicker: ppts.config.mcsComponentBaseUrl + 'libs/date-time-3.0.0/daterangepicker.min',
            dateTimePicker: ppts.config.mcsComponentBaseUrl + 'libs/date-time-3.0.0/bootstrap-datetimepicker.min',
            dateLocale: ppts.config.mcsComponentBaseUrl + 'libs/date-time-3.0.0/locales/bootstrap-datepicker.zh-CN',
            ztree: ppts.config.mcsComponentBaseUrl + 'libs/zTree-3.5.22/js/jquery.ztree.all.min',
            select2: ppts.config.mcsComponentBaseUrl + 'libs/angular-ui-select-0.13.2/dist/select2',
            select2Locale: ppts.config.mcsComponentBaseUrl + 'libs/angular-ui-select-0.13.2/dist/select2_locale_zh-CN',
            autocomplete: ppts.config.mcsComponentBaseUrl + 'libs/ng-tags-input-3.0.0/ng-tags-input',
            moment: ppts.config.mcsComponentBaseUrl + 'libs/date-time-3.0.0/moment.min',
            momentLocale: ppts.config.mcsComponentBaseUrl + 'libs/fullcalendar-2.6.1/moment-with-locales',
            fullCalendar: ppts.config.mcsComponentBaseUrl + 'libs/fullcalendar-2.6.1/fullcalendar',
            fullCalendarLang: ppts.config.mcsComponentBaseUrl + 'libs/fullcalendar-2.6.1/lang-all',
            ace: ppts.config.mcsComponentBaseUrl + 'libs/ace-1.3.1/ace.min',
            ngFileUpload:ppts.config.mcsComponentBaseUrl + 'libs/ng-file-upload/dist/ng-file-upload-all.min',
            aceExtra: ppts.config.mcsComponentBaseUrl + 'libs/ace-1.3.1/ace-extra.min',
            angular: ppts.config.mcsComponentBaseUrl + 'libs/angular-1.5.0/angular',
            ngLocale: ppts.config.mcsComponentBaseUrl + 'libs/angular-1.5.0/angular-locale_zh-cn',
            uiRouter: ppts.config.mcsComponentBaseUrl + 'libs/angular-ui-router-0.2.18/release/angular-ui-router.min',
            ngSanitize: ppts.config.mcsComponentBaseUrl + 'libs/angular-sanitize-1.4.6/angular-sanitize.min',
            ngResource: ppts.config.mcsComponentBaseUrl + 'libs/angular-resource-1.5.0/angular-resource',
            ngBreadcrumb: ppts.config.mcsComponentBaseUrl + 'libs/angular-breadcrumb-0.4.1/dist/angular-breadcrumb.min',
            ngCalendar: ppts.config.mcsComponentBaseUrl + 'libs/angular-ui-calendar-1.0.0/src/calendar',
            ngCookies: ppts.config.mcsComponentBaseUrl + 'libs/angular-cookies-1.4.6/angular-cookies.min',
            localStorage: ppts.config.mcsComponentBaseUrl + 'libs/angular-cookies-1.4.6/angularLocalStorage.min',
            blockUI: ppts.config.mcsComponentBaseUrl + 'libs/angular-block-ui-0.2.2/dist/angular-block-ui',
            uiBootstrapTpls: ppts.config.mcsComponentBaseUrl + 'libs/ui-bootstrap-1.1.0/ui-bootstrap-tpls-1.1.0.min',
            mcsComponent: ppts.config.mcsComponentBaseUrl + 'libs/mcs-jslib-1.0.0/component/mcs.component',
            //uiSelect: ppts.config.mcsComponentBaseUrl + 'libs/angular-ui-select-0.13.2/dist/select.min',
            dialogs: ppts.config.mcsComponentBaseUrl + 'libs/angular-dialog-service-5.3.0/dist/dialogs.min',
            ppts: ppts.config.pptsComponentBaseUrl + 'build/ppts.global'
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
            select2: {
                exports: 'select2',
                deps: ['jquery']
            },
            select2Locale: {
                exports: 'select2Locale',
                deps: ['select2']
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
            ngFileUpload:{
                exports: 'ngFileUpload',
                deps:['angular']
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
            ngCookies: {
                exports: 'ngCookies',
                deps: ['angular']
            },
            localStorage: {
                exports: 'localStorage',
                deps: ['ngCookies']
            },
            uiBootstrapTpls: {
                exports: 'uiBootstrapTpls',
                deps: ['angular']
            },
            //uiSelect: {
            //    exports: 'uiSelect',
            //    deps: ['angular']
            //},
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
                'select2',
                'select2Locale',
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
                'ngCookies',
                'localStorage',
                'ngFileUpload',
                'uiBootstrapTpls',
                //'uiSelect',
                'mcsComponent',
                'dialogs',
                'ppts'
            ], function () {
                window.onload = function () {
                    if (mcs.browser.s.msie) {
                        if (!sessionStorage.getItem('ppts_expand_sidebar')) {
                            ace.handle_side_menu($);
                            sessionStorage.setItem('ppts_expand_sidebar', true);
                        }
                    };
                };

                angular.element(document).ready(function () {
                    angular.bootstrap(document, ['ppts']);
                    // the script will not run for ie & edge
                    if (!mcs.browser.s.msie) {
                        ace.handle_side_menu($);
                    }
                });
            });
        }
    });
})();