(function () {
    angular.module('app.component')

    .controller('MCSTreeController', ['$scope', function ($scope) {
        var vm = this;

        vm.nodeClick = function (event, treeId, treeNode) {
            alert('you are selecting ' + treeNode.name);
        }
        vm.onCheck = function (event, treeId, treeNode) {
            alert('you are selecting ' + treeNode.name);
        }

        vm.loadDataWaiting = function (treeId, treeNode) {

        }



        vm.treeSetting = {
            data: {
                key: {
                    children: 'children',
                    name: 'name',
                    title: 'name'
                }
            },
            view: {
                selectedMulti: true,
                showIcon: true,
                showLine: true,
                nameIsHTML: false,
                fontCss: {

                }

            },
            check: {
                enable: true,
                chkStyle: 'checkbox'

            },
            async: {
                enable: false,
                autoParam: ["id"],
                contentType: "application/json",
                type: 'post',
                dataType: "json",
                url: 'api/users'
            },
            callback: {
                onClick: vm.nodeClick,
                onCheck: vm.nodeCheck,
                beforeAsync: vm.loadDataWaiting
            }
        };



        vm.treeData = [{
            id: '0',
            name: 'root',
            open: true,

            children: [{

                id: '1',
                name: 'company-1',
                open: true,
                children: [{
                    id: 11,
                    name: 'HR',
                    checked: true,
                    chkDisabled: false,
                    icon: '',
                    iconOpen: '',
                    iconClose: '',
                    iconSkin: '',
                    isHidden: false


                }, {
                    id: 12,
                    name: 'IT'
                }]
            }, {
                id: '2',
                name: 'company-2',

                children: [{
                    id: '21',
                    name: 'HR'

                }]
            }]
        }];

    }]);
})();