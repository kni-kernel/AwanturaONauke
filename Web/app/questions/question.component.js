angular.
module('question').
component('question', {
  templateUrl: "questions/question.template.html",

  controller: function QuestionController() {
        this.QuestionNumber = 6;
        this.ToWin = 69666;
        this.Question = "Jaka wiadomość smuci Stańczyka na znanym obrazie Jana Matejki z 1862 roku?	o śmierci Władysława Warneńczyka pod Warną	o utracie Smoleńska na rzecz Rosji	o przegranej z Tatarami bitwie pod Sokalem 	o przegranej husarii pod Żółtymi Wodami?aka wiadomość smuci Stańczyka na znanym obrazie Jana Matejki z 1862 roku?	o śmierci Władysława Warneńczyka pod Warną	o utracie Smoleńska na rzecz Rosji	o przegranej z Tatarami bitwie pod Sokalem 	o przegranej husarii pod Żółtymi Wodami?";
        this.Class = "black";

        this.Time = 60;
        var self = this;

        this.hintEnabled = true;
        this.HintA = "Jan Matejko";
        this.HintB = "Sołtys ze wsi";
        this.HintC = "Czy można powtórzyć pytanie?";
        this.HintD = "Zaraz wracam";
  }
});