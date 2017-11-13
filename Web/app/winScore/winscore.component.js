angular.
module('winScore').
component('winScore', {
  templateUrl: "winScore/winscore.template.html",

  controller: function WinStateController($rootScope, $sessionStorage, $scope, $http) {
    
        var gs = $sessionStorage.GameState;
        var self = this;
    
        $rootScope.AoNListen($http, 1000, () => {
          if (!this.init)
            window.location.reload();
          initFromGS($sessionStorage.GameState);
        });
    
        if (gs == null) {
          return;
        }
    
        initFromGS(gs);
    
        function initFromGS(gs) {
            self.init = true;
            self.Class = gs.Teams[gs.CurrentTeam].ClassName;
            self.Score = gs.Licitation.Pool;
    
    
        };
    
      }
});