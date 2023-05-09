using TrashCat.Tests.pages;

namespace TrashCat.Tests
{
    public class MainMenuTests
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
            mainMenuPage.TapOnObject(mainMenuPage.LeaderBoardButton);
            Assert.NotNull(mainMenuPage.LeaderBoardText);
            Assert.That(mainMenuPage.GetLeaderboardText(), Is.EqualTo("Leaderboard"));
            mainMenuPage.TapOnObject(mainMenuPage.CloseLeaderBoardButton);
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        public void TestShowMissionsContainsTwo()
        {
            mainMenuPage.TapOnObject(mainMenuPage.MissionButton);
            Assert.NotNull(mainMenuPage.MissionsText);
            Assert.That(mainMenuPage.GetMissionsText(), Is.EqualTo("MISSIONS"));
            Assert.That(mainMenuPage.MissionsList.Count, Is.EqualTo(2));
            mainMenuPage.TapOnObject(mainMenuPage.CloseMissionsButton);
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        public void TestShowStore()
        {
            mainMenuPage.TapOnObject(mainMenuPage.StoreButton);
            Assert.NotNull(storePage.StoreText);
            Assert.That(storePage.GetStoreText(), Is.EqualTo("STORE"));
            Assert.That(storePage.StoreTabsList.Count, Is.EqualTo(4));
            Assert.True(storePage.StoreTabsAreDisplayed());
            mainMenuPage.TapOnObject(mainMenuPage.CloseStoreButton);
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        public void TestShowSettings()
        {
            mainMenuPage.TapOnObject(mainMenuPage.SettingsButton);
            Assert.NotNull(mainMenuPage.SettingsText);
            Assert.That(mainMenuPage.GetSettingsText(), Is.EqualTo("SETTINGS"));
            Assert.True(mainMenuPage.SettingsSlidersAreDisplayed());
            mainMenuPage.TapOnObject(mainMenuPage.CloseSettingsButton);
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
            mainMenuPage.TapOnObject(mainMenuPage.SettingsButton);
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
                mainMenuPage.TapOnObject(mainMenuPage.CloseSettingsButton);
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
        [Test]
        public void TestScenesMethods()
        {
            const string mainSceneName = "Main";
            const string shopSceneName = "Shop";
            Assert.That(altDriver.GetAllLoadedScenes().Count, Is.EqualTo(1));
            Assert.That(altDriver.GetCurrentScene(), Is.EqualTo(mainSceneName));

            Assert.Multiple(() =>
            {
                mainMenuPage.TapOnObject(mainMenuPage.StoreButton);

                List<string> loadedSceneNames = altDriver.GetAllLoadedScenes();
                Assert.That(loadedSceneNames.Count, Is.EqualTo(2));

                Assert.That(altDriver.GetCurrentScene(), Is.EqualTo(mainSceneName));

                Assert.That(loadedSceneNames[0], Is.EqualTo(mainSceneName));
                Assert.That(loadedSceneNames[1], Is.EqualTo(shopSceneName));

                altDriver.UnloadScene(mainSceneName);
                Assert.That(altDriver.GetAllLoadedScenes().Count, Is.EqualTo(1));
                Assert.That(altDriver.GetCurrentScene(), Is.EqualTo(shopSceneName));

                // load scene additive, together with current loaded scene
                altDriver.LoadScene(mainSceneName, false);
                List<string> loadedSceneNamesSecond = altDriver.GetAllLoadedScenes();
                Assert.That(loadedSceneNamesSecond.Count, Is.EqualTo(2));
                Assert.That(loadedSceneNames[0], Is.EqualTo(mainSceneName));
                Assert.That(loadedSceneNames[1], Is.EqualTo(shopSceneName));

                mainMenuPage.TapOnObject(mainMenuPage.CloseStoreButton);
            });
        }
        [Test]
        public void TestGetAndSetTimeScale()
        {
            Assert.That(altDriver.GetTimeScale(), Is.EqualTo(1f));

            Assert.Multiple(() =>
            {
                altDriver.SetTimeScale(0.4f);
                Thread.Sleep(2000);
                var timeScaleFromGame = altDriver.GetTimeScale();
                Assert.That(timeScaleFromGame, Is.EqualTo(0.4f));

                altDriver.SetTimeScale(1f);
                Assert.That(altDriver.GetTimeScale(), Is.EqualTo(1f));
            });
        }
        [TestCase("enter")]
        public void TestStringKeyPlayerPref(string key)
        {
            string setStringPref = "stringplayerPrefInTrashcat";
            var stringPlayerPref = mainMenuPage.UseGetSetStringKeyPlayerPref(key, setStringPref);
            Assert.That(stringPlayerPref, Is.EqualTo(setStringPref));
        }
        [TestCase("enter")]
        public void TestIntKeyPlayerPref(string key)
        {
            int setIntPref = 13;
            var intPlayerPref = mainMenuPage.UseGetSetIntKeyPlayerPref(key, setIntPref);
            Assert.That(intPlayerPref, Is.EqualTo(setIntPref));
        }
        [TestCase("enter")]
        public void TestFloatKeyPlayerPref(string key)
        {
            float setFloatPref = 13f;
            var floatPlayerPref = mainMenuPage.UseGetSetFloatKeyPlayerPref(key, setFloatPref);
            Assert.That(floatPlayerPref, Is.EqualTo(setFloatPref));
        }
        [Test]
        public void TestGetAndSetPlayerPrefs()
        {
            TestStringKeyPlayerPref("space");
            TestIntKeyPlayerPref("enter");
            TestFloatKeyPlayerPref("enter");
        }
        [Test]
        public void TestDeleteKeyPlayerPref()
        {
            var keyName = "testMe";
            TestStringKeyPlayerPref(keyName);
            altDriver.DeleteKeyPlayerPref(keyName);

            Assert.Throws<NotFoundException>(
                () => altDriver.GetStringKeyPlayerPref(keyName));
        }
        [Test]
        public void TestDeletePlayerPref()
        {
            var keyName = "testMe";
            var anotherKeyName = "keyTwo";
            TestStringKeyPlayerPref(keyName);
            TestFloatKeyPlayerPref(anotherKeyName);

            altDriver.DeletePlayerPref();
            Assert.Throws<NotFoundException>(
                () => altDriver.GetStringKeyPlayerPref(keyName));

            Assert.Throws<NotFoundException>(
                () => altDriver.GetFloatKeyPlayerPref(anotherKeyName));
        }
        [Test]
        public void TestGetServerVersion()
        {
            var serverVersion = altDriver.GetServerVersion();
            Console.WriteLine("App was instrumented with server version: " + serverVersion);
            Assert.That(serverVersion, Is.EqualTo("1.8.0"));
        }
        [Test]
        public void TestGetAllScenes()
        {
            var listSceneNames = altDriver.GetAllScenes();
            Assert.That(listSceneNames.Count(), Is.EqualTo(3));

            Assert.That(listSceneNames, Is.EqualTo(ListOfScenes()));
        }
        [Test]
        public void TestGetAllCameras()
        {
            var listCameras = altDriver.GetAllCameras();
            Assert.That(listCameras.Count(), Is.EqualTo(2));

            Assert.That(listCameras[0].name, Is.EqualTo("UICamera"));
            Assert.That(listCameras[1].name, Is.EqualTo("Main Camera"));
        }
        [Test]
        public void TestGetActiveCameras()
        {
            var listActiveCameras = altDriver.GetAllActiveCameras();
            Assert.That(listActiveCameras.Count(), Is.EqualTo(2));

            Assert.That(listActiveCameras[0].name, Is.EqualTo("UICamera"));
            Assert.That(listActiveCameras[1].name, Is.EqualTo("Main Camera"));

            //toDo: research how to de-activate camera an FindObject mentioning Camera
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
        public static List<string> ListOfScenes()
        {
            var listScenes = new List<string>()
            {
                "Main",
                "Shop",
                "Start"
            };
            return listScenes;
        }
    }
}