/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, {
/******/ 				configurable: false,
/******/ 				enumerable: true,
/******/ 				get: getter
/******/ 			});
/******/ 		}
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports, __webpack_require__) {

//libs
//require("express");
// require("./bower_components/fs/lib/fs.js")
// require("../node_modules/steal-tools/index.js")
// require("./bower_components/express/index.js")



__webpack_require__(1);
__webpack_require__(2);
__webpack_require__(3);
__webpack_require__(5);
__webpack_require__(7);
__webpack_require__(9);
__webpack_require__(11);
__webpack_require__(13);
__webpack_require__(15);
__webpack_require__(17);


/***/ }),
/* 1 */
/***/ (function(module, exports) {

// Define the `phonecatApp` module


var app = angular.module('AoN', [
  "idleState",
  "questionState",
  "winState",
  "oneState",
  "initState",
  "ngRoute",
  "ngStorage"
]);

angular.
module('AoN').
config(['$locationProvider', '$routeProvider', '$httpProvider',
  function config($locationProvider, $routeProvider, $httpProvider) {

    $locationProvider.hashPrefix('!');

    $routeProvider.
    when('/Init', {
      template: '<init-State></init-State>'
    }).
    when('/Idle', {
      template: '<idle-State></idle-State>'
    }).
    when('/OneOnOne', {
      template: '<one-State></one-State>'
    }).
    when('/Question', {
      template: '<question-State></question-State>'
    }).
    when('/Win', {
      template: '<win-State></win-State>'
    });
  }
]);

app.run(function ($rootScope, $timeout, $sessionStorage) {
  $rootScope.AoNListen = function ($http, timeoutTime, onReceive) {
    $timeout(function () {

      function setURL(url) {
        if (window.location.hash === url) {
          if (onReceive)
            onReceive();
        } else
          window.location.href = url;

      }

      var ip = window.location.hostname;
      var address = "http://" + ip + ":8002";

      function parseResponse(data) {
        console.log(data);
        if (data == null || data.Pool == null) {
          return;
        }
        $sessionStorage.GameState = data;

        if (data != null) {
          if (data.State == 0)
            setURL("#!/Idle");
          if (data.State == 1)
            setURL("#!/Idle");
          if (data.State == 2)
            setURL("#!/OneOnOne");
          if (data.State == 3)
            setURL("#!/Question");
          if (data.State == 4)
            setURL("#!/Question");
          if (data.State == 5)
            setURL("#!/Win");
        }
      }

      $http({
        method: "POST",
        url: address,
        timeout: 4000
      }).then(data => {
        parseResponse(data.data);
        $rootScope.AoNListen($http, 1500, onReceive);
      }, data => {
        console.log('error');
        $rootScope.AoNListen($http, 2500, onReceive);
        //console.log(data);
        //parseResponse(data.data);
      });
    }, 1000); //$rootScope.GameState == null ? 500 : 1000);
  }
});

/***/ }),
/* 2 */
/***/ (function(module, exports) {

angular.module('initState', []);

var app = 

angular.
module('initState').
component('initState', {
  templateUrl: "states/init.template.html",

  controller: function initStateController($http, $rootScope, $scope) {
    console.log('init');

  }
});

/***/ }),
/* 3 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('idleState', ["score"]);

__webpack_require__(4);

/***/ }),
/* 4 */
/***/ (function(module, exports) {

var app = 

angular.
module('idleState').
component('idleState', {
  templateUrl: "states/idle.template.html",

  controller: function IdleStateController($http, $rootScope, $scope) {
    
  }
});

/***/ }),
/* 5 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('questionState', ["question"]);

__webpack_require__(6);

/***/ }),
/* 6 */
/***/ (function(module, exports) {

angular.
module('questionState').
component('questionState', {
  templateUrl: "states/question.template.html",

  controller: function IdleStateController($http, $rootScope) {
    $rootScope.AoNListen($http);
  }
});

/***/ }),
/* 7 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('winState', ["winScore"]);

__webpack_require__(8);

/***/ }),
/* 8 */
/***/ (function(module, exports) {

angular.
module('winState').
component('winState', {
  templateUrl: "states/win.template.html",

  controller: function WinStateController($http, $rootScope) {
    $rootScope.AoNListen($http);
  }
});

/***/ }),
/* 9 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('oneState', ["oneOnOne"]);

__webpack_require__(10);

/***/ }),
/* 10 */
/***/ (function(module, exports) {

angular.
module('oneState').
component('oneState', {
  templateUrl: "states/one.template.html",

  controller: function OneStateController($http, $rootScope) {
    $rootScope.AoNListen($http);
  }
});

/***/ }),
/* 11 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('score', []);

__webpack_require__(12);

/***/ }),
/* 12 */
/***/ (function(module, exports) {

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

      self.Teams.push({
        Score: gs.Pool,
        Enabled: true,
        Name: "Pula",
        Class: "pool teamScore"
      });

      if (gs.State == 1) {
        self.isAuction = true;

        for (let i = 0; i < gs.Teams.length; ++i) {
          var team = gs.Teams[i];
          if (team != null && team.isPlaying)
            self.Auctions.push({
              Class: "teamAuction centerVerticalFlex centerHorizontalFlex " + team.ClassName,
              Score: team.Points > 200 ? gs.Licitation.Bid[i] : "-"
            });
        }
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

/***/ }),
/* 13 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('question', []);

__webpack_require__(14);

/***/ }),
/* 14 */
/***/ (function(module, exports) {

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
      if (self.Time <= 0)
        self.Time = "Koniec czasu!";
      self.Class = gs.Teams[gs.CurrentTeam].ClassName;

    };
    this.QuestionNumber = 6;


  }
});

/***/ }),
/* 15 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('winScore', []);

__webpack_require__(16);

/***/ }),
/* 16 */
/***/ (function(module, exports) {

angular.
module('winScore').
component('winScore', {
  templateUrl: "winScore/winscore.template.html",

  controller: function WinStateController($rootScope, $sessionStorage, $scope, $http) {
    
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
            self.Class = gs.Teams[gs.CurrentTeam].ClassName;
            self.Score = gs.Licitation.Pool;
    
    
        };
    
      }
});

/***/ }),
/* 17 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('oneOnOne', []);

__webpack_require__(18);

/***/ }),
/* 18 */
/***/ (function(module, exports) {

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
            for(let i = 0; i < gs.OneOnOneCategories.length; ++i)
            {
                var cat = gs.OneOnOneCategories[i];
                
                var item = 
                {
                    Name: cat.Key,
                    Class: (cat.Value == "true" || cat.Value == "True" || cat.Value == true) ? "enabled" :"disabled"
                };

                self.Categories.push(item);
            }
            
    
        };
    
      }
});

/***/ })
/******/ ]);