(function() {
    'use strict';
    mcs.ng.service('autoCompleteDataService', function($http) {
        var autoCompleteDataService = {};
        autoCompleteDataService.query = function(url, query, filter) {

            return $http.post(url, query, {
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'autoComplete': true
                }
            }).then(function(result) {
                if (result.data) {
                    return filter ? filter()(result.data) : result.data;
                }
            });
        };

        return autoCompleteDataService;
    });
})();
