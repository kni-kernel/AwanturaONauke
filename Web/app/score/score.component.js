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

    $rootScope.AoNListen($http, 1000, () => {
      if (!this.init)
        window.location.reload();
      initFromGS($sessionStorage.GameState);
      //$rootScope.$$phase || $rootScope.$apply();
    });

    if (gs == null) {
      return;
    }
    initFromGS(gs);

    function initFromGS(gs) {
      self.init = true;
      self.Teams = [];
      self.Auctions = [];
      self.isAuction = false;


      for (let i = 0; i < gs.Teams.length; ++i) {

        var team = gs.Teams[i];
        if (team != null && team.isPlaying) {
          var vmTeam = {
            Score: (team.Points > 200 ? team.Points : "-"), //;// + " " + team.Hints,
            Enabled: team.isPlaying,
            Name: team.Name,
            Class: "teamScore " + team.ClassName
          };

          if (gs.State == 1 && team.Points > 200)
            vmTeam.Score -= gs.Licitation.Bid[i];
          self.Teams.push(vmTeam);
        }
      }
      if (gs.State == 0) {
        self.Teams.push({
          Score: gs.Pool,
          Enabled: true,
          Name: "Pula",
          Class: "pool teamScore"
        });
      }

      if (gs.State == 1) {
        self.isAuction = true;
        self.isDone = false;
        var pool = gs.Pool;
        for (let i = 0; i < gs.Teams.length; ++i) {
          var team = gs.Teams[i];
          if (team != null && team.isPlaying) {
            pool += team.Points > 200 ? gs.Licitation.Bid[i] : 0;
            self.Auctions.push({
              Class: "teamAuction centerVerticalFlex centerHorizontalFlex " + team.ClassName,
              Score: team.Points > 200 ? gs.Licitation.Bid[i] : "-"
            });

           for(let i = 0; i < gs.Teams.length; ++i)
           {
             var isDone = true;
             for(let j = 0; j < gs.Teams.length; ++j)
             {
                if(i == j) continue;
                if(gs.Teams[j].isPlaying == false)
                continue;

                if(gs.Licitation.Bid[i] < gs.Teams[j].Points)
                {
                 // console.log(gs.Licitation.Bid[i] + " < " + gs.Teams[j].Points + "(" + i +", " + j + ")");
                  isDone = false;
                  break; 
                }
             }
             if(isDone)
             {
               console.log
                self.isDone = true;
                break;
             }
           }
              
          }
        }
        

          console.log(self.isDone);

        self.Teams.push({
          Score: pool,
          Enabled: true,
          Name: "Pula",
          Class: "pool teamScore"
        });
      } else
        self.isAuction = false;








      self.teamScoreStyle = {
        width: 1.0 / self.Teams.length * 100.0 + "%",
        display: "inline-block"
      }

      self.teamAuctionStyle = {
        width: 1.0 / self.Teams.length * 100.0 + "%",
        display: "inline-flex"
      }

      self.auctionStyle = {
        width: ((1.0 / self.Teams.length * 100.0) * (self.Teams.length - 1)) + "%",
      };
    }



  }
});