(function () {
    angular.module('app.page').controller('MCSPageController', [
        '$scope', '$http', function ($scope, $http) {
            var vm = this;

            //查询条件封装
            vm.criteria = {
                pageParams: {
                    pageIndex: 1,
                    pageSize: 10,
                    totalCount: -1
                },
                orderBy: [{ dataField: 'CreateTime', sortDirection: 1 }]
            };

            vm.data = {
                showCheckbox: true,
                headers: [{
                    name: '家长姓名',
                    field: 'parentName',
                    template: '<a href="#">{{row.parentName}}</a>'
                }, {
                    name: '性别',
                    field: 'gender'
                }, {
                    name: '年龄',
                    field: 'age'
                }, {
                    name: '手机',
                    field: 'mobile'
                }, {
                    name: '生日',
                    field: 'birthday'
                }],
                pager: vm.criteria.pageParams
            };

            vm.init = function () {
                // ajax query
                $http.get(mcs.app.config.componentBaseUrl + '/samples/app/page/demo.json').then(function (result) {
                    vm.data.rows = result.data.people;
                    vm.criteria.pageParams.totalCount = result.data.people.length;
                    vm.dictionaries = {
                        grades: [
                            { key: 'G1', value: '一年级' },
                            { key: 'G2', value: '二年级' },
                            { key: 'G3', value: '三年级' },
                            { key: 'G4', value: '四年级' },
                            { key: 'G5', value: '五年级' },
                            { key: 'G6', value: '六年级' }
                        ],
                        years: [
                            { key: 'Y1', value: '三年制' },
                            { key: 'Y2', value: '五年制' },
                            { key: 'Y3', value: '六年制' },
                            { key: 'Y4', value: '九年制' }
                        ],
                        genders: [{
                            key: 'G1',
                            value: '男'
                        }, {
                            key: 'G2',
                            value: '女'
                        }, {
                            key: 'G3',
                            value: '未知'
                        }]
                    };
                });
            };
            vm.init();
        }
    ]);
})();