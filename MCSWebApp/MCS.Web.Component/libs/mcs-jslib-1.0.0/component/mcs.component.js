// 依赖于mcs.js, angularjs, 将会在应用程序启动之后执行
(function() {
    'use strict';

    mcs.ng = mcs.ng || angular.module('mcs.ng', ['mcs.ng.datatable', 'mcs.ng.filesUpload', 'mcs.ng.uiCopy', 'mcs.ng.treeControl', 'mcs.ng.paging', 'dialogs.main']);
    mcs.ng.constant('httpErrorHandleMessage', {
        '404': 'no file!',
        '401': 'unauthenticated access!',
        'other': ''
    }).service('httpErrorHandleService', function(httpErrorHandleMessage, dialogs, $sce) {

        var httpErrorHandleService = this;
        httpErrorHandleService.process = function(response) {
            //dialogs.error('error', httpErrorHandleMessage[response.StatusCode]);
            //var title = response.status + ' ' + response.statusText;
            var title = '操作失败';
            var message = response.data.message || response.data.description;
            var detail = response.data.messageDetail || response.data.stackTrace;

            var errorDetail = '<span> <a style="cursor:pointer" data-toggle="collapse" data-target="#dialogErrorBody">' + message + '</a></span><div id="dialogErrorBody" class="collapse out">' + detail + '</div>';

            dialogs.error(title, $sce.trustAsHtml(errorDetail));
        };

        return this;
    });

    mcs.ng.factory('safeApply', function($rootScope) {
        return function(scope, fn) {
            var phase = scope.$root.$$phase;
            if (phase == '$apply' || phase == '$digest') {
                if (fn && (typeof(fn) === 'function')) {
                    fn();
                }
            } else {
                scope.$apply(fn);
            }
        };
    });


    //for controller scope event mechanism
    mcs.ng.factory('eventService', function() {

        var eventService = this;

        eventService.registerEvent = function(eventName) {
            eventService[eventName] = [];
        };

        eventService.unRegisterEvent = function(eventName) {
            delete eventService[eventName];
        };

        eventService.fire = function(eventName, eventArgs) {

            eventService[eventName].forEach(function(handler) {
                handler(eventArgs);
            });

        };

        eventService.clearHandler = function(eventName) {
            eventService[eventName] = [];
        };

        eventService.bind = function(eventName, handler) {
            eventService[eventName].push(handler);
        };

        return eventService;

    });

})();

(function () {
    'use strict';

    mcs.ng.filter('props', function () {
        return function (items, props) {
            var out = [];
            if (angular.isArray(items)) {
                var keys = Object.keys(props);
                items.forEach(function (item) {
                    var itemMatches = false;
                    for (var i = 0; i < keys.length; i++) {
                        var prop = keys[i];
                        var text = props[prop].toLowerCase();
                        if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                            itemMatches = true;
                            break;
                        }
                    }
                    if (itemMatches) {
                        out.push(item);
                    }
                });
            } else {
                // Let the output be the input untouched
                out = items;
            }
            return out;
        };
    });

    mcs.ng.filter('trusted', ['$sce', function ($sce) {
        return function (text) {
            return $sce.trustAsHtml(text);
        };
    }]);

    mcs.ng.filter('normalize', function () {
        return function (text) {
            text += '';
            return !mcs.util.bool(text) || text.indexOf('0001-01-01') > -1 || text.indexOf('9999-09-09') > -1 || (text.indexOf('￥') > -1 && parseFloat(text.replace('￥', '')) == 0) ? '' : text;
        };
    });

    mcs.ng.filter('truncate', function () {
        return function (text, length) {
            if (!text) return '';
            if (!length) {
                // 按照数组进行拦截
                text = text.replace('，', ',');
                var array = mcs.util.toArray(text);
                if (array.length > 1) {
                    return array[0] + '...';
                }
            }
            if (length > 0 && length < text.length) {
                return text.substr(0, length) + '...';
            }
            return text;
        }
    });

    mcs.ng.filter('tooltip', function () {
        return function (text, length) {
            if (!text) return '';
            length = length || 10;
            return text && mcs.util.isString(text) && text.length > length ? text : '';
        };
    });

    mcs.ng.filter('rmb', function () {
        return function (input) {
            if (!/^(0|[1-9]\d*)(\.\d+)?$/.test(input))
                return '';
            if (parseFloat(input) == 0) return '零元整';
            var unit = '仟佰拾亿仟佰拾万仟佰拾元角分', str = '';
            input += '00';
            var p = input.indexOf('.');
            if (p >= 0)
                input = input.substring(0, p) + input.substr(p + 1, 2);
            unit = unit.substr(unit.length - input.length);
            for (var i = 0; i < input.length; i++)
                str += '零壹贰叁肆伍陆柒捌玖'.charAt(input.charAt(i)) + unit.charAt(i);
            return str.replace(/零(仟|佰|拾|角)/g, '零').replace(/(零)+/g, '零').replace(/零(万|亿|元)/g, '$1').replace(/(亿)万|壹(拾)/g, '$1$2').replace(/^元零?|零分/g, "").replace(/元$/g, '元整');
        };
    });
})();

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

(function() {
    'use strict';
    mcs.ng.service('autoCompleteDataService', function($http) {
        var autoCompleteDataService = {};
        autoCompleteDataService.query = function(url, query, filter) {

            return $http.post(url, query, {
                headers: {
                    'Content-Type': 'application/json',
                    'Accept': 'application/json',
                    'autoComplete': true
                }
            }).then(function(result) {
                if (result.data) {
                    return filter ? filter()(result.data) : result.data;
                }
            });
        };

        return autoCompleteDataService;
    });
})();

