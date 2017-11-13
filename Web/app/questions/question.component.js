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

    initFromGS(gs);

    function initFromGS(gs) {
        self.init = true;
        var question = gs.Question;
        self.ToWin = gs.Licitation.Pool;
        self.Question = question.Content;
        var hints = [];
        hints.push(question.Tip1);
        hints.push(question.Tip2);
        hints.push(question.Tip3);
        hints.push(question.Tip4);
        

        self.hintEnabled = gs.State == 4;
        self.HintA = hints[0];
        self.HintB = hints[1];
        self.HintC = hints[2];
        self.HintD = hints[3];

        self.Time = gs.Timer;
        if(self.Time <= 0)
        self.Time = "Koniec czasu!";


    };
    this.QuestionNumber = 6;
    this.Class = "black";

  }
});