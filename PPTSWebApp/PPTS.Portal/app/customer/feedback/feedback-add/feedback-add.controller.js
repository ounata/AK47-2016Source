/*
    名    称: feedback-add.controller
    功能概要: 学大反馈【联系家长】 Controller js
    作    者: Lucifer
    创建时间: 2016年5月10日 17:12:42
    修正履历：
    修正时间:
*/
define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.feedbackDataService],
        function (customer) {
            customer.registerController('feedbackAddController', ['$scope', '$location', '$state', '$stateParams', 'feedbackDataService', 'feedbackAddDataViewService', 'mcsValidationService',
                function ($scope, $location, $state, $stateParams, feedbackDataService, feedbackAddDataViewService, mcsValidationService) {
                    var vm = this;
                    vm.weekFeedbackMemo = "500";
                    vm.replyParentMemo = "500";

                    mcsValidationService.init($scope);
                    // 初始化
                    vm.customerId = $stateParams.id;
                    feedbackAddDataViewService.init(vm);

                    //新增
                    vm.addCustomerreplies = function () {
                        //if (!vm.weekFeedback && !vm.replyParent) {
                        //    alert("周反馈与家长回复不能同时为空!");
                        //    return;
                        //}
                        //if (0 == $.trim(vm.weekFeedback).length && 0 == $.trim(vm.replyParent.length)) {
                        //    alert("周反馈与家长回复不能同时为空!");
                        //    return;
                        //}
                        if (mcsValidationService.run($scope)) {

                            var items = [];
                            if (vm.weekFeedback) {
                                var weekFeedback = {
                                    customerID: $stateParams.id,
                                    replyObject: 6,
                                    replyContent: vm.weekFeedback
                                };
                                items.push(weekFeedback);
                            }
                            if (vm.replyParent) {
                                var replyParent = {
                                    customerID: $stateParams.id,
                                    replyObject: 3,
                                    replyContent: vm.replyParent
                                };
                                items.push(replyParent);
                            }

                            vm.customerId = $stateParams.id;
                            vm.items = items;
                            feedbackDataService.createCustomerReplies(vm, function () {
                                //alert("添加成功!");
                                var par = {};
                                var url = "";
                                if ($stateParams.prev == "ppts.feedback"){
                                    par = { customerId: $stateParams.id, prev: $stateParams.prev };
                                    url = "ppts.feedback-view";
                                }
                                else {
                                    url = "ppts.student-view.feedbacks";
                                    par = { id: $stateParams.id, prev: $stateParams.prev };
                                }
                                $state.go(url, par);
                            });
                        }
                    }

                    vm.cancel = function () {
                        $state.go('ppts.feedback-view');
                    };
                    $scope.$watch('vm.weekFeedback', function (newValue, oldValue, scope) {
                        if (newValue){
                            vm.weekFeedbackMemo = (500 - newValue.length);
                        }
                    });
                    $scope.$watch('vm.replyParent', function (newValue, oldValue, scope) {
                        if (newValue) {
                            vm.replyParentMemo = (500 - newValue.length);
                        }
                    });
                    
                }]);
        });