//loading
ppts.ng.factory('timestampMarker', ["$rootScope", 'blockUI', function ($rootScope, blockUI) {
    var timestampMarker = {
        request: function (config) {
            blockUI.start();
            $rootScope.loading = true;
            config.requestTimestamp = new Date().getTime();
            return config;
        },
        response: function (response) {
            blockUI.stop();
            if (response.data && response.data.dictionaries) {
                mcs.util.merge(response.data.dictionaries);
            }
            $rootScope.loading = false;
            response.config.responseTimestamp = new Date().getTime();
            return response;
        }
    };
    return timestampMarker;
}]);