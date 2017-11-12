angular.
module('oneState').
component('oneState', {
  templateUrl: "states/one.template.html",

  controller: function OneStateController($http, $rootScope) {
    $rootScope.AoNListen($http);
  }
});