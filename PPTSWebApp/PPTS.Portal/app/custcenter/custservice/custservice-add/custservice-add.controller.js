define([ppts.config.modules.custcenter,
        ppts.config.dataServiceConfig.custserviceDataService],
        function (customer) {
            customer.registerController('custserviceAddController', [
                'dataSyncService', 'customerDataViewService', 'utilService', 'customerRelationType', 'customerAdvanceSearchItems',
                function (dataSyncService, customerDataViewService, util, relationType, searchItems) {
                    var vm = this;

                  
                }]);
        });