(function () {
    'use strict';

    mcs.ng.constant('buttonConfig', {
        categories: ['add', 'edit', 'delete', 'search', 'view', 'save', 'cancel', 'ok', 'close', 'print', 'upload', 'download', 'history', 'export', 'refresh', 'submit', 'approve', 'reject'],
        defaultTexts: ['新 增', '编 辑', '删 除', '查 询', '查 看', '保 存', '取 消', '确 定', '关 闭', '打 印', '上 传', '下 载', '历 史', '导 出', '刷 新', '提交审批', '同 意', '驳 回'],
        sizes: ['mini', 'medium', 'large'],
        sizeCss: ['btn-xs', 'btn-sm', 'btn-lg'],
        iconClass: ['fa-plus', 'fa-pencil', 'fa-trash-o', 'fa-search', 'fa-eye', 'fa-save', 'fa-undo', 'fa-check', 'fa-times', 'fa-print', 'fa-upload', 'fa-download', 'fa-history', 'fa-share', 'fa-refresh', 'fa-arrow-right', 'fa-check-square', 'fa-undo']
    })
    .directive('mcsDropdownButton', function () {
        return {
            restrict: 'E',
            scope: {
                category: '@', //"info", "primary", "warning", "success", "danger"
                css: '@',
                title: '@',
                items: '=',
                icon: '@'
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-dropdown-button.tpl.html',
            replace: true,
            link: function ($scope, $elem, $attrs, $ctrl) {
                $scope.category = $scope.category || 'info';
                $scope.title = $scope.title || '新 增';
                if ($scope.icon) {
                    if ($scope.icon.indexOf('fa-') == -1) {
                        $scope.icon = 'fa-' + $scope.icon;
                    }
                } else {
                    $scope.icon = 'fa-plus';
                }
                $elem.find('i').addClass($scope.icon);
            }
        };
    }).directive('mcsButton', function (buttonConfig) {

        return {
            restrict: 'E',
            scope: {
                category: '@',
                text: '@',
                icon: '@',
                size: '@',
                css: '@',
                title: '&',
                click: '&'
            },

            template: '<button class="btn" type="button" ng-click="click()" ng-class="{\'mcs-width-130\':category!=\'icon\'}"><i class="ace-icon fa bigger-110"></i><span></span></button>',
            replace: true,
            link: function ($scope, $elem, $attrs, $ctrl) {
                var index = buttonConfig.categories.indexOf($scope.category);
                var buttonCss = 'btn-info';
                switch ($scope.category) {
                    case 'search':
                    case 'view':
                        buttonCss = 'btn-primary';
                        break;
                    case 'delete':
                        buttonCss = 'btn-danger';
                        break;
                    case 'save':
                    case 'ok':
                    case 'approve':
                        buttonCss = 'btn-success';
                        break;
                    case 'upload':
                    case 'download':
                    case 'history':
                    case 'export':
                    case 'print':
                    case 'export':
                    case 'refresh':
                    case 'reject':
                        buttonCss = 'btn-yellow';
                        break;
                    case 'submit':
                        buttonCss = 'btn-purple';
                        break;
                    case 'cancel':
                    case 'close':
                        buttonCss = 'btn-light';
                        break;
                }
                $elem.addClass(buttonCss);
                if ($scope.size) {
                    var sizeIndex = buttonConfig.sizes.indexOf($scope.size);
                    $elem.addClass(buttonConfig.sizeCss[sizeIndex]);
                }
                if ($scope.icon && index === -1) {
                    if ($scope.icon.indexOf('fa-') == 0) {
                        $elem.find('i').addClass($scope.icon);
                    } else {
                        $elem.find('i').addClass('fa-' + $scope.icon);
                    }
                } else {
                    $elem.find('i').addClass(buttonConfig.iconClass[index]);
                }
                if ($scope.css) {
                    $elem.addClass($scope.css);
                }
                if ($scope.category != 'icon') {
                    $elem.find('span').text($scope.text || buttonConfig.defaultTexts[index]);
                } else {
                    $elem.find('span').text('');
                }
            }
        };
    });
})();
(function () {
    mcs.ng.directive('mcsCascadingSelect', ['$http', 'mcsValidationService', function ($http, validationService) {
        return {
            restrict: 'E',
            scope: {
                level: '@',
                caption: '@',
                data: '=?',
                model: '=?',
                async: '@',
                url: '@',
                path: '@',
                root: '@',
                params: '=?',
                otherParams: '=?',
                parentKey: '@',
                customStyle: '@',
                disabledLevel: '=?',
                callback: '&'
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-cascading-select.tpl.html',
            replace: true,
            controller: function ($scope) {
                if (!$scope.caption) {
                    $scope.level = $scope.level || 3;
                    $scope.caption = [];
                    for (var i = 0; i < $scope.level; i++) {
                        $scope.caption.push('请选择');
                    }
                } else {
                    $scope.level = mcs.util.toArray($scope.caption).length;
                }
            },
            link: function ($scope, $elem, $attrs, $ctrl) {
                $scope.path = mcs.util.bool($scope.path || false);
                $scope.root = $scope.root || '0';
                $scope.async = mcs.util.bool($scope.async || true);
                $scope.model = $scope.model || {};
                $scope.params = $scope.params || {};
                $scope.parentKey = $scope.parentKey || 'parentKey';

                var loadData = function (elem, id) {
                    if (!id) return;
                    // 支持异步
                    var $$model = mcs.util.clone($scope.model);
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
                            $scope.model = $$model;
                            for (var i in result.data) {
                                var item = result.data[i];
                                $scope.data[id] = $scope.data[id] || {};
                                $scope.data[id][item.key] = item.value;
                            }
                            loadDataCallback(elem, $scope.data[id]);
                        });
                    } else {
                        if ($scope.data == undefined || typeof ($scope.data[id]) == undefined)
                            return false;
                        loadDataCallback(elem, $scope.data[id]);
                    }
                };

                var captions = mcs.util.toArray($scope.caption);
                // 添加验证消息
                $scope.$watch('$parent.required', function () {
                    if ($scope.$parent.required) {
                        $elem.attr('required', 'required');
                    }
                    if ($scope.$parent.requiredLevel) {
                        $elem.attr('required-level', $scope.$parent.requiredLevel);
                    }
                    loadWatchDataCallback();
                });

                var loadDataCallback = function (elem, json) {
                    var width = ($elem.width() - ($scope.level * 10)) / $scope.level;
                    // 获取当前下拉框在级联容器中的索引
                    var index = elem.closest('ul').children().index(elem.parent());
                    var selectedValue = '';
                    $elem.find('.select2-container').width(width);
                    if (json) {
                        $.each(json, function (k, v) {
                            var option = '<option value="' + k + '">' + v + '</option>';
                            if (($scope.model && k == $scope.model[index]) ||
                                ($scope.$parent && k == $scope.$parent.model[index])) {
                                selectedValue = k;
                            }
                            elem.append(option);
                        });
                    }
                    // 默认选中
                    elem.select2('val', selectedValue);
                    // 如果选中则加载下一下拉框
                    if (selectedValue) {
                        var next = $elem.find('select').eq(index + 1);
                        var parent = $scope.model[index];
                        if ($scope.path) {
                            switch (index) {
                                case 0:
                                    parent = $scope.root + ',' + parent;
                                    break;
                                case 1:
                                    parent = $scope.root + ',' + $elem.find('select').eq(index - 1).val() + ',' + parent;
                                    break;
                                case 2:
                                    parent = $scope.root + ',' + $elem.find('select').eq(index - 2).val() + ',' + $elem.find('select').eq(index - 1).val() + ',' + parent;
                                    break;
                            }
                        }
                        // 回传当前选中的数据
                        $scope.model['selected'] = $scope.model['selected'] || {};
                        $scope.model['selected'][index] = $scope.model['selected'][index] || {};
                        $scope.model['selected'][index]['key'] = parent;
                        $scope.model['selected'][index]['value'] = $elem.find('select').eq(index).select2('data').text;
                        // 注册回调事件
                        var isLast = (index == captions.length - 1);
                        if (isLast) {
                            $scope.$watch('$scope.model', $scope.callback);
                        }
                        // 扩展其他参数配置
                        if (mcs.util.bool($scope.async)) {
                            $scope.params[$scope.parentKey] = parent;
                            if ($scope.otherParams) {
                                for (var prop in $scope.params) {
                                    if ($scope.otherParams[prop]) {
                                        $scope.params[prop] = $scope.otherParams[prop];
                                    }
                                }
                            }
                        }

                        loadData(next, parent);
                        //next.change();
                    }
                };

                var loadWatchDataCallback = function () {
                    if (mcs.util.hasAttr($elem, 'required') || mcs.util.hasAttr($elem, 'required-level')) {
                        var parent = $elem.parent();
                        if ($scope.$parent.required || $scope.$parent.requiredLevel) {
                            parent = parent.parent();
                        }
                        var validationItem = $elem.closest('.form-group');
                        var message = validationItem.find('.help-block');
                        var validateRow = $elem.closest('.row');
                        var validationItems = validateRow.find('.form-group');
                        if (!message || !message.length) {
                            // 对于单行中只有一个验证项则附加水平消息框
                            if (validationItems.length == 1) {
                                validationItem.append('<div class="help-block horizontal"></div>');
                            } else {
                                parent.append('<div class="help-block"></div>');
                            }
                        }
                    }

                    // 构建option
                    $.each(captions, function (k, v) {
                        var option = '<option value="">' + v + '</option>';
                        var select = $elem.find('select').eq(k);
                        var length = captions.length;
                        var isLast = (k == captions.length - 1);

                        // 返回已选择的数据
                        $scope.model[k] = select.val() || $scope.model[k];

                        if ($scope.customStyle) {
                            select.attr('style', $scope.customStyle);
                        }

                        // 添加必选规则
                        if (mcs.util.hasAttr($elem, 'required') ||
                           (mcs.util.hasAttr($elem, 'required-level') && $elem.attr('required-level') == length)) {
                            select.attr('required', 'required');
                        } else {
                            // 添加部分必选规则
                            if (mcs.util.hasAttr($elem, 'required-level')) {
                                var requiredLevel = parseInt($elem.attr('required-level'));
                                if (requiredLevel > 0 && requiredLevel < length) {
                                    if (k < requiredLevel) {
                                        select.attr('required-level', $elem.attr('required-level'));
                                    }
                                }
                            }
                        }

                        select.append(option).select2().change(function () {
                            // 返回已选择的数据
                            $scope.model[k] = select.val();
                            $scope.model['selected'] = $scope.model['selected'] || {};
                            // 注册回调事件
                            $scope.$watch('$scope.model', $scope.callback);
                            switch (k) {
                                case 0:
                                    $scope.model['selected'].key = select.val();
                                    $scope.model['selected'].value = select.select2('data').text;
                                    break;
                                case 1:
                                    var prev = $elem.find('select').eq(k - 1);
                                    $scope.model['selected'].key = prev.val() + ',' + select.val();
                                    $scope.model['selected'].value = prev.select2('data').text + ',' + select.select2('data').text;
                                    break;
                                case 2:
                                    var prev = $elem.find('select').eq(k - 1);
                                    var last = $elem.find('select').eq(k - 2);
                                    $scope.model['selected'].key = last.val() + ',' + prev.val() + ',' + select.val();
                                    $scope.model['selected'].value = last.select2('data').text + ',' + prev.select2('data').text + ',' + select.select2('data').text;
                                    break;
                            }

                            if (!isLast) {
                                var next = $elem.find('select').eq(k + 1);
                                next.empty().append('<option value="">' + captions[k + 1] + '</option>');
                                var parent = select.val();

                                if ($scope.path) {
                                    switch (k) {
                                        case 0:
                                            parent = $scope.root + ',' + parent;
                                            break;
                                        case 1:
                                            parent = $scope.root + ',' + $elem.find('select').eq(k - 1).val() + ',' + parent;
                                            break;
                                        case 2:
                                            parent = $scope.root + ',' + $elem.find('select').eq(k - 2).val() + ',' + $elem.find('select').eq(k - 1).val() + ',' + parent;
                                            break;
                                    }
                                }
                                // 判断是否为异步且只加载当前项的数据
                                if (mcs.util.bool($scope.async) && select.val()) {
                                    $scope.params[$scope.parentKey] = parent;
                                    if ($scope.otherParams) {
                                        for (var prop in $scope.params) {
                                            if ($scope.otherParams[prop]) {
                                                $scope.params[prop] = $scope.otherParams[prop];
                                            }
                                        }
                                    }
                                }
                                loadData(next, parent);
                                next.change();
                            } else {
                                var validateElem = select;
                                if (mcs.util.hasAttr($elem, 'required-level')) {
                                    validateElem = select.parent().parent().find('select[required-level]:last');
                                }
                                validationService.validate(validateElem, $scope);
                            }
                            // 触发$digest
                            $scope.$apply('$scope.model');
                        });
                    });

                    // 默认加载第一级
                    loadData($elem.find('select').eq(0), $scope.root);
                };
            }
        }
    }]);
})();
(function () {
    'use strict';

    mcs.ng.directive('mcsCheckboxGroup', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '=',
                defaultKey: '@'
            },          
            template: '<label class="checkbox-inline" ng-repeat="item in data" ng-class="{\'all\':item.key==-1}" ng-style="customStyle" uib-popover="{{item.value | tooltip:6}}" popover-trigger="mouseenter"><input type="checkbox" class="ace" ng-checked="model && model.indexOf(item.key)!=-1" ng-disabled="disabled" ng-click="change(item, $event)"><span class="lbl"></span> {{item.value | truncate:6}}</label>',
            controller: function ($scope) {
                $scope.defaultKey = $scope.defaultKey || '-1';
                $scope.disabled = mcs.util.bool($scope.$parent.disabled);
                $scope.change = function (item, event) {
                    $scope.model = $scope.model || [];
                    if (item.key == $scope.defaultKey) {
                        var items = $scope.data;
                        $scope.model = [];
                        if (event.target.checked) {
                            for (var index in items) {
                                var item = items[index];
                                $scope.model.push(item.key);
                            }
                        } 
                    } else {
                        var length = !$scope.data ? 0 : $scope.data.length;
                        mcs.util.setSelectedItems($scope.model, item, event, length, $scope.defaultKey);
                    }
                    validationService.validate($(event.target), $scope);
                };
                $scope.customStyle = {};
                if ($scope.$parent.width) {
                    $scope.customStyle['width'] = $scope.$parent.width;
                }
            },
            link: function ($scope, $elem) {
                $scope.$watch('data', function (value) {
                    if (value && $scope.$parent.required) {
                        $elem.find(':checkbox').attr('required', 'required');
                    }
                });
            }
        }
    }]);
})();
(function () {
    'use strict';

    mcs.ng.directive('mcsDatarange', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                width: '@',
                min: '=',
                minText: '@',
                max: '=',
                maxText: '@',
                datatype: '@',
                disabled: '@',
                unit: '@'
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/datarange.tpl.html',
            controller: function ($scope) {
                $scope.customStyle = {};

                if ($scope.width) {
                    $scope.customStyle['width'] = $scope.width;
                }
            },
            link: function ($scope, $elem) {
                $scope.minText = $scope.minText || '起始金额';
                $scope.maxText = $scope.maxText || '截止金额';
                $scope.unit = $scope.unit || '元';
                $scope.disabled = mcs.util.bool($scope.disabled || false);
                var dataType = ($scope.datatype || 'number').toLowerCase();
                var inputs = $elem.find('input[type=text]');
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    inputs.attr('required', 'required');

                    mcs.util.appendMessage($elem);
                }

                if (dataType == 'int') {
                    inputs.bind('keyup afterpaste', function () {
                        var $this = $(this);
                        mcs.util.limit($this);
                    });
                } else if (dataType == 'number') {
                    inputs.bind('keyup afterpaste', function () {
                        var $this = $(this);
                        mcs.util.number($this);
                    });
                }

                inputs.each(function () {
                    var $this = $(this);
                    $this.next().on('click', function () {
                        $scope.$apply(function () {
                            $scope[$this.attr('ng-model')] = null;
                        });
                    });
                });

                $scope.$watch('min', function (data) {
                    if (data == undefined || data == '') return;
                    var max = parseFloat($scope.max);
                    if (data > max) {
                        $scope.min = max;
                    }
                    validationService.validate(inputs, $scope);
                });
                $scope.$watch('max', function (data) {
                    if (data == undefined || data == '') return;
                    var min = parseFloat($scope.min);
                    if (data < min) {
                        $scope.max = min;
                    }
                    validationService.validate(inputs, $scope);
                });
            }
        }
    }]);
})();
/**
 * '每页<select ng-model="conf.pageSize" ng-options="option for option in conf.perPageOptions "></select>'
 * you can use this for specify items in one page
 */
