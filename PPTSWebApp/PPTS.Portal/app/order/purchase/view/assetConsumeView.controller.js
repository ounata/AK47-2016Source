define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService],
        function (helper) {
            helper.registerController('assetConsumeViewController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'purchaseCourseDataService', '$uibModalInstance', 'data',
                function ($scope, $state, $stateParams, dataSyncService, purchaseCourseDataService, $uibModalInstance, data) {
                    
                    var vm = this;

                    //列表
                    vm.data = {  
                        keyFields: ['consumeID'],
                        pager: {
                            pageIndex: 1,
                            pageSize: ppts.config.pageSizeItem,
                            totalCount: -1,
                            pageChange: function () {
                                purchaseCourseDataService.getPageAssetConsumeViews(vm.criteria, function (result) {
                                    vm.data.rows = result.pagedData;                                    
                                });
                            }
                        },
                        orderBy: [{ dataField: 'consumeTime', sortDirection: 1 }],
                        headers: [{
                            field: "consumeTimeView",
                            name: "上课日期",
                            headerCss: 'datatable-header-align-right',
                            template: '<span>{{row.consumeTimeView}}</span>'
                        }, {
                            field: "consumeType",
                            name: "类型",
                            headerCss: 'datatable-header-align-right',
                            template: '<span>{{row.consumeType|consumeType}}</span>'
                        }, {
                            field: "startTime2EndTime",
                            name: "上课时段",
                            headerCss: 'datatable-header-align-right',
                            template: '<span>{{row.startTime2EndTime}}</span>'
                        }, {
                            field: "hours",
                            name: "实际课时/小时",
                            headerCss: 'datatable-header-align-right',
                            template: '<span>{{row.hours}}</span>'
                        }, {
                            field: "price",
                            name: "单价(元)",
                            headerCss: 'datatable-header-align-right',
                            template: '<span>{{row.price| currency:"￥" | normalize}}</span>'
                        }, {
                            field: "teacherName",
                            name: "上课教师",
                            headerCss: 'datatable-header-align-right',
                            template: '<span>{{row.teacherName}}</span>'
                        }, {
                            field: "inComeMonye",
                            name: "课时收入扣除(元)",
                            headerCss: 'datatable-header-align-right',
                            template: '<span>{{row.inComeMonye| currency:"￥" | normalize}}</span>'
                        }, {
                            field: "expendMoney",
                            name: "课时收入返还(元)",
                            headerCss: 'datatable-header-align-right',
                            template: '<span>{{row.expendMoney| currency:"￥" | normalize}}</span>'
                        }, {
                            field: "assetMoney",
                            name: "订购剩余金额",
                            headerCss: 'datatable-header-align-right',
                            template: '<span>{{row.assetMoney| currency:"￥" | normalize}}</span>'
                        }
                        ]
                    }

                    vm.search = function () {
                        dataSyncService.initCriteria(vm);
                        vm.criteria.assetRefPID = data.row.billID;
                        purchaseCourseDataService.getAssetConsumeViews(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            dataSyncService.updateTotalCount(vm, result.queryResult);
                            $scope.$broadcast('dictionaryReady');
                        }, function () { })
                    }

                    vm.search();

                    //关闭窗口
                    vm.close = function () {
                        $uibModalInstance.dismiss('Canceled');
                    };
                }]);

        });

