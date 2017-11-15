angular.module('hint', []);

angular.
module('hint').
component('hint', {
    templateUrl: "hint/hint.template.html",

    controller: function LogoController($rootScope, $sessionStorage, $scope, $interval, $timeout, $http) {
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
            self.Points = gs.Teams[gs.CurrentTeam].Points - gs.Licitation.Bid[gs.CurrentTeam];
            self.HintPrice = gs.HintPrice;
            
        };
    
    }

});