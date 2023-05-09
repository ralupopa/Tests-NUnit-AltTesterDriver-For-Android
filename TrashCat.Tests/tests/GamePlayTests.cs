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
            mainMenuPage.TapOnObject(mainMenuPage.RunButton);
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
            gamePlayPage.TapPause();
            Assert.True(pauseOverlayPage.IsDisplayed());
            pauseOverlayPage.TapResume();
            Assert.True(gamePlayPage.IsDisplayed());
        }
        [Test]
        public void TestGameCanBePausedAndStopped()
        {
            gamePlayPage.TapPause();
            pauseOverlayPage.TapMainMenu();
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        [Order(1)]
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
        [Test]
        public void TestWaitForObjectNotBePresent()
        {
            Assert.Multiple(() =>
            {
                Assert.True(gamePlayPage.IsDisplayed());
                altDriver.WaitForObjectNotBePresent(By.NAME, "StartButton", timeout: 5);

                Assert.NotNull(gamePlayPage.PauseButton);
                gamePlayPage.TapPause();
                altDriver.WaitForObjectNotBePresent(By.NAME, "pauseButton", timeout: 15);

                Assert.True(pauseOverlayPage.IsDisplayed());
                pauseOverlayPage.TapMainMenu();
            });
        }
        [Test]
        [Order(1)]
        public void TestGetComponentPropertyInt()
        {
            Assert.True(gamePlayPage.IsDisplayed());
            Assert.That(gamePlayPage.GetCurrentLife(), Is.EqualTo(3));
        }

        [Test]
        [Order(2)]
        public void TestCallComponentMethodBoolean()
        {
            Assert.False(gamePlayPage.GetCheatInvincible());
        }

        [Test]
        [Order(3)]
        public void TestSetComponentPropertyInt()
        {
            gamePlayPage.SetCurrentLife(5);
            Assert.That(gamePlayPage.GetCurrentLife(), Is.EqualTo(5));
        }
        [Test]
        [Order(4)]
        public void TestUpdateObject()
        {
            Assert.NotNull(gamePlayPage.Character);

            Assert.Multiple(() =>
            {
                var TrashCat = gamePlayPage.Character;

                AltVector3 initialPostion = TrashCat.GetWorldPosition();

                Thread.Sleep(8000);

                AltVector3 AfterStartPostion = TrashCat.UpdateObject().GetWorldPosition();

                Assert.That(initialPostion, Is.Not.EqualTo(AfterStartPostion));

                gamePlayPage.TapPause();

                Assert.True(pauseOverlayPage.IsDisplayed());
                pauseOverlayPage.TapMainMenu();
            });
        }
    }
}