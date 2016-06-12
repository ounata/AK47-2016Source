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
                ['$scope', "$state", '$stateParams', "customerMeetingEditDataViewService", "mcsValidationService",
                function ($scope, $state, $stateParams, customerMeetingEditDataViewService, mcsValidationService) {
                    var vm = this;
                    customerMeetingEditDataViewService.initDate(vm);
                    mcsValidationService.init($scope);
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
                        if (mcsValidationService.run($scope)) {
                            customerMeetingEditDataViewService.saveCustomerMeetings(vm, function () {
                                //alert("保存成功!");
                                //$scope.$broadcast('dictionaryReady');
                                $state.go("ppts.customermeeting", { prev: 'ppts' });
                            });
                        }
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
                    $scope.$watch("vm.customerMeeting.meetingTime", function (newValue, oldValue, scope) {
                        if (newValue) {
                            if (vm.customerMeeting.meetingEndTime) {
                                var startDate = new Date(newValue);
                                var endDate = new Date(vm.customerMeeting.meetingEndTime);
                                if (startDate.getTime() > endDate.getTime()) {
                                    //newValue = vm.customerMeeting.meetingEndTime;
                                    vm.customerMeeting.meetingEndTime = vm.customerMeeting.meetingTime;
                                }
                                if (startDate.getDate() != endDate.getDate()) {
                                    vm.customerMeeting.meetingEndTime = vm.customerMeeting.meetingTime;
                                    newValue = vm.customerMeeting.meetingEndTime;
                                }
                                
                            }
                            customerMeetingEditDataViewService.calDiff(vm, new Date(newValue), new Date(vm.customerMeeting.meetingEndTime));

                        }
                    });
                    $scope.$watch("vm.customerMeeting.meetingEndTime", function (newValue, oldValue, scope) {
                        if (newValue) {
                            if (vm.customerMeeting.meetingTime) {
                                var startDate = new Date(vm.customerMeeting.meetingTime);
                                var endDate = new Date(newValue);
                                if (startDate.getTime() > endDate.getTime()) {
                                    vm.customerMeeting.meetingTime = vm.customerMeeting.meetingEndTime;
                                }
                                if (startDate.getDate() != endDate.getDate()) {
                                    vm.customerMeeting.meetingTime = vm.customerMeeting.meetingEndTime;
                                    newValue = vm.customerMeeting.meetingTime;
                                }
                                
                            }
                            customerMeetingEditDataViewService.calDiff(vm, new Date(vm.customerMeeting.meetingTime), new Date(newValue));
                        }
                    });
               
                }]);
        });