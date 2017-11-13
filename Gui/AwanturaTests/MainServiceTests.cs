using AwanturaLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaTests
{
    [TestClass]
    public class MainServiceTests
    {
        MainService mainService = new MainService();
        [TestMethod]
        public void SetAboveMaxTest()
        {

            var gs = CreateSampleGamestate();
           // gs = mainService.StartGame();
           

            Assert.AreEqual(false, gs.Teams[4].isPlaying);
            Assert.AreEqual(false,  gs.Teams[3].isPlaying);

            gs = mainService.StartSecondRound(gs, 1000);

          
            Assert.AreEqual(false, gs.Teams[0].isPlaying);
            Assert.AreEqual(true, gs.Teams[1].isPlaying);
            Assert.AreEqual(false, gs.Teams[2].isPlaying);
            Assert.AreEqual(false, gs.Teams[3].isPlaying);
            Assert.AreEqual(true, gs.Teams[4].isPlaying);
        }

        private GameState CreateSampleGamestate()
        {
            return new GameState()
            {
                Pool = 0,
                Teams = new Team[5]
                {
                    new Team() {Points = 500, isPlaying = true},
                    new Team() {Points = 1000, isPlaying = true},
                    new Team() {isPlaying = false},
                    new Team() {isPlaying = false},
                    new Team() {isPlaying = false},
                }

            };
        }
    }
}
