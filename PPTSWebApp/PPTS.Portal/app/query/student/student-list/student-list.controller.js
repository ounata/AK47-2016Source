define([ppts.config.modules.query,
        ppts.config.dataServiceConfig.studentQueryService], function (query) {
            query.registerController('studentListController', [
                'dataSyncService', 'studentQueryService', 'studentListDataHeader',
                function (dataSyncService, studentQueryService, studentListDataHeader) {
                    var vm = this;

                    /*
                    dataSyncService.configDataHeader(vm, studentListDataHeader, studentQueryService.queryCustomer);

                    vm.search = function () {
                        dataSyncService.initDataList(vm, studentQueryService.queryCustomer);
                    };
                    vm.search();
                    */

                }]);
        });