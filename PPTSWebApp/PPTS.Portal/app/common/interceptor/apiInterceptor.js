define(['angular'], function (ng) {
    'use strict';
    ng.constant('apiInterceptorConfig', {
        message: {
            errorMessage: '发生错误!',
            exceptionMessage: '请联系管理员!'
        },
        handler: {
            'dataEmpty': function ($q) {
                var defer = $q.defer();
                //show modal dialog
                defer.notify({
                    status: 'done',
                    result: ''
                });
                return defer.promise;
            },
            'validataFailed': function ($q) {
                var defer = $q.defer();
                //show modal dialog
                defer.resolve({
                    status: 'done',
                    result: ''
                });
                return defer.promise;
            },
            'other': function ($q, response) {
                var defer = $q.defer();
                defer.resolve(response.result);
                return defer.promise;
            }
        }
    }).factory('apiInterceptor', function ($q, apiInterceptorConfig) {
        var apiInterceptor = {
            response: function (response) {
                apiInterceptorConfig[response.code] ? apiInterceptorConfig[response.code]($q) : apiInterceptorConfig['other']($q, response);
            },

            responseError: function () {
                apiInterceptorConfig[response.code] ? apiInterceptorConfig[response.code]($q) : apiInterceptorConfig['other']($q, response);
            }
        };
    });
});