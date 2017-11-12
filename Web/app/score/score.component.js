angular.
module('score').
component('score', {
  templateUrl: "score/score.template.html",

  controller: function ScoreController() {

    this.Teams = [];

    this.Pool = 5000;

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

    this.Teams.push({
      Score: 5000,
      Enabled: true,
      Name: "Niebiescy",
      Class: "blue teamScore"
    });

    this.Teams.push({
      Score: 5000,
      Enabled: true,
      Name: "Zieloni",
      Class: "green teamScore"
    });

    this.Teams.push({
      Score: 5000,
      Enabled: true,
      Name: "Żółci",
      Class: "yellow teamScore"
    });

    this.Teams.push({
      Score: 5000,
      Enabled: true,
      Name: "Pula",
      Class: "pool teamScore"
    });

    /*this.Teams.push({
      Score: 5000,
      Enabled: true,
      Name: "Mistrzowie",
      Class: "black teamScore"
    });*/

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
    this.isAuction = true;
    console.log(this.teamWidth);
  }
});