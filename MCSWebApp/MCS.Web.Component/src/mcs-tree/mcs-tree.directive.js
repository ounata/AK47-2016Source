(function() {
    'use strict';

    angular.module('mcs.ng.treeControl', [])
        .constant('treeSetting', {
            data: {
                key: {
                    checked: 'checked',
                    children: 'children',
                    name: 'name',
                    title: 'name'
                },
                simpleData: {
                    enable: true
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
                enable: true
            },
            async: {
                enable: true,
                autoParam: ["id"],
                contentType: "application/json",
                type: 'post'


            }


        })
        .directive('mcsTree', function(treeSetting, $http) {

            var zTreeObj;

            return {
                restrict: 'A',
                scope: {
                    setting: '=mcsTree',
                    setNodes: '&'
                },

                link: function($scope, iElm, iAttrs, controller) {


                    $scope.setting.getRawNodesChecked = function() {
                        return zTreeObj.getCheckedNodes(true);
                    };

                    $scope.setting.getNodesChecked = function(includingParent) {
                        var nodes = [];

                        zTreeObj.getCheckedNodes(true).forEach(function(node, index) {
                            if (includingParent == undefined || includingParent == false) {
                                if (!node.isParent) {
                                    nodes.push({
                                        id: node.id,
                                        name: node.name
                                    });
                                }

                            } else {
                                nodes.push({
                                    id: node.id,
                                    name: node.name
                                });
                            }

                        });

                        return nodes;
                    };


                    $scope.setting.getNamesOfNodesChecked = function(includingParent) {
                        var names = [];
                        zTreeObj.getCheckedNodes(true).forEach(function(node, index) {
                            if (includingParent == undefined || includingParent == false) {
                                if (!node.isParent) {
                                    names.push(node.name);
                                }

                            } else {
                                names.push(node.name);
                            }

                        });

                        return names;
                    };

                    $scope.setting.getIdsOfNodesChecked = function(includingParent) {
                        var ids = [];
                        zTreeObj.getCheckedNodes(true).forEach(function(node, index) {
                            if (includingParent == undefined || includingParent == false) {
                                if (!node.isParent) {
                                    ids.push(node.id);
                                }

                            } else {
                                ids.push(node.id);
                            }

                        });

                        return ids;
                    }


                    angular.extend(treeSetting, $scope.setting);

                    $scope.setNodes()(function(nodes) {
                        if (nodes) {
                            zTreeObj = $.fn.zTree.init(iElm, treeSetting, nodes);

                        }
                    });



                }
            };
        });


})();
