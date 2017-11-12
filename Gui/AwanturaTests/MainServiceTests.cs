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
            gs.Licitation = new Licitation(gs);

            gs = mainService.Bet(gs, 0, -500);
            gs = mainService.Bet(gs, 1, 1000);

            Assert.AreEqual(500, gs.Pool);
        }

        private GameState CreateSampleGamestate()
        {
            return new GameState()
            {
                Pool = 500,
                Teams = new Team[5]
                {
                    new Team() {Points = 1000},
                    new Team() {Points = 5000},
                    new Team() {isPlaying = false},
                    new Team() {isPlaying = false},
                    new Team() {isPlaying = false},
                }

            };
        }
    }
}
