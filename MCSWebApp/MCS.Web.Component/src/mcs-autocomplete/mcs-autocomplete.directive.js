(function() {
    'use strict';

    mcs.ng.directive('pptsAutoComplete', function(autoCompleteDataService, $templateCache) {


        var template = '<tags-input min-length="1" template="tagItemTemp" add-from-autocomplete-only="{{addJustSuggest != undefined?addJustSuggest:true}}"  ng-blur="tagBlur()" ';
        template += 'text="text"  max-tags="{{maxTags||1}}" ';
        template += 'ng-model="ngModel" key-property="{{key}}" display-property="{{displayProperty}}" placeholder="{{placeholder}}">';
        template += '<auto-complete load-on-focus="true" min-length="1" template="autoCompleteItemTemp" display-property="{{fullPath||displayProperty}}" source="query($query)"></auto-complete></tags-input>';


        return {
            restrict: 'E',
            template: template,
            scope: {
                placeholder: '@?',
                ngModel: '=',
                resultFilter: '&?',
                key: '@',
                async: '=?',
                addJustSuggest: '=?',
                minLength: '=?',
                displayProperty: '@',
                fullPath: '@?',
                maxTags: '=?',
                data: '=',
                remoteData: '@?',
                maxQueryResult: '@?',
                param: '=?'

            },

            link: function($scope, iElm, iAttrs, modelCtrl) {
                $scope.text = '';

                var autoTemp = '<div class="autoCompleteMatchItem" title="{{data.' + ($scope.fullPath || $scope.displayProperty) + '}}"><span ng-bind-html="$highlight(data.' + $scope.displayProperty + ')"></span><span ng-if="data.' + $scope.fullPath + '">({{data.' + $scope.fullPath + '}})</span></div>';

                var tagtemp = '<div uib-popover="{{data.' + ($scope.fullPath || $scope.displayProperty) + '}}" class="tag-template" popover-trigger="mouseenter"><span>{{$getDisplayText()}}</span> <a class="remove-button" ng-click="$removeTag()">&#10006;</a></div>';

                if (!$templateCache.put('autoCompleteItemTemp')) {
                    $templateCache.put('autoCompleteItemTemp', autoTemp);
                }

                if (!$templateCache.put('tagItemTemp')) {
                    $templateCache.put('tagItemTemp', tagtemp);
                }

                $scope.ngModel = $scope.ngModel || [];

                $scope.tagBlur = function() {
                    $scope.text = '';
                };

                $scope.tagChanged = function($tag) {
                    if ($scope.ngModel.length > $scope.maxTags) {
                        $scope.ngModel.pop();
                    }
                };

                $scope.query = function(query) {
                    if ($scope.async && $scope.async == true) {
                        return autoCompleteDataService.query($scope.remoteData, JSON.stringify(angular.extend({
                            searchTerm: query
                        }, $scope.param)), $scope.resultFilter);
                    } else {

                        var result = [];

                        var localData = $scope.resultFilter ? $scope.resultFilter()($scope.data) : $scope.data;

                        localData.forEach(function(item) {
                            if (item.name.indexOf(query) > -1) {
                                result.push(item);
                            }

                        });

                        return result;

                    }

                };


            }
        };
    });



    mcs.ng.directive('mcsAutocomplete', ['$templateCache', 'autoCompleteDataService', function($templateCache, autoCompleteDataService) {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                customStyle: '@',
                category: '@',
                placeholder: '@?',
                model: '=',
                filter: '&?',
                async: '@?',
                minValidLength: '=?', // 最小合法性数据长度
                minInputLength: '=?', // 最小输入数据开始搜索长度
                keyProperty: '@',
                displayProperty: '@',
                fullPath: '@?',
                maxTags: '=?', // 最多可以选择的数据项
                data: '=',
                url: '@?',
                maxQueryResult: '@?', // 最多可以显示的查询数据项
                params: '=?'
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-autocomplete.tpl.html',
            controller: function($scope) {
                $scope.async = mcs.util.bool($scope.async || true);
                $scope.category = $scope.category || '';
                $scope.placeholder = $scope.placeholder || '请选择';
                $scope.maxTags = $scope.maxTags || 1;
                $scope.minValidLength = $scope.minValidLength || 1;
                $scope.minInputLength = $scope.minInputLength || 1;
                $scope.maxQueryResult = $scope.maxQueryResult || 10;
            },
            link: function($scope, $elem) {
                $scope.text = '';
                $scope.model = $scope.model || [];
                var tagTemplate = '<div class="tag-template" uib-popover="{{data.' + $scope.fullPath + '}}" popover-trigger="mouseenter"><span>{{$getDisplayText()}}</span> <a class="remove-button" ng-click="$removeTag()">&#10006;</a></div>';
                var autocompleteTemplate = '<div class="autocomplete-template" title="{{data.' + $scope.fullPath + '}}"><span ng-bind-html="$highlight(data.' + $scope.displayProperty + ')"></span><span ng-if="data.' + $scope.fullPath + '">({{data.' + $scope.fullPath + '}})</span></div>';
                if (!$templateCache.get($scope.category + 'tag-template')) {
                    $templateCache.put($scope.category + 'tag-template', tagTemplate);
                }
                if (!$templateCache.get($scope.category + 'autocomplete-template')) {
                    $templateCache.put($scope.category + 'autocomplete-template', autocompleteTemplate);
                }

                $scope.tagBlur = function() {
                    $scope.text = '';
                };

                $scope.tagChanged = function($tag) {
                    if ($scope.model.length > $scope.maxTags) {
                        $scope.model.pop();
                    }
                };

                // fix the placeholder
                $scope.$watch('$parent.model', function(value) {
                    var defaultPlaceHolder = $scope.placeholder;
                    if (value && value.length) {
                        $elem.find('.tags input').attr('placeholder', '');
                    } else {
                        $elem.find('.tags input').attr('placeholder', defaultPlaceHolder).css('width', '100%');
                    }
                });

                $scope.query = function(term) {
                    if (mcs.util.bool($scope.async)) {
                        if (!$scope.url) return;
                        return autoCompleteDataService.query($scope.url, JSON.stringify(angular.extend({
                            searchTerm: term,
                            maxCount: $scope.maxQueryResult
                        }, $scope.params)), $scope.filter);
                    } else {
                        var result = [];
                        var localData = $scope.filter ? $scope.filter()($scope.data) : $scope.data;
                        localData.forEach(function(item) {
                            if (item.name.indexOf(term) > -1) {
                                result.push(item);
                            }
                        });

                        return result;
                    }
                };
            }
        }
    }]);
})();
