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
                ['$scope', "$state", '$stateParams', 'dataSyncService', "customerMeetingEditDataViewService", "mcsValidationService",
                function ($scope, $state, $stateParams, dataSyncService, customerMeetingEditDataViewService, mcsValidationService) {
                    var vm = this;
                    vm.url = ppts.config.customerApiBaseUrl;
                    //上传附件回写model
                    vm.customerMeetingUpload = {
                        files: []
                    };
                    vm.now = new Date(Date.now());
                    //customerMeetingEditDataViewService.initDate(vm);
                    mcsValidationService.init($scope);
                    ////初始化数据
                    (function () {
                        customerMeetingEditDataViewService.initData($stateParams.id, vm,function () {
                            //vm.customerMeeting.CustomerID = $stateParams.id;
                            //vm.organizerName = "张三";
                            //监控文字变化
                            $scope.$watch("vm.items", function (newValue, oldValue, scope) {
                                if (newValue) {
                                    for (var index = 0 in vm.items) {
                                        var len = !vm.items[index].contentData ? 0 : vm.items[index].contentData.length;

                                        vm.items[index].message = (3000 - len);
                                    }
                                }
                            }, true);
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
                        var itemId = mcs.util.newGuid();
                        vm.items.push({
                            id: itemId,
                            objectType: 0,
                            objectName: "",
                            contentType: "0",
                            contentData: ""
                        });
                        dataSyncService.injectDynamicDict(ppts.dict[ppts.config.dictMappingConfig['participants']], { category: 'participants_' + itemId });
                        dataSyncService.injectDynamicDict(ppts.dict[ppts.config.dictMappingConfig['contentType']], { category: 'contentType_' + itemId });
                    };


                    vm.removeItem = function (id) {
                        var currentIndex = -1;
                        vm.items.forEach(function (item, index) {
                            if (item.id == id) {
                                currentIndex = index;
                                return true;
                            }
                        });
                        if (currentIndex != -1) {
                            vm.items.splice(currentIndex, 1);
                            $scope.$broadcast('dictionaryReady');
                        }
                    }
                    vm.cancelEditCustomerMeeting = function () {
                        $state.go('ppts.customermeeting-view', { id: $stateParams.id, prev: 'ppts.customermeeting' });
                    }
                    vm.calDiff = function () {
                        if (vm.minutes)
                        {
                            var hours = parseInt(vm.minutes / 60);
                            var minutes = vm.minutes %60;
                            vm.customerMeeting.hours = hours;
                            vm.customerMeeting.minutes = minutes;
                            return hours + '小时' + minutes + '分钟';

                        }
                        return '';
                       
                    }
                    //$scope.$watch("vm.customerMeeting.meetingTime", function (newValue, oldValue, scope) {
                    //    if (newValue) {
                    //        if (vm.customerMeeting.meetingEndTime) {
                    //            var startDate = new Date(newValue);
                    //            var endDate = new Date(vm.customerMeeting.meetingEndTime);
                    //            if (startDate.getTime() > endDate.getTime()) {
                    //                //newValue = vm.customerMeeting.meetingEndTime;
                    //                vm.customerMeeting.meetingEndTime = vm.customerMeeting.meetingTime;
                    //            }
                    //            if (startDate.getDate() != endDate.getDate()) {
                    //                vm.customerMeeting.meetingEndTime = vm.customerMeeting.meetingTime;
                    //                newValue = vm.customerMeeting.meetingEndTime;
                    //            }
                                
                    //        }
                    //       // customerMeetingEditDataViewService.calDiff(vm, new Date(newValue), new Date(vm.customerMeeting.meetingEndTime));

                    //    }
                    //});
                    //$scope.$watch("vm.customerMeeting.meetingEndTime", function (newValue, oldValue, scope) {
                    //    if (newValue) {
                    //        if (vm.customerMeeting.meetingTime) {
                    //            var startDate = new Date(vm.customerMeeting.meetingTime);
                    //            var endDate = new Date(newValue);
                    //            if (startDate.getTime() > endDate.getTime()) {
                    //                vm.customerMeeting.meetingTime = vm.customerMeeting.meetingEndTime;
                    //            }
                    //            if (startDate.getDate() != endDate.getDate()) {
                    //                vm.customerMeeting.meetingTime = vm.customerMeeting.meetingEndTime;
                    //                newValue = vm.customerMeeting.meetingTime;
                    //            }
                                
                    //        }
                    //       // customerMeetingEditDataViewService.calDiff(vm, new Date(vm.customerMeeting.meetingTime), new Date(newValue));
                    //    }
                    //});
               
                }]);
        });