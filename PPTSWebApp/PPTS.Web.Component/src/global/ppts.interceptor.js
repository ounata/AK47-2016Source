//loading
ppts.ng.factory('viewLoading', ["$rootScope", '$q', 'storage', 'userService', function ($rootScope, $q, storage, userService) {
    var viewLoading = {
        request: function (config) {
           
            config.headers['pptsCurrentJobID'] = storage.get('ppts.user.currentJobId');
            config.headers['x-session-token'] = userService.sessionToken;
            return config;
        },
        response: function (response) {          
            if (response.data && response.data.dictionaries) {
                // 过滤掉失效的字典值
                var result = {};
                var dict = response.data.dictionaries;
                for (var prop in dict) {
                    result[prop] = result[prop] || [];
                    for (var index in dict[prop]) {
                        var item = dict[prop][index];
                        // 兼容自定义字典
                        if (item.isValidate == undefined || item.isValidate) {
                            result[prop].push(item);
                        }
                    }
                }
                mcs.util.merge(result);
            }
           
            return response || $q.when(response);
        },
        responseError: function (error) {
            
            return $q.reject(error);
        }
    };
    return viewLoading;
}]);