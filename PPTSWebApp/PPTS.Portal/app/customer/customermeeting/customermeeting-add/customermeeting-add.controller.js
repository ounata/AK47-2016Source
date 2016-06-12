/*
    名    称: customermeeting-add.controller
    功能概要: 新增会议Controller js
    作    者: Lucifer
    创建时间: 2016年4月29日 14:45:06
    修正履历：
    修正时间:
*/
define([ppts.config.modules.customer, ppts.config.dataServiceConfig.customerMeetingDataService],
        function (customer) {
            customer.registerController('customerMeetingAddController',
                ['$scope', '$state', '$stateParams', "customerMeetingAddDataViewService", "mcsValidationService",
                function ($scope, $state, $stateParams, customerMeetingAddDataViewService, mcsValidationService) {
                    var vm = this;
                    var cmAddViewService = customerMeetingAddDataViewService;
                    cmAddViewService.initDate(vm);
                    mcsValidationService.init($scope);


                    ////初始化数据
                    (function () {
                        cmAddViewService.initData(vm, function () {
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
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    ////参会对象文本获取事件处理
                    //vm.selectParticipants = function (item, model) {
                    //    vm.customerMeeting.selectParticipantsText = item.value;
                    //}

                    //新增会议
                    vm.addCustomerMeeting = function () {
                        if (mcsValidationService.run($scope)) {
                            cmAddViewService.saveCustomerMeetings(vm, function () {
                                //alert("添加成功!");
                                //$scope.$broadcast('dictionaryReady');
                                $state.go("ppts.student-view.studentmeetinglist", { prev: 'ppts.student' });
                            });
                        }
                    }

                    vm.items = [{
                        objectType: 0,
                        objectName: "",
                        contentType: "0",
                        contentData: ""
                    }];

                    vm.addItem = function () {
                        vm.items.push({
                            objectType: 0,
                            objectName: "",
                            contentType: "0",
                            contentData: ""
                        })
                        //$scope.$broadcast('dictionaryReady');
                    };

                    vm.removeItem = function () {
                        // mcs.util.removeByObject(files, file);
                        vm.items.shift();
                    }
                    vm.cancelEditCustomerMeeting = function () {
                        $state.go('ppts.student-view.studentmeetinglist', { id: $stateParams.id, prev: 'ppts.student-view' });
                    }

                    $scope.$watch("vm.customerMeeting.meetingTime", function (newValue, oldValue, scope) {
                        if (newValue) {
                            if (vm.customerMeeting.meetingEndTime) {
                                var startDate = new Date(newValue);
                                var endDate = new Date(vm.customerMeeting.meetingEndTime);
                                if (startDate.getTime() > endDate.getTime()) {
                                    vm.customerMeeting.meetingEndTime = vm.customerMeeting.meetingTime;
                                }
                                if (startDate.getDate() != endDate.getDate()) {
                                    vm.customerMeeting.meetingEndTime = vm.customerMeeting.meetingTime;
                                    newValue = vm.customerMeeting.meetingEndTime;
                                }
                                
                            }
                            cmAddViewService.calDiff(vm, new Date(newValue), new Date(vm.customerMeeting.meetingEndTime));

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
                            cmAddViewService.calDiff(vm, new Date(vm.customerMeeting.meetingTime), new Date(newValue));
                        }
                    });
                }]);
        });