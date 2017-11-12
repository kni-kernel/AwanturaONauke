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
            gs = mainService.StartGame();
            gs = mainService.StartSecondRound(gs, 0, 1000, 1000);
            Assert.AreEqual(1000, gs.Teams[0].Points);
            Assert.AreEqual(1000, gs.Teams[4].Points);

           


        }

        private GameState CreateSampleGamestate()
        {
            return new GameState()
            {
                Pool = 0,
                Teams = new Team[5]
                {
                    new Team() {Points = 1000, isPlaying = true},
                    new Team() {Points = 5000, isPlaying = true},
                    new Team() {isPlaying = false},
                    new Team() {isPlaying = false},
                    new Team() {isPlaying = false},
                }

            };
        }
    }
}
