angular.
module('oneOnOne').
component('oneOnOne', {
  templateUrl: "OneOnOne/OneOnOne.template.html",

  controller: function OneOnOneController() {
        

    this.Categories = [];

    this.Categories.push({
        Name: "Mleko",
        Class: "enabled"
    });

    this.Categories.push({
        Name: "Pomarańcze",
        Class: "enabled",
    });

    this.Categories.push({
        Name: "Gruszki",
        Class: "enabled",
    });

    this.Categories.push({
        Name: "Groszek",
        Class: "disabled",
    });

    this.Categories.push({
        Name: "Banany",
        Class: "enabled",
    });

    this.Categories.push({
        Name: "Herbata",
        Class: "enabled",
    });

    this.Categories.push({
        Name: "Piwo",
        Class: "enabled",
    });

    this.Categories.push({
        Name: "Kawa",
        Class: "disabled",
    });

   

    this.Categories.push({
        Name: "Wege",
        Class: "disabled",
    });
  }
});