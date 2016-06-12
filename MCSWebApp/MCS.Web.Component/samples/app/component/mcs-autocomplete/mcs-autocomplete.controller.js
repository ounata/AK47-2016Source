(function() {
    'use strict';
    angular.module('app.component').controller('MCSAutoCompleteController', [
        '$scope', 'autoCompleteDataService', '$q',

        function($scope, autoCompleteDataService, $q) {
            var vm = this;
            $scope.vm = vm;

            vm.teachers = [{
                "teacherId": "1",
                "name": "tom"
            }];

            vm.tags = [{
                teacherId: '1',
                name: 'jack'
            }, {
                teacherId: '2',
                name: 'tom'
            }, {
                teacherId: '3',
                name: 'json'
            }];

            vm.students = [];

            vm.queryTeacherList = function(query) {
                var result = [];

                vm.tags.forEach(function(item) {
                    if (item.name.indexOf(query) > -1) {
                        result.push(item);
                    }

                });

                return result;
            }



            vm.queryStudentList = function(query) {


                return autoCompleteDataService.query('http://localhost/MCSWebApp/MCS.Web.API/api/UserGraph/query', JSON.stringify({
                    searchTerm: query,
                    maxCount: 10,
                    listMark: 15
                }));



            };

        }
    ]);
})();
