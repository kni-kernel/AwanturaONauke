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
        if(team != null && team.isPlaying)
        self.Teams.push({
          Score: (team.Points > 0 ? team.Points : "-"),//;// + " " + team.Hints,
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

        for(let i = 0;i < gs.Teams.length; ++i)
        {
          var team = gs.Teams[i];
          if(team != null && team.isPlaying)
          self.Auctions.push({
            Class: "teamAuction centerVerticalFlex centerHorizontalFlex " + team.ClassName,
            Score:gs.Licitation.Bid[i]
        });
        }
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