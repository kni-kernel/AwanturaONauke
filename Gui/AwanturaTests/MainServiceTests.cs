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
        public void when_GameStarted_then_TeamPointsAndStateAreEmpty()
        {

            var gs = CreateFullGamestate();
            gs = mainService.StartGame();
            foreach (Team team in gs.Teams)
            {
                Assert.AreEqual(0, team.Points);
                Assert.IsFalse(team.isPlaying);
            }
        }

        [TestMethod]
        public void when_FirstRoundStarted_then_TeamStateAndPointsAreFilled()
        {

            var gs = CreateFullGamestate();
            gs = mainService.StartGame();
            gs = mainService.StartFirstRound(gs);
            foreach (Team team in gs.Teams)
            {
                if (team.ClassName == "black")
                {
                    Assert.AreEqual(0, team.Points);
                    Assert.IsFalse(team.isPlaying);
                }
                else
                {
                    Assert.AreEqual(5000, team.Points);
                    Assert.IsTrue(team.isPlaying);
                }
            }
        }

        [TestMethod]
        public void when_SecondRoundStarted_then_TeamPointsAreUpdated()
        {

            var gs = CreateFullGamestate();
            gs = mainService.StartSecondRound(gs, 1000);
            Assert.AreEqual(5000, gs.Teams[0].Points);
            Assert.AreEqual(1000, gs.Teams[4].Points);
        }

        [TestMethod]
        public void when_SecondRoundStarted_then_PlayStateIsUpdated()
        {

            var gs = CreateFullGamestate();
            gs = mainService.StartSecondRound(gs, 1000);
            for(int i=0; i < gs.Teams.Length; i++)
            {
                if(i==0 || i==4)
                    Assert.IsTrue(gs.Teams[i].isPlaying);
                else
                    Assert.IsFalse(gs.Teams[i].isPlaying);
            }
        }

        [TestMethod]
        public void when_TeamBets_then_GameStateChanges()
        {

            var gs = CreateFullGamestate();
            gs = mainService.StartGame();
            gs = mainService.StartFirstRound(gs);
            gs = mainService.StartLicitation(gs);
            Assert.AreEqual(States.Licitation, gs.State);
            gs = mainService.Bet(gs, 0, 500);
            gs = mainService.Bet(gs, 1, 400);
            Assert.AreEqual(1100, gs.Licitation.Pool);

            //TODO: Fin!
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
        private GameState CreateFullGamestate()
        {
            return new GameState()
            {
                Pool = 0,
                Teams = new Team[5]
                {
                    new Team() {Points = 5000, isPlaying = true},
                    new Team() {Points = 4000, isPlaying = true},
                    new Team() {Points = 3000, isPlaying = true},
                    new Team() {Points = 2000, isPlaying = true},
                    new Team() {Points = 3000, isPlaying = true},
                }

            };
        }
    }
}
