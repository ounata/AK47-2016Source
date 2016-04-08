(function () {
    angular.module('app.page').controller('MCSAdvanceQueryController', [
        '$scope', '$http', function ($scope, $http) {
            var vm = this;

            //查询条件封装
            vm.criteria = {
                pageParams: {
                    pageIndex: 1,
                    pageSize: 5,
                    totalCount: 0
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
                $http.get(mcs.app.config.componentBaseUrl + '/samples/app/page/query/demo.json').then(function (result) {
                    vm.data.rows = result.data.simple;
                    vm.criteria.pageParams.totalCount = result.data.simple.length;
                });
            };
            vm.init();
        }
    ]);
})();