// Define the `phonecatApp` module


var app = angular.module('AoN', [
  "idleState",
  "questionState",
  "winState",
  "oneState",
  "initState",
  "ngRoute",
  "ngStorage"
]);

angular.
module('AoN').
config(['$locationProvider', '$routeProvider', '$httpProvider',
  function config($locationProvider, $routeProvider, $httpProvider) {

    $locationProvider.hashPrefix('!');

    $routeProvider.
    when('/Init', {
      template: '<init-State></init-State>'
    }).
    when('/Idle', {
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
    });
  }
]);

app.run(function ($rootScope, $interval, $sessionStorage) {
  $rootScope.AoNListen = function ($http, onReceive) {
    $interval(function () {

      function setURL(url) {
        console.log(url);
        if (window.location.hash === url) {
          if (onReceive)
            onReceive();
        } else
          window.location.href = url;

      }

      var ip = window.location.hostname;
      var address = "http://" + ip + ":8002";
      console.log("Moving to " + address);

      function parseResponse(data) {
        console.log(data);
        if (data == null || data.Pool == null) {
          console.log("null!");
          return;
        }
        console.log("received!");
        $sessionStorage.GameState = data;


        if (data == null || data.State == 0)
          setURL("#!/Idle");
        if (data.State == 1)
          setURL("#!/Idle");
        if (data.State == 2)
          setURL("#!/OneOnOne");
        if (data.State == 3)
          setURL("#!/Question");
        if (data.State == 4)
          setURL("#!/Question");
        if (data.State == 5)
          setURL("#!/Hint");
        else
          setURL("#!/Idle");
      }

      $http({
        method: "POST",
        url: address,
      }).then(data => {
        
        parseResponse(data.data);
      }, data => {
        console.log('error');
        //console.log(data);
        //parseResponse(data.data);
      });
    }, 1000); //$rootScope.GameState == null ? 500 : 1000);
  }
});