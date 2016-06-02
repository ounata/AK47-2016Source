// 依赖于mcs.js, angularjs, 将会在应用程序启动之后执行
(function() {
    'use strict';

    mcs.ng = mcs.ng || angular.module('mcs.ng', ['mcs.ng.datatable', 'mcs.ng.filesUpload', 'mcs.ng.uiCopy', 'mcs.ng.treeControl', 'mcs.ng.paging', 'dialogs.main']);
    mcs.ng.constant('httpErrorHandleMessage', {
        '404': 'no file!',
        '401': 'unauthenticated access!',
        'other': ''
    }).service('httpErrorHandleService', function(httpErrorHandleMessage, dialogs) {

        var httpErrorHandleService = this;
        httpErrorHandleService.process = function(response) {
            //dialogs.error('error', httpErrorHandleMessage[response.StatusCode]);
            var title = response.status + ' ' + response.statusText;
            var message = response.data.message || response.data.description;
            var detail = response.data.messageDetail || response.data.stackTrace;

            dialogs.error(title, message + '<\n>' + detail);
        }

        return this;
    });
})();
