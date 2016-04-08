// 依赖于mcs.js, angularjs, 将会在应用程序启动之后执行
(function() {
    'use strict';

    mcs.ng = mcs.ng || angular.module('mcs.ng', ['mcs.ng.datatable', 'mcs.ng.paging', 'dialogs.main']);
    mcs.ng.constant('mcsComponentConfig', {
        rootUrl: mcs.app.config.componentBaseUrl
    })


    .constant('httpErrorHandleMessage', {
        '404': 'no file!',
        '401': 'unauthenticated access!',
        'other': ''
    })

    .service('httpErrorHandleService', function(httpErrorHandleMessage, dialogs) {

        var httpErrorHandleService = this;
        httpErrorHandleService.process = function(response) {
            dialogs.error('error', httpErrorHandleMessage[response.StatusCode]);
        }

        return this;

    });
})();
