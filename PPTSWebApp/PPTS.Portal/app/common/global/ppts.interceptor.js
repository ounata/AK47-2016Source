//loading
ppts.ng.factory('viewLoading', ["$rootScope", 'blockUI', '$q', function ($rootScope, blockUI, $q) {
    var viewLoading = {
        request: function (config) {
            blockUI.start();
            config.headers['pptsCurrentJobID'] = ppts.user.currentJobId;
            config.headers['requestToken'] = ppts.user.token;
            return config;
        },
        response: function (response) {
            blockUI.stop();
            if (response.data && response.data.dictionaries) {
                mcs.util.merge(response.data.dictionaries);
            }
            return response || $q.when(response);
        },
        responseError: function (error) {
            blockUI.stop();
            return $q.reject(error);
        }
    };
    return viewLoading;
}]);