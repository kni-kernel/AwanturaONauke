angular.
module('winState').
component('winState', {
  templateUrl: "states/win.template.html",

  controller: function WinStateController($http) {

    $http.get("127.0.0.1:8001", {
        
    });
    
  }
});