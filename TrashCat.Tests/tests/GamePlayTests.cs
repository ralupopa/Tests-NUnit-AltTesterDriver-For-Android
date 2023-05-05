using TrashCat.Tests.pages;

namespace TrashCat.Tests
{
    public class GamePlayTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;
        GamePlay gamePlayPage;
        PauseOverlayPage pauseOverlayPage;
        GetAnotherChancePage getAnotherChancePage;
        [SetUp]
        public void Setup()
        {

            AltPortForwarding.ForwardAndroid();
            altDriver = new AltDriver(port: 13000);
            mainMenuPage = new MainMenuPage(altDriver);
            mainMenuPage.LoadScene();
            mainMenuPage.PressRun();
            gamePlayPage = new GamePlay(altDriver);
            pauseOverlayPage = new PauseOverlayPage(altDriver);
            getAnotherChancePage = new GetAnotherChancePage(altDriver);
        }
        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            AltPortForwarding.RemoveForwardAndroid();
            Thread.Sleep(1000);
        }
        [Test]
        public void TestGamePlayDisplayedCorrectly()
        {
            Assert.True(gamePlayPage.IsDisplayed());
        }
        [Test]
        public void TestGameCanBePausedAndResumed()
        {
            gamePlayPage.PressPause();
            Assert.True(pauseOverlayPage.IsDisplayed());
            pauseOverlayPage.PressResume();
            Assert.True(gamePlayPage.IsDisplayed());
        }
        [Test]
        public void TestGameCanBePausedAndStopped()
        {
            gamePlayPage.PressPause();
            pauseOverlayPage.PressMainMenu();
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        public void TestAvoidingObstacles()
        {
            gamePlayPage.AvoidObstacles(10);
            Assert.True(gamePlayPage.GetCurrentLife() > 0);
        }
        [Test]
        public void TestPlayerDiesWhenObstacleNotAvoided()
        {
            float timeout = 20;
            while (timeout > 0)
            {
                try
                {
                    getAnotherChancePage.IsDisplayed();
                    break;
                }
                catch (Exception)
                {
                    timeout -= 1;
                }
            }
            Assert.True(getAnotherChancePage.IsDisplayed());
        }
    }
}