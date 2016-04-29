(function() {
    angular.module('app.component')

    .controller('MCSTreeController', ['$scope', function($scope) {
        var vm = this;


        vm.nodeClick = function(event, treeId, treeNode) {
            alert('you are selecting ' + treeNode.name);
        }
        vm.onCheck = function(event, treeId, treeNode) {
            alert('you are selecting ' + treeNode.name);
        }

        vm.loadDataWaiting = function(treeId, treeNode) {

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
                enable: true,
                autoParam: ["id"],
                otherParam: {
                    "age": "1",
                    "name": "test"
                },
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

        vm.setTreeData = function(callback) {
            callback(vm.treeData);
        }

        vm.treeData = [{
            id: '0',
            name: 'root',
            open: true,

            children: [{

                id: '1',
                name: 'company-1',
                open: true,
                iconSkin: 'depart',
                children: [{
                    id: 11,
                    name: 'HR',
                    checked: true,
                    chkDisabled: false,

                    iconSkin: 'user',
                    isHidden: false


                }, {
                    id: 12,
                    name: 'IT'
                }]
            }, {
                id: '2',
                name: 'company-2',
                isParent: true,
                open: false,

                children: []
            }]
        }];

    }]);
})();
