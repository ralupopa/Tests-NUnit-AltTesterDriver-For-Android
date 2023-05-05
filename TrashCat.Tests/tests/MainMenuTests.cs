using TrashCat.Tests.pages;

namespace TrashCat.Tests
{
    public class MainMenuTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;
        StorePage storePage;
        [SetUp]
        public void SetUp()
        {
            AltPortForwarding.ForwardAndroid();
            altDriver = new AltDriver(port: 13000);
            mainMenuPage = new MainMenuPage(altDriver);
            storePage = new StorePage(altDriver);
            mainMenuPage.LoadScene();
        }
        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            AltPortForwarding.RemoveForwardAndroid();
            Thread.Sleep(1000);
        }

        [Test]
        public void TestMainMenuPageIsLoadedCorrectly()
        {
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        public void TestShowLeaderboard()
        {
            mainMenuPage.PressLeaderboard();
            Assert.NotNull(mainMenuPage.LeaderBoardText);
            Assert.AreEqual(mainMenuPage.GetLeaderboardText(), "Leaderboard");
            mainMenuPage.PressCloseLeaderboard();
            Assert.True(mainMenuPage.IsDisplayed());
        }

        [Test]
        public void TestShowMissionsContainsTwo()
        {
            mainMenuPage.PressMissions();
            Assert.NotNull(mainMenuPage.MissionsText);
            Assert.AreEqual(mainMenuPage.GetMissionsText(), "MISSIONS");
            Assert.AreEqual(2, mainMenuPage.MissionsList.Count);
            mainMenuPage.PressCloseMissions();
            Assert.True(mainMenuPage.IsDisplayed());
        }

        [Test]
        public void TestShowStore()
        {
            mainMenuPage.PressStore();

            Assert.NotNull(storePage.StoreText);
            Assert.AreEqual(storePage.GetStoreText(), "STORE");
            Assert.AreEqual(4, storePage.StoreTabsList.Count);
            Assert.True(storePage.StoreTabsAreDisplayed());
            storePage.TapCloseStore();
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        public void TestShowSettings()
        {
            mainMenuPage.PressSettings();
            Assert.NotNull(mainMenuPage.SettingsText);
            Assert.AreEqual(mainMenuPage.GetSettingsText(), "SETTINGS");
            Assert.True(mainMenuPage.SettingsSlidersAreDisplayed());
            mainMenuPage.PressCloseSettings();
            Assert.True(mainMenuPage.IsDisplayed());
        }
    }
}