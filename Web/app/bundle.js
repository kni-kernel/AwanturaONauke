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

__webpack_require__(1);
__webpack_require__(2);
__webpack_require__(4);
__webpack_require__(6);
__webpack_require__(8);
__webpack_require__(10);
__webpack_require__(12);




/***/ }),
/* 1 */
/***/ (function(module, exports) {

// Define the `phonecatApp` module
angular.module('AoN', [
    "idleState",
    "questionState",
    "winState",
]);



/***/ }),
/* 2 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('idleState', ["score"]);

__webpack_require__(3);

/***/ }),
/* 3 */
/***/ (function(module, exports) {

angular.
module('idleState').
component('idleState', {
  templateUrl: "states/idle.template.html",

  controller: function IdleStateController($http) {
    
  }
});

/***/ }),
/* 4 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('questionState', ["question"]);

__webpack_require__(5);

/***/ }),
/* 5 */
/***/ (function(module, exports) {

angular.
module('questionState').
component('questionState', {
  templateUrl: "states/question.template.html",

  controller: function IdleStateController($http) {
    
  }
});

/***/ }),
/* 6 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('winState', ["winScore"]);

__webpack_require__(7);

/***/ }),
/* 7 */
/***/ (function(module, exports) {

angular.
module('winState').
component('winState', {
  templateUrl: "states/win.template.html",

  controller: function WinStateController($http) {
    
  }
});

/***/ }),
/* 8 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('score', []);

__webpack_require__(9);

/***/ }),
/* 9 */
/***/ (function(module, exports) {

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

/***/ }),
/* 10 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('question', []);

__webpack_require__(11);

/***/ }),
/* 11 */
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
/* 12 */
/***/ (function(module, exports, __webpack_require__) {

angular.module('winScore', []);

__webpack_require__(13);

/***/ }),
/* 13 */
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

/***/ })
/******/ ]);