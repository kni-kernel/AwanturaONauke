// Define the `phonecatApp` module


var app = angular.module('AoN', [
  "idleState",
  "questionState",
  "winState",
  "oneState",
  "ngRoute"
]);

angular.
module('AoN').
config(['$locationProvider', '$routeProvider', '$httpProvider',
  function config($locationProvider, $routeProvider, $httpProvider) {

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

app.run(function ($rootScope) {
  $rootScope.AoNListen = function ($http) {
    setTimeout(function() {


    var ip = window.location.hostname;
    var address = "http://" + ip + ":8001";
    console.log(address);
    $http({
      method: "POST",
      url: address,

    }).success(function (data) {
      $rootScope.GameState = data;
      //console.log(data);
      
      if (data.State == 0)
        window.location.href = "/#!/Idle";
      if (data.State == 1)
        window.location.href = "/#!/Idle";
      if (data.State == 2)
        window.location.href = "/#!/OneOnOne";
      if (data.State == 3)
        window.location.href = "/#!/Question";
      if (data.State == 4)
        window.location.href = "/#!/Question";
      if (data.State == 5)
       window.location.href= "/#!/Hint";
    });
  }, 1000);
  }
});