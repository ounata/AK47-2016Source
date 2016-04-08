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
                }

                $scope.pageChange = function(callback) {
                    callback();
                    $scope.reMatchRowsSelected();
                }

                $scope.reMatchRowsSelected = function() {

                    if ($scope.data.rowsSelected.length == 0) {
                        return;
                    }

                    for (var rowDataSelected in $scope.data.rowsSelected) {
                        for (var row in $scope.data.rows) {
                            for (var key in $scope.data.keyFields) {
                                if (row[key] !== rowDataSelected[key]) {
                                    row.selected = false;
                                    break;
                                }

                                if (row.selected == undefined) {
                                    row.selected = true;
                                    break;
                                }
                            }
                        }
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
                            $scope.reMatchRowsSelected();
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
                    var td = $compile('<td class="' + header.headerCss +
                        '">' + (header.template || ('<span>{{row.' + header.field + '}}</span>')) + '</td>')(scope);
                    iElement.append(td);
                });

            }
        };
    });
})();
