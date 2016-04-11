(function () {
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
                type: 'post',
                dataType: "json",
                url: ''
            }

        })
        .directive('mcsTree', function (treeSetting, $http) {



            return {
                restrict: 'A',
                scope: {
                    setting: '=mcsTree',
                    nodes: '='
                },

                link: function ($scope, iElm, iAttrs, controller) {
                    angular.extend($scope.setting, treeSetting);
                    if ($scope.nodes) {
                        $.fn.zTree.init(iElm, $scope.setting, $scope.nodes);
                    } else {
                        if (setting.async.url) {
                            $http.post(setting.async.url, {
                                id: null
                            }).success(function (data) {
                                $.fn.zTree.init(iElm, setting, data.Data.List);
                            })

                        }

                    }

                }
            };
        });


})();