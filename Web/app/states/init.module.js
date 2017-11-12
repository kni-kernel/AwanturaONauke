angular.module('initState', []);

var app = 

angular.
module('initState').
component('initState', {
  templateUrl: "states/init.template.html",

  controller: function initStateController($http, $rootScope, $scope) {
    console.log('init');

  }
});