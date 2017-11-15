// Define the `phonecatApp` module


var app = angular.module('AoN', [
  "idleState",
  "questionState",
  "winState",
  "oneState",
  "initState",
  "hintState",
  "ngRoute",
  "ngStorage",
  "logo"
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
    when('/Hint', {
      template: '<hint-State></hint-State>'
    }).
    when('/Win', {
      template: '<win-State></win-State>'
    });
  }
]);

app.run(function ($rootScope, $timeout, $sessionStorage) {
  $rootScope.onReceive = function () {};
  $rootScope.started = false;
  $rootScope.AoNListen = function ($http, timeoutTime, onReceive) {
    $rootScope.onReceive = onReceive;
    if ($rootScope.started === false) {
      $rootScope.AoNListenMy($http, timeoutTime, onReceive);
      $rootScope.started = true;
    }

  }


  $rootScope.AoNListenMy = function ($http, timeoutTime, onReceive) {
    $timeout(function () {

      function setURL(url) {
        if (window.location.hash === url) {
          if (onReceive)
            onReceive();
        } else
          window.location.href = url;
      }

      var ip = window.location.hostname;
      var address = "http://" + ip + ":8002";

      function parseResponse(data) {
        //  console.log(data);
        if (data == null || data.Pool == null) {
          return;
        }
        $sessionStorage.GameState = data;
        if (data != null) {
          if (data.State == 0)
            setURL("#!/Idle");
          if (data.State == 1)
            setURL("#!/Idle");
          if (data.State == 2 && $rootScope.master === false)
            setURL("#!/OneOnOne");
          if (data.State == 3 || $rootScope.master === true)
            setURL("#!/Question");
          if (data.State == 4)
            setURL("#!/Question");
          if (data.State == 5)
            setURL("#!/Win");
          if (data.State == 6)
            setURL("#!/Hint");
        }
      }

      $http({
        method: "POST",
        url: address,
        timeout: 4000
      }).then(data => {
        parseResponse(data.data);
        $rootScope.AoNListenMy($http, 900, $rootScope.onReceive);
      }, data => {
        console.log('error');
        $rootScope.AoNListenMy($http, 1500, $rootScope.onReceive);
        //console.log(data);
        //parseResponse(data.data);
      });
    }, 1000); //$rootScope.GameState == null ? 500 : 1000);
  }
});