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
            mainMenuPage.TapLeaderboard();
            Assert.NotNull(mainMenuPage.LeaderBoardText);
            Assert.That(mainMenuPage.GetLeaderboardText(), Is.EqualTo("Leaderboard"));
            mainMenuPage.TapCloseLeaderboard();
            Assert.True(mainMenuPage.IsDisplayed());
        }

        [Test]
        public void TestShowMissionsContainsTwo()
        {
            mainMenuPage.TapMissions();
            Assert.NotNull(mainMenuPage.MissionsText);
            Assert.That(mainMenuPage.GetMissionsText(), Is.EqualTo("MISSIONS"));
            Assert.That(mainMenuPage.MissionsList.Count, Is.EqualTo(2));
            mainMenuPage.TapCloseMissions();
            Assert.True(mainMenuPage.IsDisplayed());
        }

        [Test]
        public void TestShowStore()
        {
            mainMenuPage.TapStore();

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
            mainMenuPage.TapSettings();
            Assert.NotNull(mainMenuPage.SettingsText);
            Assert.That(mainMenuPage.GetSettingsText(), Is.EqualTo("SETTINGS"));
            Assert.True(mainMenuPage.SettingsSlidersAreDisplayed());
            mainMenuPage.TapCloseSettings();
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        [Description("Use GetComponentProperty and SetComponentProperty for property from 'Fields' column")]
        public void TestGetAndSetComponentPropertyString()
        {
            var newCharacterName = "Rich Cat";
            mainMenuPage.SetCharNameDisplay(newCharacterName);
            Assert.That(mainMenuPage.GetCharNameDisplay(), Is.EqualTo(newCharacterName));
        }
    }
}