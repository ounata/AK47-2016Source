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
            callback: {

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
                    setNodes: '&',
                    distinctLevel: '@?'
                },

                link: function($scope, iElm, iAttrs, controller) {

                    var parentNodeChecked = null;
                    var distinctLevel = parseInt($scope.distinctLevel);


                    $scope.setting.getRawNodesChecked = function() {
                        return zTreeObj.getCheckedNodes(true);
                    };

                    $scope.setting.clearChecked = function() {

                        zTreeObj.checkAllNodes(false);

                    };

                    $scope.setting.expendCheckedNode = function(treeId, treeNode) {
                        zTreeObj.expandNode(treeNode, true, true, true);
                    };
                    treeSetting.callback.onAsyncSuccess = function(event, treeId, treeNode, msg) {
                        zTreeObj.checkNode(treeNode, true, true);
                    };

                    treeSetting.callback.beforeCheck = $scope.setting.expendCheckedNode;

                    $scope.setting.checkNodes = function(nodes) {

                        nodes.forEach(function(node) {
                            zTreeObj.checkNode(node, true, true);
                        });

                    };

                    $scope.setting.justCheckWithinSameParent = function(treeId, treeNode) {

                        if (treeNode.checked) {
                            if ($scope.setting.getRawNodesChecked().length == 0) {
                                parentNodeChecked = null;
                            }
                            return true;
                        }

                        if (treeNode.level > distinctLevel) {

                            if (!parentNodeChecked) {
                                parentNodeChecked = treeNode.getParentNode();

                                return true;

                            }

                            if (parentNodeChecked != treeNode.getParentNode()) {
                                $scope.setting.clearChecked();
                                parentNodeChecked = treeNode.getParentNode();
                            }



                            return true;

                        } else if (treeNode.level == distinctLevel) {
                            if (parentNodeChecked) {
                                $scope.setting.clearChecked();

                            }
                            parentNodeChecked = treeNode.getParentNode();
                            return true;


                        }

                        return false;

                    }

                    $scope.setting.getNodesChecked = function(includingParent) {
                        var nodes = [];

                        zTreeObj.getCheckedNodes(true).forEach(function(node, index) {
                            if (includingParent == undefined || includingParent == false) {
                                if (!node.isParent) {
                                    nodes.push({
                                        id: node.id,
                                        name: node.name,
                                        level: node.level
                                    });
                                }

                            } else {
                                nodes.push({
                                    id: node.id,
                                    name: node.name,
                                    level: node.level
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
