(
function () {
  'use strict';
  mcs.ng
.directive('phone', function () {
  return {
    restrict: 'A',
    link: function (scope, iElement, iAttrs) {

    }
  };
})

.directive('email', function () {

  return {
    restrict: 'A',
    link: function ($scope, iElm, iAttrs, controller) {

    }
  };
})

.directive('personCode', function () {
  return {
    restrict: 'A',
    link: function (scope, iElement, iAttrs) {

    }
  };
});

}

)();