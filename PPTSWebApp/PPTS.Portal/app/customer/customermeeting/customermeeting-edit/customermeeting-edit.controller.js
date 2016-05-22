/*
    名    称: customermeeting-edit.controller
    功能概要: 编辑会议Controller js
    作    者: Lucifer
    创建时间: 2016年4月29日 14:45:06
    修正履历：
    修正时间:
*/
define([ppts.config.modules.customer, ppts.config.dataServiceConfig.customerMeetingDataService],
        function (customer) {
            customer.registerController('customerMeetingEditController',
                ['$scope', "$state", '$stateParams', "customerMeetingEditDataViewService",
                function ($scope, $state, $stateParams, customerMeetingEditDataViewService) {
                    var vm = this;
                    
                    ////初始化数据
                    (function () {
                        customerMeetingEditDataViewService.initData($stateParams.id, vm,function () {
                            //vm.customerMeeting.CustomerID = $stateParams.id;
                            //vm.organizerName = "张三";
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    //参会对象文本获取事件处理
                    vm.selectParticipants = function (item, model) {
                        vm.customerMeeting.selectParticipantsText = item.value;
                    }

                    //编辑会议
                    vm.editCustomerMeeting = function () {
                        customerMeetingEditDataViewService.saveCustomerMeetings(vm, function () {
                            alert("保存成功!");
                            $scope.$broadcast('dictionaryReady');
                        });
                    }
                    vm.addItem = function () {
                        vm.items.push({
                            objectType: 0,
                            objectName: "",
                            contentType: "0"
                        })
                        //$scope.$broadcast('dictionaryReady');
                    };

                    vm.removeItem = function () {
                        vm.items.shift();
                    }
                    vm.cancelEditCustomerMeeting = function () {
                        $state.go('ppts.customermeeting-view', { id: $stateParams.id, prev: 'ppts.customermeeting' });
                    }
                }]);
        });