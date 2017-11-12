angular.
module('questionState').
component('questionState', {
  templateUrl: "states/question.template.html",

  controller: function IdleStateController($http, $rootScope) {
    $rootScope.AoNListen($http);
  }
});