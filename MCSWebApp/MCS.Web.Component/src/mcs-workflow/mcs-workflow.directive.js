(function () {
    'use strict';

    mcs.ng.directive('mcsWorkflowStep', function () {
        return {
            restrict: 'E',
            scope: {
                model: '='
            },
            controller: function ($scope) {
                $scope.$watch('model', function (value) {
                    if (!value || !mcs.util.isArray(value.activities)) return;
                    var activities = value.activities;
                    var index = mcs.util.indexOf(activities, function (item) {
                        return item.isActive;
                    });

                    for (var i in activities) {
                        var item = activities[i];
                        item.isComplete = i < index ? true : false;
                    }
                });
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-workflow-step.tpl.html'
        }
    });

    mcs.ng.directive('mcsWorkflowHistory', ['$stateParams', 'mcsWorkflowService', function ($stateParams, mcsWorkflowService) {
        return {
            restrict: 'E',
            scope: {
                model: '='
            },
            controller: function ($scope) {
                $scope.data = {
                    keyFields: ['activityID'],
                    headers: [{
                        field: "activityName",
                        name: "审批岗位"
                    }, {
                        field: "approver",
                        name: "审批人"
                    }, {
                        field: "action",
                        name: "审批结果"
                    }, {
                        field: "comment",
                        name: "意见",
                        template: '<span uib-popover="{{row.comment|tooltip:20}}" popover-trigger="mouseenter">{{row.comment | truncate:20}}</span>'
                    }, {
                        field: "startTime",
                        name: "审批时间",
                        template: '<span>{{row.approvalTime | date: "yyyy-MM-dd HH:mm" | normalize}}</span>'
                    }],
                    pagable: false,
                    orderBy: [{
                        dataField: 'approvalTime',
                        sortDirection: 1
                    }],
                };

                $scope.params = {
                    processID: $stateParams.pid,
                    activityID: $stateParams.aid,
                    resourceID: $stateParams.rid
                };

                $scope.$watch('model', function (value) {
                    if (!value) return;
                    var activities = value.activityHistories && value.activityHistories.length ? value.activityHistories : value.activities;
                    if (!mcs.util.isArray(activities)) return;

                    $scope.data.rows = activities.filter(function (item) {
                        return item.activityStatus != 'Completed' || item.activityStatus != 'Aborted';
                    });
                });
            },
            template: '<mcs-datatable data="data" />'
        }
    }]);

    mcs.ng.directive('mcsWorkflowComment', function () {
        return {
            restrict: 'E',
            scope: {
                model: '='
            },
            template: '<textarea ng-model="model.currentOpinion.content" rows="7" class="mcs-width-full"></textarea>'
        };
    });

    mcs.ng.directive('mcsWorkflowToolbar', function () {
        return {
            restrict: 'E',
            scope: {
                model: '='
            },
            controller: function ($scope) {
                $scope.switch = $scope.$parent.switch;
                $scope.callbacks = $scope.$parent.callbacks;
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-workflow-toolbar.tpl.html'
        };
    });

    mcs.ng.directive('mcsWorkflow', ['$state', 'mcsWorkflowService', function ($state, mcsWorkflowService) {
        return {
            restrict: 'E',
            scope: {
                model: '=',
                callbacks: '=?'
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-workflow.tpl.html',
            controller: function ($scope) {
                $scope.callbacks = $scope.callbacks || {};
                $scope.$watch('model', function (value) {
                    if (value) {
                        $scope.switch = value.uiSwitches;

                        var params = {
                            processID: $scope.model.processID,
                            resourceID: $scope.model.resourceID
                        };
                        /*流转*/
                        $scope.moveTo = function () {
                            params.currentOpinion = $scope.model.currentOpinion;
                            if (angular.isFunction($scope.callbacks.moveTo)) {
                                mcsWorkflowService.moveto(params, function (result) {
                                    $scope.model = result;
                                    $scope.callbacks.moveTo()();
                                });
                            } else {
                                mcsWorkflowService.moveto(params, function (result) {
                                    $scope.model = result;
                                    //mcsWorkflowService.goBack();
                                });
                            }
                        };
                        /*驳回*/
                        $scope.cancel = function () {
                            params.currentOpinion = $scope.model.currentOpinion;
                            if (angular.isFunction($scope.callbacks.cancel)) {
                                mcsWorkflowService.cancel(params, function (result) {
                                    $scope.model = result;
                                    $scope.callbacks.cancel()();
                                });
                            } else {
                                mcsWorkflowService.cancel(params, function (result) {
                                    $scope.model = result;
                                    //mcsWorkflowService.goBack();
                                });
                            }
                        };
                        /*保存*/
                        $scope.save = function () {
                            params.currentOpinion = $scope.model.currentOpinion;
                            if (angular.isFunction($scope.callbacks.save)) {
                                mcsWorkflowService.save(params, function (result) {
                                    $scope.model = result;
                                    $scope.callbacks.save()();
                                });
                            } else {
                                mcsWorkflowService.save(params, function (result) {
                                    $scope.model = result;
                                    $state.reload();
                                });
                            }
                        };
                        /*撤回*/
                        $scope.withdraw = function () {
                            if (angular.isFunction($scope.callbacks.withdraw)) {
                                mcsWorkflowService.withdraw(params, function (result) {
                                    $scope.model = result;
                                    $scope.callbacks.withdraw()();
                                });
                            } else {
                                mcsWorkflowService.withdraw(params, function (result) {
                                    $scope.model = result;
                                    $state.reload();
                                });
                            }
                        };
                    }
                });
            }
        }
    }]);

})();