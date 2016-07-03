define([ppts.config.modules.order,
        ppts.config.dataServiceConfig.purchaseCourseDataService],
        function (helper) {
            helper.registerController('orderApproveController', [
                '$scope', '$state', '$stateParams', 'dataSyncService', 'mcsDialogService', 'purchaseCourseDataService',
                function ($scope, $state, $stateParams, dataSyncService, mcsDialogService, purchaseCourseDataService) {

                    //define
                    var vm = this;
                    var orderId = $stateParams.resourceID;
                    var headers = [{
                        field: "productCode",
                        name: "产品编码",
                    }, {
                        field: "productName",
                        name: "产品名称",
                    }, {
                        field: "catalogName",
                        name: "产品类型"
                    }, {
                        field: "grade",
                        name: "年级",
                        template: '<span>{{row.grade | grade}}</span>'
                    }, {
                        field: "courseLevel",
                        name: "课程级别",
                        template: '<span>{{row.courseLevel | courseLevel}}</span>'
                    }, ];



                    //init
                    vm.wfParams = {
                        processID: $stateParams.processID,
                        activityID: $stateParams.activityID,
                        resourceID: $stateParams.resourceID
                    };

                    //function
                    vm.approve = function (clientProcess) {

                        mcsDialogService.info({ title: '提示', message: '审批通过！' })
                           .result.then(function () {
                               location.href = "/";
                               location.reload();
                           });
                    };
                    vm.reject = function (clientProcess) {

                        mcsDialogService.info({ title: '提示', message: '审批拒绝！' })
                           .result.then(function () {
                               location.href = "/";
                               location.reload();
                           });
                    };



                    //load
                    var init=(function () {
                        

                        purchaseCourseDataService.getOrderByWorkflow(vm.wfParams, function (result) {

                            console.log(result);

                            vm.clientProcess = result.clientProcess;

                            vm.account = result.model.account;
                            vm.order = result.model.order;
                            
                            if (vm.order.orderType == 2) {
                                headers = headers.concat([{
                                    name: "课时时长(分钟)",
                                    template: '<span>{{row.lessonDurationValue  }}</span>'
                                }, {
                                    name: "总课次",
                                    template: '<span>{{ row.categoryType==2 ? row.lessonCount : "--" }}</span>'
                                }]);
                            } else {

                                headers = headers.concat([{
                                    name: "课时时长(分钟)",
                                    template: '<span>{{row.periodDurationValue }}</span>'
                                }]);

                            }

                            headers = headers.concat([ {
                                field: "productUnit",
                                name: "产品颗粒度",
                                template: '<span>{{ row.productUnit | unit }}</span>'
                            }, {
                                name: "产品单价（元）",
                                template: '<span>{{row.orderPrice | currency }}</span>',
                            }, {
                                name: "实际单价",
                                template: '{{ row.realPrice | currency }}'
                            }, {
                                name: "订购数量",
                                template: '{{ row.orderAmount }}',
                            }, {
                                name: "特殊折扣",
                                template: '{{ row.specialRate >=0 ?row.specialRate:"--" }}',
                            }, {
                                name: "订购金额",
                                template: '{{ row.realPrice*row.realAmount | currency}}',
                            }]);


                            vm.data = {
                                headers: headers,
                                pager: {
                                    pageable: false
                                },
                                orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }],
                            }

                            vm.totalPrice = function () {
                                var total = 0;
                                $(result.model.items).each(function (i, v) { total += v.orderPrice * v.orderAmount; });
                                return total;
                            };
                            vm.totalRealPrice = function () {
                                var total = 0;
                                $(result.model.items).each(function (i, v) {
                                    total += v.realPrice * v.realAmount * v.specialRate;
                                });
                                return total;
                            };


                            vm.data.rows = result.model.items;
                            $scope.$broadcast('dictionaryReady');
                        });

                    })();

                }]);

        });

