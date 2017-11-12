// Define the `phonecatApp` module
angular.module('AoN', [
    "idleState",
    "questionState",
    "winState",
    "oneState",
    "ngRoute"
]);

angular.
module('AoN').
config(['$locationProvider', '$routeProvider',
  function config($locationProvider, $routeProvider) {
    $locationProvider.hashPrefix('!');

    $routeProvider.
      when('/idle', {
        template: '<idle-State></idle-State>'
      }).
      when('/OneOnOne', {
        template: '<one-State></one-State>'
      }).
      when('/Question', {
        template: '<question-State></question-State>'
      }).
      when('/Win', {
        template: '<win-State></win-State>'
      }).
      otherwise('/idle');
  }
]);