/*
    名    称: customermeeting-add.controller
    功能概要: 新增会议Controller js
    作    者: Lucifer
    创建时间: 2016年4月29日 14:45:06
    修正履历：
    修正时间:
*/
define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerMeetingDataService],
        function (customer) {
            customer.registerController('customerMeetingAddController',
                ['$scope', '$state', '$stateParams', 'dataSyncService', 'customerMeetingAddDataViewService', 'mcsValidationService',
                function ($scope, $state, $stateParams, dataSyncService, customerMeetingAddDataViewService, mcsValidationService) {
                    var vm = this;
                    vm.items = [];
                    var cmAddViewService = customerMeetingAddDataViewService;
                    vm.url = ppts.config.customerApiBaseUrl;
                    if (!vm.customerMeeting) vm.customerMeeting = {};

                    vm.now = new Date(Date.now());
                    //上传附件回写model
                    vm.customerMeetingUpload = {
                        files: []
                    };
                    cmAddViewService.initDate(vm);
                    mcsValidationService.init($scope);


                    ////初始化数据
                    (function () {
                        cmAddViewService.initData(vm, $stateParams.id, function (result) {
                            vm.customerMeeting.CustomerID = $stateParams.id;
                            vm.customerMeeting.organizerName = ppts.user.name;

                            //监控文字变化
                            $scope.$watch("vm.items", function (newValue, oldValue, scope) {
                                if (newValue) {
                                    for (var index = 0 in vm.items) {
                                        var len = !vm.items[index].contentData ? 0 : vm.items[index].contentData.length;

                                        vm.items[index].message = (3000 - len);
                                    }
                                }
                            }, true);

                            vm.addItem();

                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    //新增会议
                    vm.addCustomerMeeting = function () {
                        if (mcsValidationService.run($scope)) {
                            cmAddViewService.saveCustomerMeetings(vm, function () {
                                $state.go("ppts.student-view.studentmeetinglist", { prev: 'ppts.student' });
                            });
                        }
                    }
                    vm.downloadFile = function (file) {
                        mcs.util.postMockForm(vm.url + "api/customermeetings/downloadmaterial", file);
                    };


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
                        $state.go('ppts.student-view.studentmeetinglist', { id: $stateParams.id, prev: 'ppts.student-view' });
                    }
                    vm.calDiff = function () {
                        if (vm.minutes) {
                            var hours = parseInt(vm.minutes / 60);
                            var minutes = vm.minutes % 60;
                            vm.customerMeeting.hours = hours;
                            vm.customerMeeting.minutes = minutes;
                            //return hours + '小时' + minutes + '分钟';

                        }
                        //return '';

                    }
                    //$scope.$watch("vm.customerMeeting.meetingTime", function (newValue, oldValue, scope) {
                    //    if (newValue) {
                    //        if (vm.customerMeeting.meetingEndTime) {
                    //            var startDate = new Date(newValue);
                    //            var endDate = new Date(vm.customerMeeting.meetingEndTime);
                    //            if (startDate.getTime() > endDate.getTime()) {
                    //                vm.customerMeeting.meetingEndTime = vm.customerMeeting.meetingTime;
                    //            }
                    //            if (startDate.getDate() != endDate.getDate()) {
                    //                vm.customerMeeting.meetingEndTime = vm.customerMeeting.meetingTime;
                    //                newValue = vm.customerMeeting.meetingEndTime;
                    //            }

                    //        }
                    //        cmAddViewService.calDiff(vm, new Date(newValue), new Date(vm.customerMeeting.meetingEndTime));

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
                    //        cmAddViewService.calDiff(vm, new Date(vm.customerMeeting.meetingTime), new Date(newValue));
                    //    }
                    //});
                }]);
        });