define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService],
    function (customer) {
        customer.registerController('parentsViewController', [
            '$scope',
            '$state',
            '$stateParams',
            'customerService',
            'dataSyncService',
            'utilService',
            'customerDataService',
            function ($scope, $state, $stateParams, customerService, dataSyncService, util, customerDataService) {
                var vm = this;

                vm.data = {
                    selection: 'radio',
                    rowsSelected: [],
                    keyFields: ['parentID'],
                    headers: [{
                        field: "parentName",
                        name: "家长姓名"
                    }, {
                        field: "parentRole",
                        name: "亲属关系",
                        template: '<span ng-if="row.gender==1">{{ row.parentRole | parentMale | normalize }}</span>'
                                + '<span ng-if="row.gender==2">{{ row.parentRole | parentFemale | normalize }}</span>'
                    }, {
                        field: "isPrimary",
                        name: "是否主监护人",
                        template: '<span>{{ row.isPrimary | ifElse }}</span>'
                    }],
                    pagable: false,
                    orderBy: [{ dataField: 'isPrimary', sortDirection: 0 }]
                }

                vm.init = function () {
                    customerService.handle('get-parents', function (result) {
                        vm.isCustomer = result.isCustomer;
                        vm.customerName = result.customerName;
                        vm.data.rows = result.queryResult;
                        if (!vm.data.rows) return;
                        dataSyncService.injectDynamicDict('ifElse');
                        vm.data.rows.filter(function (obj) {
                            if (obj.isPrimary == true) {
                                vm.parent = obj;
                            }
                        });
                        $scope.$broadcast('dictionaryReady');
                    });
                };

                vm.showParentInfo = function (rowData) {
                    vm.parent = rowData;
                };

                vm.edit = function () {
                    if (util.selectOneRow(vm)) {
                        customerService.handle('get-parent', { parentID: vm.data.rowsSelected[0].parentID });
                    }
                };

                $scope.$watch('vm.data.rowsSelected[0].parentID', function () {
                    if (!vm.data.rowsSelected || !vm.data.rowsSelected.length) return;
                    var parentID = vm.data.rowsSelected[0].parentID;
                    vm.data.rows.filter(function (obj) {
                        if (obj.parentID == parentID)
                            vm.parent = obj;
                    });
                });
                vm.init();
            }]);
    });