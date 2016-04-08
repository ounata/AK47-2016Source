(function () {
    'use strict';

    /**
     * 两个对象判等
     *
     */
    mcs.util.isObjectsEqual = function (a, b) {

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

   
    /**
     * 删除数组中指定元素
     *
     */
    mcs.util.removeByValue = function (_array, val) {
        for (var i = 0; i < _array.length; i++) {
            if (this[i] == val) {
                _array.splice(i, 1);
                break;
            }
        }
    };


    /**
     * 从对象集合中删除指定对象
     *
     */
    mcs.util.removeByObject = function (_array, obj) {
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
     * 字典对象合并
     */
    mcs.util.merge = function(dictionary) {
        for (var item in dictionary) {
            mcs.app.dict[item] = dictionary[item];
        }
    };

    /*
    * 对象列表映射成字典
    * data 对象数组, kvp 键值对{key,value},category 所属类别
    */
    mcs.util.mapping = function (data, kvp, category) {
        if (!data || !kvp.key || !kvp.value) return;
        if (category == undefined) {
            var result = [];
            for (var index in data) {
                result.push({
                    key: data[index][kvp.key],
                    value: data[index][kvp.value]
                });
            }
            return result;
        } else {
            category = category.indexOf('c_codE_ABBR_') == 0 ? category : 'c_codE_ABBR_' + category;
            var result = {};
            result[category] = [];
            for (var index in data) {
                result[category].push({
                    key: data[index][kvp.key],
                    value: data[index][kvp.value]
                });
            }
            return result;
        }
    };

    /*
    * 对象复制
    */
    mcs.util.clone = function (obj) {
        if (typeof (obj) != 'object') return obj;
        if (obj == null) return obj;
        var newObject = new Object();
        for (var i in obj)
            newObject[i] = mcs.util.clone(obj[i]);
        return newObject;
    };

    /*
    * 从对象数组中查找某属性值对应的索引
    */
    mcs.util.indexOf = function (data, key, value) {
        if (!data || !data.length) return -1;
        for (var index in data) {
            if (!data[index][key]) return -1;
            if (data[index][key] == value) {
                return index;
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
    mcs.util.setSelectedItems = function(selected, item, event) {
        var index = selected.indexOf(item.key);
        if (event.target.checked) {
            if (index === -1) {
                selected.push(item.key);
            }
        } else {
            if (index !== -1) {
                selected.splice(index, 1);
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
    mcs.util.getDictionaryItemValue = function (items, key) {
        if (key == 0) return '';
        if (!items || !items.length || !key) return key;
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
     * 加载单独路由
     * $stateProvider, 路由提供者服务
     * route, 当前需要加载的路由
     */
    mcs.util.loadRoute = function($stateProvider, route) {
        $stateProvider.state(route.name, {
            url: route.url,
            templateUrl: route.templateUrl,
            controller: route.controller,
            controllerAs: route.controllerAs || 'vm',
            //ncyBreadcrumb: defaultRoute.breadcrumb,
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
            //ncyBreadcrumb: defaultRoute.breadcrumb,
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

    /**
     * 配置应用的拦截器以及设置白名单
     */
    mcs.util.configInterceptor = function($httpProvider, $sceDelegateProvider, interceptors) {
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
            mcs.app.config.componentBaseUrl + '**'
        ]);
    };

    return mcs.util;
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
        var reChorme = new RegExp("Chrome/(\\d+\\.\\d+(?:\\.\\d+\\.\\d+))?");
        reChorme.test(sUserAgent);
        _browser.version = parseFloat(RegExp['$1']);
        _browser.chrome = true;
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
    if (isIE) {
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
        _browser.mozilla = true;
    }

    return {
        version: _browser.version,
        get: function () {
            return _browser;
        }
    };
}();


// 调用
//var browser = mcs.browser.get();
//console.info("broswer.version: ", browser.version);
//console.info("broswer.msie is ", browser.msie);
//console.info("broswer.safari is ", browser.safari);
//console.info("broswer.opera is ", browser.opera);
//console.info("broswer.mozilla is ", browser.mozilla);
//console.info("broswer.chrome is ", browser.chrome);
//console.info("broswer.konqueror is ", browser.konqueror);