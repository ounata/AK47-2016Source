(function() {
    'use strict';
    angular.module('app.component').controller('MCSAutoCompleteController', [
        '$scope', 'autoCompleteDataService', '$q',

        function($scope, autoCompleteDataService, $q) {
            var vm = this;
            $scope.vm = vm;



            vm.teachers = [];

            vm.text = '';

            vm.tagBlur = function() {
                vm.text = '';
            };

            vm.filetr = function(dataSet) {
                dataSet.forEach(function(data) {
                    data.name += "haha";
                });

                return dataSet;

            };

            vm.tags = [{
                teacherId: '1',
                name: 'jack',
                fullPath: 'name'
            }, {
                teacherId: '2',
                name: 'tom'
            }, {
                teacherId: '3',
                name: 'json',
                fullPath: 'json'
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

            vm.tagChanged = function($tag) {
                if (vm.teachers.length > 1) {
                    vm.teachers.shift();
                }
            }



            vm.queryStudentList = function(query) {



                return autoCompleteDataService.query('http://localhost/MCSWebApp/MCS.Web.API/api/UserGraph/query', JSON.stringify({
                    searchTerm: query,
                    maxCount: 10,
                    listMark: 15
                }));



            };

            vm.load = function() {
                $(".js-data-example-ajax").select2({
                    ajax: {
                        url: "https://api.github.com/search/repositories",
                        dataType: 'json',
                        delay: 250,
                        data: function(params) {
                            return {
                                q: params.term, // search term
                                page: params.page
                            };
                        },
                        processResults: function(data, params) {
                            // parse the results into the format expected by Select2
                            // since we are using custom formatting functions we do not need to
                            // alter the remote JSON data, except to indicate that infinite
                            // scrolling can be used
                            params.page = params.page || 1;

                            return {
                                results: data.items,
                                pagination: {
                                    more: (params.page * 30) < data.total_count
                                }
                            };
                        },
                        cache: true
                    },
                    escapeMarkup: function(markup) {
                        return markup;
                    }, // let our custom formatter work
                    minimumInputLength: 1 //,
                        // templateResult: formatRepo, // omitted for brevity, see the source of this page
                        // templateSelection: formatRepoSelection // omitted for brevity, see the source of this page
                });

            };

            //vm.load();
        }
    ]);
})();
