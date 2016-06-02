define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.discountDataService,
        ppts.config.dataServiceConfig.customerService],
        function (customer) {
            customer.registerController('discountEditController', ['$scope', '$state', '$stateParams', 'discountDataService', 'discountDataViewService', 'mcsDialogService', 'discountMaximum', 'discountEditData', 'discountCampusData', 'mcsValidationService',
            function ($scope, $state, $stateParams, discountDataService, discountDataViewService, mcsDialogService, discountMaximum, discountData, discountCampusData, mcsValidationService) {
                var vm = this;
                mcsValidationService.init($scope);
                // 页面初始化加载
                (function () {
                    discountData.rows = [];
                    discountCampusData.rows = [];
                    var paramsData = [];
                    vm.criteria = {};
                    paramsData.discountId = $stateParams.discountId;
                    discountDataService.getDiscountForView($stateParams.discountId, function (result) {
                        vm.criteria = result.discount;
                        var discountPermissions = result.discountPermissionCollection;
                        vm.criteria.campusNames = [];
                        vm.criteria.usedStates = [];
                        vm.criteria.endTimes = [];
                        vm.criteria.campusIds = [];
                        if (discountPermissions && discountPermissions.length > 0) {
                            for (var i in discountPermissions) {
                                vm.criteria.campusIds.push(discountPermissions[i].campusID);
                                vm.criteria.campusNames.push(discountPermissions[i].campusName);
                                vm.criteria.usedStates.push(discountPermissions[i].campusUseStatus);
                                vm.criteria.endTimes.push(discountPermissions[i].endDateView);

                            }
                        }
                        else {
                            var discountApplies = result.discountPermissionsApplieCollection;
                            if (discountApplies && discountApplies.length > 0) {
                                for (var i in discountApplies) {
                                    vm.criteria.campusIds.push(discountApplies[i].campusID);
                                    vm.criteria.campusNames.push(discountApplies[i].campusName);
                                    vm.criteria.usedStates.push(discountApplies[i].campusUseStatus);
                                    vm.criteria.endTimes.push(discountApplies[i].endDateView);
                                }
                            }
                        }
                        discountDataViewService.configDiscountAddDataTable(vm, discountData, discountCampusData);
                        vm.selectBranch(vm.criteria);

                        var discountItem = result.discountItemCollection;
                        if (discountItem && discountItem.length > 0) {
                            var rowCount = discountItem.length;
                            if (discountItem.length < discountMaximum)
                                rowCount = rowCount + (discountMaximum - rowCount);
                            for (var i = 0; i < rowCount; i++) {
                                discountData.rows.push({
                                    stall: i + 1,
                                    discountStandard: (discountItem[i] ? discountItem[i].discountStandard : 0),
                                    discountValue: (discountItem[i] ? discountItem[i].discountValue : 0),
                                    validStandard: true,
                                    validValue: true
                                });
                            }
                        }
                    });
                })();

                vm.updateDiscountStandardRank = function (row, index) {
                    if (!row.discountStandard) return;
                    var result = true;
                    if (index == 0) {
                        var next = discountData.rows[index + 1];
                        if (!next.discountStandard) return;
                        result = row.discountStandard < next.discountStandard;
                    } else if (index == discountData.rows.length - 1) {
                        var last = discountData.rows[index - 1];
                        if (!last.discountStandard) {
                            last.validStandard = false;
                            return;
                        }
                        result = row.discountStandard > last.discountStandard;
                    } else {
                        var last = discountData.rows[index - 1];
                        var next = discountData.rows[index + 1];
                        if (!last.discountStandard) {
                            last.validStandard = false;
                            return;
                        } else {
                            result = row.discountStandard > last.discountStandard;
                            if (result) {
                                if (next.discountStandard) {
                                    result = row.discountStandard < next.discountStandard;
                                }
                            }
                        }
                    }
                    row.validStandard = result;
                    $scope.$apply('discountData');
                    return result;
                };

                vm.updateDiscountValueRank = function (row, index) {
                    if (!row.discountValue) return;
                    var result = true;
                    if (index == 0) {
                        var next = discountData.rows[index + 1];
                        if (!next.discountValue) return;
                        result = row.discountValue > next.discountValue;
                    } else if (index == discountData.rows.length - 1) {
                        var last = discountData.rows[index - 1];
                        if (!last.discountValue) {
                            last.validValue = false;
                            return;
                        }
                        result = row.discountValue < last.discountValue;
                    } else {
                        var last = discountData.rows[index - 1];
                        var next = discountData.rows[index + 1];
                        if (!last.discountValue) {
                            last.validValue = false;
                            return;
                        } else {
                            result = row.discountValue < last.discountValue;
                            if (result) {
                                if (next.discountValue) {
                                    result = row.discountValue > next.discountValue;
                                }
                            }
                        }
                    }
                    row.validValue = result;
                    $scope.$apply('discountData');
                    return result;
                };

                vm.addRow = function () {
                    vm.relationData.rows.push({
                        stall: vm.relationData.rows.length + 1,
                        discountStandard: '',
                        discountValue: '',
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
                    if (vm.relationData.rows.length > discountMaximum) {
                        vm.relationData.rows.pop();
                        vm.campusData.rows.pop();
                    }
                    else {
                        vm.errorMessage = mcs.util.format('行数不能少于{0}行！', discountMaximum);
                    }
                }

                vm.save = function () {
                    if (mcsValidationService.run($scope)) {
                        for (var value in discountData.rows) {
                            if (!discountData.rows[value].validStandard || !discountData.rows[value].validValue) {
                                vm.errorMessage = '充值折扣关系表填写的值验证不通过,请重新填写！';
                                return;
                            }
                            if (discountData.rows[value].discountStandard && !discountData.rows[value].discountValue) {
                                vm.errorMessage = '请将充值额和折扣率填写完整！';
                                return;
                            }
                            if (!discountData.rows[value].discountStandard && discountData.rows[value].discountValue) {
                                vm.errorMessage = '请将充值额和折扣率填写完整！';
                                return;
                            }
                        }
                        var createDiscountModel = { discount: { startDate: vm.criteria.startDate }, discountPermissionsApplieCollection: [], discountItemCollection: discountData.rows };
                        for (var i in vm.criteria.campusIds) {
                            createDiscountModel.discountPermissionsApplieCollection.push({ campusID: vm.criteria.campusIds[i] });
                        }
                        discountDataService.createDiscount(createDiscountModel, function () {
                            $state.go('ppts.discount');
                        });
                    }
                };

            }]);
        });