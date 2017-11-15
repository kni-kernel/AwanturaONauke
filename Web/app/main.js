//libs
//require("express");
// require("./bower_components/fs/lib/fs.js")
// require("../node_modules/steal-tools/index.js")
// require("./bower_components/express/index.js")



require("./app.module");
require("./states/init.module.js");
require("./states/idle.module.js");
require("./states/question.module.js");
require("./states/win.module.js");
require("./states/one.module.js");
require("./states/hint.module.js");
require("./logo/logo.module.js");
require("./score/score.module.js");
require("./questions/question.module.js");
require("./winScore/winscore.module.js");
require("./OneOnOne/OneOnOne.module.js");
require("./hint/hint.module.js");
