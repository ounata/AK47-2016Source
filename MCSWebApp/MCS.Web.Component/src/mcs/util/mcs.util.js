(function () {
    'use strict';

    /*
    * JS 产生一个新的GUID随机数
    */
    mcs.util.newGuid = function () {
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
    mcs.util.merge = function (dictionary) {
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
    mcs.util.selectAll = function (data) {
        var selectedResult = [];
        angular.forEach(data, function (item) {
            selectedResult.push(item.key);
        });
        return selectedResult;
    };

    /*
     * 全部不选中
     */
    mcs.util.unSelectAll = function () {
        return [];
    };

    /*
    * 反选
    */
    mcs.util.inverseSelect = function (data, selectedResult) {
        var temp = selectedResult;
        selectedResult = [];
        angular.forEach(data, function (item) {
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
    mcs.util.buildCascadeDataSource = function (data, result) {
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
    mcs.util.setSelectedItems = function (selected, item, event) {
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
    mcs.util.setDefaultSelected = function (items, key) {
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

    mcs.util.loadDependencies = function (dependencies) {
        return {
            resolver: ['$q', '$rootScope', function ($q, $rootScope) {
                var defered = $q.defer();

                require(dependencies, function () {
                    $rootScope.$apply(function () {
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
    mcs.util.loadRoute = function ($stateProvider, route) {
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
    mcs.util.loadDefaultRoute = function ($stateProvider, $urlRouterProvider, defaultRoute) {
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
    mcs.util.configProvider = function (ngModule, $controllerProvider, $compileProvider, $filterProvider, $provide) {
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
    mcs.util.configInterceptor = function ($httpProvider, $sceDelegateProvider, interceptors) {
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