(function() {
    angular.module('mcs.ng.paging', [])
        .controller('pagingController', function($scope) {

        })

    .directive('tmPagination', function() {
        return {
            restrict: 'EA',
            template: '<div class="page-list">' +
                '<ul class="pagination" ng-show="conf.totalCount > 0">' +
                '<li ng-class="{disabled: conf.pageIndex == 1}" ng-click="prevPage()" style="cursor:pointer;"><span>&laquo;</span></li>' +
                '<li ng-repeat="item in pageList track by $index"  ng-class="{active: item == conf.pageIndex, separate: item == \'...\'}" ' +
                'ng-click="item != conf.pageIndex?changepageIndex(item):null" style="cursor:pointer;">' +
                '<span>{{ item }}</span>' +
                '</li>' +
                '<li ng-class="{disabled: conf.pageIndex == conf.numberOfPages}" ng-click="nextPage()" style="cursor:pointer;"><span>&raquo;</span></li>' +
                '</ul>' +
                '<div class="page-total" ng-show="conf.totalCount > 0">' +
                '第 <input type="text" ng-model="jumpPageNum" class="mcs-input-small" ng-keypress="$event.keyCode == 13?jumpToPage($event):null" onkeyup="mcs.util.limit(this)" onafterpaste="mcs.util.limit(this)"/>页 ' +

                '共<strong> {{ conf.totalCount }} </strong>条记录 ' +
                '<button class="btn btn-minier btn-primary" ng-click="jumpToPage($event)"><i class="ace-icon fa bigger-110 fa-mail-forward"></i> 跳 转</button>' +
                '</div>' +
                '<div class="no-items" ng-show="conf.totalCount <= 0">暂无数据</div>' +
                '</div>',
            replace: true,
            scope: {
                conf: '=',
                pageChange: '&'
            },
            link: function(scope, element, attrs) {

                // 变更当前页
                scope.changepageIndex = function(item) {
                    if (item == '...') {
                        return;
                    } else {
                        scope.conf.pageIndex = item;
                        scope.pageChange()
                    }
                };

                // 定义分页的长度必须为奇数 (default:9)
                scope.conf.pagesLength = parseInt(scope.conf.pagesLength) ? parseInt(scope.conf.pagesLength) : 9;
                if (scope.conf.pagesLength % 2 === 0) {
                    // 如果不是奇数的时候处理一下
                    scope.conf.pagesLength = scope.conf.pagesLength - 1;
                }

                // conf.erPageOptions
                if (!scope.conf.perPageOptions) {
                    scope.conf.perPageOptions = [10, 15, 20, 30, 50];
                }

                // pageList数组
                function getPagination(newValue, oldValue) {


                    // conf.pageIndex
                    scope.conf.pageIndex = parseInt(scope.conf.pageIndex) ? parseInt(scope.conf.pageIndex) : 1;



                    // conf.totalCount
                    scope.conf.totalCount = parseInt(scope.conf.totalCount) ? parseInt(scope.conf.totalCount) : 0;

                    // conf.pageSize (default:15)
                    scope.conf.pageSize = parseInt(scope.conf.pageSize) ? parseInt(scope.conf.pageSize) : 15;


                    // numberOfPages
                    scope.conf.numberOfPages = Math.ceil(scope.conf.totalCount / scope.conf.pageSize);

                    // judge pageIndex > scope.numberOfPages
                    if (scope.conf.pageIndex < 1) {
                        scope.conf.pageIndex = 1;
                    }

                    // 如果分页总数>0，并且当前页大于分页总数
                    if (scope.conf.numberOfPages > 0 && scope.conf.pageIndex > scope.conf.numberOfPages) {
                        scope.conf.pageIndex = scope.conf.numberOfPages;
                    }

                    // jumpPageNum
                    scope.jumpPageNum = scope.conf.pageIndex;

                    // 如果pageSize在不在perPageOptions数组中，就把pageSize加入这个数组中
                    var perPageOptionsLength = scope.conf.perPageOptions.length;
                    // 定义状态
                    var perPageOptionsStatus;
                    for (var i = 0; i < perPageOptionsLength; i++) {
                        if (scope.conf.perPageOptions[i] == scope.conf.pageSize) {
                            perPageOptionsStatus = true;
                        }
                    }
                    // 如果pageSize在不在perPageOptions数组中，就把pageSize加入这个数组中
                    if (!perPageOptionsStatus) {
                        scope.conf.perPageOptions.push(scope.conf.pageSize);
                    }

                    // 对选项进行sort
                    scope.conf.perPageOptions.sort(function(a, b) {
                        return a - b
                    });

                    scope.pageList = [];
                    if (scope.conf.numberOfPages <= scope.conf.pagesLength) {
                        // 判断总页数如果小于等于分页的长度，若小于则直接显示
                        for (i = 1; i <= scope.conf.numberOfPages; i++) {
                            scope.pageList.push(i);
                        }
                    } else {
                        // 总页数大于分页长度（此时分为三种情况：1.左边没有...2.右边没有...3.左右都有...）
                        // 计算中心偏移量
                        var offset = (scope.conf.pagesLength - 1) / 2;
                        if (scope.conf.pageIndex <= offset) {
                            // 左边没有...
                            for (i = 1; i <= offset + 1; i++) {
                                scope.pageList.push(i);
                            }
                            scope.pageList.push('...');
                            scope.pageList.push(scope.conf.numberOfPages);
                        } else if (scope.conf.pageIndex > scope.conf.numberOfPages - offset) {
                            scope.pageList.push(1);
                            scope.pageList.push('...');
                            for (i = offset + 1; i >= 1; i--) {
                                scope.pageList.push(scope.conf.numberOfPages - i);
                            }
                            scope.pageList.push(scope.conf.numberOfPages);
                        } else {
                            // 最后一种情况，两边都有...
                            scope.pageList.push(1);
                            scope.pageList.push('...');

                            for (i = Math.ceil(offset / 2); i >= 1; i--) {
                                scope.pageList.push(scope.conf.pageIndex - i);
                            }
                            scope.pageList.push(scope.conf.pageIndex);
                            for (i = 1; i <= offset / 2; i++) {
                                scope.pageList.push(scope.conf.pageIndex + i);
                            }

                            scope.pageList.push('...');
                            scope.pageList.push(scope.conf.numberOfPages);
                        }
                    }

                    if (scope.conf.onChange) {


                        // 防止初始化两次请求问题
                        if (!(oldValue != newValue && oldValue[0] == 0)) {
                            scope.conf.onChange();
                        }

                    }
                    scope.$parent.conf = scope.conf;
                }

                // prevPage
                scope.prevPage = function() {
                    if (scope.conf.pageIndex > 1) {
                        scope.conf.pageIndex -= 1;
                        scope.pageChange();

                    }
                };
                // nextPage
                scope.nextPage = function() {
                    if (scope.conf.pageIndex < scope.conf.numberOfPages) {
                        scope.conf.pageIndex += 1;
                        scope.pageChange();
                    }
                };

                // 跳转页
                scope.jumpToPage = function() {
                    //scope.jumpPageNum = scope.jumpPageNum.replace(/[^0-9]/g, '');
                    if (!scope.jumpPageNum) return;
                    scope.conf.pageIndex = scope.jumpPageNum;
                    scope.pageChange();
                };



                scope.$watch(function() {


                    if (!scope.conf.totalCount) {
                        scope.conf.totalCount = 0;
                    }


                    var newValue = scope.conf.totalCount + ' ' + scope.conf.pageIndex + ' ' + scope.conf.pageSize;


                    return newValue;



                }, getPagination);

            }
        };
    });

})();

