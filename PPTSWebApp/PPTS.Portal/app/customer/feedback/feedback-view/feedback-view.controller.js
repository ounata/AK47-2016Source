﻿/*
    名    称: feedback-view.controller
    功能概要: 学大反馈【家校互动】 Controller js
    作    者: Lucifer
    创建时间: 2016年5月10日 17:12:42
    修正履历：
    修正时间:
*/
define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.feedbackDataService],
        function (customer) {
            customer.registerController('feedbackViewController',
                [
                    '$scope',
                    '$state',
                    '$location',
                    '$stateParams',
                    'dataSyncService', 'feedbackDataService', 'feedbackViewDataViewService',
                function ($scope, $state, $location, $stateParams, dataSyncService, feedbackDataService, feedbackViewDataViewService) {
                    var vm = this;
                    vm.tabs = [];
                    if (!vm.criteria) vm.criteria = {};
                    switch ($stateParams.prev) {
                        case "ppts.feedback": //来源学大反馈
                           
                            vm.customerId = $stateParams.customerId || $stateParams.id;
                            //vm.criteria.replyID = $stateParams.replyID;
                            vm.criteria.replyTimeEnd =""!=$stateParams.replyTime? new Date(parseInt($stateParams.replyTime)):null;
                            vm.criteria.customerId = vm.customerId;
                            vm.fromRouteName = "ppts.student-view.feedbacks";
                            break;
                        default:
                            vm.customerId = $stateParams.id;
                            vm.criteria.customerId = $stateParams.id;
                            vm.fromRouteName = "ppts.feedback-view";
                            break;
                    }

                    feedbackViewDataViewService.initConfig(vm);
                    feedbackViewDataViewService.initData(vm, function () {
                        $scope.$broadcast('dictionaryReady');
                    });

                    vm.changeTab = function (tab) {
                        dataSyncService.initCriteria(vm);
                        if (vm.criteria) {
                            vm.criteria.replyObjects = [];
                            vm.criteria.replyObjects.push(tab.key);
                        }
                        feedbackDataService.GetCustomerRepliesList(vm.criteria, function (result) {
                            vm.data.rows = result.queryResult.pagedData;
                            vm.data.pager.totalCount = result.queryResult.totalCount;
                            $scope.$broadcast('dictionaryReady');
                        });
                    }
                    vm.add = function () {
                        $state.go('ppts.feedback-add', { id: vm.customerId, prev: $stateParams.prev});
                    }
                    vm.search = function () {
                        vm.tabs = [];
                        feedbackViewDataViewService.initData(vm, function () {
                            $scope.$broadcast('dictionaryReady');
                        });
                    }
                }]);
        });