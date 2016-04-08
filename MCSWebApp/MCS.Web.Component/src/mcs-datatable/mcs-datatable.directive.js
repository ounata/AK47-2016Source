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
            templateUrl: mcs.app.config.componentBaseUrl + '/src/tpl/mcs-datatable.tpl.html ',
            replace: true,
            scope: {

                data: '=',
                onOrder: '&?',
                pageChanged: '&'

            },
            link: function($scope, iElm, iAttrs, controller) {

                $scope.selectAll = function() {
                    if ($scope.data.selectAll) {
                        $scope.data.rows.forEach(function(row) {
                            row.selected = true;
                        })
                    } else {
                        $scope.data.rows.forEach(function(row) {
                            row.selected = false;
                        })
                    }
                }

                $scope.rowSelect = function(rowSelected) {
                    $scope.data.selectAll = false;
                    if ($scope.data.selection == 'radio') {
                        rowSelected.selected = true;
                        $scope.data.rows.forEach(function(row) {
                            if (rowSelected[$scope.data.keyField] != row[$scope.data.keyField]) {
                                row.selected = false;
                            }
                        })

                    }
                }



                $scope.reorder = function(header, callback) {
                    header.orderDirection = (header.orderDirection == 'asc' || header.orderDirection == undefined) ? 'desc' : 'asc';

                    if (header.sortable) {
                        if ($scope.onOrder && angular.isFunction($scope.onOrder())) {
                            $scope.onOrder({
                                dataField: header.field,
                                sortDirection: header.orderDirection == 'asc' ? 0 : 1
                            });
                        } else {
                            $scope.data.orderBy[0].dataField = header.field;
                            $scope.data.orderBy[0].sortDirection = header.orderDirection == 'asc' ? 0 : 1;
                            callback();
                        }

                    }


                }

                var reg = /click=['"]\w*/ig

                $scope.data.headers.forEach(function(header, index) {

                    if (!header.field) {
                        var results = header.template.match(reg);
                        if (results) {
                            results.forEach(function(r) {
                                var fnName = r.replace('"', '\'').split('\'')[1];
                                $scope[fnName] = $scope.$parent[fnName];

                            })
                        }

                    }
                })

            }
        };
    })

    .directive('mcsDatatableRow', function($compile) {
        return {
            restrict: 'A',
            link: function(scope, iElement, iAttrs) {
                var dataHeaders = scope.$parent.data.headers;
                var rowData = scope.row;


                dataHeaders.forEach(function(header, index) {
                    var td = $compile('<td>' + (header.template || ('<span>{{row.' + header.field + '}}</span>')) + '</td>')(scope);
                    iElement.append(td);
                });

            }
        };
    });
})();