(function() {

    angular.module('mcs.ng.datatable', [])

    .controller('datatableController', function($scope) {
        $scope.paginationConf = {
            currentPage: 1,
            itemsPerPage: 5,
            totalItems: 20
        };

    })



    .directive('mcsDatatable', function($compile) {

        return {
            restrict: 'E',
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-datatable.tpl.html ',
            replace: true,
            scope: {

                data: '=',
                nodataTip: '@?',
                retrieveData: '&?',
                pageChanged: '&?',
                condition: '=?',



            },
            link: function($scope, iElm, iAttrs, controller) {

                if (!$scope.data) {
                    return;
                }



                if ($scope.retrieveData) {
                    $scope.retrieveData().then(function() {
                        $scope.reMatchRowsSelected();
                    });
                }


                $scope.selectAll = function() {
                    if ($scope.data.selectAll) {
                        $scope.data.rows.forEach(function(rowSelected) {
                            rowSelected.selected = true;
                            var rowDataSelected = {};
                            $scope.data.keyFields.forEach(function(key) {
                                rowDataSelected[key] = rowSelected[key];
                            });
                            mcs.util.removeByObject($scope.data.rowsSelected, rowDataSelected);
                            $scope.data.rowsSelected.push(rowDataSelected);

                        })
                    } else {
                        $scope.data.rows.forEach(function(rowSelected) {
                            rowSelected.selected = false;
                            var rowDataSelected = {};
                            $scope.data.keyFields.forEach(function(key) {
                                rowDataSelected[key] = rowSelected[key];
                            });
                            mcs.util.removeByObject($scope.data.rowsSelected, rowDataSelected);
                        })
                    }
                }

                function findoutRowFromSelectedSet(targetRow) {
                    $scope.data.rowsSelected.forEach(function(row) {

                    })
                }

                $scope.rowSelect = function(rowSelected) {
                    $scope.data.selectAll = false;
                    if ($scope.data.selection == 'radio') {
                        var rowDataSelected = $scope.data.rowsSelected[0] = $scope.data.rowsSelected[0] || {};

                        $scope.data.keyFields.forEach(function(key) {
                            rowDataSelected[key] = rowSelected[key];
                        });



                        rowSelected.selected = true;
                        $scope.data.rows.forEach(function(row) {
                            if (rowSelected[$scope.data.keyFields[0]] != row[$scope.data.keyFields[0]]) {

                                row.selected = false;
                            }
                        })

                    } else {

                        var rowDataSelected = {};
                        $scope.data.keyFields.forEach(function(key) {
                            rowDataSelected[key] = rowSelected[key];
                        });

                        if (rowSelected.selected) {

                            $scope.data.rowsSelected.push(rowDataSelected);
                        } else {
                            mcs.util.removeByObject($scope.data.rowsSelected, rowDataSelected);
                        }

                    }
                };

                $scope.pageChange = function(callback) {

                    if (!angular.equals($scope.conditionCopy, $scope.condition)) {
                        angular.copy($scope.conditionCopy, $scope.condition);
                    }

                    if (callback()) {
                        callback().then(function() {
                            $scope.reMatchRowsSelected();
                        })
                    }


                }

                $scope.reMatchRowsSelected = function() {

                    var selecteds = $scope.data.rowsSelected;
                    var rows = $scope.data.rows;
                    var keys = $scope.data.keyFields;

                    if (selecteds.length == 0 || keys.length == 0 || rows.length == 0) {
                        return;
                    }



                    for (var indexSelected in selecteds) {
                        for (var rowIndex in rows) {

                            var counter = 0;
                            for (var keyIndex in keys) {
                                if (rows[rowIndex][keys[keyIndex]] == selecteds[indexSelected][keys[keyIndex]]) {
                                    counter = counter + 1;
                                }


                            }

                            if (counter == keys.length) {
                                rows[rowIndex].selected = true;
                            }
                        }
                    }

                };


                $scope.reorder = function(header, callback) {
                    header.orderDirection = (header.orderDirection == 'asc' || header.orderDirection == undefined) ? 'desc' : 'asc';

                    if (header.sortable) {
                        if ($scope.onOrder && angular.isFunction($scope.onOrder())) {
                            $scope.onOrder({
                                dataField: header.sort || header.field,
                                sortDirection: header.orderDirection == 'asc' ? 0 : 1
                            });
                        } else {
                            $scope.data.orderBy[0].dataField = header.sort || header.field;
                            $scope.data.orderBy[0].sortDirection = header.orderDirection == 'asc' ? 0 : 1;
                            callback();
                            $scope.reMatchRowsSelected();
                        }

                    }


                };

                $scope.data.searching = function() {
                    if ($scope.condition) {
                        $scope.conditionCopy = {};
                        angular.copy($scope.condition, $scope.conditionCopy);
                    }
                };


                var reg = /click=['"]\w*/ig

                $scope.data.headers.forEach(function(header, index) {

                    if (!header.field) {
                        var results = header.template && header.template.match(reg);
                        if (results) {
                            results.forEach(function(r) {
                                var fnName = r.replace('"', '\'').split('\'')[1];
                                $scope[fnName] = $scope.$parent[fnName];
                            })
                        }

                    }
                });



            }
        };
    })

    .directive('mcsDatatableRow', function($compile) {
        return {
            restrict: 'A',
            link: function(scope, iElement, iAttrs) {
                var dataHeaders = scope.$parent.data.headers;
                var rowData = scope.row;
                scope.vm = scope.$parent.$parent.vm;

                dataHeaders.forEach(function(header, index) {
                    var td = $compile('<td class="' + header.headerCss +
                        '">' + (header.template || ('<span>{{row.' + header.field + '}}</span>')) + '</td>')(scope);
                    iElement.append(td);
                });

            }
        };
    });
})();

(function() {
    'use strict';

    mcs.ng.directive('datetimeType', function($filter) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function($scope, $elem, $attrs, $ctrl) {
                //$ctrl.$parsers.push(function(data) {
                //    return new Date(data);
                //});

                $ctrl.$formatters.push(function(data) {
                    return $filter('date')(data, 'yyyy-MM-dd HH:mm:ss');
                });
            }
        }
    });

    mcs.ng.directive('dateType', function($filter) {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function($scope, $elem, $attrs, $ctrl) {
                // $ctrl.$parsers.push(function(data) {
                //     return new Date(data);
                // });

                $ctrl.$formatters.push(function(data) {
                    return $filter('date')(data, 'yyyy-MM-dd');
                });
            }
        }
    });

    mcs.ng.directive('clearMinDate', function() {
        function clearMinDateFormatter(value) {
            if (!value || (value && (value.toLowerCase() == 'invalid date') || value.toLowerCase().match(/0001-01-01/)) || (value && value.getFullYear && value.getFullYear() == 1)) {
                return '';
            }

            return value;
        }

        return {
            restrict: 'A',
            require: 'ngModel',
            link: function($scope, iElm, iAttrs, ngModelController) {
                ngModelController.$formatters.unshift(clearMinDateFormatter);
            }
        };
    });

})();

(function() {
    'use strict';

    mcs.ng.directive('mcsDatepicker', ['mcsValidationService', function(validationService) {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                model: '=',
                placeholder: '@',
                zIndex: '@',
                startDate: '=?',
                endDate: '=?',
                disabled: '=?' //禁用(默认false,true) 
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/datepicker.tpl.html',
            controller: function($scope) {
                if ($scope.zIndex) {
                    $scope.dateStyle = {
                        'z-index': $scope.zIndex
                    };
                }
            },
            link: function($scope, $elem) {
                var $this = $elem.find('.date-picker');
                $scope.placeholder = $scope.placeholder || '输入日期';
                $scope.disabled = mcs.util.bool($scope.disabled || false);
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    $elem.find('input[type=text]').attr('required', 'required');

                    mcs.util.appendMessage($elem);
                }

                $this.datetimepicker({
                        autoclose: true,
                        todayBtn: true,
                        todayHighlight: true,
                        startDate: $scope.startDate,
                        endDate: $scope.endDate,
                        minView: 'month', //选择日期后，不会再跳转去选择时分秒 
                        maxView: 'decade',
                        format: ppts.config.datePickerFormat,
                        language: ppts.config.datePickerLang
                    }) //show datepicker when clicking on the icon
                    .on("hide", function() {
                        var _this = $(this);
                        $scope.$apply(function() {
                            $scope.model = _this.val() == '' ? null : moment(_this.val()).utc()._d;
                        });
                        validationService.validate($elem.find('input[required]'), $scope);
                    }).next().on('click', function() {
                        $scope.$apply(function() {
                            $scope[$this.attr('ng-model')] = null;
                        });
                    }).next().on('click', function() {
                        $this.focus();
                    });
            }
        }
    }]);
})();

(function() {
    'use strict';

    mcs.ng.directive('mcsDaterangepicker', ['mcsValidationService', function(validationService) {
        return {
            restrict: 'E',
            scope: {
                width: '@',
                css: '@',
                size: '@',
                mode: '@', //date, datetime
                startDate: '=?',
                endDate: '=?',
                startText: '@',
                endText: '@',
                rangeRestrict: '@?', //开始日期和结束日期的关系限制
                offset: '@?', //偏移量
                minutes: '=?', //计算时间差（仅用于同一天）,
                disabled: '@?', //禁用(默认none,both,before,after) 
                zIndex: '@'
            },
            templateUrl: function($elem, $attrs) {
                return mcs.app.config.mcsComponentBaseUrl + ($attrs['mode'] && $attrs['mode'].toLowerCase() === 'datetime' ?
                    '/src/tpl/datetimerangepicker.tpl.html' : '/src/tpl/daterangepicker.tpl.html');
            },
            controller: function($scope) {
                $scope.dateStyle = {};

                if ($scope.zIndex) {
                    $scope.dateStyle['z-index'] = $scope.zIndex;
                }
                if ($scope.width) {
                    $scope.dateStyle['width'] = $scope.width;
                }
            },
            link: function($scope, $elem) {
                var $this = $elem.find('.date-picker,.date-timepicker');
                var $start = $this.filter('input[name=start]');
                var $end = $this.filter('input[name=end]');
                var offset = parseInt($scope.offset);
                var restrict = parseInt($scope.rangeRestrict);
                $scope.disabled = $scope.disabled || 'none';

                $scope.startText = $scope.startText || ($scope.mode == 'datetime' ? '开始时间' : '开始日期');
                $scope.endText = $scope.endText || ($scope.mode == 'datetime' ? '结束时间' : '结束日期');
                $scope.size = $scope.size || 'lg';
                $scope.mode = $scope.mode || 'date';
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    $elem.find('input[type=text]').attr('required', 'required');

                    mcs.util.appendMessage($elem);
                }

                var config = {
                    autoclose: true,
                    todayBtn: true,
                    todayHighlight: true,
                    language: ppts.config.datePickerLang,
                    startDate: mcs.date.getLeftBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset),
                    endDate: mcs.date.getRightBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset)
                };

                if ($scope.mode == 'date') {
                    config.minView = 'month'; //选择日期后，不会再跳转去选择时分秒 
                    config.maxView = 'decade';
                    config.format = ppts.config.datePickerFormat;
                } else {
                    config.format = ppts.config.datetimePickerFormat;
                }

                $start.datetimepicker(config).on('changeDate', function(selected) {
                    $end.datetimepicker('setStartDate', new Date(selected.date.valueOf()));

                    if (!isNaN(restrict)) {
                        $end.datetimepicker('setEndDate', mcs.date.getRightBoundDatetime($scope.startDate, restrict));
                    }


                }).on('hide', function() {
                    var _this = $(this);
                    $scope.$apply(function() {
                        $scope.startDate = _this.val() == '' ? null : moment(_this.val()).utc()._d;
                        // 仅针对同一天进行计算
                        if (offset == 0 || restrict == 0) {
                            if ($start.val() && $end.val()) {
                                $scope.minutes = mcs.date.datepart($start.val(), $end.val(), 'm');
                            }
                        }
                    });
                    validationService.validate($elem.find('input[required]'), $scope);
                }).next().on('click', function() {
                    $scope.$apply(function() {

                        $scope.startDate = null;
                        $end.datetimepicker('setStartDate', mcs.date.getLeftBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset));
                        $end.datetimepicker('setEndDate', mcs.date.getRightBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset));

                    });
                }).next().on('click', function() {
                    $start.focus();
                });


                $end.datetimepicker(config).on('changeDate', function(selected) {
                    $start.datetimepicker('setEndDate', new Date(selected.date.valueOf()));
                    if (!isNaN(restrict)) {
                        $start.datetimepicker('setStartDate', mcs.date.getLeftBoundDatetime($scope.endDate, restrict));
                    }
                }).on('hide', function() {
                    var _this = $(this);
                    $scope.$apply(function() {
                        $scope.endDate = _this.val() == '' ? null : moment(_this.val()).utc()._d;
                        // 仅针对同一天进行计算
                        if (offset == 0 || restrict == 0) {
                            if ($start.val() && $end.val()) {
                                $scope.minutes = mcs.date.datepart($start.val(), $end.val(), 'm');
                            }
                        }
                    });
                    validationService.validate($elem.find('input[required]'), $scope);
                }).next().on('click', function() {
                    $scope.$apply(function() {

                        $scope.endDate = null;

                        $start.datetimepicker('setEndDate', mcs.date.getRightBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset));
                        $start.datetimepicker('setStartDate', mcs.date.getLeftBoundDatetime(offset >= 0 ? $scope.startDate : $scope.endDate, offset));
                    });
                }).next().on('click', function() {
                    $end.focus();
                });
            }
        }
    }]);
})();

