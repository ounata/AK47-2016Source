// 依赖于mcs.js, angularjs, 将会在应用程序启动之后执行
(function() {
    'use strict';

    mcs.ng = mcs.ng || angular.module('mcs.ng', ['mcs.ng.datatable', 'mcs.ng.filesUpload', 'mcs.ng.uiCopy', 'mcs.ng.treeControl', 'mcs.ng.paging', 'dialogs.main']);
    mcs.ng.constant('httpErrorHandleMessage', {
        '404': 'no file!',
        '401': 'unauthenticated access!',
        'other': ''
    }).service('httpErrorHandleService', function(httpErrorHandleMessage, dialogs, $sce) {

        var httpErrorHandleService = this;
        httpErrorHandleService.process = function(response) {
            //dialogs.error('error', httpErrorHandleMessage[response.StatusCode]);
            //var title = response.status + ' ' + response.statusText;
            var title = '操作失败';
            var message = response.data.message || response.data.description;
            var detail = response.data.messageDetail || response.data.stackTrace;

            var errorDetail = '<span> <a style="cursor:pointer" data-toggle="collapse" data-target="#dialogErrorBody">' + message + '</a></span><div id="dialogErrorBody" class="collapse out">' + detail + '</div>';

            dialogs.error(title, $sce.trustAsHtml(errorDetail));
        };

        return this;
    });

    mcs.ng.factory('safeApply', function($rootScope) {
        return function(scope, fn) {
            var phase = scope.$root.$$phase;
            if (phase == '$apply' || phase == '$digest') {
                if (fn && (typeof(fn) === 'function')) {
                    fn();
                }
            } else {
                scope.$apply(fn);
            }
        };
    });


    //for controller scope event mechanism
    mcs.ng.factory('eventService', function() {

        var eventService = this;

        eventService.registerEvent = function(eventName) {
            eventService[eventName] = [];
        };

        eventService.unRegisterEvent = function(eventName) {
            delete eventService[eventName];
        };

        eventService.fire = function(eventName, eventArgs) {

            eventService[eventName].forEach(function(handler) {
                handler(eventArgs);
            });

        };

        eventService.clearHandler = function(eventName) {
            eventService[eventName] = [];
        };

        eventService.bind = function(eventName, handler) {
            eventService[eventName].push(handler);
        };

        return eventService;

    });

})();
