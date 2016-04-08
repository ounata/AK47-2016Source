// 依赖于mcs.js, angularjs, 将会在应用程序启动之后执行
(function() {
    'use strict';

    mcs.ng = mcs.ng || angular.module('mcs.ng', ['mcs.ng.datatable', 'mcs.ng.paging', 'mcs.ng.treeControl']);
    mcs.ng.constant('mcsComponentConfig', {
        rootUrl: mcs.app.config.componentBaseUrl
    });
})();
