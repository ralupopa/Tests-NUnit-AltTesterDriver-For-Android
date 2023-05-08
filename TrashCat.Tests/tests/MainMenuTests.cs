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
        [Test]
        public void TestFindObjectByPathSelectChild()
        {
            Assert.NotNull(mainMenuPage.StartButtonChild);
        }
        [Test]
        public void TestGetAndSetTextOnStartButton()
        {
            var newTextStartBtn = "Just play";
            Assert.That(mainMenuPage.GetTextRunButton(), Is.EqualTo("Run!"));
            mainMenuPage.SetTextRunButton(newTextStartBtn);
            Assert.That(mainMenuPage.GetTextRunButton(), Is.EqualTo(newTextStartBtn));
        }
        [Test]
        public void TestGetParent()
        {
            Assert.NotNull(mainMenuPage.StartButtonChild);
            var startButton = mainMenuPage.StartButtonChild.GetParent();
            Assert.That(startButton.name, Is.EqualTo("StartButton"));
        }
        [Test]
        public void TestMoveMouseToObjectCoordinates()
        {
            mainMenuPage.TapSettings();
            var AboutBtn = mainMenuPage.AboutButton;
            Assert.NotNull(AboutBtn);
            var btnWorldCoordinates = AboutBtn.GetWorldPosition();

            Assert.Multiple(() =>
            {
                var stateBeforeMoveMouse = mainMenuPage.GetCurrentSelectionForObject(AboutBtn);
                Assert.That(stateBeforeMoveMouse, Is.EqualTo("0"));

                altDriver.MoveMouse(new AltVector2(btnWorldCoordinates.x, btnWorldCoordinates.y), 1);

                var stateAfterMoveMouse = mainMenuPage.GetCurrentSelectionForObject(AboutBtn);
                altDriver.SetDelayAfterCommand(2);
                Assert.That(stateAfterMoveMouse, Is.EqualTo("1"));

                Assert.That(stateBeforeMoveMouse, Is.Not.EqualTo(stateAfterMoveMouse));
                mainMenuPage.TapCloseSettings();
            });
        }
        [Test]
        public void TestGetAllComponents()
        {
            var expectedComponents = ListOfComponentNamesForStoreButton();
            var storeBtnComponentsList = mainMenuPage.StoreButton.GetAllComponents();

            Assert.IsNotEmpty(storeBtnComponentsList);
            for (int index = 0; index <= storeBtnComponentsList.Count-1; index++)
            {
                Assert.That(storeBtnComponentsList[index].componentName, Is.EqualTo(expectedComponents[index]));
            }
        }
        [Test]
        public void TestGetAllProperties()
        {
            AltComponent testComponent = new AltComponent("UnityEngine.CanvasRenderer", "UnityEngine.UIModule");
            var storeBtnProperties = mainMenuPage.StoreButton.GetAllProperties(testComponent);

            Assert.That(storeBtnProperties.Count, Is.EqualTo(16));
            Assert.That(storeBtnProperties[0].name, Is.EqualTo("hasPopInstruction"));
            Assert.That(storeBtnProperties[0].value, Is.EqualTo("False"));
        }
        [Test]
        public void TestGetAllFields()
        {
            AltComponent testComponent = new AltComponent("UnityEngine.UI.Button", "UnityEngine.UI");
            var storeBtnFields = mainMenuPage.StoreButton.GetAllFields(testComponent);

            Assert.That(storeBtnFields.Count, Is.EqualTo(2));
            Assert.That(storeBtnFields[0].name, Is.EqualTo("m_OnClick"));
            Assert.That(storeBtnFields[1].name, Is.EqualTo("m_CurrentIndex"));
        }
        [Test]
        public void TestGetAllMethods()
        {
            AltComponent testComponent = new AltComponent("UnityEngine.CanvasRenderer", "UnityEngine.UIModule");
            var storeBtnMethods = mainMenuPage.StoreButton.GetAllMethods(testComponent);

            Assert.That(storeBtnMethods.Count, Is.EqualTo(108));
            Assert.That(storeBtnMethods[0], Is.EqualTo("Boolean get_hasPopInstruction()"));
            Assert.That(storeBtnMethods[1], Is.EqualTo("Void set_hasPopInstruction(Boolean)"));
        }
        [Test]
        public void TestGetApplicationScreenSize()
        {
            var appScreenX = altDriver.GetApplicationScreenSize().x;
            var screenWidth = mainMenuPage.GetScreenWidth();
            Assert.That(appScreenX, Is.EqualTo(screenWidth));

            var appScreenY = altDriver.GetApplicationScreenSize().y;
            var screenHeight = mainMenuPage.GetScreenHeight();
            Assert.That(appScreenY, Is.EqualTo(screenHeight));
        }
        [Test]
        public void TestCallStaticMethod()
        {
            var screenWidth = mainMenuPage.GetScreenWidth();
            var screenHeight = mainMenuPage.GetScreenHeight();
            var screenshot = altDriver.GetScreenshot();
            Assert.True(screenshot.textureSize.x == screenWidth);
            Assert.True(screenshot.textureSize.y == screenHeight);

            // because struct AltTextureInformation has AltVector3 as attribute
            Assert.That(screenshot.textureSize.z, Is.EqualTo(0));
            Console.WriteLine(screenshot.scaleDifference.x);
        }
        [Test]
        public void TestGetStaticProperty()
        {
            dynamic resolutionData = mainMenuPage.GetCurrentResolutionUsingGetStaticProperty();

            Assert.True(resolutionData.ContainsKey("width"));
            Assert.True(resolutionData.ContainsKey("height"));
            Assert.True(resolutionData.ContainsKey("refreshRate"));
        }
        [Test]
        public void TestGetStaticPropertyAndCallStaticMethod()
        {
            var screenWidthBefore = mainMenuPage.GetScreenWidthFromProperty();
            var screenHeightBefore = mainMenuPage.GetScreenHeightFromProperty();

            var newWidth = "1240";
            var newHeight = "680";

            Assert.Multiple(() =>
            {
                mainMenuPage.SetScreenResolutionUsingCallStaticMethod(newWidth, newHeight);

                var screenWidthAfter = mainMenuPage.GetScreenWidthFromProperty();
                Assert.That(screenWidthAfter, Is.EqualTo(newWidth));
                Assert.That(screenWidthAfter, Is.Not.EqualTo(screenWidthBefore));

                var screenHeightAfter = mainMenuPage.GetScreenHeightFromProperty();
                Assert.That(screenHeightAfter, Is.EqualTo(newHeight));
                Assert.That(screenHeightAfter, Is.Not.EqualTo(screenHeightBefore));
                
                mainMenuPage.SetScreenResolutionUsingCallStaticMethod(screenWidthBefore, screenHeightBefore);
            });
        }
        [Test]
        public void TestGetCurrentScene()
        {
            mainMenuPage.LoadScene();
            Assert.That(altDriver.GetCurrentScene(), Is.EqualTo("Main"));
        }
        [Test]
        public void TestWaitForCurrentSceneToBe()
        {
            const string mainSceneName = "Main";
            const string shopSceneName = "Shop";
            Assert.That(altDriver.GetAllLoadedScenes().Count, Is.EqualTo(1));
            Assert.That(altDriver.GetCurrentScene(), Is.EqualTo(mainSceneName));

            Assert.Multiple(() =>
            {
                altDriver.LoadScene(shopSceneName);
                altDriver.WaitForCurrentSceneToBe(shopSceneName);

                Assert.That(altDriver.GetCurrentScene(), Is.EqualTo(shopSceneName));

                // load scene non-additive (as default)
                altDriver.LoadScene(mainSceneName);
                Assert.That(altDriver.GetAllLoadedScenes().Count, Is.EqualTo(1));
                Assert.That(altDriver.GetCurrentScene(), Is.EqualTo(mainSceneName));
            });
        }

        public static List<string> ListOfComponentNamesForStoreButton()
        {
            var listComponents = new List<string>()
            {
                "UnityEngine.RectTransform",
                "UnityEngine.CanvasRenderer",
                "UnityEngine.UI.Image",
                "UnityEngine.UI.Button",
                "LevelLoader",
                "UnityEngine.AudioSource"
            };
            return listComponents;
        }
    }
}