(function() {
    'use strict';

    mcs.ng.directive('mcsDatetimepicker', ['mcsValidationService', function(validationService) {
        return {
            restrict: 'E',
            scope: {
                css: '@',
                model: '=',
                placeholder: '@',
                zIndex: '@',
                startDate: '=?',
                endDate: '=?',
                disabled: '=?' //禁用(默认false,true) 
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/datetimepicker.tpl.html',
            controller: function($scope) {
                if ($scope.zIndex) {
                    $scope.dateStyle = {
                        'z-index': $scope.zIndex
                    };
                }
            },
            link: function($scope, $elem) {
                var $this = $elem.find('.date-timepicker');
                $scope.placeholder = $scope.placeholder || '输入时间';
                $scope.disabled = mcs.util.bool($scope.disabled || false);
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    $elem.find('input[type=text]').attr('required', 'required');

                    mcs.util.appendMessage($elem);
                }

                $this.datetimepicker({
                    format: ppts.config.datetimePickerFormat,
                    language: ppts.config.datePickerLang,
                    autoclose: true,
                    startDate: $scope.startDate,
                    endDate: $scope.endDate
                }).on("hide", function() {
                    var _this = $(this);
                    $scope.$apply(function() {
                        $scope.model = _this.val() == '' ? null : moment(_this.val()).utc()._d;
                    });
                    validationService.validate($elem.find('input[required]'), $scope);
                }).next().on('click', function() {
                    $scope.$apply(function() {                       
                        $scope[$this.attr('ng-model')] = null;
                    });
                }).next().on('click', function() {
                    $this.focus();
                });
            }
        }
    }]);
})();

(function() {
    'use strict';

    mcs.ng.service('mcsDialogService', ['dialogs', function(dialogs) {
        var mcsDialogService = this;
        mcsDialogService.messageConfig = {
            wait: {
                title: '操作中',
                message: '操作进行中，请稍后！',
            },
            info: {
                title: '消息',
                message: '至少选择一条数据！'
            },
            error: {
                title: '错误',
                message: '操作发生错误，请重试或联系系统管理员！'
            },
            confirm: {
                title: '请确认',
                message: '确认进行此操作吗？'
            }
        };

        this.info = function(options) {
            options = jQuery.extend({
                title: mcsDialogService.messageConfig.info.title,
                message: mcsDialogService.messageConfig.info.message,
                settings: {
                    backdrop: 'static'
                }
            }, options);

            return dialogs.notify(options.title, options.message, options.settings);
        };

        this.confirm = function(options) {
            options = jQuery.extend({
                title: mcsDialogService.messageConfig.confirm.title,
                message: mcsDialogService.messageConfig.confirm.message,
                settings: {
                    backdrop: 'static'
                }
            }, options);

            return dialogs.confirm(options.title, options.message, options.settings);
        };

        this.error = function(options) {
            options = jQuery.extend({
                title: mcsDialogService.messageConfig.error.title,
                message: mcsDialogService.messageConfig.error.message,
                settings: {
                    backdrop: 'static'
                }
            }, options);

            return dialogs.error(options.title, options.message, options.settings);
        };

        this.wait = function(options) {
            options = jQuery.extend({
                title: mcsDialogService.messageConfig.wait.title,
                message: mcsDialogService.messageConfig.wait.message,
                settings: {
                    backdrop: 'static'
                }
            }, options);

            return dialogs.wait(options.title, options.message);
        };

        this.create = function(url, options) {
            if (!url) return;
            options = jQuery.extend({
                controller: '',
                params: {},
                settings: {
                    backdrop: 'static',
                    size: 'md'
                },
                controllerAs: 'vm'
            }, options);

            return dialogs.create(url, options.controller, options.params, options.settings, options.controllerAs);
        };
    }]);
})();

(function () {
    'use strict';
    mcs.ng.directive('parseInputValue', function () {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function ($scope, $elem, $attrs, $ctrl) {
                $ctrl.$parsers.push(function (data) {
                    if ($attrs.datatype) {
                        var dataType = $attrs.datatype.toLowerCase();
                        switch (dataType) {
                            case 'int':
                                return parseInt(data);
                            case 'number':
                                return parseFloat(data);
                            default:
                                return data;
                        }
                    } else {
                        return data;
                    }
                });
            }
        };
    });

    mcs.ng.directive('mcsInput', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                id: '@',
                type: '@',
                placeholder: '@',
                readonly: '@',
                css: '@',
                customStyle: '@',
                datatype: '@', //int, number, string
                model: '='
            },
            template: '<input placeholder="{{placeholder}}" class="mcs-default-size-input mcs-margin-right-20 {{css}}" popover-trigger="mouseenter" ng-model="model" style="{{customStyle}}" parse-input-value/>',
            link: function ($scope, $elem, $attrs) {
                var dataType = ($scope.datatype || 'string').toLowerCase();
                if ($scope.id) {
                    $elem.attr('id', $scope.id);
                }
                if (dataType == 'int') {
                    $elem.bind('keyup afterpaste', function () {
                        mcs.util.limit($elem);
                    });
                } else if (dataType == 'number') {
                    $elem.bind('keyup afterpaste', function () {
                        mcs.util.number($elem);
                    });
                }
                $elem.attr('type', $scope.type || 'text');
                var readonly = mcs.util.bool($scope.readonly);
                if (readonly) {
                    //$elem.attr('readonly', 'readonly');
                    $elem.attr('disabled', 'disabled');
                }
                $scope.$watch('$parent.maxlength', function () {
                    if ($scope.$parent.maxlength) {
                        $elem.attr('maxlength', $scope.$parent.maxlength);
                    }
                });
                // 执行验证
                $scope.$watch('$parent.required', function () {
                    if ($scope.$parent.required) {
                        $elem.attr('required', 'required');
                    }
                    var events = $elem.data('events');
                    if (!events || !events['blur']) {
                        $elem.blur(function () {
                            validationService.validate($elem, $scope.$parent);
                        });
                    }
                });
            }
        };
    }]);
})();
(function () {
    'use strict';

    mcs.ng.directive('mcsLabel', function () {
        return {
            restrict: 'E',
            replace: true,
            scope: {
                required: '@',
                icons: '@',//square,caret-up
                text: '@',
                css: '@',
                prefix: '@?',
                suffix: '@?'
            },
            template: '<label class="control-label {{css}}"> {{value || text}}</label>',
            controller: function ($scope) {
                $scope.required = mcs.util.bool($scope.required || false);
                $scope.prefix = $scope.prefix || '';
                $scope.suffix = $scope.suffix || '：';
                $scope.value = '';
            },
            link: function ($scope, $elem, $attrs) {
                if ($scope.required) {
                    $elem.prepend('<span class="required">*</span>');
                }
                if ($scope.icons) {
                    var icons = mcs.util.toArray($scope.icons);
                    icons.reverse();
                    for (var i in icons) {
                        var icon = icons[i];
                        if (icon == 'caret-up') {
                            $elem.prepend('<i class="ace-icon fa fa-' + icon + ' red"/> ');
                        } else {
                            $elem.prepend('<i class="ace-icon fa fa-' + icon + ' red bigger-70"/> ');
                        }
                    }
                }
                if ($scope.css) {
                    $elem.addClass($scope.css);
                }
                if ($scope.prefix != undefined) {
                    $scope.value += $scope.prefix;
                }
                if ($scope.suffix != undefined) {
                    $scope.$watch('text', function (value) {
                        if (!value) return;
                        $scope.value += value.replace(':', $scope.suffix);
                        if (value.indexOf($scope.suffix) == -1) {
                            $scope.value += $scope.suffix;
                        }
                    });
                }
            }
        };
    });
})();
(
    function() {
        mcs.ng.service('printService', function() {
            var printService = this;

            function isIE() {
                if (!!window.ActiveXObject || "ActiveXObject" in window)
                    return true;
                else
                    return false;
            }



            printService.checkTargetPageLoaded = function(newwin, nopreview, content) {
                printService.timeoutObj = setTimeout(function() {
                    var container = newwin.document.getElementById('printDiv');
                    if (container) {
                        container.innerHTML = content.innerHTML;

                        if (nopreview) {
                            newwin.printWindow(nopreview);
                        }
                    } else {
                        clearTimeout(printService.timeoutObj);
                        printService.checkTargetPageLoaded();
                    }
                }, 100);
            };



            printService.print = function(nopreview) {



                var content = document.getElementById("printArea").cloneNode(true);


                var newwin = window.open(mcs.app.config.mcsComponentBaseUrl + '/src/mcs-print/mcs-print.html', '', '');


                if (mcs.browser.s.msie) {
                    printService.checkTargetPageLoaded(newwin, nopreview, content);
                    return;
                }


                newwin[newwin.addEventListener ? 'addEventListener' : 'attachEvent'](
                    (newwin.attachEvent ? 'on' : '') + 'load',
                    function() {
                        var container = newwin.document.getElementById('printDiv');
                        container.innerHTML = content.innerHTML;

                        if (nopreview) {
                            newwin.printWindow(nopreview);
                        }


                    }, false);

            };

            return printService;
        });
    }
)();

(function() {
    'use strict';

    mcs.ng.directive('mcsRadiobuttonGroup', ['mcsValidationService', function(validationService) {

        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '=',
                value: '='
            },

            template: '<label class="radio-inline" ng-repeat="item in data" ng-show="item.state==undefined||item.state"><span uib-popover="{{item.tooltip}}" placement="auto" popover-trigger="mouseenter"><input name="{{groupName}}" type="radio" class="ace" ng-checked="item.key==model" ng-disabled="disabled" ng-click="change(item, $event)"><span class="lbl"></span> {{item.value}}</span></label>',
            controller: function($scope) {
                $scope.groupName = mcs.util.newGuid();
                $scope.disabled = mcs.util.bool($scope.$parent.disabled);
                $scope.change = function(item, event) {
                    $scope.model = item.key;
                    $scope.value = item.value;
                    validationService.validate($(event.target), $scope);
                };
            },
            link: function($scope, $elem) {
                $elem.attr('groupName', $scope.groupName);
                $scope.$watch('data', function(value) {
                    if (value && $scope.$parent.required) {
                        $elem.find(':radio').attr('required', 'required');
                    }
                });
            }
        }
    }]);
})();

