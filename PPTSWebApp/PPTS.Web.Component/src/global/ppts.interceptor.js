//loading
ppts.ng.factory('viewLoading', ["$rootScope", '$q', 'storage', 'blockUI', function ($rootScope, $q, storage, blockUI) {
    var viewLoading = {
        request: function (config) {
            blockUI.start();
            config.headers['pptsCurrentJobID'] = storage.get('ppts.user.currentJobId');
            return config;
        },
        response: function (response) {          
            if (response.data && response.data.dictionaries) {
                mcs.util.merge(response.data.dictionaries);
            }
            blockUI.stop();
            return response || $q.when(response);
        },
        responseError: function (error) {
            blockUI.stop();
            return $q.reject(error);
        }
    };
    return viewLoading;
}]);