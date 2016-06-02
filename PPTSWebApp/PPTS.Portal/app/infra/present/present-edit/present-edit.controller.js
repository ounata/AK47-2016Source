define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.presentDataService,
        ppts.config.dataServiceConfig.customerService],
        function (customer) {
            customer.registerController('presentEditController', ['$scope', '$state', '$stateParams', 'presentDataService', 'presentDataViewService', 'mcsDialogService', 'presentMaximum', 'presentEditData', 'presentCampusData', 'mcsValidationService',
            function ($scope, $state, $stateParams, presentDataService, presentDataViewService, mcsDialogService, presentMaximum, presentData, presentCampusData, mcsValidationService) {
                var vm = this;
                mcsValidationService.init($scope);
                // 页面初始化加载
                (function () {
                    presentData.rows = [];
                    presentCampusData.rows = [];
                    var paramsData = {};
                    vm.criteria = {};
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
                            for (var i = 0; i < rowCount; i++) {
                                presentData.rows.push({
                                    stall: i + 1,
                                    presentStandard: (presentItem[i] ? presentItem[i].presentStandard : 0),
                                    presentValue: (presentItem[i] ? presentItem[i].presentValue : 0),
                                    validStandard: true,
                                    validValue: true
                                });
                            }
                        }
                    });
                })();

                vm.updatePresentStandardRank = function (row, index) {
                    if (!row.presentStandard) return;
                    var result = true;
                    if (index == 0) {
                        var next = presentData.rows[index + 1];
                        if (!next.presentStandard) return;
                        result = row.presentStandard < next.presentStandard;
                    } else if (index == presentData.rows.length - 1) {
                        var last = presentData.rows[index - 1];
                        if (!last.presentStandard) {
                            last.validStandard = false;
                            return;
                        }
                        result = row.presentStandard > last.presentStandard;
                    } else {
                        var last = presentData.rows[index - 1];
                        var next = presentData.rows[index + 1];
                        if (!last.presentStandard) {
                            last.validStandard = false;
                            return;
                        } else {
                            result = row.presentStandard > last.presentStandard;
                            if (result) {
                                if (next.presentStandard) {
                                    result = row.presentStandard < next.presentStandard;
                                }
                            }
                        }
                    }
                    row.validStandard = result;
                    $scope.$apply('presentData');
                    return result;
                };

                vm.updatePresentValueRank = function (row, index) {
                    if (!row.presentValue) return;
                    var result = true;
                    if (index == 0) {
                        var next = presentData.rows[index + 1];
                        if (!next.presentValue) return;
                        result = row.presentValue < next.presentValue;
                    } else if (index == presentData.rows.length - 1) {
                        var last = presentData.rows[index - 1];
                        if (!last.presentValue) {
                            last.validValue = false;
                            return;
                        }
                        result = row.presentValue > last.presentValue;
                    } else {
                        var last = presentData.rows[index - 1];
                        var next = presentData.rows[index + 1];
                        if (!last.presentValue) {
                            last.validValue = false;
                            return;
                        } else {
                            result = row.presentValue > last.presentValue;
                            if (result) {
                                if (next.presentValue) {
                                    result = row.presentValue < next.presentValue;
                                }
                            }
                        }
                    }
                    row.validValue = result;
                    $scope.$apply('presentData');
                    return result;
                };

                vm.addRow = function () {
                    vm.relationData.rows.push({
                        stall: vm.relationData.rows.length + 1,
                        presentStandard: '',
                        presentValue: '',
                        validStandard: true,
                        validValue: true
                    });
                    vm.campusData.rows.push({
                        campusName: '',
                        usedState: '-',
                        endTime: '-'
                    });
                }

                vm.removeRow = function () {
                    if (vm.relationData.rows.length > presentMaximum) {
                        vm.relationData.rows.pop();
                        vm.campusData.rows.pop();
                    }
                    else {
                        vm.errorMessage = mcs.util.format('行数不能少于{0}行！', presentMaximum);
                    }
                }

                vm.save = function () {
                    if (mcsValidationService.run($scope)) {
                        for (var value in presentData.rows) {
                            if (!presentData.rows[value].validStandard || !presentData.rows[value].validValue) {
                                vm.errorMessage = '购买于赠送关系表填写的值验证不通过,请重新填写！';
                                return;
                            }
                            if (presentData.rows[value].presentStandard && !presentData.rows[value].presentValue) {
                                vm.errorMessage = '请将购买数量和赠送数量填写完整！';
                                return;
                            }
                            if (!presentData.rows[value].presentStandard && presentData.rows[value].presentValue) {
                                vm.errorMessage = '请将购买数量和赠送数量填写完整！';
                                return;
                            }
                        }
                        var createPresentModel = { present: { startDate: vm.criteria.startDate }, presentPermissionsApplieCollection: [], presentItemCollection: presentData.rows };
                        for (var i in vm.criteria.campusIds) {
                            createPresentModel.presentPermissionsApplieCollection.push({ campusID: vm.criteria.campusIds[i] });
                        }
                        presentDataService.createPresent(createPresentModel, function () {
                            $state.go('ppts.present');
                        });
                    }
                };
            }]);
        });