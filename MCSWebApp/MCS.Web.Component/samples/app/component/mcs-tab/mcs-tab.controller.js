(function(){
'use strict';
 angular.module('app.component').controller('MCSTabController',function($scope,$state){
  var vm = this;
    $scope.vm = vm;

    vm.tabs = [{
      title: 'one',
      active: true
    }, {
      title: 'two',
      active: false
    }];

  

    vm.changeTab = function(tab) {
        tab.active = tab.active ? false : true;
        vm.tabs[0].active = true;
    }

    if ($state.includes('tab.one') && !vm.tabs[0].active) {
      $state.go('tab.one');
    }

    if ($state.includes('tab.two') && !vm.tabs[1].active) {
      $state.go('tab.two');
    }

   
 
 });

})();