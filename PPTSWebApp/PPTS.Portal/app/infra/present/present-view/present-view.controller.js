define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.presentDataService,
        ppts.config.dataServiceConfig.customerService],
        function (customer) {
            customer.registerController('presentViewController', ['$scope', '$state', '$stateParams', 'presentDataService', 'presentDataViewService', 'mcsDialogService', 'presentMaximum', 'presentInfoData', 'presentCampusData',
            function ($scope, $state, $stateParams, presentDataService, presentDataViewService, mcsDialogService, presentMaximum, presentData, presentCampusData) {
                var vm = this;
                presentData.rows = [];
                presentCampusData.rows = [];
                // 页面初始化加载
                (function () {
                    var paramsData = {};
                    vm.criteria = [];
                    paramsData.presentId = $stateParams.presentId;
                    presentDataService.getPresentForView($stateParams.presentId, function (result) {
                        vm.criteria = result.present;
                        var presentPermissions = result.presentPermissionCollection;
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
                            var presentApplies = result.presentPermissionsApplieCollection;
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

                        var presentItem = result.presentItemCollection;
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
                                    standard = '0';
                                    value = '0';
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

                vm.edit = function () {
                    $state.go('ppts.present-edit', { presentId: $stateParams.presentId });
                };

                vm.approve = function () {
                    var createPresentModel = { present: { startDate: vm.criteria.startDate }, presentPermissionsApplieCollection: [], presentItemCollection: presentData.rows };
                    for (var i in vm.criteria.campusIds) {
                        createPresentModel.presentPermissionsApplieCollection.push({ campusID: vm.criteria.campusIds[i] });
                    }
                    presentDataService.createPresent(createPresentModel, function () {
                        $state.go('ppts.present');
                    });
                };

            }]);
        });