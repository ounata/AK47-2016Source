define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerDataService],
        function (customer) {
            customer.registerController('customerStaffRelationController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'customerDataService',
                function ($scope, $state, $stateParams, dataSyncService, customerDataService) {
                    var vm = this;

                    vm.data = {
                        headers: [{
                            field: "createTime",
                            name: "开始时间",
                            template: '<span>{{ row.createTime | date:"yyyy-MM-dd" }}</span>'
                        }, {
                            field: "createTime",
                            name: "结束时间",
                            template: '<span>{{ row.createTime | date:"yyyy-MM-dd" }}</span>'
                        }, {
                            field: "orgName",
                            name: "所属分公司"
                        }, {
                            field: "orgName",
                            name: "所属校区"
                        }, {
                            field: "staffJobName",
                            name: "所属岗位"
                        }, {
                            field: "staffName",
                            name: "当前在岗者",
                            template: '<span>{{ row.staffName }}</span>'
                        }],
                        pager: {
                            pageIndex: 1,
                            pageSize: 10,
                            totalCount: -1,
                            pageChange: function () {
                                dataSyncService.initCriteria(vm);
                                customerDataService.getPagedStaffRelations(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;
                                });
                            }
                        },
                        orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
                    }

                    vm.init = function () {
                        dataSyncService.initCriteria(vm);
                        vm.criteria.id = $stateParams.id;
                        vm.criteria.relationType = $stateParams.type;
                        customerDataService.getStaffRelations(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        });
                    };

                    vm.init();

                }]);
        });