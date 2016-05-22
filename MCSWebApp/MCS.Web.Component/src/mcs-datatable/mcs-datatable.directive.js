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
            templateUrl: mcs.app.config.mcsComponentBaseUrl + '/src/tpl/mcs-datatable.tpl.html ',
            replace: true,
            scope: {

                data: '=',
                retrieveData: '&?',
                pageChanged: '&?',


            },
            link: function($scope, iElm, iAttrs, controller) {

                if (!$scope.data) {
                    return;
                }

                if ($scope.retrieveData) {
                    $scope.retrieveData().then(function() {
                        $scope.reMatchRowsSelected();
                    })
                }


                $scope.selectAll = function() {
                    if ($scope.data.selectAll) {
                        $scope.data.rows.forEach(function(rowSelected) {
                            rowSelected.selected = true;
                            var rowDataSelected = {};
                            $scope.data.keyFields.forEach(function(key) {
                                rowDataSelected[key] = rowSelected[key];
                            });
                            mcs.util.removeByObject($scope.data.rowsSelected, rowDataSelected);
                            $scope.data.rowsSelected.push(rowDataSelected);

                        })
                    } else {
                        $scope.data.rows.forEach(function(rowSelected) {
                            rowSelected.selected = false;
                            var rowDataSelected = {};
                            $scope.data.keyFields.forEach(function(key) {
                                rowDataSelected[key] = rowSelected[key];
                            });
                            mcs.util.removeByObject($scope.data.rowsSelected, rowDataSelected);
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
                    if (callback()) {
                        callback().then(function() {
                            $scope.reMatchRowsSelected();
                        })
                    }


                }

                $scope.reMatchRowsSelected = function() {

                    var selecteds = $scope.data.rowsSelected;
                    var rows = $scope.data.rows;
                    var keys = $scope.data.keyFields;

                    if (selecteds.length == 0 || keys.length == 0 || rows.length == 0) {
                        return;
                    }



                    for (var indexSelected in selecteds) {
                        for (var rowIndex in rows) {

                            var counter = 0;
                            for (var keyIndex in keys) {
                                if (rows[rowIndex][keys[keyIndex]] == selecteds[indexSelected][keys[keyIndex]]) {
                                    counter = counter + 1;
                                }


                            }

                            if (counter == keys.length) {
                                rows[rowIndex].selected = true;
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
                        var results = header.template && header.template.match(reg);
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
                scope.vm = scope.$parent.$parent.vm;

                dataHeaders.forEach(function(header, index) {
                    var td = $compile('<td class="' + header.headerCss +
                        '">' + (header.template || ('<span>{{row.' + header.field + '}}</span>')) + '</td>')(scope);
                    iElement.append(td);
                });

            }
        };
    });
})();
