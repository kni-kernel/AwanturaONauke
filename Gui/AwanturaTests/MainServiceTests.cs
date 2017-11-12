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
            gs.Licitation = new Licitation(gs);

            

            //Assert.AreEqual(00, gs.Licitation.Pool);
            Assert.AreEqual(0, gs.Pool);
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
