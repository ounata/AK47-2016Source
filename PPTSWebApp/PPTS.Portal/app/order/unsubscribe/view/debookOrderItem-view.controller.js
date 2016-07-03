define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.unsubscribeCourseDataService], function (helper) {
            helper.registerController('debookOrderItem-viewController', [
                '$scope', '$state', 'dataSyncService', 'unsubscribeCourseDataService', '$uibModalInstance', 'data',
                function ($scope, $state, dataSyncService, unsubscribeCourseDataService, $uibModalInstance, data) {
                    var vm = this;

                    vm.data = {
                        headers: [{
                            field: "debookTime",
                            name: "上课日期",
                            template: '<span>{{row.debookTime|date:"yyyy-MM-dd"}}</span>'
                        }, {
                            field: "debookAmount",
                            name: "类型",
                            template: '<span>{{row.debookAmount}}</span>'
                        }, {
                            field: "debookMoney",
                            name: "上课时段",
                            template: '<span>{{row.debookMoney| currency }}</span>'
                        }, {
                            field: "submitterName",
                            name: "实际课时/小时",
                            template: '<span>{{row.submitterName}}</span>'
                        }, {
                            field: "submitterJobName",
                            name: "单价(元)",
                            template: '<span>{{row.submitterJobName}}</span>'
                        }, {
                            field: "submitterJobName",
                            name: "上课教师",
                            template: '<span>{{row.submitterJobName}}</span>'
                        }, {
                            field: "submitterJobName",
                            name: "课时收入扣除(元)",
                            template: '<span>{{row.submitterJobName}}</span>'
                        }, {
                            field: "submitterJobName",
                            name: "课时收入返还(元)",
                            template: '<span>{{row.submitterJobName}}</span>'
                        }, {
                            field: "submitterJobName",
                            name: "订购剩余金额",
                            template: '<span>{{row.submitterJobName}}</span>'
                        }]
                    }

                    vm.search = function () {
                        unsubscribeCourseDataService.getDebookOrderDetial( data.row.billID , function (result) {
                            vm.data.rows = [result];
                        })
                    }

                    vm.search();

                    //关闭窗口
                    vm.close = function () {
                        $uibModalInstance.dismiss('Canceled');                        
                    };
                }]);
        });