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
            customer.registerController('feedbackListController', ['$scope', 'feedbackDataViewService',
                function ($scope, feedbackDataViewService, searchItems) {
                    var vm = this;

                    // 配置跟列表数据表头
                    feedbackDataViewService.configFeedbackListHeaders(vm);

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        if (vm.criteria) {
                            vm.criteria.replyObjects = new Array();
                            //家长反馈
                            if (vm.criteria.replyType == "7") {
                                vm.criteria.poster = "2";
                            }
                            else {
                                vm.criteria.replyObjects.push(vm.criteria.replyType);
                            }
                            vm.criteria.replyObjects.push(vm.criteria.replyObject);

                        }
                        if (vm.criteria) {
                            if ("" == vm.criteria.replyObject)
                                vm.criteria.replyObject = undefined;
                        }
                        feedbackDataViewService.initFeedbackList(vm, function () {
                            //vm.searchItems = searchItems;

                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    vm.export = function () {
                        mcs.util.postMockForm(ppts.config.customerApiBaseUrl + '/api/feedback/exportCustomerReplies', vm.criteria);
                    }
                    vm.search();
                    vm.criteria.replyObject = "2";
                }]);
        });