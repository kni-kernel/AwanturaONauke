angular.
module('oneOnOne').
component('oneOnOne', {
  templateUrl: "OneOnOne/OneOnOne.template.html",

  controller: function OneOnOneController($rootScope, $sessionStorage, $scope, $http) {
    
        var gs = $sessionStorage.GameState;
        var self = this;
        //Name Class enabled disabled
        this.Categories = [];
        
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
            self.Categories = [];
            self.init = true;

            for (var property in gs.OneOnOneCategories) {
              if (gs.OneOnOneCategories.hasOwnProperty(property)) {
                var cat =
                {
                  Key : property,
                  Value : gs.OneOnOneCategories[property]
                };
                var item = 
                {
                    Name: cat.Key,
                    Class: (cat.Value == "true" || cat.Value == "True" || cat.Value == true) ? "enabled" :"disabled"
                };
                self.Categories.push(item);
              }
          }
            
    
        };
    
      }
});