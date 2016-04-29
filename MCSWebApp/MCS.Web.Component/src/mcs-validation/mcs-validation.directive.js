(
    function() {
        'use strict';
        mcs.ng.directive('number', function() {

            return {
                restrict: 'A',
                require: 'ngModel',

                link: function($scope, iElm, iAttrs, ngCtrl) {

                    var precision = parseInt(iAttrs.precision);
                    var float = iAttrs.float;
                    var numberLength = parseInt(iAttrs.numberLength);
                    var positiveInteger = iAttrs.positiveInteger;
                    var max = parseFloat(iAttrs.maxnum);
                    var min = parseFloat(iAttrs.minnum);

                    var p = /^[1-9]+[0-9]*$/;
                    var f = /^[0-9]+.?[0-9]*$/;
                    var l = eval('/^[0-9]+.[0-9]{' + precision + '}$/');



                    function checkVal(val) {
                        if (!val || val.length === 0) {
                            return;
                        }
                    }

                    iElm.bind('blur', function(event) {
                        var val = parseFloat(iElm.val());
                        checkVal(val);
                        if (!angular.isNumber(val)) {
                            $scope.$apply(ngCtrl.$setValidity('number', false));


                        } else {
                            $scope.$apply(ngCtrl.$setValidity('number', true));
                        }

                        if (positiveInteger) {
                            if (p.test(val)) {
                                $scope.$apply(ngCtrl.$setValidity('positiveInteger', true));

                            } else {
                                $scope.$apply(ngCtrl.$setValidity('positiveInteger', false));

                            }
                        }

                        if (numberLength != NaN) {
                            if (val.toString().length > numberLength) {
                                $scope.$apply(ngCtrl.$setValidity('numberLength', false));

                            } else {
                                $scope.$apply(ngCtrl.$setValidity('numberLength', true));

                            }
                        }

                        if (float) {
                            if (f.test(val)) {
                                $scope.$apply(ngCtrl.$setValidity('float', true));


                            } else {
                                $scope.$apply(ngCtrl.$setValidity('float', false));

                            }

                        }

                        if (precision != NaN) {
                            if (l.test(val)) {
                                $scope.$apply(ngCtrl.$setValidity('precision', true));

                            } else {
                                $scope.$apply(ngCtrl.$setValidity('precision', false));

                            }
                        }
                        if (max != NaN) {
                            if (parseFloat(val) > max) {
                                $scope.$apply(ngCtrl.$setValidity('max', false));

                            } else {
                                $scope.$apply(ngCtrl.$setValidity('max', true));

                            }
                        }

                        if (min != NaN) {

                            if (parseFloat(val) < min) {
                                $scope.$apply(ngCtrl.$setValidity('min', false));

                            } else {
                                $scope.$apply(ngCtrl.$setValidity('min', true));

                            }
                        }


                    });



                }
            };
        })

        .directive('phone', function() {

            return {
                require: 'ngModel',
                restrict: 'A',
                link: function($scope, iElm, iAttrs, ngCtrl) {
                    var reg = /(^1[34578]\d{9}$)|(^\d{3,4}-\d{7,8}-\d{1,5}$)|(^\d{3,4}-\d{7,8}$)/;



                    iElm.bind('blur', function(event) {
                        var val = iElm.val().trim();
                        if (val == undefined || val.length == 0) {
                            return;
                        }

                        if (val.length < 19 && reg.test(val)) {
                            $scope.$apply(ngCtrl.$setValidity('phone', true));

                        } else {

                            $scope.$apply(ngCtrl.$setValidity('phone', false));

                        }

                    });



                }
            };
        })

        .directive('identityCard', function() {

            return {
                require: 'ngModel',
                restrict: 'A',
                link: function($scope, iElm, iAttrs, ngCtrl) {
                    var reg = /^(\d{18,18}|\d{15,15}|\d{17,17}x|\d{17,17}X)$/;

                    iElm.bind('blur', function(event) {
                        var val = iElm.val().trim();

                        if (val.length == 18 && reg.test(val)) {
                            $scope.$apply(ngCtrl.$setValidity('identityCard', true));

                        } else {
                            $scope.$apply(ngCtrl.$setValidity('identityCard', false));
                        }
                    });

                }
            };
        });

    }

)();
