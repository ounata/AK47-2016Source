/*
    名    称: feedback-list.controller
    功能概要: 学大反馈列表 Controller js
    作    者: Lucifer
    创建时间:2016年5月9日 11:17:26
    修正履历：
    修正时间:
*/
define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.feedbackDataService],
        function (customer) {
            customer.registerController('feedbackListController', ['$scope', 'dataSyncService', 'feedbackDataService', 'feedbackListDataHeader',
                function ($scope, dataSyncService, feedbackDataService, feedbackListDataHeader) {
                    var vm = this;
                    if (!vm.criteria) {
                        vm.criteria = {};
                        vm.criteria.replyType = "6";
                        var dFirst = new Date();
                        dFirst = new Date(dFirst.setDate(1));
                        dFirst = new Date(dFirst.setHours(0));
                        dFirst=new Date(dFirst.setMinutes(0));
                        dFirst=new Date(dFirst.setMilliseconds(0));
                        vm.criteria.replyTimeStart = dFirst;
                        vm.criteria.replyTimeEnd = new Date();
                    }

                    // 配置跟列表数据表头
                    //feedbackDataViewService.configFeedbackListHeaders(vm);

                    // 配置数据表头 
                    dataSyncService.configDataHeader(vm, feedbackListDataHeader, feedbackDataService.GetPagedCustomerRepliesList, function () {
                        $scope.$broadcast('dictionaryReady');
                    });

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        if (vm.criteria) {
                            vm.criteria.replyObjects = [];

                            //家长反馈
                            if (vm.criteria.replyType == "7") {
                                vm.criteria.poster = "2";
                            }
                            else {
                                vm.criteria.replyObjects.push(vm.criteria.replyType);
                            }
                            vm.criteria.replyObjects.push(vm.criteria.replyObject);
                            //移除默认值是空字符串的项
                            vm.criteria.replyObjects.splice(0, 1);
                            for (var index in vm.criteria.replyObjects) {
                                if (!vm.criteria.replyObjects[index] || "" == vm.criteria.replyObjects[index]) {
                                    vm.criteria.replyObjects.splice(index, 1);
                                }
                            }
                        }

                        if (vm.criteria) {
                            if ("" == vm.criteria.replyObject)
                                vm.criteria.replyObject = undefined;
                        }
                        dataSyncService.initDataList(vm, feedbackDataService.GetCustomerRepliesList, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    };

                    //// 页面初始化加载或重新搜索时查询
                    //vm.search = function () {
                    //    if(vm.criteria) {
                    //        vm.criteria.replyObjects = [];

                    //        //家长反馈
                    //        if (vm.criteria.replyType == "7") {
                    //            vm.criteria.poster = "2";
                    //        }
                    //        else {
                    //            vm.criteria.replyObjects.push(vm.criteria.replyType);
                    //        }
                    //        vm.criteria.replyObjects.push(vm.criteria.replyObject);
                    //        //移除默认值是空字符串的项
                    //        vm.criteria.replyObjects.splice(0, 1);
                    //        for (var index in vm.criteria.replyObjects) {
                    //            if (!vm.criteria.replyObjects[index] || "" == vm.criteria.replyObjects[index]) {
                    //                vm.criteria.replyObjects.splice(index, 1);
                    //            }
                    //        }
                    //    }

                    //    if (vm.criteria) {
                    //        if ("" == vm.criteria.replyObject)
                    //            vm.criteria.replyObject = undefined;
                    //    }
                    //    feedbackDataViewService.initFeedbackList(vm, function () {
                    //        //vm.searchItems = searchItems;

                    //        $scope.$broadcast('dictionaryReady');
                    //    });
                    //};
                    vm.export = function () {
                        mcs.util.postMockForm(ppts.config.customerApiBaseUrl + '/api/feedback/exportCustomerReplies', vm.criteria);
                    }
                    vm.search();
                    vm.criteria.replyObject = "2";
                }]);
        });