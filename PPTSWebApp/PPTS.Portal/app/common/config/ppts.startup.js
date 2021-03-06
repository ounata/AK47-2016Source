// 应用程序的启动文件, 整个系统唯一的全局变量, 此文件主要管理命名空间
var mcs = mcs || {};
(function () {
    'use strict';

    mcs.g = mcs.g || {};
    mcs.util = mcs.util || {};
    mcs.date = mcs.date || {};
    mcs.app = mcs.app || { name: "app", version: "1.0" };
    mcs.app.dict = mcs.app.dict || {};
    mcs.app.config = mcs.app.config || {};
    mcs.config = mcs.config || {};

    return mcs;
})();
(function() {
    'use strict';

    var fileTypes = {
        css: 'css',
        javascript: 'js'
    };
    var getFileName = function(fileType, filePath, isLocal) {
        var fileName = !isLocal ? mcs.app.config.mcsComponentBaseUrl.replace('http://', 'http:\\') : '';
        var extension = '';
        switch (fileType) {
            case fileTypes.css:
                extension += '.' + fileTypes.css;
                break;
            case fileTypes.javascript:
                extension += '.' + fileTypes.javascript;
                break;
        }
        if (!extension) return;

        if (filePath.substring(filePath.length - extension.length) != extension) {
            fileName += filePath + extension;
        } else {
            fileName += filePath;
        }
        fileName = fileName.replace(new RegExp('\/\/', 'gm'), '/').replace('http:\\', 'http://');
        return fileName;
    };

    var handleParam = function(fileType, params) {
        if (!params.length) return;

        var assets = {
            files: [],
            localFiles: [],
            container: ''
        };

        if (params.length == 1) {
            if (params[0] instanceof Object && params[0].constructor == Object) {
                assets = params;
            } else if (params[0] instanceof Array && params[0].constructor == Array) {
                assets.files = params[0];
            } else if (typeof params[0] == 'string') {
                assets.files = [params[0]];
            }
        } else {
            if (params[0] instanceof Array && params[0].constructor == Array) {
                assets.files = params[0];
            } else if (typeof params[0] == 'string') {
                assets.files = [params[0]];
            }
            if (params[1] instanceof Array && params[1].constructor == Array) {
                assets.localFiles = params[1];
            } else if (typeof params[1] == 'string') {
                assets.localFiles = [params[1]];
            }

            assets.container = document.getElementById(arguments[2]) || '';
        }

        if (fileType == fileTypes.css) {
            return assets[0] || {
                cssFiles: assets.files,
                localCssFiles: assets.localFiles,
                container: assets.container
            };
        } else {
            return assets[0] || {
                jsFiles: assets.files,
                localJsFiles: assets.localFiles,
                container: assets.container
            };
        }
    };

    /*
     * 动态加载CSS文件列表，可指定页面上的任意位置
     * cssFiles: 来自远程服务器的CSS文件列表(如：/libs/demo.css,lib/demo,lib/demo.css)
     * localCssFiles: 来自本地服务器的CSS文件列表(如：/local/demo.css, local/demo)
     * container: 可不指定将附加到head中，否则将附加到指定的标签位置
     */
    mcs.g.loadCss = function( /*{cssFiles:[],localCssFiles:[],container:'#containerId'}*/ ) {
        var assets = handleParam(fileTypes.css, arguments);
        var mergeFiles = [{
            isLocal: false,
            data: assets.cssFiles || []
        }, {
            isLocal: true,
            data: assets.localCssFiles || []
        }];

        for (var i = 0, iLen = mergeFiles.length; i < iLen; i++) {
            var file = mergeFiles[i];
            for (var j = 0, jLen = file.data.length; j < jLen; j++) {
                var cssFile = file.data[j];
                var length = cssFile.length;
                if (!length) continue;
                var fileName = getFileName(fileTypes.css, cssFile, file.isLocal);
                var cssElem = document.createElement('link');
                cssElem.setAttribute('rel', 'stylesheet');
                cssElem.setAttribute('href', fileName);

                var container = assets.container || document.getElementsByTagName("head")[0];
                container.appendChild(cssElem);
            }
        }
    };


    /*
     * 动态加载Js文件列表，可指定页面上的任意位置
     * jsFiles: 来自远程服务器的JS文件列表(如：/libs/demo.js,lib/demo,lib/demo.js)
     * localJsFiles: 来自本地服务器的JS文件列表(如：/local/demo.css, local/demo)
     * container: 可不指定将附加到head中，否则将附加到指定的标签位置
     */
    mcs.g.loadJs = function( /*{jsFiles:[],localJsFiles:[],container:'#containerId'}*/ ) {
        var assets = handleParam(fileTypes.javascript, arguments);
        var mergeFiles = [{
            isLocal: false,
            data: assets.jsFiles || []
        }, {
            isLocal: true,
            data: assets.localJsFiles || []
        }];

        for (var i = 0, iLen = mergeFiles.length; i < iLen; i++) {
            var file = mergeFiles[i];
            for (var j = 0, jLen = file.data.length; j < jLen; j++) {
                var jsFile = file.data[j];
                var length = jsFile.length;
                if (!length) continue;
                var fileName = getFileName(fileTypes.javascript, jsFile, file.isLocal);
                var jsElem = document.createElement('script');
                jsElem.setAttribute('src', fileName);

                var container = assets.container || document.getElementsByTagName("head")[0];
                container.appendChild(jsElem);
            }
        }
    };

    /*
     * 对requirejs单独做处理
     * requireFile: 来自远程或本地服务器的RequireJS文件地址(如：libs/require)
     * requireConfig: 来自本地服务器的RequireJS配置文件地址(如：./app/config/require.config),
     * isLocal: 是否来自本地服务器(默认为false)
     */
    mcs.g.loadRequireJs = function(requireFile, requireConfig, isLocal) {
        if (!requireFile || !requireConfig) return;
        var fileType = fileTypes.javascript;
        var fileName = getFileName(fileType, requireFile, isLocal);
        var extension = '.' + fileType;
        if (requireConfig.substring(requireConfig.length - extension.length) != extension) {
            requireConfig += extension;
        }
        var jsElem = document.createElement('script');
        jsElem.setAttribute('src', fileName);
        jsElem.setAttribute('data-main', requireConfig);

        document.getElementsByTagName("head")[0].appendChild(jsElem);
    }

    return mcs.g;

})();

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