(function () {
    'use strict';

    mcs.ng.directive('mcsSelect', ['mcsValidationService', function (validationService) {
        return {
            restrict: 'E',
            scope: {
                category: '@',
                css: '@',
                caption: '@',
                filter: '@',
                prop: '@',
                model: '=?',
                data: '=?',
                value: '=?',
                selected: '=?',
                async: '@',
                callback: '&?',
                disabled: '=?',
                parseType: '@',
                showDefault: '@',
                isDynamic: '@', // 数据源是否是动态获取
                ignoreAsync: '@', // 始终加载数据（不管异步还是同步）
                defaultKey: '@',
                customStyle: '@?'
            },
            template: '<div class="col-sm-12 {{css}}"><select style="width:100%;" ng-disabled="disabled" ng-style="customStyle"></select></div>',
            link: function ($scope, $elem) {
                $scope.caption = $scope.caption || '请选择';
                $scope.disabled = mcs.util.bool($scope.disabled || false);
                $scope.showDefault = mcs.util.bool($scope.showDefault || true);
                $scope.isDynamic = mcs.util.bool($scope.isDynamic || false);
                $scope.ignoreAsync = mcs.util.bool($scope.ignoreAsync || false);
                $scope.async = mcs.util.bool($scope.async || true);
                $scope.parseType = $scope.parseType || 'string';
                $scope.defaultKey = $scope.defaultKey || '-1';

                var select = $elem.find('select');
                if (mcs.util.hasAttr($elem, 'required')) {
                    $scope.required = true;
                    select.attr('required', 'required');

                    mcs.util.appendMessage($elem, 'mcs-padding-left-15');
                }
                var prepareDataDict = function () {
                    var items = ppts.dict[ppts.config.dictMappingConfig[$scope.category]];

                    $scope.$watch('filter', function () {
                        if (mcs.util.bool($scope.isDynamic) && ($scope.filter == '' || $scope.filter == undefined)) {
                            $scope.data = $scope.data || [];
                        } else {
                            if ($scope.filter && $scope.prop) {
                                $scope.data = [];
                                for (var index in items) {
                                    var item = items[index];
                                    var prop = $scope.prop;
                                    if (mcs.util.containsElement(item[prop], $scope.filter)) {
                                        $scope.data.push(item);
                                    }
                                }
                            } else {
                                $scope.data = items;
                            }
                        }
                    });
                    $scope.$watch('data', function () {
                        select.empty();
                        if (mcs.util.bool($scope.showDefault)) {
                            select.append('<option value="">' + $scope.caption + '</option>');
                        }
                        $.each($scope.data, function (k, v) {
                            v.parentKey = v.parentKey || '';
                            // 过滤掉全部选项
                            if (v.key != $scope.defaultKey) {
                                var option = '<option value="' + v.key + '" parent="' + v.parentKey + '">' + v.value + '</option>';
                                if (v.key == $scope.model) {
                                    option = '<option value="' + v.key + '" parent="' + v.parentKey + '" selected="selected">' + v.value + '</option>';
                                }
                                select.append(option);
                            }
                        });

                        select.select2().change(function () {
                            // 返回已选择的数据
                            $scope.model = select.val();
                            $scope.value = !select.val() ? '' : select.select2('data').text;
                            $scope.selected = $scope.selected || {};
                            $scope.selected.key = select.val();
                            $scope.selected.value = select.select2('data').text == $scope.caption ? '' : select.select2('data').text;
                            $scope.selected.parentKey = $(select.select2('data').element).attr('parent');
                            // 注册回调事件
                            if ($scope.parseType == 'int') {
                                $scope.model = parseInt($scope.model);
                            }
                            if ($scope.parseType == 'float') {
                                $scope.model = parseFloat($scope.model);
                            }
                            if (angular.isFunction($scope.callback)) {
                                $.each($scope.data, function (k, v) {
                                    if (v.key == $scope.model) {
                                        $scope.selected.selectItem = v.selectItem;
                                        return false;
                                    }
                                });
                                $scope.callback({
                                    item: $scope.selected,
                                    model: $scope.model
                                });
                            }
                            // 执行验证
                            validationService.validate(select, $scope);
                            // 触发$digest
                            $scope.$apply('$scope.model');
                        });
                    });
                };

                // 默认加载当前选择的数据
                $scope.model = $scope.model == undefined ? select.val() : $scope.model;

                if ($scope.customStyle) {
                    select.attr('style', $scope.customStyle);
                }

                if (mcs.util.bool($scope.ignoreAsync)) {
                    prepareDataDict();
                    if ($scope.category) {
                        $scope.$on('dictionaryReady', prepareDataDict);
                    } else {
                        $scope.$on('dataReady', prepareDataDict);
                    }
                } else {
                    if ($scope.async) {
                        if ($scope.category) {
                            $scope.$on('dictionaryReady', prepareDataDict);
                        } else {
                            $scope.$on('dataReady', prepareDataDict);
                        }
                    } else {
                        prepareDataDict();
                    }
                }
            }
        }
    }]);
})();
/*
mcs.ng.directive('pptsTimepicker', function () {
    return {
        restrict: 'E',
        scope: {
            css: '@',
            step: '@',
            model: '=',
            zIndex: '@'
        },
        templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/timepicker.tpl.html',
        controller: function ($scope) {
            if ($scope.zIndex) {
                $scope.dateStyle = { 'z-index': $scope.zIndex };
            }
        },
        link: function ($scope, $elem) {
            var $this = $elem.find('.time-picker');

            $this.timepicker({
                minuteStep: $scope.step || 30,
                showSeconds: true,
                showMeridian: false
            }).next().on('click', function () {
                $(this).prev().focus();
            });
        }
    }
});
*/
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

(function function_name(argument) {
    'use strict';

    angular.module('mcs.ng.uiCopy', [])

    .controller('uiCopyController', ['$scope', function() {

    }])

    .directive('mcsUicopy', function($compile, $http) {

        return {
            restrict: 'E',
            scope: {
                data: '=',
                model: '=?',
                itemTemplateUrl: '@'
            },
            template: '<table style="width:100%" class="table-layout"><tr ng-repeat="row in data track by $index"><td class="mcs-padding-0"><div ng-include="itemTemplateUrl"></div></td></tr></table>',

            link: function($scope, iElm, iAttrs, controller) {


            }
        };
    });
})();

(
    function() {
        mcs.ng.service('excelImportService', function (mcsDialogService, storage) {
            var excelImportService = this;

            excelImportService.import = function(param, apiUrl) {
                var win = window.open('../src/tpl/mcs-excelImport.tpl.html', '_blank', "height=600, width=700, toolbar =no, menubar=no, scrollbars=no, resizable=no, location=no, status=no");
                win.onload = function () {
                    var currentJobId = storage.get('ppts.user.currentJobId_' + ppts.user.id);
                    win.setParam(angular.extend({ pptsCurrentJobID: currentJobId },param));
                    win.setApiUrl(apiUrl);
                };

            };

            excelImportService.importOnDialog = function(tpl, config) {
                mcsDialogService.create(tpl, config);
            };



            return excelImportService;
        });
    }
)();

(
    function() {
        angular.module('mcs.ng.filesUpload', ['ngFileUpload'])

        .directive('filesUpload', ['Upload', '$timeout', '$http', function(Upload, $timeout, $http) {



            return {
                restrict: 'A',
                templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/upload.tpl.html',
                scope: {
                    filesAmount: '=?',
                    pattern: '@?',
                    model: '=?',
                    url: '@',
                    errorMessage: '@',
                    downloadUrl: '@',
                    moduleName: '@',
                    resourceId: '@',
                    readonly: '=?'

                },
                link: function($scope, iElm, iAttrs, formCtrl) {

                    $scope.filesUpload = [];

                    $scope.statusEnum = {
                        inserted: 1,
                        updated: 2,
                        deleted: 3
                    };

                    $scope.fileStatusFilter = function(e) {
                        return e.status != $scope.statusEnum.deleted;
                    };



                    $scope.delecteFile = function(file) {
                        file.status = $scope.statusEnum.deleted;

                    };

                    $scope.downloadFile = function(file) {

                        mcs.util.postMockForm($scope.downloadUrl, file);


                    };

                    $scope.uploadFiles = function(files) {

                        angular.forEach(files, function(file) {

                            file.upload = Upload.upload({
                                url: $scope.url,
                                data: {
                                    materialUploadModel: JSON.stringify({
                                        materialClass: $scope.moduleName,
                                        resourceID: $scope.resourceId,
                                        originalName: file.name,
                                        title: file.title,
                                        status: file.status


                                    }),
                                    file: file
                                }
                            });

                            file.upload.then(function(response) {
                                $timeout(function() {
                                    mcs.util.removeByObject(files, file);
                                    response.data[0].status = $scope.statusEnum.inserted;

                                    $scope.model.push(response.data[0]);
                                });
                            }, function(response) {

                                $scope.errorMsg = '上传失败';


                            }, function(evt) {
                                file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                            });
                        });



                    };



                    $scope.fileSelect = function(files) {


                        files.forEach(function(file, index) {
                            file.status = $scope.statusEnum.inserted;
                            file.title = file.name;

                        });

                        $scope.uploadFiles(files);
                    };


                }
            };
        }]);


    }
)();

