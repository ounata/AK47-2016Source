'use strict';

angular.module('app.common', []);
angular.module('app.main', []);
angular.module('app.component', ['ui.router', 'ngTagsInput', 'ngFileUpload']);
angular.module('app.widget', []);

angular.module('app.lib', ['ngSanitize', 'ui.select', 'app.common']);
angular.module('app.page', []);
angular.module('app.issue', []);

angular.module('app', [
        'ngCookies', 'ngSanitize', 'ui.router', 'ui.bootstrap', 'blockUI', 'mcs.ng', 'app.common',
        'app.main', 'app.lib', 'app.component', 'app.widget', 'app.page', 'app.issue', 'dialogs.main'
    ])
    .config(['$stateProvider', '$urlRouterProvider', function($stateProvider, $urlRouterProvider) {
        $urlRouterProvider.otherwise("/main");

        $stateProvider.state('main', {
                url: '/main',
                templateUrl: 'app/main/main.html'
            }).state('select', {
                url: '/select',
                templateUrl: 'app/component/mcs-select/main.html',
                controller: 'MCSSelectController',
                controllerAs: 'vm'
            }).state('datatable', {
                url: '/datatable',
                templateUrl: 'app/component/mcs-datatable/main.html',
                controller: 'MCSDatatableController',
                controllerAs: 'vm'
            }).state('label', {
                url: '/label',
                templateUrl: 'app/component/mcs-label/main.html',
                controller: 'MCSLabelController',
                controllerAs: 'vm'
            }).state('inputs', {
                url: '/inputs',
                templateUrl: 'app/component/mcs-input/main.html',
                controller: 'MCSInputController',
                controllerAs: 'vm'
            }).state('buttons', {
                url: '/buttons',
                templateUrl: 'app/component/mcs-button/main.html',
                controller: 'MCSButtonController',
                controllerAs: 'vm'
            }).state('checkboxgroup', {
                url: '/checkboxgroup',
                templateUrl: 'app/component/mcs-checkboxgroup/main.html',
                controller: 'MCSCheckboxGroupController',
                controllerAs: 'vm'
            }).state('radiobuttongroup', {
                url: '/radiobuttongroup',
                templateUrl: 'app/component/mcs-radiobuttongroup/main.html',
                controller: 'MCSRadiobuttonGroupController',
                controllerAs: 'vm'
            })
            .state('dropdownButton', {
                url: '/dropdownButton',
                templateUrl: 'app/component/mcs-dropdown-button/main.html',
                controller: 'MCSDropdownButtonController',
                controllerAs: 'vm'
            })
            .state('autoComplete', {
                url: '/autoComplete',
                templateUrl: 'app/component/mcs-autocomplete/main.html',
                controller: 'MCSAutoCompleteController',
                controllerAs: 'vm'
            })
            .state('cascadingSelect', {
                url: '/cascadingSelect',
                templateUrl: 'app/component/mcs-cascadingselect/main.html',
                controller: 'MCSCascadingSelectController',
                controllerAs: 'vm'
            }).state('tab', {
                url: '/tab',
                templateUrl: 'app/component/mcs-tab/main.html',
                controller: 'MCSTabController',
                controllerAs: 'vm'
            }).state('tab.one', {
                url: "/one",
                templateUrl: "app/component/mcs-tab/tab.one.html"
            }).state('tab.two', {
                url: "/two",
                templateUrl: "app/component/mcs-tab/tab.two.html"
            }).state('dialog', {
                url: '/dialog',
                templateUrl: 'app/component/mcs-dialog/main.html',
                controller: 'MCSCascadingSelectController',
                controllerAs: 'vm'
            })
            .state('datepicker', {
                url: '/datepicker',
                templateUrl: 'app/component/mcs-datepicker/main.html',
                controller: 'MCSDatetimePickerController',
                controllerAs: 'vm'
            })

        .state('layout', {
            url: '/layout',
            templateUrl: 'app/component/mcs-layout/main.html',
            controller: 'MCSLayoutController',
            controllerAs: 'vm'
        })

        .state('uiCopy', {
            url: '/uiCopy',
            templateUrl: 'app/component/mcs-uiCopy/main.html',
            controller: 'MCSUICopyController',
            controllerAs: 'vm'
        })

        .state('print', {
            url: '/print',
            templateUrl: 'app/component/mcs-print/main.html',
            controller: 'MCSPrintController',
            controllerAs: 'vm'
        })

        .state('validation', {
                url: '/validation',
                templateUrl: 'app/component/mcs-validation/main-new.html',
                controller: 'MCSValidationController',
                controllerAs: 'vm'
            })
            .state('router', {
                url: '/router',
                templateUrl: 'app/component/mcs-route/main.html',
                controller: 'routeController',
                controllerAs: 'vm'
            })

        .state('tree', {
                url: '/tree',
                templateUrl: 'app/component/mcs-tree/main.html',
                controller: 'MCSTreeController',
                controllerAs: 'vm'
            })
            .state('error', {
                url: '/error',
                templateUrl: 'app/component/mcs-error/main.html'

            })

        .state('upload', {
            url: '/upload',
            templateUrl: 'app/component/mcs-upload/main.html',
            controller: 'MCSUploadController',
            controllerAs: 'vm'
        })

        .state('simplequery', {
            url: '/simplequery',
            templateUrl: 'app/page/simple.html',
            controller: 'MCSPageController',
            controllerAs: 'vm'
        }).state('advancequery', {
            url: '/advancequery',
            templateUrl: 'app/page/advance.html',
            controller: 'MCSPageController',
            controllerAs: 'vm'
        }).state('view', {
            url: '/view',
            templateUrl: 'app/page/view.html',
            controller: 'MCSPageController',
            controllerAs: 'vm'
        }).state('create', {
            url: '/create',
            templateUrl: 'app/page/create.html',
            controller: 'MCSPageController',
            controllerAs: 'vm'
        }).state('edit', {
            url: '/edit',
            templateUrl: 'app/page/edit.html',
            controller: 'MCSPageController',
            controllerAs: 'vm'
        }).state('serializedate', {
            url: '/serializedate',
            templateUrl: 'app/issue/date-serialize.html',
            controller: 'MCSDateSerializeController',
            controllerAs: 'vm'
        }).state('pptsdatarange', {
            url: '/ppts-datarange',
            templateUrl: 'app/widget/ppts-datarange/main.html',
            controller: 'PPTSDatarangeController',
            controllerAs: 'vm'
        }).state('pptscheckboxgroup', {
            url: '/ppts-checkbox-group',
            templateUrl: 'app/widget/ppts-checkbox-group/main.html',
            controller: 'PPTSCheckboxGroupController',
            controllerAs: 'vm'
        }).state('pptsradiobuttongroup', {
            url: '/ppts-radiobutton-group',
            templateUrl: 'app/widget/ppts-radiobutton-group/main.html',
            controller: 'PPTSRadiobuttonGroupController',
            controllerAs: 'vm'
        }).state('pptsselect', {
            url: '/ppts-select',
            templateUrl: 'app/widget/ppts-select/main.html',
            controller: 'PPTSSelectController',
            controllerAs: 'vm'
        }).state('pptsdatepicker', {
            url: '/ppts-datepicker',
            templateUrl: 'app/widget/ppts-datepicker/main.html',
            controller: 'PPTSDatepickerController',
            controllerAs: 'vm'
        }).state('pptsdaterangepicker', {
            url: '/ppts-daterangepicker',
            templateUrl: 'app/widget/ppts-daterangepicker/main.html',
            controller: 'PPTSDaterangepickerController',
            controllerAs: 'vm'
        }).state('pptsdatetimepicker', {
            url: '/ppts-datetimepicker',
            templateUrl: 'app/widget/ppts-datetimepicker/main.html',
            controller: 'PPTSDatetimepickerController',
            controllerAs: 'vm'
        });
    }])
    .config(['$httpProvider', function($httpProvider) {
        $httpProvider.defaults.headers.put['Content-Type'] = 'application/x-www-form-urlencoded';
        $httpProvider.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded';

        // Override $http service's default transformRequest
        $httpProvider.defaults.transformRequest = [function(data) {
            /**
             * The workhorse; converts an object to x-www-form-urlencoded serialization.
             * @param {Object} obj
             * @return {String}
             */
            var param = function(obj) {
                var query = '';
                var name, value, fullSubName, subName, subValue, innerObj, i;

                for (name in obj) {
                    value = obj[name];

                    if (value instanceof Array) {
                        for (i = 0; i < value.length; ++i) {
                            subValue = value[i];
                            fullSubName = name + '[' + i + ']';
                            innerObj = {};
                            innerObj[fullSubName] = subValue;
                            query += param(innerObj) + '&';
                        }
                    } else if (value instanceof Object) {
                        for (subName in value) {
                            subValue = value[subName];
                            fullSubName = name + '[' + subName + ']';
                            innerObj = {};
                            innerObj[fullSubName] = subValue;
                            query += param(innerObj) + '&';
                        }
                    } else if (value !== undefined && value !== null) {
                        query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
                    }
                }

                return query.length ? query.substr(0, query.length - 1) : query;
            };

            return angular.isObject(data) && String(data) !== '[object File]' ? param(data) : data;
        }];
    }])
    .config(['blockUIConfig', function(blockUIConfig) {
        blockUIConfig.message = '正在加载数据 ...';
    }]);
