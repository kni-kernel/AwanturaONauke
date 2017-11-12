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
/******/ 	return __webpack_require__(__webpack_require__.s = 632);
/******/ })
/************************************************************************/
/******/ ({

/***/ 1372:
/***/ (function(module, exports) {

// Define the `phonecatApp` module


var app = angular.module('AoN', [
  "idleState",
  "questionState",
  "winState",
  "oneState",
  "ngRoute"
]);

angular.
module('AoN').
config(['$locationProvider', '$routeProvider', '$httpProvider',
  function config($locationProvider, $routeProvider, $httpProvider) {

    $locationProvider.hashPrefix('!');

    $routeProvider.
    when('/idle', {
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
    }).
    otherwise('/idle');
  }
]);

app.run(function ($rootScope) {
  $rootScope.AoNListen = function ($http) {
    setTimeout(function() {


    var ip = window.location.hostname;
    var address = "http://" + ip + ":8001";
    console.log(address);
    $http({
      method: "POST",
      url: address,

    }).success(function (data) {
      $rootScope.GameState = data;
      //console.log(data);
      
      if (data.State == 0)
        window.location.href = "/#!/Idle";
      if (data.State == 1)
        window.location.href = "/#!/Idle";
      if (data.State == 2)
        window.location.href = "/#!/OneOnOne";
      if (data.State == 3)
        window.location.href = "/#!/Question";
      if (data.State == 4)
        window.location.href = "/#!/Question";
      if (data.State == 5)
       window.location.href= "/#!/Hint";
    });
  }, 1000);
  }
});

/***/ }),

/***/ 1373:
/***/ (function(module, exports, __webpack_require__) {

angular.module('idleState', ["score"]);

__webpack_require__(1374);

/***/ }),

/***/ 1374:
/***/ (function(module, exports) {

var app = 

angular.
module('idleState').
component('idleState', {
  templateUrl: "states/idle.template.html",

  controller: function IdleStateController($http, $rootScope) {
    $rootScope.AoNListen($http);
   
  }
});

/***/ }),

/***/ 1375:
/***/ (function(module, exports, __webpack_require__) {

angular.module('questionState', ["question"]);

__webpack_require__(1376);

/***/ }),

/***/ 1376:
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

/***/ 1377:
/***/ (function(module, exports, __webpack_require__) {

angular.module('winState', ["winScore"]);

__webpack_require__(1378);

/***/ }),

/***/ 1378:
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

/***/ 1379:
/***/ (function(module, exports, __webpack_require__) {

angular.module('oneState', ["oneOnOne"]);

__webpack_require__(1380);

/***/ }),

/***/ 1380:
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

/***/ 1381:
/***/ (function(module, exports, __webpack_require__) {

angular.module('score', []);

__webpack_require__(1382);

/***/ }),

/***/ 1382:
/***/ (function(module, exports) {

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

/***/ }),

/***/ 1383:
/***/ (function(module, exports, __webpack_require__) {

angular.module('question', []);

__webpack_require__(1384);

/***/ }),

/***/ 1384:
/***/ (function(module, exports) {

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

/***/ }),

/***/ 1385:
/***/ (function(module, exports, __webpack_require__) {

angular.module('winScore', []);

__webpack_require__(1386);

/***/ }),

/***/ 1386:
/***/ (function(module, exports) {

angular.
module('winScore').
component('winScore', {
  templateUrl: "winScore/winscore.template.html",

  controller: function WinStateController($http) {
    this.Class = "blue";
    this.Score = 666;
  }
});

/***/ }),

/***/ 1387:
/***/ (function(module, exports, __webpack_require__) {

angular.module('oneOnOne', []);

__webpack_require__(1388);

/***/ }),

/***/ 1388:
/***/ (function(module, exports) {

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

/***/ }),

/***/ 632:
/***/ (function(module, exports, __webpack_require__) {

//libs
//require("express");
// require("./bower_components/fs/lib/fs.js")
// require("../node_modules/steal-tools/index.js")
// require("./bower_components/express/index.js")



__webpack_require__(1372);
__webpack_require__(1373);
__webpack_require__(1375);
__webpack_require__(1377);
__webpack_require__(1379);
__webpack_require__(1381);
__webpack_require__(1383);
__webpack_require__(1385);
__webpack_require__(1387);


/***/ })

/******/ });