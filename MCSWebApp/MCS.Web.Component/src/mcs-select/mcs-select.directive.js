(function () {
    mcs.ng.directive('mcsSelect', ['$http', function ($http) {
        return {
            restrict: 'E',
            scope: {
                caption: '@',
                data: '=?',
                model: '=',
                selected: '=?',
                async: '@',
                url: '@',
                root: '@',
                params: '=',
                parentKey: '@',
                customStyle: '@?',
                callback: '&'
            },
            templateUrl: mcs.app.config.componentBaseUrl + '/src/tpl/mcs-select.tpl.html',
            replace: true,
            link: function ($scope, $elem, $attrs, $ctrl) {
                $scope.caption = $scope.caption || '请选择';
                $scope.async = mcs.util.bool($scope.async || true);
                $scope.model = $scope.model || {};
                $scope.params = $scope.params || {};

                var loadData = function (elem, id) {
                    if (!id) return;
                    // 支持异步
                    if (mcs.util.bool($scope.async)) {
                        if (!$scope.url) return;
                        $http({
                            method: 'post',
                            url: $scope.url,
                            data: $scope.params,
                            cache: true
                        }).then(function (result) {
                            if (!result.data) return;
                            $scope.data = $scope.data || {};
                            for (var i in result.data) {
                                var item = result.data[i];
                                $scope.data[id] = $scope.data[id] || {};
                                $scope.data[id][item.key] = item.value;
                            }
                            loadDataCallback(elem, $scope.data[id]);
                        });
                    } else {
                        if (typeof ($scope.data[id]) == "undefined")
                            return false;
                        loadDataCallback(elem, $scope.data[id]);
                    }
                    elem.select2("val", "");
                };

                var loadDataCallback = function (elem, json, selected_id) {
                    if (json) {
                        var index = 1;
                        var selected_index = 0;
                        $.each(json, function (k, v) {
                            var option = '<option value="' + k + '">' + v + '</option>';
                            elem.append(option);

                            if (k == selected_id) {
                                selected_index = index;
                            }

                            index++;
                        })
                        //el.attr('selectedIndex' , selected_index); 
                    }
                }

                // 构建option
                var option = '<option value="">' + $scope.caption + '</option>';
                var select = $elem.find('select');

                // 返回已选择的数据
                $scope.model = select.val();
                if ($scope.customStyle) {
                    select.attr('style', $scope.customStyle);
                }

                select.append(option).select2().change(function () {
                    // 返回已选择的数据
                    $scope.model = select.val();
                    $scope.selected = $scope.selected || {};
                    // 注册回调事件
                    $scope.$watch('$scope.model', $scope.callback);
                    $scope.selected.key = select.val();
                    $scope.selected.value = select.select2('data').text;
                    // 触发$digest
                    $scope.$apply('$scope.model');
                });

                // 默认加载第一级
                loadData(select, $scope.root);
            }
        }
    }]);
})();