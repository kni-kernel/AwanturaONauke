angular.
module('score').
component('score', {
  templateUrl: "score/score.template.html",

  controller: function ScoreController($rootScope, $sessionStorage, $scope, $http) {
    
    var gs = $sessionStorage.GameState;
    var self = this;
    self.Teams = [];
    self.Auctions = [];
    self.isAuction = false;

    console.log($scope);
    $rootScope.AoNListen($http, () =>  {
      if(!this.init)
        window.location.reload();
        initFromGS($sessionStorage.GameState);
        //$rootScope.$$phase || $rootScope.$apply();
    });

    if(gs == null)
    {
      return;
    }
    initFromGS(gs);
    function initFromGS(gs)
    {
      self.init = true;
      self.Teams = [];
      self.Auctions = [];
      self.isAuction = false;
      
  
      for(let i = 0;i < gs.Teams.length; ++i)
      {
  
        var team = gs.Teams[i];
        if(team != null)
        self.Teams.push({
          Score: team.isPlaying ? team.Points : "-",
          Enabled: team.isPlaying,
          Name: team.Name,
          Class: "teamScore " + team.ClassName
        });
      }
  
      self.Teams.push({
        Score: gs.Pool,
        Enabled: true,
        Name: "Pula",
        Class: "pool teamScore"
      });
  
      if(gs.State == 1)
      {
        self.isAuction = true;
        
        self.Auctions.push({
                Class: "blue teamAuction centerVerticalFlex centerHorizontalFlex",
                Score:5000
            });
        
            self.Auctions.push({
              Class: "green teamAuction centerVerticalFlex centerHorizontalFlex",
              Score:5000
          });
        
          self.Auctions.push({
            Class: "yellow teamAuction centerVerticalFlex centerHorizontalFlex",
            Score:5000
        });
        
        self.Auctions.push({
          Class: "red teamAuction centerVerticalFlex centerHorizontalFlex",
          Score:5000
        });
      }
      else
      self.isAuction = false;
  
      
  
  
   
      
  
  
      self.teamScoreStyle = {
        width: 1.0 / self.Teams.length * 100.0 + "%",
        display:"inline-block"
      }
  
      self.teamAuctionStyle = {
        width: 1.0 / self.Teams.length * 100.0 + "%",
        display:"inline-flex"
      }
  
      self.auctionStyle = {
        width: ((1.0 / self.Teams.length * 100.0) * (self.Teams.length - 1)) + "%",
      };
    }

    
    
  }
});