(function() {
    /** * 获取本周、本季度、本月、上月的开端日期、停止日期 */
    //当前日期 
    var now = new Date();
    //今天本周的第几天 
    var nowDayOfWeek = now.getDay();
    //当前日 
    var nowDay = now.getDate();
    //当前月
    var nowMonth = now.getMonth();
    //当前年
    var nowYear = now.getYear();
    nowYear += (nowYear < 2000) ? 1900 : 0;
    //上月日期
    var lastMonthDate = new Date();
    lastMonthDate.setDate(1);
    lastMonthDate.setMonth(lastMonthDate.getMonth() - 1);
    //上年
    var lastYear = lastMonthDate.getYear();
    var lastMonth = lastMonthDate.getMonth();
    //格局化日期：yyyy-MM-dd 
    mcs.date.format = function(date) {
            var myyear = date.getFullYear();
            var mymonth = date.getMonth() + 1;
            var myweekday = date.getDate();
            if (mymonth < 10) {
                mymonth = "0" + mymonth;
            }
            if (myweekday < 10) {
                myweekday = "0" + myweekday;
            }
            return (myyear + "-" + mymonth + "-" + myweekday);
        }
        //比较两个时间的大小
    mcs.date.compare = function(beginTime, endTime) {
        //将字符串转换为日期
        var begin = new Date(beginTime.replace(/-/g, "/"));
        var end = new Date(endTime.replace(/-/g, "/"));
        return begin < end ? 1 : (begin == end ? 0 : -1);
    };
    //获得某月的天数 
    mcs.date.getMonthDays = function(month) {
            var monthStartDate = new Date(nowYear, month, 1);
            var monthEndDate = new Date(nowYear, month + 1, 1);
            var days = (monthEndDate - monthStartDate) / (1000 * 60 * 60 * 24);
            return days;
        }
        //获得本季度的开端月份 
    mcs.date.getQuarterStartMonth = function() {
            var quarterStartMonth = 0;
            if (nowMonth < 3) {
                quarterStartMonth = 0;
            }
            if (2 < nowMonth && nowMonth < 6) {
                quarterStartMonth = 3;
            }
            if (5 < nowMonth && nowMonth < 9) {
                quarterStartMonth = 6;
            }
            if (nowMonth > 8) {
                quarterStartMonth = 9;
            }
            return quarterStartMonth;
        }
        // 获取今天
    mcs.date.today = function() {
        var todayDate = new Date(nowYear, nowMonth, nowDay, now.getHours(), now.getMinutes(), now.getSeconds());
        return todayDate;
    };
    //获取今天之前的某一天
    mcs.date.lastDay = function(offsetDay) {
        var lastStartDate = new Date(nowYear, nowMonth, nowDay + offsetDay + 1);
        return lastStartDate;
    };
    //得到左边界时间点
    mcs.date.getLeftBoundDatetime = function(date, offset) {
        if (isNaN(offset)) {
            return null;
        }
        var selectionTime = new Date(date || Date.now());
        var ticks = selectionTime.getTime();
        var times = selectionTime.getHours() * 60 * 60 * 1000 + selectionTime.getMinutes() * 60 * 1000 + selectionTime.getSeconds() * 1000;

        if (offset < 0) {
            return new Date(ticks + (offset + 1) * 24 * 60 * 60 * 1000 - times + 1000);
        } else {
            return new Date(ticks - times);
        }
    };
    //得到右边界时间点
    mcs.date.getRightBoundDatetime = function(date, offset) {
        if (isNaN(offset)) {
            return null;
        }
        var selectionTime = new Date(date || Date.now());
        var ticks = selectionTime.getTime();
        var times = selectionTime.getHours() * 60 * 60 * 1000 + selectionTime.getMinutes() * 60 * 1000 + selectionTime.getSeconds() * 1000;

        if (offset < 0) {
            return new Date(ticks - times + 1 * 24 * 60 * 60 * 1000 - 1000);

        } else {

            return new Date(ticks + (offset + 1) * 24 * 60 * 60 * 1000 - times - 1000);
        }
    };



    // 获取指定日期的前后几天
    // mcs.date.siblingsDay = function(date, offsetDay) {
    //     var currentDay = new Date(date);
    //     if (isNaN(parseInt(offsetDay))) return currentDay;
    //     var currentYear = currentDay.getYear();
    //     currentYear += (currentYear < 2000) ? 1900 : 0;
    //     var siblingsDate = new Date(currentYear, currentDay.getMonth(), currentDay.getDate() + offsetDay, 23, 59, 59);
    //     return siblingsDate;
    // };
    //获得本周的开始日期 
    mcs.date.getWeekStartDate = function() {
        var weekStartDate = new Date(nowYear, nowMonth, nowDay - nowDayOfWeek);
        return weekStartDate;
    };
    //获得本周的停止日期 
    mcs.date.getWeekEndDate = function() {
        var weekEndDate = new Date(nowYear, nowMonth, nowDay + (6 - nowDayOfWeek));
        return weekEndDate;
    };
    //获得本月的开始日期 
    mcs.date.getMonthStartDate = function() {
        var monthStartDate = new Date(nowYear, nowMonth, 1);
        return monthStartDate;
    };
    //获得本月的停止日期 
    mcs.date.getMonthEndDate = function() {
        var monthEndDate = new Date(nowYear, nowMonth, mcs.date.getMonthDays(nowMonth));
        return monthEndDate;
    };
    //获得上月开始日期 
    mcs.date.getLastMonthStartDate = function() {
        var lastMonthStartDate = new Date(nowYear, lastMonth, 1);
        return lastMonthStartDate;
    };
    //获得上月停止日期 
    mcs.date.getLastMonthEndDate = function() {
        var lastMonthEndDate = new Date(nowYear, lastMonth, mcs.date.getMonthDays(lastMonth));
        return lastMonthEndDate;
    };
    //获得本季度的开始日期 
    mcs.date.getQuarterStartDate = function() {
        var quarterStartDate = new Date(nowYear, getQuarterStartMonth(), 1);
        return quarterStartDate;
    };
    //获得本季度的停止日期 
    mcs.date.getQuarterEndDate = function() {
        var quarterEndMonth = getQuarterStartMonth() + 2;
        var quarterStartDate = new Date(nowYear, quarterEndMonth, mcs.date.getMonthDays(quarterEndMonth));
        return quarterStartDate;
    };
    //获取时间差
    mcs.date.datepart = function(startDate, endDate, part) {
        //if (!mcs.util.isDate(startDate) || !mcs.util.isDate(endDate)) return;
        var start = new Date(startDate.replace(/-/g, "/"));
        var end = new Date(endDate.replace(/-/g, "/"));
        var diff = end.getTime() - start.getTime(); //时间差的毫秒数
        var section = {};

        section.year = diff / (12 * 30 * 24 * 3600 * 1000);
        section.month = diff % (12 * 30 * 24 * 3600 * 1000);
        section.day = section.month % (30 * 24 * 3600 * 1000);
        section.hour = section.day % (24 * 3600 * 1000); //计算天数后剩余的毫秒数
        section.minute = section.hour % (3600 * 1000); //计算小时数后剩余的毫秒数
        section.second = section.minute % (60 * 1000); //计算分钟数后剩余的毫秒数

        switch (part) {
            case 'y':
            case 'year':
                return Math.floor(section.year);
            case 'M':
            case 'month':
                return Math.floor(section.month / (30 * 24 * 3600 * 1000));
            case 'd':
            case 'day':
                return Math.floor(section.day / (24 * 3600 * 1000));
            case 'h':
            case 'hour':
                return Math.floor(section.hour / (3600 * 1000)) + (Math.floor(section.day / (24 * 3600 * 1000))) * 24;
            case 'm':
            case 'minute':
                return Math.floor(section.minute / (60 * 1000)) + (Math.floor(section.hour / (3600 * 1000)) + (Math.floor(section.day / (24 * 3600 * 1000))) * 24) * 60;
            case 's':
            case 'second':
                return Math.round(section.second / 1000) + (Math.floor(section.minute / (60 * 1000)) + (Math.floor(section.hour / (3600 * 1000)) + (Math.floor(section.day / (24 * 3600 * 1000))) * 24) * 6) * 60;
            default:
                return;
        }
    };

    return mcs.date;
})();

