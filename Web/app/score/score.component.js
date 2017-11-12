angular.
module('score').
component('score', {
  templateUrl: "score/score.template.html",

  controller: function ScoreController($rootScope) {
    var gs = $rootScope.GameState;
    console.log(gs);
    this.Teams = [];

    for(let i = 0;i < gs.Teams.length; ++i)
    {

      var team = gs.Teams[i];
      if(team != null)
      this.Teams.push({
        Score: "a",
        Enabled: team.isPlaying,
        Name: team.Name,
        Class: "teamScore " + team.ClassName
      });
    }

    this.Teams.push({
      Score: gs.Pool,
      Enabled: true,
      Name: "Pula",
      Class: "pool teamScore"
    });

    if(gs.State == 1)
    {
      this.isAuction = true;
      this.Auctions = [];
      
          this.Auctions.push({
              Class: "blue teamAuction centerVerticalFlex centerHorizontalFlex",
              Score:5000
          });
      
          this.Auctions.push({
            Class: "green teamAuction centerVerticalFlex centerHorizontalFlex",
            Score:5000
        });
      
        this.Auctions.push({
          Class: "yellow teamAuction centerVerticalFlex centerHorizontalFlex",
          Score:5000
      });
      
      this.Auctions.push({
        Class: "red teamAuction centerVerticalFlex centerHorizontalFlex",
        Score:5000
      });
    }
    else
    this.isAuction = false;

    


 
    


    this.teamScoreStyle = {
      width: 1.0 / this.Teams.length * 100.0 + "%",
      display:"inline-block"
    }

    this.teamAuctionStyle = {
      width: 1.0 / this.Teams.length * 100.0 + "%",
      display:"inline-flex"
    }

    this.auctionStyle = {
      width: ((1.0 / this.Teams.length * 100.0) * (this.Teams.length - 1)) + "%",
    };
    
    console.log(this.teamWidth);
  }
});