(function () {
    'use strict';

    mcs.ng.service('mcsUserTaskService', ['$resource', function ($resource) {
        var service = this;

        var resource = $resource(ppts.config.mcsApiBaseUrl + 'api/usertask/:operation/:id',
            { operation: '@operation', id: '@id' },
            {
                'post': { method: 'POST' },
                'polling': { method: 'POST', headers: { 'pollingTasks': true } },
                'query': { method: 'GET', isArray: false }
            });

        service.queryUserTasksAndCount = function (searchParams, success, error) {
            resource.polling({ operation: 'QueryUserTasksAndCount' }, JSON.stringify(searchParams), success, error);
        };

        service.queryUserTasks = function (searchParams, success, error) {
            if (!searchParams || !searchParams.dataType) return;
            var action = '';
            switch (searchParams.dataType) {
                case 'userTask':
                    action = 'QueryUserTasks';
                    break;
                case 'completedTask':
                    action = 'QueryUserCompletedTasks';
                    break;
                case 'notify':
                    action = 'QueryUserNotifies';
                    break;
            }
            resource.post({ operation: action }, JSON.stringify(searchParams), success, error);
        };
      
        return service;
    }]);
})();
(function() {
    'use strict';

    mcs.ng.service('mcsValidationService', ['mcsValidationRules', 'mcsValidationMessageConfig', '$parse', '$timeout', function(rules, config, $parse, $timeout) {
        var service = this;

        var checkValidationResult = function(elem, options) {
            var opts = $.extend({
                errorMessage: elem.attr('data-validation-custom-message') || config.general,
                validate: true
            }, options);

            var parent = elem.closest('.form-group');
            var validateRow = elem.closest('.row');
            if (!parent || !parent.length) {
                var message = elem.parent().find('.help-block');
                if (!message || !message.length) {
                    elem.parent().append('<div class="help-block"></div>');
                    message = elem.parent().find('.help-block');
                }
                if (opts.validate) {
                    elem.removeClass('has-error');
                    elem.parent().find('.control-label').removeClass('has-error');
                    message.removeClass('has-error').text('').css('visibility', 'hidden');
                } else {
                    elem.addClass('has-error');
                    elem.parent().find('.control-label').addClass('has-error');
                    message.addClass('has-error').text(opts.errorMessage).css('visibility', 'visible');
                }
            } else {
                var message = parent.find('.help-block');
                var validationItems = validateRow.find('.form-group');
                if (!message || !message.length) {
                    // 对于单行中只有一个验证项则附加水平消息框
                    if (validationItems.length == 1) {
                        parent.append('<div class="help-block horizontal"></div>');
                    } else {
                        elem.parent().append('<div class="help-block"></div>');
                    }
                    message = parent.find('.help-block');
                }

                if (opts.validate) {
                    parent.removeClass('validate has-error');
                    message.text('').css('visibility', 'hidden');
                } else {
                    parent.addClass('validate has-error');
                    message.text(opts.errorMessage).css('visibility', 'visible');
                }
            }

            // 设置已经过验证（如果启用提交时验证则不需要设置验证结果）
            if (!mcs.util.hasAttr(elem, 'submit-validate') || !mcs.util.hasAttr(elem, 'ignore-hidden')) {
                elem.attr('data-validate-result', opts.validate);
            }

            return opts.validate;
        };

        var filterValidationElems = function(form, callback) {
            // 查找页面中需要进行验证的元素
            var elems = !form ? $('input, select, textarea') : $('input, select, textarea', form);
            elems.each(function() {
                var $this = $(this);
                // 如果是隐藏域或文件输入或父级隐藏则不参与验证
                if ($this.is(':hidden') && !mcs.util.hasAttr($this, 'ignore-hidden') && !($this.is('select') && $this.next().hasClass('select2-container')) || $this.is(':file') || $this.closest('.form-group').is(':hidden') || !mcs.util.hasAttrs($this, rules)) {
                    return true;
                }
                //如果当前元素从未验证过则参与验证，否则将维持原状
                if (!mcs.util.hasAttr($this, 'data-validate-result')) {
                    if (typeof callback === 'function') {
                        callback($this);
                    };
                }
            });
        };

        service.check = function(elem, options) {
            return checkValidationResult(elem, options);
        };

        service.validate = function(elem, scope) {
            var validationResult = true;

            // 如不包含任何验证规则，则不参与验证
            if (!mcs.util.hasAttrs(elem, rules)) return validationResult;
            // 如果验证通过，则跳转到下一规则继续验证
            // required
            if (validationResult && mcs.util.hasAttr(elem, 'required')) {
                var cascading = elem.is('select') && elem.parent().siblings().not(':hidden').length > 0;
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-required-message') || function() {
                        if (elem.is(':checkbox') || elem.is(':radio')) {
                            return config.selected;
                        } else if (elem.is('select')) {
                            return cascading ? config.requiredAll : config.selected;
                        } else {
                            return config.required;
                        }
                    }(),
                    validate: function() {
                        if (elem.is(':checkbox')) {
                            return scope.model == undefined ? elem.closest('label').parent().find(':checkbox:checked').length > 0 : scope.model.length > 0;
                        } else if (elem.is(':radio')) {
                            return scope.model == undefined ? elem.closest('label').parent().find(':radio:checked').length > 0 : !!scope.model;
                        } else if (elem.is('select')) {
                            if (cascading) {
                                // 如果是级联框
                                var selected = true;
                                elem.parent().not(':hidden').find('select[required]').each(function() {
                                    var $this = $(this);
                                    if ($.trim($this.find('option:selected').val()).length == 0) {
                                        selected = false;
                                        return false; // 跳出循环
                                    }
                                });
                                return selected;
                            } else {
                                // 如果是单选框
                                return scope.model == undefined ? $.trim(elem.find('option:selected').val()).length > 0 : !!scope.model;
                            }
                        } else if (mcs.util.hasClasses(elem, 'date-picker data-range')) {
                            // 对日期范围，数据范围控件单独处理
                            var valid = true;
                            elem.each(function() {
                                var $this = $(this);
                                if ($.trim($this.val()).length == 0) {
                                    valid = false;
                                    return false; // 跳出循环
                                }
                            });
                            return valid;
                        } else {
                            return $.trim(elem.val()).length > 0;
                        }
                    }()
                });
            }
            // required-level(only for mcs-cascading-select)
            if (validationResult && mcs.util.hasAttr(elem, 'required-level')) {
                // 如果当前元素是级联控件才参与验证
                var parent = elem.closest('.mcs-cascading-select-container');
                if (parent) {
                    var requiredLevel = parseInt(parent.attr('required-level'));
                    if (requiredLevel > 0) {
                        validationResult = checkValidationResult(elem, {
                            errorMessage: elem.attr('data-validation-required-message') || mcs.util.format(config.requiredLevel, requiredLevel),
                            validate: function() {
                                var selected = true;
                                elem.parent().not(':hidden').find('select[required-level]').each(function(index) {
                                    var $this = $(this);
                                    if ($.trim($this.find('option:selected').val()).length == 0) {
                                        selected = false;
                                        return false; // 跳出循环
                                    }
                                });

                                return selected;
                            }()
                        });
                    }
                }
            }
            // 如果不参与required验证，则不需要再进行其他规则验证
            var skipRequiredValidation = validationResult && !mcs.util.hasAttrs(elem, 'required required-level') && $.trim(elem.val()).length == 0;
            // minlength
            if (validationResult && mcs.util.hasAttr(elem, 'minlength')) {
                var minlength = parseInt(elem.attr('minlength'));
                if (minlength > 0) {
                    validationResult = checkValidationResult(elem, {
                        errorMessage: elem.attr('data-validation-minlength-message') || mcs.util.format(config.minlength, minlength),
                        validate: skipRequiredValidation || $.trim(elem.val()).length >= minlength
                    });
                }
            }
            //maxlength
            if (validationResult && mcs.util.hasAttr(elem, 'maxlength')) {
                var maxlength = parseInt(elem.attr('maxlength'));
                if (maxlength > 0) {
                    validationResult = checkValidationResult(elem, {
                        errorMessage: elem.attr('data-validation-maxlength-message') || mcs.util.format(config.maxlength, maxlength),
                        validate: skipRequiredValidation || $.trim(elem.val()).length <= maxlength
                    });
                }
            }
            //min
            if (validationResult && mcs.util.hasAttr(elem, 'min')) {
                var min = parseFloat(elem.attr('min'));
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-min-message') || mcs.util.format(config.min, min),
                    validate: skipRequiredValidation || parseFloat($.trim(elem.val())) >= min
                });
            }
            //max
            if (validationResult && mcs.util.hasAttr(elem, 'max')) {
                var max = parseFloat(elem.attr('max'));
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-max-message') || mcs.util.format(config.max, max),
                    validate: skipRequiredValidation || parseFloat($.trim(elem.val())) <= max
                });
            }
            //positive
            if (validationResult && mcs.util.hasAttr(elem, 'positive')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-positive-message') || config.positive,
                    validate: skipRequiredValidation || ($.trim(elem.val()) == '0' || (/^[1-9]+[0-9]*$/).test($.trim(elem.val())))
                });
            }
            //negative
            if (validationResult && mcs.util.hasAttr(elem, 'negative')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-negative-message') || config.negative,
                    validate: skipRequiredValidation || ($.trim(elem.val()) == '0' || (/^-[1-9]+[0-9]*$/).test($.trim(elem.val())))
                });
            }
            //number
            if (validationResult && mcs.util.hasAttr(elem, 'number')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-number-message') || config.number,
                    validate: skipRequiredValidation || (/^(([1-9]\d*)|0)(\.\d+)?$/).test($.trim(elem.val()))
                });
            }
            //currency
            if (validationResult && mcs.util.hasAttr(elem, 'currency')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-currency-message') || config.currency,
                    validate: skipRequiredValidation || (/^(([1-9]\d*)|0)(\.\d{1,2})?$/).test($.trim(elem.val()))
                });
            }
            //less
            if (validationResult && mcs.util.hasAttr(elem, 'less')) {
                var less = parseFloat(elem.attr('less'));
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-less-message') || mcs.util.format(config.less, less),
                    validate: skipRequiredValidation || parseFloat($.trim(elem.val())) > less
                });
            }
            //great
            if (validationResult && mcs.util.hasAttr(elem, 'great')) {
                var great = parseFloat(elem.attr('great'));
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-great-message') || mcs.util.format(config.great, great),
                    validate: skipRequiredValidation || parseFloat($.trim(elem.val())) < great
                });
            }
            //between
            if (validationResult && mcs.util.hasAttr(elem, 'between')) {
                var between = elem.attr('between').split(',');
                if (between.length == 2) {
                    var min = parseFloat(between[0]);
                    var max = parseFloat(between[1]);
                    if (!isNaN(min) && !isNaN(max)) {
                        validationResult = checkValidationResult(elem, {
                            errorMessage: elem.attr('data-validation-between-message') || mcs.util.format(config.between, min, max),
                            validate: skipRequiredValidation || (parseFloat($.trim(elem.val())) >= min && parseFloat($.trim(elem.val())) <= max)
                        });
                    }
                }
            }
            //phone
            if (validationResult && mcs.util.hasAttr(elem, 'phone')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-phone-message') || config.phone,
                    validate: skipRequiredValidation || (/(^1[34578]\d{9}$)|(^\d{3,4}-\d{7,8}-\d{1,5}$)|(^\d{3,4}-\d{7,8}$)/).test($.trim(elem.val()))
                });
            }
            //email
            if (validationResult && mcs.util.hasAttr(elem, 'email')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-email-message') || config.email,
                    validate: skipRequiredValidation || (/^[0-9a-z][_.0-9a-z-]{0,31}@([0-9a-z][0-9a-z-]{0,30}[0-9a-z]\.){1,4}[a-z]{2,4}$/).test($.trim(elem.val()))
                });
            }
            //idcard
            if (validationResult && mcs.util.hasAttr(elem, 'idcard')) {
                validationResult = checkValidationResult(elem, {
                    errorMessage: elem.attr('data-validation-idcard-message') || config.idcard,
                    validate: skipRequiredValidation || (/^(\d{18,18}|\d{15,15}|\d{17,17}x|\d{17,17}X)$/).test($.trim(elem.val()))
                });
            }
            //validate
            if (validationResult && mcs.util.hasAttr(elem, 'validate')) {
                $timeout(function() {
                    return skipRequiredValidation || scope.$eval(elem.attr('validate'));
                }, 200, true);
            }

            return validationResult;
        };

        service.init = function(scope, form) {
            // 查找页面中需要进行验证的元素
            scope.$on('$viewContentLoaded', function(event) {
                filterValidationElems(form, function(elem) {
                    if (elem.is(':input[type=text]') || elem.is('textarea')) {
                        elem.blur(function() {
                            service.validate(elem, scope);
                        });
                    }
                });
            });
        };
        // 提交整个页面时开始触发
        service.run = function(scope, form) {
            // 查找页面中需要进行验证的元素
            filterValidationElems(form, function(elem) {
                if ((elem.is(':input[type=text]') || elem.is('textarea')) && !mcs.util.hasClasses(elem, 'date-picker date-timepicker data-range')) {
                    elem.blur();
                } else {
                    service.validate(elem, scope);
                }
            });
            return !$('.has-error').length;
        };

        return service;
    }]).constant('mcsValidationRules', [
        'required',
        'required-level', // 用于指定级联下拉规则
        'minlength',
        'maxlength',
        'min',
        'max',
        'positive',
        'negative',
        'number',
        'currency',
        'great',
        'less',
        'between',
        'phone',
        'email',
        'idcard',
        'validate'
    ]).constant('mcsValidationMessageConfig', {
        general: '输入数据项不正确，请重新输入!',
        required: '输入数据项为必填！',
        requiredLevel: '请选择到第{0}级数据！',
        requiredAll: '请选择所有级别数据！',
        selected: '请至少选择一项！',
        minlength: '应至少输入{0}个字符！',
        maxlength: '最多只能输入{0}个字符！',
        min: '输入数据项应大于或等于{0}！',
        max: '输入数据项应小于或等于{0}！',
        positive: '输入数据项应为正整数！',
        negative: '输入数据项应为负整数！',
        number: '输入数据项应为小数！',
        currency: '输入数据项应为货币值(最多保留两位小数)！',
        great: '输入数据项应小于{0}！',
        less: '输入数据项应大于{0}！',
        between: '输入数据项应在{0}和{1}之间！',
        phone: '输入数据项不是合法电话！',
        email: '输入数据项不是合法电子邮件！',
        idcard: '输入数据项不是合法身份证号！',
        custom: '输入数据项不合法！'
    });
})();