mcs.browser = function () {
    var _browser = {};
    var sUserAgent = navigator.userAgent;
    console.info("useragent: ", sUserAgent);

    var isOpera = sUserAgent.indexOf("Opera") > -1;
    if (isOpera) {
        //首先检测Opera是否进行了伪装
        if (navigator.appName == 'Opera') {
            //如果没有进行伪装，则直接后去版本号
            _browser.version = parseFloat(navigator.appVersion);
        } else {
            var reOperaVersion = new RegExp("Opera (\\d+.\\d+)");
            //使用正则表达式的test方法测试并将版本号保存在RegExp.$1中
            reOperaVersion.test(sUserAgent);
            _browser.version = parseFloat(RegExp['$1']);
        }
        _browser.opera = true;
    }

    var isChrome = sUserAgent.indexOf("Chrome") > -1;
    if (isChrome) {
        if (sUserAgent.indexOf("Edge") > -1) {
            var reEdge = new RegExp("Edge/(\\d+\\.\\d+)");
            reEdge.test(sUserAgent);
            _browser.version = parseFloat(RegExp['$1']);
            _browser.edge = true;
        } else {
            var reChorme = new RegExp("Chrome/(\\d+\\.\\d+(?:\\.\\d+\\.\\d+))?");
            reChorme.test(sUserAgent);
            _browser.version = parseFloat(RegExp['$1']);
            _browser.chrome = true;
        }
    }

    //排除Chrome信息，因为在Chrome的user-agent字符串中会出现Konqueror/Safari的关键字
    var isKHTML = (sUserAgent.indexOf("KHTML") > -1
            || sUserAgent.indexOf("Konqueror") > -1 || sUserAgent
            .indexOf("AppleWebKit") > -1)
            && !isChrome;

    if (isKHTML) {//判断是否基于KHTML，如果时的话在继续判断属于何种KHTML浏览器
        var isSafari = sUserAgent.indexOf("AppleWebKit") > -1;
        var isKonq = sUserAgent.indexOf("Konqueror") > -1;

        if (isSafari) {
            var reAppleWebKit = new RegExp("Version/(\\d+(?:\\.\\d*)?)");
            reAppleWebKit.test(sUserAgent);
            var fAppleWebKitVersion = parseFloat(RegExp["$1"]);
            _browser.version = parseFloat(RegExp['$1']);
            _browser.safari = true;
        } else if (isKonq) {
            var reKong = new RegExp(
                   "Konqueror/(\\d+(?:\\.\\d+(?\\.\\d)?)?)");
            reKong.test(sUserAgent);
            _browser.version = parseFloat(RegExp['$1']);
            _browser.konqueror = true;
        }
    }

    // !isOpera 避免是由Opera伪装成的IE  
    var isIE = sUserAgent.indexOf("compatible") > -1
           && sUserAgent.indexOf("MSIE") > -1 && !isOpera;
    if (isIE || _browser.edge) { //将edge当做ie作为处理，但也可以单独判断为edge
        var reIE = new RegExp("MSIE (\\d+\\.\\d+);");
        reIE.test(sUserAgent);
        _browser.version = parseFloat(RegExp['$1']);
        _browser.msie = true;
    }

    // 排除Chrome 及 Konqueror/Safari 的伪装
    var isMoz = sUserAgent.indexOf("Gecko") > -1 && !isChrome && !isKHTML;
    if (isMoz) {
        var reMoz = new RegExp("rv:(\\d+\\.\\d+(?:\\.\\d+)?)");
        reMoz.test(sUserAgent);
        _browser.version = parseFloat(RegExp['$1']);
        if (_browser.version == 11) {
            _browser.msie = true; //fix the IE11
        }
        _browser.mozilla = true;
    }

    return {
        s: _browser
    };
}();


// 调用
//var browser = mcs.browser.s;
//console.info("broswer.version: ", browser.version);
//console.info("broswer.msie is ", browser.msie);
//console.info("broswer.msie is ", browser.edge);
//console.info("broswer.safari is ", browser.safari);
//console.info("broswer.opera is ", browser.opera);
//console.info("broswer.mozilla is ", browser.mozilla);
//console.info("broswer.chrome is ", browser.chrome);
//console.info("broswer.konqueror is ", browser.konqueror);
//全局配置文件(基于具体项目,如:PPTS)
var ppts = ppts || mcs.app;

