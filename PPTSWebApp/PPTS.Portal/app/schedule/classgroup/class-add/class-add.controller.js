define([ppts.config.modules.schedule,
        ppts.config.dataServiceConfig.productDataService,
        ppts.config.dataServiceConfig.classgroupDataService],
    function (schedule) {
        schedule.registerController('classAddController', [
            '$scope',
            '$state',
            '$stateParams',
            'classgroupDataService',
            'productDataService',
            'mcsDialogService',
            'gradeFilter',
            function ($scope, $state, $stateParams, classgroupDataService, productDataService, mcsDialogService, gradeFilter) {
                var vm = this;                
                vm.dayOfWeeks_Str = "";

                vm.class = {
                    campusID: "18", //测试
                    productID: $stateParams.id,                    
                    dayOfWeeks: [],
                    assets:[]
                };

                //获取产品信息
                productDataService.getProduct($stateParams.id, function (result) {
                    vm.product = result.product;
                    vm.startTime = vm.convertStartTime(vm.product.startDate);
                    vm.class.grade = vm.product.grade;
                    vm.class.gradeName = gradeFilter(vm.product.grade);                    
                    $scope.$broadcast('dictionaryReady');
                });

                //计算 上课开始日期时间
                vm.convertStartTime = function (dt) {
                    var dt_now = new Date();
                    var resultDT = dt;
                    if (dt_now.getDay() == 1) {
                        var preMonth = getPreMonth(dt_now);
                        resultDT == preMonth < dt ? dt : preMonth;
                    }
                    else {
                        resultDT == new Date(dt_now.getYear(), dt_now.getMonth(), dt_now.getDate(), 0, 0, 0, 0) < dt ? dt : new Date(dt_now.getYear(), dt_now.getMonth(), dt_now.getDate(), 0, 0, 0, 0);
                    }
                    return resultDT;
                }

                //获取上月第一天
                vm.getPreMonth = function (dt) {
                    var year = dt.getYear();
                    var month = dt.getMonth();
                    if (month == 1)
                        return new Date(year - 1, 12, 1, 0, 0, 0, 0);
                    else
                        return new Date(year, month - 1, 1, 0, 0, 0, 0);
                }

                //选择按周的上课时间
                vm.selectDayOfWeeks = function (classForm) {
                    mcsDialogService.create('app/schedule/classgroup/class-add/dayofweek.html', {
                        controller: 'dayOfWeekController',
                        params: { dayOfWeeks: vm.class.dayOfWeeks, form: classForm }
                    }).result.then(function (data) {
                        vm.class.dayOfWeeks = data;
                        vm.dayOfWeeks_Str = convertDayOfWeekNumsToStr(vm.class.dayOfWeeks)
                    }, function () {

                    });
                }

                //星期数组、字符串转换
                function convertDayOfWeekNumsToStr(nums) {
                    var result = [];
                    if (nums && nums.length > 0) {
                        for (var i = 0; i < nums.length; i++) {                            
                            switch (nums[i]) {
                                case '0':
                                    result.push( "日");
                                    break;
                                case '1':
                                    result.push("一");
                                    break;
                                case '2':
                                    result.push( "二");
                                    break;
                                case '3':
                                    result.push( "三");
                                    break;
                                case '4':
                                    result.push("四");
                                    break;
                                case '5':
                                    result.push( "五");
                                    break;
                                case '6':
                                    result.push("六");
                                    break;
                            }
                        }
                    }
                    return result.join("、");
                }

                //选择上课学生
                vm.selectCustomers = function (classForm) {
                    mcsDialogService.create('app/schedule/classgroup/customer-add/customer-add.html', {
                        controller: 'customerAddController',
                        params: {
                            productID: $stateParams.id,
                            assets: vm.class.assets,
                            form: classForm
                        },
                        settings: {
                            size: 'lg'
                        }
                    }).result.then(function (data) {
                        vm.class.assets = data;
                        var cus = [];
                        for (var i = 0; i < data.length; i++) {
                            cus.push(data[i].customerName);
                        }
                        vm.customerNames = cus.join("、");
                    }, function () {

                    });
                }

                //选择上课教师
                vm.selectTeacher = function (classForm) {                    
                    mcsDialogService.create('app/schedule/classgroup/teacher-add/teacher-add.html', {
                        controller: 'teacherAddController',
                        params: {  teacher: vm.teacher ? vm.teacher : [] ,form:classForm},
                        settings: {
                            size: 'lg'
                        }
                    }).result.then(function (data) {
                        vm.teacher = data;
                        if (data && data.length == 1) {
                            vm.class.teacherID = data[0].teacherID;
                            vm.class.teacherCode = data[0].teacherCode;
                            vm.class.teacherName = data[0].teacherName;
                        }

                    }, function () {

                    });
                }

                //选择科目
                vm.selectSubject = function (item, model) {
                    vm.class.subjectName = item.value;
                }

                //保存
                vm.save = function (classForm) {
                    //页面验证
                    if (classForm.$valid) {
                        classgroupDataService.createClass(vm.class, function () {
                            $state.go('ppts.classgroup');
                        });
                    }
                }
                
                //关闭
                vm.close = function () {
                    $state.go('ppts.product');
                }
            }]);
    });