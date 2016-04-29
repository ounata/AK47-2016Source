define([ppts.config.modules.customer,
        ppts.config.dataServiceConfig.customerService,
        ppts.config.dataServiceConfig.customerDataService],
    function (customer) {
        customer.registerController('parentsViewController', [
            '$scope',
            '$state',
            '$stateParams',
            'customerService',
            'dataSyncService',
            'utilService',
            'customerDataService',
            function ($scope, $state, $stateParams, customerService, dataSyncService, util, customerDataService) {
                var vm = this;

                vm.data = {
                    selection: 'radio',
                    rowsSelected: [],
                    keyFields: ['parentID'],
                    headers: [{
                        field: "parentName",
                        name: "家长姓名",
                        template: '<mcs-button text="{{ row.parentName }}" click="vm.showParentInfo(row)" css="btn-link" />'
                    }, {
                        field: "parentRole",
                        name: "亲属关系",
                        template: '<span ng-if="row.gender==1">{{ row.parentRole | parentMale }}</span><span ng-if="row.gender==2">{{ row.parentRole | parentFemale }}</span>'
                    }, {
                        field: "isPrimary",
                        name: "是否主监护人",
                        template: '<span>{{ row.isPrimary | ifElse }}</span>'
                    }],
                    pager: {
                        pagable: false,
                        totalCount: -1
                    },
                    orderBy: [{ dataField: 'isPrimary', sortDirection: 0 }]
                }

                vm.init = function () {
                    customerService.handle('get-parents', function (result) {
                        vm.data.rows = getCustomerRelationData(result);
                        dataSyncService.injectPageDict(['ifElse']);
                        vm.data.rows.filter(function (obj) {
                            if (obj.isPrimary == true)
                                vm.parent = obj;
                        });
                        $scope.$broadcast('dictionaryReady');
                    });
                };

                vm.showParentInfo = function (rowData) {
                    vm.parent = rowData;
                };

                vm.edit = function () {
                    if (util.selectOneRow(vm)) {
                        customerService.handle('get-parent', { parentID: vm.data.rowsSelected[0].parentID });
                    }
                };

                var getCustomerRelationData = function (result) {
                    if (!result.parents || !result.relations) return;
                    var data = [], parentID = '';
                    for (var i in result.parents) {
                        parentID = result.parents[i].parentID;
                        for (var j in result.relations) {
                            if (result.relations[j].parentID == parentID) {
                                data.push({
                                    'parentID': parentID,
                                    'parentRole': result.relations[j].parentRole,
                                    'parentName': result.parents[i].parentName,
                                    'isPrimary': result.relations[j].isPrimary,
                                    'gender': result.parents[i].gender,
                                    'email': result.parents[i].email,
                                    'idType': result.parents[i].idType,
                                    'idNumber': result.parents[i].idNumber,
                                    'income': result.parents[i].income,
                                    'primaryPhone': result.parents[i].primaryPhone,
                                    'secondaryPhone': result.parents[i].secondaryPhone
                                });
                                break;
                            }
                        }
                    }
                    return data;
                };
                vm.init();
            }]);
    });