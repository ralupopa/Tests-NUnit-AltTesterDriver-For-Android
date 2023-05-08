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
        [Test]
        public void TestGetScreenPosition()
        {
            AltObject premiumBtn = storePage.PremiumCounter;
            AltVector2 screenPosition = premiumBtn.GetScreenPosition();
            Assert.NotNull(screenPosition);
            Assert.That(premiumBtn.x, Is.EqualTo(screenPosition.x));
            Assert.That(premiumBtn.y, Is.EqualTo(screenPosition.y));
        }
        [Test]
        public void TestGetWorldPosition()
        {
            AltObject premiumBtn = storePage.PremiumCounter;
            AltVector3 worldPosition = premiumBtn.GetWorldPosition();
            Assert.NotNull(worldPosition);
            Assert.That(worldPosition.x, Is.EqualTo(premiumBtn.worldX));
            Assert.That(worldPosition.y, Is.EqualTo(premiumBtn.worldY));
            Assert.That(worldPosition.z, Is.EqualTo(premiumBtn.worldZ));
        }

        [Test]
        public void TestFindObjectWhichContains()
        {
            Assert.NotNull(storePage.FindObjectWhichContainsPremium);
        }
        [Test]
        public void TestFindObjectsWhichContain()
        {
            Assert.NotNull(storePage.FindObjectsWhichContainItemEntry);
            Assert.That(storePage.FindObjectsWhichContainItemEntry.Count, Is.EqualTo(4));
        }
        [Test]
        public void TestFindObjectAtCoordinates()
        {
            Assert.NotNull(storePage.StoreTitleFindObjectAtCoordinates);
            Assert.That(storePage.StoreTitleFindObjectAtCoordinates.GetText(), Is.EqualTo("Life"));
        }
        [Test]
        public void TestGetAllEnabledObjects()
        {
            Assert.IsNotEmpty(storePage.GetAllEnabledObjects);
            var enabledObjectsCount = storePage.GetAllEnabledObjects.Count;
            Console.WriteLine("There are " + enabledObjectsCount + " enabled objects.");
            Assert.Greater(enabledObjectsCount, 100);
        }
        [Test]
        public void TestGetAllDisabledObjects()
        {
            Assert.IsNotEmpty(storePage.GetAllDisabledObjects);
            var disabledObjectsCount = storePage.GetAllDisabledObjects.Count;
            Console.WriteLine("There are " + disabledObjectsCount + " disabled objects.");
            Assert.Greater(disabledObjectsCount, 100);
        }

        [Test]
        public void TestWaitForObjectWhichContains()
        {
            var foundObject = storePage.WaitForObjectWhichContainsPremium;
            Assert.NotNull(foundObject);
            Assert.That(foundObject.name, Is.EqualTo("Premium"));
        }
        [Test]
        public void TestPointerUpAndDown()
        {
            var ItemsTabObject = storePage.ItemsTab;
            Assert.NotNull(ItemsTabObject);
            Assert.That(storePage.GetColorOfObject(), Is.EqualTo(0.9607843f));

            var stateBeforePointDown = storePage.GetCurrentSelectionForObject(ItemsTabObject);
            ItemsTabObject.PointerDownFromObject();

            var stateAfterPointDown = storePage.GetCurrentSelectionForObject(ItemsTabObject);
            Assert.That(stateBeforePointDown, Is.Not.EqualTo(stateAfterPointDown));

            ItemsTabObject.PointerUpFromObject();
            var stateAfterPointerUp = storePage.GetCurrentSelectionForObject(ItemsTabObject);
            Assert.That(stateAfterPointDown, Is.Not.EqualTo(stateAfterPointerUp));
        }
        [Test]
        public void TestCallComponentMethodForGetColors()
        {
            Assert.NotNull(storePage.CallComponentMethodGetColor());
        }
    }
}