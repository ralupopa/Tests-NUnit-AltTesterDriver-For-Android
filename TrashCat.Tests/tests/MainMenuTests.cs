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
            Assert.That(mainMenuPage.GetLeaderboardText(), Is.EqualTo("Leaderboard"));
            mainMenuPage.PressCloseLeaderboard();
            Assert.True(mainMenuPage.IsDisplayed());
        }

        [Test]
        public void TestShowMissionsContainsTwo()
        {
            mainMenuPage.PressMissions();
            Assert.NotNull(mainMenuPage.MissionsText);
            Assert.That(mainMenuPage.GetMissionsText(), Is.EqualTo("MISSIONS"));
            Assert.That(mainMenuPage.MissionsList.Count, Is.EqualTo(2));
            mainMenuPage.PressCloseMissions();
            Assert.True(mainMenuPage.IsDisplayed());
        }

        [Test]
        public void TestShowStore()
        {
            mainMenuPage.PressStore();

            Assert.NotNull(storePage.StoreText);
            Assert.That(storePage.GetStoreText(), Is.EqualTo("STORE"));
            Assert.That(storePage.StoreTabsList.Count, Is.EqualTo(4));
            Assert.True(storePage.StoreTabsAreDisplayed());
            storePage.TapCloseStore();
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        public void TestShowSettings()
        {
            mainMenuPage.PressSettings();
            Assert.NotNull(mainMenuPage.SettingsText);
            Assert.That(mainMenuPage.GetSettingsText(), Is.EqualTo("SETTINGS"));
            Assert.True(mainMenuPage.SettingsSlidersAreDisplayed());
            mainMenuPage.PressCloseSettings();
            Assert.True(mainMenuPage.IsDisplayed());
        }
    }
}