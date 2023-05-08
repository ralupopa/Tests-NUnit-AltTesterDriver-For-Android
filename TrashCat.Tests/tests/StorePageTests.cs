using TrashCat.Tests.pages;

namespace TrashCat.Tests
{
    public class StorePageTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;
        StorePage storePage;

        [SetUp]
        public void Setup()
        {
            AltPortForwarding.ForwardAndroid();
            altDriver = new AltDriver(port: 13000);
            mainMenuPage = new MainMenuPage(altDriver);
            storePage = new StorePage(altDriver);
            storePage.LoadScene();
            mainMenuPage.TapStore();
            storePage.TapStoreToIncreaseCoins();
        }

        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            AltPortForwarding.RemoveForwardAndroid();
            Thread.Sleep(1000);
        }
        [Test]
        public void TestAccessStoreIsDislayed()
        {
            Assert.IsTrue(storePage.IsDisplayed());
        }
        [Test]
        public void TestAccessStoreAndIncreaseCoins()
        {
            storePage.IsDisplayed();
            storePage.TapStoreToIncreaseCoins();
            var coinsText = storePage.GetCoinsCounterText();
            Assert.True(Int32.Parse(coinsText) != 0);
        }

        [Test(Author = "Ralu", Description = "Can Buy Night theme")]
        public void TestBuyNightTime()
        {
            Assert.Multiple(() =>
            {
                storePage.PressThemes();
                Assert.NotNull(storePage.OwnedButtonText);
                Assert.NotNull(storePage.BuyButtonText);
                storePage.PressBuy();
                storePage.TapCloseStore();
                Assert.True(mainMenuPage.IsDisplayed());
                mainMenuPage.ButtonsLeftRightAreDisplayed();
                Assert.NotNull(mainMenuPage.ThemeName);
                Assert.That(mainMenuPage.GetThemeNameText(), Is.EqualTo("Day"));
                mainMenuPage.PressButtonRight();
                StringAssert.Contains("Night", mainMenuPage.GetThemeNameText());

                mainMenuPage.DeleteData();
            });
        }

        [Test]
        [Order(1)]
        public void TestCheckItemsList()
        {
            storePage.IsDisplayed();
            Assert.True(storePage.StoreTabsAreDisplayed());
            Assert.That(storePage.ItemsList.Count, Is.EqualTo(4));
        }

        [Test]
        public void TestCheckCharactersList()
        {
            storePage.IsDisplayed();
            Assert.True(storePage.StoreTabsAreDisplayed());
            storePage.PressCharacters();
            Assert.That(storePage.CharactersList.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestCheckAccessoriesList()
        {
            storePage.IsDisplayed();
            Assert.True(storePage.StoreTabsAreDisplayed());
            storePage.PressAccessories();
            Assert.That(storePage.AccessoriesList.Count, Is.EqualTo(5));
            Assert.That(storePage.AccessoriesHeaderCharacter.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestCheckThemesList()
        {
            storePage.IsDisplayed();
            Assert.True(storePage.StoreTabsAreDisplayed());
            storePage.PressThemes();
            Assert.That(storePage.ThemesList.Count, Is.EqualTo(2));

            storePage.TapCloseStore();
        }

        [Test]
        public void TestBuyCharacterRubbishRaccoon()
        {
            Assert.Multiple(() =>
            {
                storePage.PressCharacters();
                Assert.NotNull(storePage.OwnedButtonText);
                Assert.NotNull(storePage.BuyButtonText);
                storePage.PressBuy();
                storePage.TapCloseStore();
                Assert.True(mainMenuPage.IsDisplayed());
                Assert.True(mainMenuPage.ButtonsLeftRightAreDisplayed());
                Assert.That(mainMenuPage.GetCharacterNameText(), Is.EqualTo("Trash Cat"));
                mainMenuPage.PressButtonRight();
                StringAssert.Contains("Rubbish Raccoon", mainMenuPage.GetCharacterNameText());

                mainMenuPage.DeleteData();
            });
        }

        [Test]
        public void TestBuyAllAccessories()
        {
            Assert.Multiple(() =>
            {
                storePage.PressAccessories();
                foreach (AltObject button in storePage.BuyButtonList)
                {
                    button.Tap();
                }

                storePage.TapCloseStore();
                Assert.True(mainMenuPage.IsDisplayed());
                Assert.True(mainMenuPage.AccessoriesSelectorIsDisplayed());
                Assert.True(mainMenuPage.ButtonsTopBottomAreDisplayed());

                mainMenuPage.DeleteData();
            });
        }
        [Test]
        public void TestFindObjectByName()
        {
            Assert.NotNull(storePage.CoinFindObjectByName);
        }
        [Test]
        public void TestFindObjectByPath()
        {
            Assert.NotNull(storePage.CoinFindObjectByPath);
        }
        [Test]
        public void TestFindObjectsByTag()
        {
            Assert.NotNull(storePage.FindObjectsByTagUntagged);
            /// <summary>
            /// FindObjects by Tag 'Untagged' returns different count each time
            /// also in desktop the count is different on each search
            /// using Assert.Greater to verify that at least 100 elements are returned
            /// </summary>
            Thread.Sleep(3000);
            Assert.Greater(storePage.FindObjectsByTagUntagged.Count, 100);
        }
        [Test]
        [Order(2)]
        public void TestFindObjectsByComponent()
        {
            Assert.NotNull(storePage.FindObjectByComponentNIS);
            Assert.NotNull(storePage.FindObjectsByComponentShopList);
            Assert.That(storePage.FindObjectsByComponentShopList.Count, Is.EqualTo(4));
        }
        [Test]
        public void TestFindObjectByText()
        {
            Assert.NotNull(storePage.MagnetFindObjectByText);
            Assert.NotNull(storePage.FindObjectsByTextBuy);
            Assert.That(storePage.FindObjectsByTextBuy.Count, Is.EqualTo(4));
        }

        [Test]
        public void TestGetScreenshot()
        {
            var path="../../../test-screenshot.png";
            altDriver.GetPNGScreenshot(path);
            FileAssert.Exists(path);
        }
    }
}