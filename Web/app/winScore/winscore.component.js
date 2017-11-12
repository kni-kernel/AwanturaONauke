angular.
module('winScore').
component('winScore', {
  templateUrl: "winScore/winscore.template.html",

  controller: function WinStateController($http) {
    this.Class = "blue";
    this.Score = 666;
  }
});