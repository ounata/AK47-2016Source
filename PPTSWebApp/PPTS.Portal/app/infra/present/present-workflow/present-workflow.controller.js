define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.presentDataService,
        ppts.config.dataServiceConfig.customerService],
        function (customer) {
            customer.registerController('presentWorkflowController', ['$scope', '$state', '$stateParams', 'presentDataService', 'presentDataViewService', 'mcsDialogService', 'presentMaximum', 'presentInfoData', 'presentCampusData', 'mcsValidationService',
            function ($scope, $state, $stateParams, presentDataService, presentDataViewService, mcsDialogService, presentMaximum, presentData, presentCampusData, mcsValidationService) {
                //var vm = this;

                //(function () {
                //    vm.searchParams = {
                //        processID: $stateParams.processID,
                //        activityID: $stateParams.activityID,
                //        resourceID: $stateParams.resourceID
                //    };
                //    presentDataService.getPresentWorkflowInfo(vm.searchParams, function (result) {
                //        vm.presentDetial = result.presentDetial;
                //        vm.clientProcess = result.clientProcess;



                //        $scope.$broadcast('dictionaryReady');
                //    });

                //})();

                var vm = this;
                presentData.rows = [];
                presentCampusData.rows = [];
                // 页面初始化加载
                (function () {

                    vm.searchParams = {
                                processID: $stateParams.processID,
                                activityID: $stateParams.activityID,
                                resourceID: $stateParams.resourceID
                            };

                    var paramsData = {};
                    vm.criteria = [];
                    
                    presentDataService.getPresentWorkflowInfo(vm.searchParams, function (result) {

                        vm.presentDetial = result.presentDetial;
                        vm.clientProcess = result.clientProcess;

                        vm.criteria = vm.presentDetial.present;
                        var presentPermissions = vm.presentDetial.presentPermissionCollection;
                        vm.criteria.campusNames = [];
                        vm.criteria.usedStates = [];
                        vm.criteria.endTimes = [];
                        vm.criteria.campusIds = [];
                        if (presentPermissions && presentPermissions.length > 0) {
                            for (var i in presentPermissions) {
                                vm.criteria.campusIds.push(presentPermissions[i].campusID);
                                vm.criteria.campusNames.push(presentPermissions[i].campusName);
                                vm.criteria.usedStates.push(presentPermissions[i].campusUseStatus);
                                vm.criteria.endTimes.push(presentPermissions[i].endDateView);
                            }
                        }
                        else {
                            var presentApplies = vm.presentDetial.presentPermissionsApplieCollection;
                            if (presentApplies && presentApplies.length > 0) {
                                for (var i in presentApplies) {
                                    vm.criteria.campusIds.push(presentApplies[i].campusID);
                                    vm.criteria.campusNames.push(presentApplies[i].campusName);
                                    vm.criteria.usedStates.push(presentApplies[i].campusUseStatus);
                                    vm.criteria.endTimes.push(presentApplies[i].endDateView);
                                }
                            }
                        }
                        presentDataViewService.configPresentAddDataTable(vm, presentData, presentCampusData);
                        vm.selectBranch(vm.criteria);

                        var presentItem = vm.presentDetial.presentItemCollection;
                        if (presentItem && presentItem.length > 0) {
                            var rowCount = presentItem.length;
                            if (presentItem.length < presentMaximum)
                                rowCount = rowCount + (presentMaximum - rowCount);
                            var standard = '';
                            var value = '';
                            for (var i = 0; i < rowCount; i++) {
                                if (presentItem[i]) {
                                    if (i == 0) {
                                        standard = '[0-' + presentItem[i].presentStandard + ')';
                                        value = presentItem[i].presentValue;
                                    }
                                    else if (i == (presentItem.length - 1)) {
                                        standard = '[' + presentItem[i].presentStandard + '-∞)';
                                        value = presentItem[i].presentValue;
                                    }
                                    else {
                                        standard = '[' + presentItem[i - 1].presentStandard + '-' + presentItem[i].presentStandard + ')';
                                        value = presentItem[i].presentValue;
                                    }
                                }
                                else {
                                    standard = '-';
                                    value = '-';
                                }
                                presentData.rows.push({
                                    stall: i + 1,
                                    presentStandard: standard,
                                    presentValue: value,
                                    validStandard: true,
                                    validValue: true
                                });
                            }
                        }
                    });
                })();

                vm.approveCallback = function (clientProcess) {
                    //alert("审批通过");
                    location.href = "/";
                    location.reload();
                };

                vm.rejectCallback = function (clientProcess) {
                    //alert("审批不通过");
                    location.href = "/";
                };

            }]);
        });