(function() {
    'use strict';

    /*
     * 两个对象判等
     */
    mcs.util.isObjectsEqual = function(a, b) {
        var aProps = Object.getOwnPropertyNames(a);
        var bProps = Object.getOwnPropertyNames(b);
        if (aProps.length != bProps.length) {
            return false;
        }
        for (var i = 0; i < aProps.length; i++) {
            var propName = aProps[i];
            if (a[propName] !== b[propName]) {
                return false;
            }
        }
        return true;
    };

    mcs.util.postMockForm = function(URL, PARAMS) {
        var currentJobId = PARAMS.pptsCurrentJobID;
        delete PARAMS.pptsCurrentJobID;
        var temp_form = document.createElement("form");
        temp_form.action = URL;
        temp_form.target = "_blank";
        temp_form.method = "post";
        temp_form.style.display = "none";
        var opt = document.createElement("textarea");
        opt.name = 'form';
        opt.value = JSON.stringify(PARAMS);
        var currentJobEle = document.createElement('textarea');
        currentJobEle.name = 'pptsCurrentJobID';
        currentJobEle.value = currentJobId;
        temp_form.appendChild(opt);
        temp_form.appendChild(currentJobEle);


        document.body.appendChild(temp_form);
        temp_form.submit();

        document.body.removeChild(temp_form);
    }

    /*
     * 删除数组中指定元素
     */
    mcs.util.removeByValue = function(_array, val) {
        for (var i = 0; i < _array.length; i++) {
            if (this[i] == val) {
                _array.splice(i, 1);
                break;
            }
        }
    };

    /*
     * 删除对象集合中具有指定特征的对象    
     */

    mcs.util.removeByObjectWithKeys = function(_array, obj) {
        var props = Object.getOwnPropertyNames(obj);
        var propsAmount = props.length;

        for (var i = _array.length - 1; i >= 0; i--) {
            var counter = 0;

            for (var j = 0; j < propsAmount; j++) {
                if (_array[i].hasOwnProperty(props[j]) && _array[i][props[j]] == obj[props[j]]) {
                    counter = counter + 1;
                }
            }



            if (counter == propsAmount) {
                _array.splice(i, 1);
            }

        }
    }


    /*
     * 删除对象集合中具有指定特征的对象集
     */
    mcs.util.removeByObjectsWithKeys = function(_array, targetArray) {
        for (var i = targetArray.length - 1; i >= 0; i--) {
            mcs.util.removeByObjectWithKeys(_array, targetArray[i]);
        }
    }


    /**
     * 从对象集合中删除指定对象
     *
     */
    mcs.util.removeByObject = function(_array, obj) {
        for (var i = 0; i < _array.length; i++) {
            if (mcs.util.isObjectsEqual(_array[i], obj)) {
                _array.splice(i, 1);
                break;
            }
        }
    };

    /*
     * JS 产生一个新的GUID随机数
     */
    mcs.util.newGuid = function() {
        var guid = "";
        for (var i = 1; i <= 32; i++) {
            var n = Math.floor(Math.random() * 16.0).toString(16);
            guid += n;
            if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                guid += "-";
        }
        return guid;
    };

    /*
     * 格式化字符串
     */
    mcs.util.format = function(str, args) {
        var result = str;
        if (arguments.length > 0) {
            if (arguments.length == 2 && typeof(args) == "object") {
                for (var key in args) {
                    if (args[key] != undefined) {
                        var reg = new RegExp("({" + key + "})", "g");
                        result = result.replace(reg, args[key]);
                    }
                }
            } else {
                for (var i = 1; i < arguments.length; i++) {
                    if (arguments[i] != undefined) {
                        //var reg = new RegExp("({[" + i + "]})", "g");//这个在索引大于9时会有问题
                        var reg = new RegExp("({)" + (i - 1) + "(})", "g");
                        result = result.replace(reg, arguments[i]);
                    }
                }
            }
        }
        return result;
    };

    /*
     * 判断是否为字符串
     */
    mcs.util.isString = function(value) {
        return typeof value === 'string';
    };

    /*
     * 判断是否为数组
     */
    mcs.util.isArray = function(value) {
        return value instanceof Array && value.constructor == Array;
    };

    /*
     * 判断是否为对象
     */
    mcs.util.isObject = function(value) {
        return value instanceof Object && value.constructor == Object;
    };

    /*
     * 判断是否为身份证
     */
    mcs.util.isIdCard = function(value) {
        return (/^(\d{18,18}|\d{15,15}|\d{17,17}x|\d{17,17}X)$/).test($.trim(value));
    };

    /*
     * 判断是否为日期
     */
    mcs.util.isDate = function(value) {
        var date = value;
        var result = date.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);

        if (result == null) return false;
        var d = new Date(result[1], result[3] - 1, result[4]);
        return (d.getFullYear() == result[1] && (d.getMonth() + 1) == result[3] && d.getDate() == result[4]);
    };

    /*
     * 字典对象合并
     */
    mcs.util.merge = function(dictionary) {
        for (var item in dictionary) {
            var prop = item;
            item = item.toLowerCase().indexOf('c_code_abbr_') == 0 ? item : 'c_codE_ABBR_' + item;
            mcs.app.dict[item] = dictionary[prop];
        }
    };

    /*
     * 字典对象转化
     */
    mcs.util.convert = function(dictionary) {
        var items = {};
        for (var index in dictionary) {
            var item = dictionary[index];
            items[item.parentKey] = items[item.parentKey] || {};
            items[item.parentKey][item.key] = item.value;
        }
        return items;
    };

    /*
     * 对象列表映射成字典
     * data 对象数组, kvp 键值对{key,value},category 所属类别
     */
    mcs.util.mapping = function(data, kvp, category) {
        if (!data || !kvp.key || !kvp.value) return;
        var getItems = function() {
            var items = [];
            for (var i in data) {
                if (isNaN(parseInt(i))) continue;
                var key = data[i][kvp.key],
                    value = data[i][kvp.value];
                if (key == undefined || value == undefined) continue;
                var item = {
                    key: key,
                    value: value
                };
                if (kvp.props) {
                    var props = mcs.util.toArray(kvp.props);
                    for (var j in props) {
                        var prop = props[j];
                        item[prop] = data[i][prop];
                    }
                }
                items.push(item);
            }
            return items;
        };
        if (category == undefined) {
            return getItems();
        } else {
            category = category.indexOf('c_codE_ABBR_') == 0 ? category : 'c_codE_ABBR_' + category;
            var result = {};
            result[category] = getItems();
            return result;
        }
    };

    /*
     * 限制文本框只能输入整数
     */
    mcs.util.limit = function(input) {
        if (input instanceof jQuery) {
            input = input[0];
        }
        if (input.value.length == 1) {
            input.value = input.value.replace(/[^0-9]/g, '');
        } else {
            input.value = input.value.replace(/\D/g, '');
        }
    };

    /*
     * 检测只能输入小数
     */
    mcs.util.number = function(input) {
        if (input instanceof jQuery) {
            input = input[0];
        }

        if (input.value != '') {
            input.value = input.value.replace(/[^\d.]/g, ''); //清除“数字”和“.”以外的字符
            input.value = input.value.replace(/^\./g, ''); //验证第一个字符是数字而不是.
            input.value = input.value.replace(/^0{2,}\./g, '0.'); //只保留小数点前第一个0. 清除多余的0
            input.value = input.value.replace(/\.{2,}/g, '.'); //只保留第一个. 清除多余的.
            input.value = input.value.replace('.', '$#$').replace(/\./g, '').replace('$#$', '.');
        }
    };

    /*
     * 文本框禁止粘贴
     */
    mcs.util.disablePaste = function(evt) {
        if (!window.event) {
            var keycode = evt.keyCode;
            var key = String.fromCharCode(keycode).toLowerCase();
            if (evt.ctrlKey && key == "v") {
                evt.preventDefault();
                evt.stopPropagation();
            }
        }
        return false;
    };

    /*
     * 从指定的数组集合中找到字符串或数组子集合中是否存在
     */
    mcs.util.contains = function(data, elems, separator) {
        if (!data || !elems) return false;
        var array = mcs.util.toArray(elems, separator);
        for (var i in array) {
            if (jQuery.inArray(array[i], data) > -1) {
                return true;
            }
        }

        return false;
    };

    /*
     * 判断对象数组中是否包含指定属性的对象
     */
    mcs.util.containsObject = function(data, elem, prop) {
        if (!data || !data.length || !elem || !elem[prop]) return false;
        for (var index in data) {
            if (data[index][prop] == elem[prop]) {
                return true;
            }
        }
        return false;
    };

    /*
     * 判断对象数组中是否包含指定属性的对象
     */
    mcs.util.containsElement = function(data, elem) {
        return mcs.util.toArray(data).indexOf(elem) > -1;
    };

    /*
     * 将指定元素转化为数组
     */
    mcs.util.toArray = function(data, separator) {
        var result = [];
        if (typeof data == "number") {
            data = data + '';
        }
        if (typeof data == 'string') {
            separator = separator || ',';
            var array = data.split(separator);
            result = array.map(function(item) {
                return (item + '').trim();
            });
        }
        if (data instanceof Array) {
            result = data;
        }
        return result;
    };

    /*
     * 判断元素是否存在属性
     */
    mcs.util.hasAttr = function(elem, attrName) {
        return typeof elem.attr(attrName) != 'undefined';
    };

    /*
     * 根据元素class获取
     */
    mcs.util.getElemsByClass = function(className, tagName) {
        var elems = [],
            all = document.getElementsByTagName(tagName || "*");
        for (var i = 0; i < all.length; i++) {
            if (all[i].className.className.match(new RegExp('(\\s|^)' + cls + '(\\s|$)'))) {
                elems[elems.length] = all[i];
            }
        }
        return elems;
    };

    /*
     * 判断元素是否存在属性
     */
    mcs.util.hasAttrs = function(elem, attrNames) {
        var attrs = mcs.util.toArray(attrNames);
        for (var index in attrs) {
            if (mcs.util.hasAttr(elem, attrs[index])) {
                return true;
            }
        }
        return false;
    };

    /*
     * 判断元素是否存在Class，以空格分隔
     */
    mcs.util.hasClasses = function(elem, classNames) {
        var names = mcs.util.toArray(classNames, ' ');
        for (var index in names) {
            if (elem.hasClass(names[index])) {
                return true;
            }
        }
        return false;
    };

    /*
     * 将字符串转化为bool类型, isIgnoreZero为解决单选框存在有0的选项
     */
    mcs.util.bool = function(str, isIgnoreZero) {
        isIgnoreZero = isIgnoreZero || false;
        if (typeof str === 'boolean') return str;
        str += '';
        if (isIgnoreZero && str == '0') return true;
        if (!str || !str.length) return false;
        str = str.toLowerCase();
        if (str === 'false' || str === '0' || str === 'undefined' || str === 'null') return false;
        return true;
    };

    /*
     * 对象复制
     */
    mcs.util.clone = function(obj) {
        if (typeof(obj) != 'object') return obj;
        if (obj == null) return obj;
        var newObject = new Object();
        for (var i in obj)
            newObject[i] = mcs.util.clone(obj[i]);
        return newObject;
    };

    /*
     * 从对象数组中查找满足某种条件/等于某种属性值对应的索引
     */
    mcs.util.indexOf = function(data, action, value) {
        if (!data || !data.length) return -1;
        if (typeof(action) == 'function') {
            for (var index in data) {
                if (action(data[index])) return index;
            }
        } else {
            var key = action;
            for (var index in data) {
                if (!data[index][key]) return -1;
                if (data[index][key] == value) {
                    return index;
                }
            }
        }

        return -1;
    };

    /*
     * 全部选中
     */
    mcs.util.selectAll = function(data) {
        var selectedResult = [];
        angular.forEach(data, function(item) {
            selectedResult.push(item.key);
        });
        return selectedResult;
    };

    /*
     * 全部不选中
     */
    mcs.util.unSelectAll = function() {
        return [];
    };

    /*
     * 反选
     */
    mcs.util.inverseSelect = function(data, selectedResult) {
        var temp = selectedResult;
        selectedResult = [];
        angular.forEach(data, function(item) {
            if (temp.indexOf(item.key) == -1) {
                selectedResult.push(item.key);
            }
        });
        return selectedResult;
    };

    /*
     * 构建级联数据源
     * data 原数据源{key,value,parentkey},
     * result 构建后的新数据源{key:{value,children:[]}}
     */
    mcs.util.buildCascadeDataSource = function(data, result) {
        for (var index in data) {
            var source = data[index];
            if (source.parentKey == 0) {
                var parent = result[source.key];
                if (!parent) {
                    result[source.key] = {
                        value: source.value,
                        children: []
                    };
                } else {
                    parent.value = source.value;
                }
            } else {
                var parent = result[source.parentKey];
                if (!parent) {
                    result[source.parentKey] = {
                        value: '',
                        children: [source]
                    };
                } else {
                    result[source.parentKey].children.push(source);
                }
            }
        }
    };

    /*
     * 设置当前的操作项(checkbox)
     */
    mcs.util.setSelectedItems = function(selected, item, event, length, defaultKey) {
        var index = selected.indexOf(item.key);
        if (event.target.checked) {
            if (index === -1) {
                selected.push(item.key);
            }
            if (selected.length == length - 1 && selected.indexOf(defaultKey) == -1) {
                selected.push(defaultKey);
            }
        } else {
            if (index !== -1) {
                selected.splice(index, 1);
                if (selected.indexOf(defaultKey) > -1) {
                    selected.splice(selected.indexOf(defaultKey), 1);
                }
            }
        }
    };

    /*
     * 设置默认选中
     */
    mcs.util.setDefaultSelected = function(items, key) {
        if (!items || !items.length) return;
        for (var i = 0, len = items.length; i < len; i++) {
            var item = items[i];
            item.checked = item.key == key;
        }
    };

    /*
     * 获取字典项的值
     */
    mcs.util.getDictionaryItemValue = function(items, key) {
        if (key == undefined) return '';
        if (!items || !items.length) return key;
        for (var i = 0, len = items.length; i < len; i++) {
            var item = items[i];
            if (item.key == key) {
                return item.value;
            }
        }
        return key;
    };

    mcs.util.loadDependencies = function(dependencies) {
        return {
            resolver: ['$q', '$rootScope', function($q, $rootScope) {
                var defered = $q.defer();

                require(dependencies, function() {
                    $rootScope.$apply(function() {
                        defered.resolve();
                    });
                });

                return defered.promise;
            }]
        };
    };

    /*
     * 配置面包屑
     */
    mcs.util.configBreadcrumb = function($breadcrumbProvider, templateUrl) {
        $breadcrumbProvider.setOptions({
            templateUrl: templateUrl
        });
    };

    /*
     * 获取URL中的Querystring参数
     */
    mcs.util.params = function(name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) return unescape(r[2]);
        return null;
    };

    /*
     * 附加错误消息
     */
    mcs.util.appendMessage = function(elem, css) {
        css = css || '';
        var validationItem = elem.closest('.form-group').length ? elem.closest('.form-group') : elem.closest('td');
        var message = validationItem.find('.help-block');
        var validateRow = elem.closest('.row');
        var validationItems = validateRow.find('.form-group');
        if (!message || !message.length) {
            // 对于单行中只有一个验证项则附加水平消息框
            if (validationItems.length == 1) {
                validationItem.append('<div class="help-block horizontal"></div>');
            } else {
                elem.parent().append('<div class="help-block ' + css + '"></div>');
            }
        }
    };

    /*
     * 加载单独路由
     * $stateProvider, 路由提供者服务
     * route, 当前需要加载的路由
     */
    mcs.util.loadRoute = function($stateProvider, route) {
        var parentState = null,
            breadcrumb = route.breadcrumb;
        if (breadcrumb) {
            parentState = breadcrumb;
            if (!route.abstract && !breadcrumb.parent) {
                parentState.parent = function($lastViewScope) {
                    return $lastViewScope.$state.params.prev;
                }
            }
        }

        $stateProvider.state(route.name, {
            url: route.url,
            abstract: route.abstract || false,
            templateUrl: route.templateUrl,
            controller: route.controller,
            controllerAs: route.controllerAs || 'vm',
            ncyBreadcrumb: parentState,
            resolve: mcs.util.loadDependencies(route.dependencies)
        });
        return mcs.util;
    };

    /*
     * 加载默认的路由
     * $stateProvider, 路由提供者服务
     * $urlRouterProvider, URL路由提供者服务
     * defaultRoute, 默认路由(配置首次导航的页面或路由无法找到时进入的页面,包含url,templateUrl,controller,dependencies)
     */
    mcs.util.loadDefaultRoute = function($stateProvider, $urlRouterProvider, defaultRoute) {
        if (!defaultRoute || !defaultRoute.name || !defaultRoute.url || !defaultRoute.templateUrl) {
            console.log('no default route settings or default route has no correct configuration, including name, url and templateUrl.');
            return;
        };
        // 加载默认路由
        var defaultRedirectUrl = !defaultRoute.layout ? defaultRoute.url : defaultRoute.layout.url + defaultRoute.url;
        $urlRouterProvider.otherwise(defaultRedirectUrl);
        // 如果设置布局页则首先加载布局页
        if (defaultRoute.layout) {
            if (!defaultRoute.layout.name || !defaultRoute.layout.url || !defaultRoute.layout.templateUrl) {
                console.log('default route layout has no correct configuration, including name, url and templateUrl.');
                return;
            };
            $stateProvider.state(defaultRoute.layout.name, {
                abstract: true,
                url: defaultRoute.layout.url,
                templateUrl: defaultRoute.layout.templateUrl
            });
        }

        $stateProvider.state(defaultRoute.name, {
            url: defaultRoute.url,
            templateUrl: defaultRoute.templateUrl,
            controller: defaultRoute.controller,
            controllerAs: defaultRoute.controllerAs || 'vm',
            ncyBreadcrumb: defaultRoute.breadcrumb,
            resolve: mcs.util.loadDependencies(defaultRoute.dependencies)
        });
        /*
        var routeConfigPaths = mcs.app.config.routeConfigPaths;
        $.each(routeConfigPaths, function (index) {
            require([routeConfigPaths[index]], function (states) {
                if (states != undefined) {
                    $.each(states, function (route, state) {
                        $stateProvider.state(route, {
                            url: state.url,
                            templateUrl: state.templateUrl,
                            controller: state.controller,
                            controllerAs: 'vm',
                            resolve: mcs.util.loadDependencies(state.dependencies)
                        });
                    });
                }
            });
        });*/
    };

    /**
     * 配置模块的Provider, 可配置全局模块, 也可以单独配置
     */
    mcs.util.configProvider = function(ngModule, $controllerProvider, $compileProvider, $filterProvider, $provide) {
        if (!ngModule || !angular.isDefined(ngModule)) return;

        ngModule.registerController = $controllerProvider.register;
        ngModule.registerDirective = $compileProvider.directive;
        ngModule.registerFilter = $filterProvider.register;
        ngModule.registerFactory = $provide.factory;
        ngModule.registerService = $provide.service;
        ngModule.registerConstant = $provide.constant;
        ngModule.registerValue = $provide.value;
    };

    /*
     * 配置应用程序的缓存模板
     */
    mcs.util.configCacheTemplate = function($templateCache, key, content) {
        var template = $templateCache.get(key);
        if (!template) {
            $templateCache.put(key, content);
        }
    };

    /**
     * 配置应用的拦截器以及设置白名单
     */
    mcs.util.configInterceptor = function($httpProvider, $sceDelegateProvider, interceptors) {
        $httpProvider.defaults.transformResponse.unshift(function(data, headers) {
            if (mcs.util.isString(data)) {
                var JSON_PROTECTION_PREFIX = /^\)\]\}',?\n/;
                var APPLICATION_JSON = 'application/json';
                var JSON_START = /^\[|^\{(?!\{)/;
                var JSON_ENDS = {
                    '[': /]$/,
                    '{': /}$/
                };
                // Strip json vulnerability protection prefix and trim whitespace
                var tempData = data.replace(JSON_PROTECTION_PREFIX, '').trim();

                if (tempData) {
                    var contentType = headers('Content-Type');
                    var jsonStart = tempData.match(JSON_START);
                    if ((contentType && (contentType.indexOf(APPLICATION_JSON) === 0)) || jsonStart && JSON_ENDS[jsonStart[0]].test(tempData)) {
                        data = (new Function("", "return " + tempData))();
                    }
                }
            }

            return data;
        });

        if (interceptors) {
            for (var interceptor in interceptors) {
                $httpProvider.interceptors.push(interceptors[interceptor]);
            }
        }

        $sceDelegateProvider.resourceUrlWhitelist([
            // Allow same origin resource loads.
            'self',
            // Allow loading from our assets domain.  Notice the difference between * and **.
            //'http://10.1.56.80/mcsweb**'
            mcs.app.config.mcsComponentBaseUrl + '**'
        ]);
    };

    return mcs.util;
})();
