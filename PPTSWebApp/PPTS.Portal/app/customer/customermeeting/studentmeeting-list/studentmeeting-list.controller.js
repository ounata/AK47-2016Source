/*
    名    称: studentmeeting-list.controller
    功能概要: 学员教学服务会列表Controller js
    作    者: Lucifer
    创建时间: 2016年4月28日 18:28:36
    修正履历：
    修正时间:
*/

define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerMeetingDataService],
        function (customer) {
            customer.registerController('studentMeetingListController', ['$scope', '$stateParams', '$state', '$location', 'studentMeetingDataViewService',
                function ($scope, $stateParams, $state, $location, studentMeetingDataViewService, searchItems) {
                    var vm = this;

                    // 配置跟列表数据表头
                    studentMeetingDataViewService.configStudentMeetingListHeaders(vm);
                    // alert($stateParams.id);
                    var customerId = $stateParams.id;

                    // 页面初始化加载或重新搜索时查询
                    vm.search = function () {
                        studentMeetingDataViewService.initStudentMeetingList(vm, customerId, function () {
                            //vm.searchItems = searchItems;
                            $scope.$broadcast('dictionaryReady');
                        });
                    };
                    //新增会议
                    vm.add = function () {
                        $state.go('ppts.student-view.customermeeting-add',{prev: $location.$$search.prev });
                    }
                    vm.search();
                }]);
        });