(function () {
    ppts.name = 'ppts';
    ppts.version = '1.0';
    ppts.user = ppts.user || {};
    ppts.enum = ppts.enum || {};

    ppts.config = {
        pageSizeItem: 20, // Datatable每页的数量
        taskDisplayItem: 5, // 待办任务在右上角显示的数量
        taskQueryInterval: 30 * 1000, // 待办任务轮询的周期(半分钟)
        datePickerFormat: 'yyyy-mm-dd',
        datetimePickerFormat: 'yyyy-mm-dd hh:ii:00',
        datePickerLang: 'zh-CN',
        modules: {
            dashboard: 'app/dashboard/ppts.dashboard',
            task: 'app/task/ppts.task',
            customer: 'app/customer/ppts.customer',
            account: 'app/account/ppts.account',
            product: 'app/product/ppts.product',
            order: 'app/order/ppts.order',
            schedule: 'app/schedule/ppts.schedule',
            infra: 'app/infra/ppts.infra',
            custcenter: 'app/custcenter/ppts.custcenter',
            query: 'app/query/ppts.query',
        },
        dictMappingConfig: {
            // 公共相关
            region: 'c_codE_ABBR_LOCATION',
            grade: 'c_codE_ABBR_CUSTOMER_GRADE',
            gender: 'c_codE_ABBR_GENDER',
            idtype: 'c_codE_ABBR_BO_Customer_CertificateType',
            income: 'c_codE_ABBR_HOMEINCOME',
            childMale: 'c_codE_ABBR_CHILDMALEDICTIONARY',
            childFemale: 'c_codE_ABBR_CHILDFEMALEDICTIONARY',
            parentMale: 'c_codE_ABBR_PARENTMALEDICTIONARY',
            parentFemale: 'c_codE_ABBR_PARENTFEMALEDICTIONARY',
            parent: 'c_codE_ABBR_PARENTDICTIONARY',
            child: 'c_codE_ABBR_CHILDDICTIONARY',
            source: 'c_codE_ABBR_BO_Customer_Source',
            contactType: 'c_codE_ABBR_Customer_CRM_NewContactType',
            // 学年
            academicYear: 'c_codE_ABBR_ACDEMICYEAR',
            // vip客户类型
            vipType: 'c_codE_ABBR_CUSTOMER_VipType',
            // vip客户等级
            vipLevel: 'c_codE_ABBR_CUSTOMER_VipLevel',

            /*
            * 学员相关
            */
            studentType: 'c_codE_ABBR_Student_Type',
            studentValid: 'c_codE_ABBR_Student_Valid',
            studentAttend: 'c_codE_ABBR_Student_Attend',
            studentCancel: 'c_codE_ABBR_Student_Cancel',
            studentSuspend: 'c_codE_ABBR_Student_Suspend',
            studentCompleted: 'c_codE_ABBR_Student_Completed',
            studentAttendRange: 'c_codE_ABBR_Student_Attend_Range',
            studentRange: 'c_codE_ABBR_Student_Range',
            sendEmailSMS: 'c_codE_ABBR_Student_SendEmailSMS',
            changeTeacherReason: 'C_Code_Abbr_BO_Customer_ChangeTeacherReason',
            //停课休学原因
            stopAlertReason: 'c_codE_ABBR_Customer_StopAlertReason',
            //退费预警原因
            refundAlertReason: 'c_codE_ABBR_Customer_RefundAlertReason',
            //停课休学类型
            stopAlertType: 'c_codE_ABBR_Customer_StopAlertType',
            //退费预警状态
            refundAlertStatus: 'c_codE_ABBR_Customer_RefundAlertStatus',
            changeTeacherReason: 'c_codE_ABBR_BO_Customer_ChangeTeacherReason',
            // 分配教师操作类型
            teacherApplyType: 'c_codE_ABBR_Customer_Teacher_ApplyType',
            // 高三毕业库学员
            graduated: 'c_codE_ABBR_Customer_Graduated',
            // 距最后上课时长
            lastCourseType: 'c_codE_ABBR_Customer_LastCourseType',

            /*
            * 教学服务会相关
            */
            meetingType: 'c_codE_ABBR_Customer_CRM_MainServiceMeeting',
            satisfaction: 'c_codE_Abbr_BO_Customer_Satisfaction',
            meetingEvent: 'c_codE_ABBR_MeetingEvent',
            meetingTitle: 'c_codE_ABBR_MeetingTitle',
            participants: 'c_codE_ABBR_Customer_CRM_MeetingObject',
            contentType: 'c_codE_ABBR_ContentType',

            /*
            * 学大反馈相关
            */
            replyType: 'c_codE_ABBR_Customer_ReplyType',
            replyObject: 'c_codE_ABBR_Customer_ReplyObject',

            /*
            * 账户相关
            */
            recordType: 'c_codE_ABBR_account_RecordType',
            //账户类型
            accountType: 'c_codE_ABBR_account_AccountType',
            //账户状态
            accountStatus: 'c_codE_ABBR_account_AccountStatus',
            //转让类型
            accountTransferType: 'c_codE_ABBR_account_TransferType',
            //缴费类型
            chargeType: 'c_codE_ABBR_account_ChargeType',
            //缴费单审核状态
            chargeAuditStatus: 'c_codE_ABBR_Account_ChargeAuditStatus',
            //退费类型
            refundType: 'c_codE_ABBR_account_RefundType',
            //退费确认操作
            refundVerifyAction: 'c_codE_ABBR_account_RefundVerifyAction',
            //退费确认状态
            refundVerifyStatus: 'c_codE_ABBR_account_RefundVerifyStatus',
            //制度外退费类型
            extraRefundType: 'c_codE_ABBR_account_ExtraRefundType',
            //支付状态
            payStatus: 'c_codE_ABBR_account_PayStatus',
            //支付类型
            payType: 'c_codE_ABBR_account_PayType',
            //发票状态
            invoiceStatus: 'c_codE_ABBR_Account_InvoiceStatus',
            //发票状态
            invoiceRecordStatus: 'c_codE_ABBR_Account_InvoiceRecordStatus',

            /*
            * 成绩相关
            */
            // 家长满意度
            scoreSatisficing: 'c_codE_ABBR_Score_Satisficing',
            // 学年度
            studyYear: 'c_codE_ABBR_Customer_StudyYear',
            // 学期
            studyTerm: 'c_codE_ABBR_Customer_StudyTerm',
            // 录取院校类型
            admissionType: 'c_codE_ABBR_Customer_AdmissionType',
            // 考试学段
            studyStage: 'c_codE_ABBR_Customer_StudyStage',
            // 考试类别
            scoreType: 'c_Code_Abbr_BO_Customer_GradeTypeExt',
            // 考试学员类别
            examCustomerType: 'c_codE_ABBR_Exam_Customer_Type',
            // 考试科目
            examSubject: 'c_codE_ABBR_Customer_Exam_Subject',
            // 考试月份
            examMonth: 'c_codE_ABBR_Exam_Month',
            // 成绩升降
            scoreChangeType: 'c_codE_ABBR_Exam_ScoreChangeType',
            // 任课教师
            scoreTeacher: 'c_codE_ABBR_scoreTeacher',
            // 教师所在学科组
            scoreTeacherOrgName: 'c_codE_ABBR_scoreTeacherOrgName',

            /*
            * 通用
            */
            //申请状态
            applyStatus: 'c_codE_ABBR_common_ApplyStatus',
            //对账状态
            checkStatus: 'c_codE_ABBR_common_CheckStatus',
            //教师类型
            teacherType: 'c_codE_ABBR_common_TeacherType',
            //打印状态
            printStatus: 'c_codE_ABBR_common_PrintStatus',
            //岗位状态
            jobStatus: 'c_codE_ABBR_common_JobStatus',
            //岗位类型
            jobType: 'c_codE_ABBR_common_JobType',
            //组织机构类型
            orgType: 'c_codE_ABBR_common_OrgType',

            /*
            * 跟进记录相关
            */
            // 跟进类型
            followType: 'c_codE_ABBR_Customer_CRM_SaleContactType',
            // 跟进阶段
            followStage: 'c_codE_ABBR_Customer_CRM_SalePhase',
            // 沟通一级结果
            mainTalk: 'c_codE_ABBR_Customer_CRM_CommunicateResultFirstEx',
            // 沟通二级结果
            subTalk: 'c_codE_ABBR_Customer_CRM_CommunicateResultSecondEx',
            // 客户级别
            customerLevel: 'c_codE_ABBR_Customer_CRM_CustomerLevelEx',
            // 无效原因
            invalidReason: 'c_codE_ABBR_Customer_CRM_InvaliCustomerType',
            // 购买意图
            purchaseIntention: 'c_codE_ABBR_Customer_CRM_PurchaseIntent',
            // 实际上门人数
            verifyPeople: 'c_codE_ABBR_Customer_CRM_RealCallPersonNum',
            // 跟进对象
            followObject: 'c_codE_ABBR_Customer_CRM_SaleContactTarget',
            //跟进人员关系
            verifyRelation: 'c_codE_ABBR_RealCallPersonRelation',

            /*
            * 产品相关
            */
            // 产品大类
            categoryType: 'c_codE_ABBR_Product_CategoryType',
            // 服务费类型
            expenseType: 'c_codE_ABBR_Product_ExpenseType',
            // 年级类型
            gradeType: 'c_codE_ABBR_Product_GradeType',
            // 科目类型
            subjectType: 'c_codE_ABBR_STUDENTBRANCH',
            // 科目
            subject: 'c_codE_ABBR_BO_Product_TeacherSubject',
            // 季节
            season: 'c_codE_ABBR_Product_Season',
            // 课次/课时时长
            duration: 'c_codE_ABBR_BO_ProductDuration',
            // 课程级别
            courseLevel: 'c_codE_ABBR_Product_CourseLevel',
            // 辅导类型
            coachType: 'c_codE_ABBR_Product_CoachType',
            // 班组类型
            groupType: 'c_codE_ABBR_Product_GroupType',
            // 班级类型
            classType: 'c_codE_ABBR_Product_GroupClassType',
            // 跨校区产品收入归属
            belonging: 'c_codE_ABBR_Product_IncomeBelonging',
            // 薪酬规则对象
            rule: 'c_codE_ABBR_Product_RuleObject',
            // 颗粒度
            unit: 'c_codE_ABBR_Product_ProductUnit',
            // 合作类型
            hasPartner: 'c_codE_ABBR_Product_HasPartner',
            // 产品状态/销售状态
            productStatus: 'c_codE_ABBR_Product_ProductStatus',


            /*
            * 排课相关
            */
            assignCondition: 'c_codE_ABBR_AssignCondition',
            asset: 'c_codE_ABBR_Asset',
            teacher: 'c_codE_ABBR_Teacher',
            hour: 'c_codE_ABBR_Hour',
            minute: 'c_codE_ABBR_Minute',
            copyCourseType: 'c_codE_ABBR_copyCourseType',
            classStatus: 'c_codE_ABBR_Order_ClassStatus',
            lessonStatus: 'c_codE_ABBR_Order_LessonStatus',
            editCourseType: 'c_codE_ABBR_EditCourseType',
            assignStatus: 'c_codE_ABBR_Course_AssignStatus',
            startLessons: 'c_codE_ABBR_Order_StartLessons',
            endLessons: 'c_codE_ABBR_Order_EndLessons',
            student: 'c_codE_ABBR_Student',
            subGrade: 'c_codE_ABBR_SubGrade',
            subSubject: 'c_codE_ABBR_SubSubject',
            assignSource: 'c_codE_ABBR_Assign_Source',
            /*课时数，补录课时用*/
            courseAmount: 'c_codE_ABBR_CourseAmount',
            /*教师年龄区间，按教师排课用*/
            ageCollection: 'c_codE_ABBR_AgeCollection',

            /*
            * 订单相关
            */
            //订单状态
            orderStatus: 'c_codE_ABBR_Order_OrderStatus',
            //操作人岗位
            post: 'c_codE_ABBR_Order_Post',
            //特殊折扣原因
            orderSpecialType: 'c_codE_ABBR_Order_SpecialType',
            //订单类型
            orderType: 'c_codE_ABBR_Order_OrderType',
            consumeType: 'c_codE_ABBR_Order_ConsumeType',

            /*
            * 客服相关
            */
            serviceType: 'c_codE_ABBR_customer_ServiceType',
            serviceStatus: 'c_codE_ABBR_customer_ServiceStatus',
            acceptLimit: 'c_codE_ABBR_customer_AcceptLimit',
            consultType: 'c_codE_ABBR_customer_ConsultType',
            complaintTimes: 'c_codE_ABBR_customer_ComplaintTimes',
            complaintLevel: 'c_codE_ABBR_customer_ComplaintLevel',
            complaintUpgrade: 'c_codE_ABBR_customer_ComplaintUpgrade',

            /*回访相关*/
            visitType: 'c_codE_ABBR_BO_Customer_ReturnInfoType',
            visitWay: 'c_codE_ABBR_Customer_CRM_ReturnWay',
            satisficing: 'c_codE_ABBR_BO_Customer_Satisfaction',
            timeType: 'c_codE_ABBR_TimeType_Service',

            /*基础数据*/
            discountStatus: 'c_codE_ABBR_BO_Infra_DiscountStatus',
            presentStatus: 'c_codE_ABBR_BO_Infra_PresentStatus',
            campusUseInfo: 'c_codE_ABBR_BO_Infra_CampusUseInfo',
            /*综合服务费*/
            serviceFeeType: 'c_codE_ABBR_ServiceFee_ServiceType'
        },
        dataServiceConfig: {
            // Task Services
            taskDataService: 'app/task/task.dataService',

            // Customer Services
            customerService: 'app/customer/ppts.customer.service',
            customerVerifyDataService: 'app/customer/customerverify/customerverify.dataService',
            feedbackDataService: 'app/customer/feedback/feedback.dataService',
            marketDataService: 'app/customer/market/market.dataService',
            customerMeetingDataService: 'app/customer/customermeeting/customermeeting.dataService',
            customerDataService: 'app/customer/potentialcustomer/potentialcustomer.dataService',
            customerVisitDataService: 'app/customer/customervisit/customervisit.dataService',
            scoreDataService: 'app/customer/score/score.dataService',
            studentDataService: 'app/customer/student/student.dataService',
            followDataService: 'app/customer/follow/follow.dataService',
            weeklyFeedbackDataService: 'app/customer/weeklyfeedback/weeklyfeedback.dataService',
            stopAlertDataService: 'app/customer/stopalerts/stopalerts.dataService',
            refundAlertDataService: 'app/customer/refundalerts/refundalerts.dataService',
            discountDataService: 'app/infra/discount/discount.dataService',

            // Account Services
            //账户公用
            accountService: 'app/account/ppts.account.service',
            // 账户显示
            accountDisplayDataService: 'app/account/display/display.dataService',
            // 账户充值
            accountChargeDataService: 'app/account/charge/charge.dataService',
            // 账户退费
            accountRefundDataService: 'app/account/refund/refund.dataService',
            // 账户服务费扣减
            accountDeductDataService: 'app/account/deduct/deduct.dataService',
            // 账户服务费返还
            accountReturnDataService: 'app/account/return/return.dataService',
            // 账户转让
            accountTransferDataService: 'app/account/transfer/transfer.dataService',

            // Product Services
            productDataService: 'app/product/productlist/product.dataService',
            productCategoryDataService: 'app/product/productcategory/productcategory.dataService',

            // Schedule Services
            classgroupDataService: 'app/schedule/classgroup/classgroup.dataService',
            classgroupCourseDataService: 'app/schedule/classgroupcourse/classgroupcourse.dataService',
            confirmCourseDataService: 'app/schedule/confirmcourse/confirmcourse.dataService',
            settingListDataService: 'app/schedule/settinglist/settinglist.dataService',
            studentAssignmentDataService: 'app/schedule/studentassignment/studentassignment.dataService',
            studentCourseDataService: 'app/schedule/studentcourse/studentcourse.dataService',
            teacherAssignmentDataService: 'app/schedule/teacherassignment/teacherassignment.dataService',
            teacherCourseDataService: 'app/schedule/teachercourse/teachercourse.dataService',

            // Order Services
            classhourCourseDataService: 'app/order/classhour/classhour.dataService',
            purchaseCourseDataService: 'app/order/purchase/purchase.dataService',
            unsubscribeCourseDataService: 'app/order/unsubscribe/unsubscribe.dataService',

            // Infra Services
            customerDiscountDataService: 'app/infra/customerdiscount/customerdiscount.dataService',
            dictionaryDataService: 'app/infra/dictionary/dictionary.dataService',
            presentDataService: 'app/infra/present/present.dataService',
            nonCustomerDiscountDataService: 'app/infra/noncustomerdiscount/noncustomerdiscount.dataService',
            servicefeeDataService: 'app/infra/servicefee/servicefee.dataService',

            // CustCenter Services
            custserviceDataService: 'app/custcenter/custservice/custservice.dataService',

            // Query Services
            accountQueryService: 'app/query/account/account-query.service',
            assignQueryService: 'app/query/assign/assign-query.service',
            orderQueryService: 'app/query/order/order-query.service',
            studentQueryService: 'app/query/student/student-query.service',
            taskQueryService: 'app/query/task/task-query.service'
        },
        sidebarMenusConfig: [{
            name: '首页', icon: 'home', route: 'ppts.dashboard', active: true
        }, {
            name: '审批管理', icon: 'check-square-o',
            children: [
                { name: '待办列表', route: 'ppts.userTask' },
                { name: '已办列表', route: 'ppts.completedTask' },
                { name: '通知列表', route: 'ppts.notify' },
                { name: '流程表单', route: 'ppts.workflow' }
            ]
        }, {
            name: '客户管理', icon: 'user',
            permission: '潜客管理,潜客管理-本部门,潜客管理-本校区,潜客管理-本分公司,潜客管理-全国,市场资源,市场资源-本部门,市场资源-本校区,市场资源-本分公司,市场资源-全国,跟进管理（学员视图-跟进记录跟进记录详情）,跟进管理（学员视图-跟进记录跟进记录详情）-本部门,跟进管理（学员视图-跟进记录跟进记录详情）-本校区,跟进管理（学员视图-跟进记录跟进记录详情）-本分公司,跟进管理（跟进记录详情）-全国,上门管理,上门管理-本部门,上门管理-本校区,上门管理-本分公司,上门管理-全国,学员管理,学员管理-本部门,学员管理-本校区,学员管理-本分公司,学员管理-全国,回访管理（学员视图-回访、回访详情）,回访管理（学员视图-回访、回访详情）-本部门,回访管理（学员视图-回访、回访详情）-本校区,回访管理（学员视图-回访、回访详情）-本分公司,回访管理（学员视图-回访、回访详情）全国,成绩管理（学员视图-成绩）,成绩管理（学员视图-成绩、成绩详情）-本部门,成绩管理（学员视图-成绩、成绩详情）-本校区,成绩管理（学员视图-成绩、成绩详情）-本分公司,成绩管理（学员视图-成绩、成绩详情）-全国,教学服务会管理（学员视图-教学服务会、详情查看）,教学服务会管理（学员视图-教学服务会、详情查看）-本部门,教学服务会管理（学员视图-教学服务会、详情查看）-本校区,教学服务会管理（学员视图-教学服务会、详情查看）-本分公司,教学服务会管理（学员视图-教学服务会、详情查看）-全国,学大反馈管理（学员视图-家校互动）,学大反馈管理（学员视图-家校互动）-本部门,学大反馈管理（学员视图-家校互动）-本校区,学大反馈管理（学员视图-家校互动）-本分公司,学大反馈管理（学员视图-家校互动）-全国',
            children: [
               { name: '潜客管理', route: 'ppts.customer', permission: '潜客管理,潜客管理-本部门,潜客管理-本校区,潜客管理-本分公司,潜客管理-全国' },
               { name: '市场资源', route: 'ppts.market', permission: '市场资源,市场资源-本部门,市场资源-本校区,市场资源-本分公司,市场资源-全国' },
               { name: '跟进管理', route: 'ppts.follow', permission: '跟进管理（学员视图-跟进记录跟进记录详情）,跟进管理（学员视图-跟进记录跟进记录详情）-本部门,跟进管理（学员视图-跟进记录跟进记录详情）-本校区,跟进管理（学员视图-跟进记录跟进记录详情）-本分公司,跟进管理（跟进记录详情）-全国' },
               { name: '上门管理', route: 'ppts.customerverify', permission: '上门管理,上门管理-本部门,上门管理-本校区,上门管理-本分公司,上门管理-全国' },
               { name: '学员管理', route: 'ppts.student', permission: '学员管理,学员管理-本部门,学员管理-本校区,学员管理-本分公司,学员管理-全国' },
               { name: '回访管理', route: 'ppts.customervisit', permission: '回访管理（学员视图-回访、回访详情）,回访管理（学员视图-回访、回访详情）-本部门,回访管理（学员视图-回访、回访详情）-本校区,回访管理（学员视图-回访、回访详情）-本分公司,回访管理（学员视图-回访、回访详情）全国' },
               { name: '成绩管理', route: 'ppts.score', permission: '成绩管理（学员视图-成绩）,成绩管理（学员视图-成绩、成绩详情）-本部门,成绩管理（学员视图-成绩、成绩详情）-本校区,成绩管理（学员视图-成绩、成绩详情）-本分公司,成绩管理（学员视图-成绩、成绩详情）-全国' },
               { name: '教学服务会', route: 'ppts.customermeeting', permission: '教学服务会管理（学员视图-教学服务会、详情查看）,教学服务会管理（学员视图-教学服务会、详情查看）-本部门,教学服务会管理（学员视图-教学服务会、详情查看）-本校区,教学服务会管理（学员视图-教学服务会、详情查看）-本分公司,教学服务会管理（学员视图-教学服务会、详情查看）-全国' },
               { name: '学大反馈', route: 'ppts.feedback', permission: '学大反馈管理（学员视图-家校互动）,学大反馈管理（学员视图-家校互动）-本部门,学大反馈管理（学员视图-家校互动）-本校区,学大反馈管理（学员视图-家校互动）-本分公司,学大反馈管理（学员视图-家校互动）-全国' }
            ]
        }, {
            name: '缴费管理', icon: 'credit-card',
            permission: '缴费单管理（缴费单详情）,缴费单管理（缴费单详情）-本部门,缴费单管理（缴费单详情）-本校区,缴费单管理（缴费单详情）-本分公司,缴费单管理（缴费单详情）-全国,收款管理,收款管理-本部门,收款管理-本校区,收款管理-本分公司,收款管理-全国,退费管理,退费管理-本部门,退费管理-本校区,退费管理-本分公司,退费管理-全国',
            children: [
               { name: '缴费单管理', route: 'ppts.accountCharge-query', permission: '缴费单管理（缴费单详情）,缴费单管理（缴费单详情）-本部门,缴费单管理（缴费单详情）-本校区,缴费单管理（缴费单详情）-本分公司,缴费单管理（缴费单详情）-全国' },
               { name: '收款管理', route: 'ppts.accountChargePayment-query', permission: '收款管理,收款管理-本部门,收款管理-本校区,收款管理-本分公司,收款管理-全国' },
               { name: '退费管理', route: 'ppts.accountRefund-query', permission: '退费管理,退费管理-本部门,退费管理-本校区,退费管理-本分公司,退费管理-全国' }
            ]
        }, {
            name: '订购管理', icon: 'shopping-bag',
            permission: '订购管理列表（订购单详情）,订购管理列表（订购单详情）-本部门,订购管理列表（订购单详情）-本校区,订购管理列表（订购单详情）-本分公司,订购管理列表（订购单详情）-全国,退订管理列表,退订管理列表-本部门,退订管理列表-本校区,退订管理列表-本分公司,退订管理列表-全国',
            children: [
               { name: '订购列表', route: 'ppts.purchase', permission: '订购管理列表（订购单详情）,订购管理列表（订购单详情）-本部门,订购管理列表（订购单详情）-本校区,订购管理列表（订购单详情）-本分公司,订购管理列表（订购单详情）-全国' },
               { name: '退订列表', route: 'ppts.unsubscribe', permission: '退订管理列表,退订管理列表-本部门,退订管理列表-本校区,退订管理列表-本分公司,退订管理列表-全国' }
            ]
        }, {
            name: '排课管理', icon: 'calendar',
            permission: '客户课表管理列表（打印课表）,客户课表管理列表（打印课表）-本部门,客户课表管理列表（打印课表）-本校区,客户课表管理列表（打印课表）-本分公司,客户课表管理列表（打印课表）-全国,按学员排课-本校区,按教师排课-本校区,教师课表,教师课表-本部门,教师课表-本校区,教师课表-本分公司,教师课表-全国,班级管理（按钮查看班级、按钮查看学生）-本校区,班级管理（按钮查看班级、按钮查看学生）-本分公司,班级管理（按钮查看班级、按钮查看学生）-全国,教师上课记录（打印上课记录）,教师上课记录（打印上课记录）-本部门,教师上课记录（打印上课记录）-本校区,教师上课记录（打印上课记录）-本分公司,教师上课记录（打印）-全国,按学员排课-本校区,按教师排课-本校区,班级管理（按钮查看班级、按钮查看学生）-本校区,班级管理（按钮查看班级、按钮查看学生）-本分公司,班级管理（按钮查看班级、按钮查看学生）-全国,教师课时量查询,教师上课记录（打印上课记录）,教师上课记录（打印上课记录）-本部门,教师上课记录（打印上课记录）-本校区,教师上课记录（打印上课记录）-本分公司,教师上课记录（打印）-全国,教师查看-教师课表',
            children: [
               { name: '客户课表', route: 'ppts.studentcourse', permission: '客户课表管理列表（打印课表）,客户课表管理列表（打印课表）-本部门,客户课表管理列表（打印课表）-本校区,客户课表管理列表（打印课表）-本分公司,客户课表管理列表（打印课表）-全国' },
               { name: '按学员排课', route: 'ppts.schedule', permission: '按学员排课-本校区' },
               { name: '按教师排课', route: 'ppts.schedule-tchasgmt', permission: '按教师排课-本校区' },
               { name: '教师课表打印', route: 'ppts.teachercourse', permission: '教师课表,教师课表-本部门,教师课表-本校区,教师课表-本分公司,教师课表-全国' },
               { name: '班级管理', route: 'ppts.classgroup', permission: '班级管理（按钮查看班级、按钮查看学生）-本校区,班级管理（按钮查看班级、按钮查看学生）-本分公司,班级管理（按钮查看班级、按钮查看学生）-全国' },
               { name: '教师课时量', route: 'ppts.classgroupcourse', permission: '教师课时量查询' },
               { name: '教师上课记录', route: 'ppts.teachercourserecord', permission: '教师上课记录（打印上课记录）,教师上课记录（打印上课记录）-本部门,教师上课记录（打印上课记录）-本校区,教师上课记录（打印上课记录）-本分公司,教师上课记录（打印）-全国' },
               { name: '教师个人课表', route: 'ppts.tchcoursepsn', permission: '教师查看-教师课表' }
            ]
        }, {
            name: '产品管理', icon: 'suitcase',
            permission: '产品管理列表（产品详情）,产品管理列表（产品详情）-本校区,产品管理列表（产品详情）-本分公司,产品管理列表（产品详情）-全国',
            children: [
               { name: '产品列表', route: 'ppts.product', permission: '产品管理列表（产品详情）,产品管理列表（产品详情）-本校区,产品管理列表（产品详情）-本分公司,产品管理列表（产品详情）-全国' }
            ]
        }, {
            name: '基础数据', icon: 'gear',
            permission: '字典管理,折扣表管理-本分公司,折扣表管理-全国,综合服务费管理-本分公司,综合服务费管理-全国,买赠表管理-本分公司,买赠表管理-全国,校区维护-本分公司,信息来源维护',
            children: [
               { name: '字典管理', route: 'ppts.dictionary', permission: '字典管理' },
               { name: '折扣表管理', route: 'ppts.discount', permission: '折扣表管理-本分公司,折扣表管理-全国' },
               { name: '综合服务费管理', route: 'ppts.servicefee', permission: '综合服务费管理-本分公司,综合服务费管理-全国' },
               { name: '买赠表管理', route: 'ppts.present', permission: '买赠表管理-本分公司,买赠表管理-全国' },
               { name: '校区维护', route: 'ppts.school', permission: '校区维护-本分公司' },
               { name: '信息来源维护', route: 'ppts.source', permission: '信息来源维护' }
            ]
        }, {
            name: '客服中心', icon: 'microphone',
            permission: '客户服务管理（客服详情）,客户服务管理（客服详情）-本部门,客户服务管理（客服详情）-本校区,客户服务管理（客服详情）-本分公司,客户服务管理（客服详情）-全国',
            children: [
               { name: '客户服务', route: 'ppts.custservice', permission: '客户服务管理（客服详情）,客户服务管理（客服详情）-本部门,客户服务管理（客服详情）-本校区,客户服务管理（客服详情）-本分公司,客户服务管理（客服详情）-全国' }
            ]
        }, {
            name: '综合查询', icon: 'search',
            children: [
              { name: '全部待办查询', route: 'ppts.query-task', permission: '' },
               { name: '学员信息列表查询', route: 'ppts.query-student-list', permission: '' },
               { name: '缴费单查询', route: 'ppts.query-charge', permission: '' },
               { name: '收款列表查询', route: 'ppts.query-payment', permission: '' },
               { name: '退款列表查询', route: 'ppts.query-refund', permission: '' },
               { name: 'POS刷卡列表查询', route: 'ppts.query-posrecord', permission: '' },
               { name: '班组班级列表查询', route: 'ppts.query-class-group', permission: '' },
               { name: '学员课时量查询', route: 'ppts.query-student-course', permission: '' },
               { name: '教师课时量查询', route: 'ppts.query-teacher-course', permission: '' },
               { name: '订购列表查询', route: 'ppts.query-order-list', permission: '' },
               { name: '课时兑换列表查询', route: 'ppts.query-order-exchange', permission: '' },
               { name: '库存单价查询', route: 'ppts.query-order-stock', permission: '' }
            ]
        }]
    };

    // 读取配置文件
    var initConfigSection = function (section) {
        if (section) {
            for (var item in section) {
                ppts.config[item] = section[item];
            }
        }
    }

    var initConfig = function () {
        window.onload = function () {
            //var configData = sessionStorage.getItem('configData');
            //if (!configData) {
            //    var parameters = document.getElementById('configData');
            //    sessionStorage.setItem('configData', parameters.value);
            //    configData = sessionStorage.getItem('configData');
            //    parameters.value = '';
            //}

            if (document.getElementById('configData').value) {
                var config = JSON.parse(document.getElementById('configData').value);

                initConfigSection(config.pptsWebAPIs);

                mcs.app.config.timeStamp = config.timestamp;

                mcs.app.config.mcsComponentBaseUrl = ppts.config.mcsComponentBaseUrl;
                mcs.app.config.pptsComponentBaseUrl = ppts.config.pptsComponentBaseUrl;
            }

            loadAssets();
        };
    };

    var loadAssets = function () {
        mcs.g.loadCss({
            cssFiles: [
                //<!--#bootstrap基础样式-->
                'libs/bootstrap-3.3.5/css/bootstrap',
                //<!--#datetime组件样式-->
                'libs/date-time-3.0.0/css/bootstrap-datetimepicker',
                //<!--#ztree组件样式-->
                'libs/zTree-3.5.22/css/metroStyle/metroStyle',
                //<!--#autocomplete组件样式-->
                'libs/ng-tags-input-3.0.0/ng-tags-input',
                //<!--#font awesome字体样式-->
                'libs/font-awesome-4.5.0/css/font-awesome.min',
                //<!--#ace admin基础样式-->
                'libs/ace-1.3.1/css/ace.min',
                'libs/ace-1.3.1/css/ace-fonts',
                //<!--#blockUI样式-->
                'libs/angular-block-ui-0.2.2/dist/angular-block-ui',
                 //<!--#日程插件的样式-->
                'libs/fullcalendar-2.6.1/fullcalendar',
                //<!--#全局组件样式-->
                'libs/mcs-jslib-1.0.0/component/mcs.component',
                //<!--下拉框样式-->
                'libs/angular-ui-select-0.13.2/dist/select2',
                'libs/angular-dialog-service-5.3.0/dist/dialogs.min',
                //<!--消息提示框-->
                'libs/gritter-1.7.4/jquery.gritter'
            ], localCssFiles: [
                //<!--#网站主样式-->
                'assets/styles/site'
            ]
        });

        mcs.g.loadRequireJs(
            'libs/requirejs-2.1.22/require',
            mcs.app.config.pptsComponentBaseUrl + 'build/require.config.min');
    };

    initConfig();

    return ppts;

})();
