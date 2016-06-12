define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.discountDataService,
        ppts.config.dataServiceConfig.customerService],
        function (customer) {
            customer.registerController('discountViewController', ['$scope', '$state', '$stateParams', 'discountDataService', 'discountDataViewService', 'mcsDialogService', 'discountMaximum', 'discountInfoData', 'discountCampusData',
            function ($scope, $state, $stateParams, discountDataService, discountDataViewService, mcsDialogService, discountMaximum, discountData, discountCampusData) {
                var vm = this;

                // 页面初始化加载
                (function () {
                    discountData.rows = [];
                    discountCampusData.rows = [];
                    var paramsData = {};
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
                            var standard = '';
                            var value = '';
                            for (var i = 0; i < rowCount; i++) {
                                if (discountItem[i]) {
                                    if (i == 0) {
                                        standard = '[0-' + discountItem[i].discountStandard + ')';
                                        value = discountItem[i].discountValue;
                                    }
                                    else if (i == (discountItem.length - 1)) {
                                        standard = '[' + discountItem[i].discountStandard + '-∞)';
                                        value = discountItem[i].discountValue;
                                    }
                                    else {
                                        standard = '[' + discountItem[i - 1].discountStandard + '-' + discountItem[i].discountStandard + ')';
                                        value = discountItem[i].discountValue;
                                    }
                                }
                                else {
                                    standard = '-';
                                    value = '-';
                                }
                                discountData.rows.push({
                                    stall: i + 1,
                                    discountStandard: standard,
                                    discountValue: value,
                                    validStandard: true,
                                    validValue: true
                                });
                            }
                        }
                    });
                })();

                vm.edit = function () {
                    $state.go('ppts.discount-edit', { discountId: $stateParams.discountId });
                };

                vm.approve = function () {
                    var createDiscountModel = { Discount: { startDate: vm.criteria.startDate }, discountPermissionsApplieCollection: [], discountItemCollection: discountData.rows };
                    for (var i in vm.criteria.campusIds) {
                        createDiscountModel.discountPermissionsApplieCollection.push({ campusID: vm.criteria.campusIds[i] });
                    }
                    discountDataService.createDiscount(createDiscountModel, function () {
                        $state.go('ppts.discount');
                    });
                };
            }]);
        });