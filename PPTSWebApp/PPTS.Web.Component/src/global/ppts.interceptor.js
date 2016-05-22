//loading
ppts.ng.factory('viewLoading', ["$rootScope", '$q', 'storage', function ($rootScope, $q, storage) {
    var viewLoading = {
        request: function (config) {         
            config.headers['pptsCurrentJobID'] = storage.get('ppts.user.currentJobId');
            return config;
        },
        response: function (response) {          
            if (response.data && response.data.dictionaries) {
                mcs.util.merge(response.data.dictionaries);
            }
            return response || $q.when(response);
        },
        responseError: function (error) {        
            return $q.reject(error);
        }
    };
    return viewLoading;
}]);