define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.discountDataService,
        ppts.config.dataServiceConfig.customerService],
        function (customer) {
            customer.registerController('discountWorkflowController', ['$scope', '$state', '$stateParams', 'discountDataService', 'discountDataViewService', 'mcsDialogService', 'discountMaximum', 'discountInfoData', 'discountCampusData',
            function ($scope, $state, $stateParams, discountDataService, discountDataViewService, mcsDialogService, discountMaximum, discountData, discountCampusData) {
                var vm = this;
                discountData.rows = [];
                discountCampusData.rows = [];
                // 页面初始化加载
                (function () {
                    
                    vm.searchParams = {
                        processID: $stateParams.processID,
                        activityID: $stateParams.activityID,
                        resourceID: $stateParams.resourceID
                    };

                    var paramsData = {};
                    vm.criteria = {};

                    discountDataService.getDiscountWorkflowInfo(vm.searchParams, function (result) {

                        vm.discountDetial = result.discountDetial;
                        vm.clientProcess = result.clientProcess;

                        vm.criteria = result.discountDetial.discount;
                        var discountPermissions = result.discountDetial.discountPermissionCollection;
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
                            var discountApplies = result.discountDetial.discountPermissionsApplieCollection;
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

                        var discountItem = result.discountDetial.discountItemCollection;
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