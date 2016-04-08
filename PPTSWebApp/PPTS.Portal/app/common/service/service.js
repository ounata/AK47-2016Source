'use strict';
define(['angular'], function(ng) {
    var pptsService = ng.module('ppts.service', []);


    pptsService.service('exceptionHandleConfig', function(dialogs) {
        var exceptionHandleConfig = this;
        exceptionHandleConfig.messages = {
            '404': {
                title: 'note',
                content: 'response is empty'
            },
            '-1': {
                title: 'error',
                content: 'some error occur'
            }
        };
            
            
            
            exceptionHandleConfig.handlers = {
            '404': function() {

                dialogs.error(exceptionHandleConfig['404'].title,exceptionHandleConfig['404'].content);

            },
            '-1': function($q, response) {
                var defer = $q.defer();
                //show modal dialog
                defer.reject({
                    result: 'error'
                });

                return defer.promise;

            },
            'other': function($q, response) {

                dialogs.error(exceptionHandleConfig['other'].title,exceptionHandleConfig['other'].content);
            }
        }

        return exceptionHandleConfig;
    });


    pptsService.factory('apiInterceptor', function($q) {
        var apiInterceptor = {
            response: function(response) {
            },

            responseError: function(response) {
            }
        };

        return apiInterceptor;

    });

    pptsService.service('exceptionHandle', function(exceptionHandleConfig) {
        var exceptionHandle = this;
        exceptionHandle.process = function(data) {
            if (!data) {
                return;
            }

            if (data.message) {
                dialogs.error(data.message.title, data.message.content);
            } else {
                exceptionHandleConfig.handlers[data.code] ? exceptionHandleConfig.handlers[data.code]() : exceptionHandleConfig.handlers['other']();
            }


        }
    });
});