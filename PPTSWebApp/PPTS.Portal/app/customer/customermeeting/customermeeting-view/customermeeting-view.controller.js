/*
    名    称: customermeeting-view.controller
    功能概要: 查看会议Controller js
    作    者: Lucifer
    创建时间: 2016年4月29日 14:45:06
    修正履历：
    修正时间:
*/
define([ppts.config.modules.customer, ppts.config.dataServiceConfig.customerMeetingDataService],
        function (customer) {
            customer.registerController('customerMeetingViewController',
                ['$scope', '$state', '$stateParams', "customerMeetingViewService",
                function ($scope, $state, $stateParams, customerMeetingViewService) {
                    var vm = this;
                    ////初始化数据
                    (function () {
                        customerMeetingViewService.initData($stateParams.id, vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    ////参会对象文本获取事件处理
                    //vm.selectParticipants = function (item, model) {
                    //    vm.customerMeeting.selectParticipantsText = item.value;
                    //}

                    //编辑会议
                    vm.editCustomerMeeting = function () {
                        $state.go('ppts.customermeeting-edit', { id: $stateParams.id, prev: 'ppts.customermeeting' });
                    }
                    vm.cancelEditCustomerMeeting = function () {
                        $state.go('ppts.customermeeting');
                        //$window.history.back();
                    }
                    vm.downloadFile = function (file) {
                        mcs.util.postMockForm(ppts.config.customerApiBaseUrl + "api/customermeetings/downloadmaterial", file);
                    };
                }]);
        });