(function () {
    'use strict';

    mcs.ng.directive('mcsWorkflowStep', function () {
        return {
            restrict: 'E',
            scope: {
                model: '='
            },
            controller: function ($scope) {
                $scope.$watch('model', function (value) {
                    if (!value || !mcs.util.isArray(value.activities)) return;
                    var activities = value.activities;
                    var index = mcs.util.indexOf(activities, function (item) {
                        return item.isActive;
                    });

                    for (var i in activities) {
                        var item = activities[i];
                        item.isComplete = i < index ? true : false;
                    }
                });
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-workflow-step.tpl.html'
        }
    });

    mcs.ng.directive('mcsWorkflowHistory', ['$stateParams', 'mcsWorkflowService', function ($stateParams, mcsWorkflowService) {
        return {
            restrict: 'E',
            scope: {
                model: '='
            },
            controller: function ($scope) {
                $scope.data = {
                    keyFields: ['activityID'],
                    headers: [{
                        field: "activityName",
                        name: "审批岗位"
                    }, {
                        field: "approver",
                        name: "审批人"
                    }, {
                        field: "action",
                        name: "审批结果"
                    }, {
                        field: "comment",
                        name: "意见",
                        template: '<span uib-popover="{{row.comment|tooltip:20}}" popover-trigger="mouseenter">{{row.comment | truncate:20}}</span>'
                    }, {
                        field: "startTime",
                        name: "审批时间",
                        template: '<span>{{row.approvalTime | date: "yyyy-MM-dd HH:mm" | normalize}}</span>'
                    }],
                    pagable: false,
                    orderBy: [{
                        dataField: 'approvalTime',
                        sortDirection: 1
                    }],
                };

                $scope.params = {
                    processID: $stateParams.pid,
                    activityID: $stateParams.aid,
                    resourceID: $stateParams.rid
                };

                $scope.$watch('model', function (value) {
                    if (!value) return;
                    var activities = value.activityHistories && value.activityHistories.length ? value.activityHistories : value.activities;
                    if (!mcs.util.isArray(activities)) return;

                    $scope.data.rows = activities.filter(function (item) {
                        return item.activityStatus != 'Completed' || item.activityStatus != 'Aborted';
                    });
                });
            },
            template: '<mcs-datatable data="data" />'
        }
    }]);

    mcs.ng.directive('mcsWorkflowComment', function () {
        return {
            restrict: 'E',
            scope: {
                model: '='
            },
            template: '<textarea ng-model="model.currentOpinion.content" rows="7" class="mcs-width-full"></textarea>'
        };
    });

    mcs.ng.directive('mcsWorkflowToolbar', function () {
        return {
            restrict: 'E',
            scope: {
                model: '='
            },
            controller: function ($scope) {
                $scope.switch = $scope.$parent.switch;
                $scope.callbacks = $scope.$parent.callbacks;
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-workflow-toolbar.tpl.html'
        };
    });

    mcs.ng.directive('mcsWorkflow', ['$state', 'mcsWorkflowService', function ($state, mcsWorkflowService) {
        return {
            restrict: 'E',
            scope: {
                model: '=',
                callbacks: '=?'
            },
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-workflow.tpl.html',
            controller: function ($scope) {
                $scope.callbacks = $scope.callbacks || {};
                $scope.$watch('model', function (value) {
                    if (value) {
                        $scope.switch = value.uiSwitches;

                        var params = {
                            processID: $scope.model.processID,
                            resourceID: $scope.model.resourceID
                        };
                        /*流转*/
                        $scope.moveTo = function () {
                            params.currentOpinion = $scope.model.currentOpinion;
                            if (angular.isFunction($scope.callbacks.moveTo)) {
                                mcsWorkflowService.moveto(params, function (result) {
                                    $scope.model = result;
                                    $scope.callbacks.moveTo()();
                                });
                            } else {
                                mcsWorkflowService.moveto(params, function (result) {
                                    $scope.model = result;
                                    //mcsWorkflowService.goBack();
                                });
                            }
                        };
                        /*驳回*/
                        $scope.cancel = function () {
                            params.currentOpinion = $scope.model.currentOpinion;
                            if (angular.isFunction($scope.callbacks.cancel)) {
                                mcsWorkflowService.cancel(params, function (result) {
                                    $scope.model = result;
                                    $scope.callbacks.cancel()();
                                });
                            } else {
                                mcsWorkflowService.cancel(params, function (result) {
                                    $scope.model = result;
                                    //mcsWorkflowService.goBack();
                                });
                            }
                        };
                        /*保存*/
                        $scope.save = function () {
                            params.currentOpinion = $scope.model.currentOpinion;
                            if (angular.isFunction($scope.callbacks.save)) {
                                mcsWorkflowService.save(params, function (result) {
                                    $scope.model = result;
                                    $scope.callbacks.save()();
                                });
                            } else {
                                mcsWorkflowService.save(params, function (result) {
                                    $scope.model = result;
                                    $state.reload();
                                });
                            }
                        };
                        /*撤回*/
                        $scope.withdraw = function () {
                            if (angular.isFunction($scope.callbacks.withdraw)) {
                                mcsWorkflowService.withdraw(params, function (result) {
                                    $scope.model = result;
                                    $scope.callbacks.withdraw()();
                                });
                            } else {
                                mcsWorkflowService.withdraw(params, function (result) {
                                    $scope.model = result;
                                    $state.reload();
                                });
                            }
                        };
                    }
                });
            }
        }
    }]);

})();
(function() {
    'use strict';

    mcs.ng.service('mcsWorkflowService', ['$resource', function($resource) {
        var service = this;

        var resource = $resource(ppts.config.workflowApiBaseUrl + 'api/workflow/:operation/:id', {
            operation: '@operation',
            id: '@id'
        }, {
            'post': {
                method: 'POST'
            },
            'polling': {
                method: 'POST',
                headers: {
                    'pollingTasks': true
                }
            },
            'query': {
                method: 'GET',
                isArray: false
            }
        });

        service.goBack = function() {
            history.back();
        };

        service.startup = function(startupParames, success, error) {
            resource.post({
                operation: 'Startup'
            }, JSON.stringify(startupParames), success, error);
        };
        //service.queryUsertask = function (searchParams, success, error) {
        //    resource.post({ operation: 'QueryUsertask' }, JSON.stringify(searchParams), success, error);
        //};
        service.startupFreeSteps = function (startupFreeStepsParams, success, error) {
            resource.post({
                operation: 'StartupFreeSteps'
            }, JSON.stringify(startupFreeStepsParams), success, error);
        };
        service.getClientProcess = function(searchParams, success, error) {
            resource.post({
                operation: 'GetClientProcess'
            }, JSON.stringify(searchParams), success, error);
        };
        service.moveto = function(movetoParames, success, error) {
            resource.post({
                operation: 'Moveto'
            }, JSON.stringify(movetoParames), success, error);
        };
        service.cancel = function(cancelParames, success, error) {
            resource.post({
                operation: 'Cancel'
            }, JSON.stringify(cancelParames), success, error);
        };
        service.save = function (saveParames, success, error) {
            resource.post({
                operation: 'Save'
            }, JSON.stringify(saveParames), success, error);
        };
        service.withdraw = function (withdrawParames, success, error) {
            resource.post({
                operation: 'Withdraw'
            }, JSON.stringify(withdrawParames), success, error);
        };
        service.getForm = function (searchParams, success, error) {
            resource.post({
                operation: 'GetForm'
            }, JSON.stringify(searchParams), success, error);
        };
        return service;
    }]);
})();
