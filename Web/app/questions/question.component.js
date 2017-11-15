angular.
module('question').
component('question', {
  templateUrl: "questions/question.template.html",

  controller: function QuestionController($rootScope, $sessionStorage, $scope, $http) {

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

    function shuffle(array) {
      var currentIndex = array.length, temporaryValue, randomIndex;
    
      // While there remain elements to shuffle...
      while (0 !== currentIndex) {
    
        // Pick a remaining element...
        randomIndex = Math.floor(Math.random() * currentIndex);
        currentIndex -= 1;
    
        // And swap it with the current element.
        temporaryValue = array[currentIndex];
        array[currentIndex] = array[randomIndex];
        array[randomIndex] = temporaryValue;
      }
    
      return array;
    }
    

    initFromGS(gs);

    function initFromGS(gs) {
      self.init = true;
      self.FileName = "";
      var question = gs.Question;
      self.ToWin = gs.Licitation.Pool;
      self.Question = question.Content;
      if(question.FileName)
        self.FileName = "images/" + question.FileName;
      self.MasterEnabled = false;
      if($rootScope.master === true)
      {
        console.log("Master on!");
        self.MasterEnabled = true;
        self.Answer = question.Tip1;
      }
      if(self.hintEnabled !== true)
      {
        var hints = [];
        hints.push(question.Tip1);
        hints.push(question.Tip2);
        hints.push(question.Tip3);
        hints.push(question.Tip4);

        hints = shuffle(hints);

        self.hintEnabled = gs.State == 4;
        self.HintA = hints[0];
        self.HintB = hints[1];
        self.HintC = hints[2];
        self.HintD = hints[3];
      }

      self.Time = gs.Timer;
      if (self.Time <= 0)
        self.Time = "Koniec czasu!";
      self.Class = gs.Teams[gs.CurrentTeam].ClassName;

      self.QuestionNumber = gs.QuestionCount;

    };

    self.fullSize = false;
    self.imageStyle = {};
    self.divStyle = {};
    $scope.onImageClick = function()
    {
      console.log("click!" + self.fullSize);
      if(self.fullSize == false)
      {
        self.divStyle =
        {
        };
        self.imageStyle = 
        {
          position: "absolute",
          top: "0",
          "z-index" : 999,
          height: "100vh",
          "max-height": "100vh",
          "max-width" : "100vw",
          "margin" : 0
          
        };
      }
      else
      {
        self.imageStyle = {};
        self.divStyle = {};
      }
      self.fullSize = !self.fullSize;
    }


  }
});