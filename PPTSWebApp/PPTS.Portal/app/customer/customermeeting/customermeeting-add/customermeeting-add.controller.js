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
                ['$scope', '$stateParams', "customerMeetingAddDataViewService",
                function ($scope, $stateParams, customerMeetingAddDataViewService) {
                    var vm = this;
                    var cmAddViewService = customerMeetingAddDataViewService;

                    ////初始化数据
                    (function () {
                        cmAddViewService.initData(vm, function () {
                            vm.customerMeeting.CustomerID = $stateParams.id;
                            vm.organizerName = "张三";
                            vm.addItem();
                            $scope.$broadcast('dictionaryReady');
                        });
                    })();

                    ////参会对象文本获取事件处理
                    //vm.selectParticipants = function (item, model) {
                    //    vm.customerMeeting.selectParticipantsText = item.value;
                    //}

                    //新增会议
                    vm.addCustomerMeeting = function () {
                        cmAddViewService.saveCustomerMeetings(vm, function () {
                            alert("添加成功!");
                            $scope.$broadcast('dictionaryReady');
                        });
                    }

                    vm.items = [{
                        objectType: 0,
                        objectName: "",
                        contentType:"0"
                    }];

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
                }]);
        });