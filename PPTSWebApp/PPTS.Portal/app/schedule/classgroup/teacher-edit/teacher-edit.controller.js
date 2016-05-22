define([ppts.config.modules.schedule],
    function (schedule) {
        schedule.registerController('teacherEditController', [
            '$scope',
            'data',
            'dataSyncService',
            '$uibModalInstance',
            'classgroupDataService',
            'mcsDialogService',
            function ($scope, data, dataSyncService, $uibModalInstance, classgroupDataService, mcsDialogService) {
                var vm = this;
                vm.data = data;
                vm.class = {
                    classID: data.classID
                }

                vm.startLessons = [];
                for (var i = data.index; i <= data.lessonCount; i++) {
                    vm.startLessons.push({ key: i, value: '第' + i + '课次' });                    
                }

                dataSyncService.injectDictData({                    
                    c_codE_ABBR_Order_StartLessons: vm.startLessons
                });

                //选择开始课次
                vm.selectStartLesson = function (item) {
                    vm.endLessons = [];
                    for (var i = item.key; i <= data.lessonCount; i++) {
                        vm.endLessons.push({ key: i, value: '第' + i + '课次' });
                    }
                    dataSyncService.injectDictData({
                        c_codE_ABBR_Order_EndLessons: vm.endLessons
                    });
                    $scope.$broadcast('dictionaryReady');
                }

                //选择上课教师
                vm.selectTeacher = function (classForm) {
                    mcsDialogService.create('app/schedule/classgroup/teacher-add/teacher-add.html', {
                        controller: 'teacherAddController',
                        params: { campusID: 18, teacher: vm.teacher ? vm.teacher : [], form: classForm },//测试校区
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

                //关闭窗口
                vm.close = function () {
                    $uibModalInstance.dismiss('Canceled');
                };

                //保存
                vm.save = function () {
                    classgroupDataService.editTeacher(vm.class, function () {
                        $uibModalInstance.close();
                    }, function () {
                    });
                }
            }]);
    });