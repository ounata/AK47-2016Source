(function () {
    'use strict';

    angular.module('app.widget').directive('pptsRadiobuttonGroup', function () {
        return {
            restrict: 'E',
            scope: {
                category: '@',
                showAll: '@',
                model: '=',
                css: '@',
                async: '@',
                value: '=?',
                parent: '=?'
            },
            template: '<mcs-radiobutton-group data="data" model="model" value="value" class="{{css}}"/>',
            link: function ($scope, $elem, $attrs, $ctrl) {
                function prepareDataDict() {
                    $scope.data = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];
                    if ($scope.showAll) {
                        $scope.data.unshift({ key: '-1', value: '全部' });
                    }
                    $scope.$watch('parent', function () {
                        for (var index in $scope.data) {
                            var item = $scope.data[index];
                            item.state = function () {
                                if (!mcs.util.bool($scope.parent) || $scope.parent == '-1') return true;
                                return !mcs.util.bool(item.parentKey) || mcs.util.containsElement(item.parentKey, $scope.parent);
                            }();
                            if (item.key == '-1') {
                                $scope.model = item.key;
                            }
                        }
                    });
                };
                $scope.showAll = mcs.util.bool($scope.showAll || false);
                $scope.async = mcs.util.bool($scope.async || true);

                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    $elem.parent().append('<p class="help-block"></p>');
                }

                if ($scope.async) {
                    $scope.$on('dictionaryReady', prepareDataDict);
                } else {
                    prepareDataDict();
                }
                $scope.model = $scope.model || '';
            }
        }
    });
})();