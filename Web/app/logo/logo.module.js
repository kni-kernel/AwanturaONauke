angular.module('logo', []);

angular.
module('logo').
component('logo', {
    templateUrl: "logo/logo.template.html",

    controller: function LogoController($rootScope, $sessionStorage, $scope, $interval, $timeout) {
        var self = this;
        var state = 0; //0 rising, 1 plateau, 2 - falling
        var temp = 0;
        var current = 0;
        var alpha = 0.0;

        self.kernel =
        {
            opacity: 0.0
        };
        self.wfiis = 
        {
            opacity: 0.0
        };
        self.agh = 
        {
            opacity : 0.0
        };
        
        $timeout(function()
    {
        $interval(function () {
            var alphas = [0.0,0.0,0.0];
                if (state == 0) {
                    alpha += 0.05;
                    if (alpha >= 1.0) {
                        state = 1;
                        alpha = 1.0;
                    }

                }
            if (state == 1) {
                if (temp >= 60) {
                    state = 2;
                    temp = 0;
                } else
                    ++temp;
            }
            if (state == 2) {
                alpha -= 0.05;
                if (alpha <= 0.0) {
                    alpha = 0.0;
                    state = 0;
                    current = (current + 1) % 3;
                }
            }
            var gs = $sessionStorage.GameState;

            if(gs.State !== 2)
                alphas[current] = alpha;

            self.kernel =
            {
                opacity: alphas[0]
            };
            self.wfiis = 
            {
                opacity: alphas[1]
            };
            self.agh = 
            {
                opacity : alphas[2]
            };
            console.log(alphas[0] + ", " + alphas[1] + ", " + alphas[2] + " - " + state);

            
        }, 50);
    }, 3000);

}
});