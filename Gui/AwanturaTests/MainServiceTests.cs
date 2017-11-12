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
            gs = mainService.StartFirtRound(gs);
            Assert.AreEqual(5000, gs.Teams[0].Points);
            gs.Licitation = new Licitation(gs);
            gs = mainService.Bet(gs, 0, 400);
            Assert.AreEqual(1000, gs.Licitation.Pool);
            gs = mainService.EndLicitationToQuestion(gs);
            Assert.AreEqual(4600, gs.Teams[0].Points);
            Assert.AreEqual(4800, gs.Teams[1].Points);
            gs = mainService.RightGuess(gs,0);
            Assert.AreEqual(5600, gs.Teams[0].Points);
            Assert.AreEqual(4800, gs.Teams[1].Points);


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
