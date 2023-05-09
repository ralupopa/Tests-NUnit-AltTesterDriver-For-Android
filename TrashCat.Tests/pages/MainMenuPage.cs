namespace TrashCat.Tests.pages
{
    public class MainMenuPage : BasePage
    {
        public MainMenuPage(AltDriver driver) : base(driver)
        {
        }
        public void LoadScene()
        {
            Driver.LoadScene("Main");
        }

        public AltObject StoreButton { get => Driver.WaitForObject(By.NAME, "StoreButton", timeout: 10); }
        public AltObject LeaderBoardButton { get => Driver.WaitForObject(By.NAME, "OpenLeaderboard", timeout: 10); }
        public AltObject SettingsButton { get => Driver.WaitForObject(By.NAME, "SettingButton", timeout: 10); }
        public AltObject MissionButton { get => Driver.WaitForObject(By.NAME, "MissionButton", timeout: 10); }
        public AltObject RunButton { get => Driver.WaitForObject(By.NAME, "StartButton", timeout: 10); }
        public AltObject CharacterName { get => Driver.WaitForObject(By.NAME, "CharName", timeout: 10); }
        public AltObject ThemeZone { get => Driver.WaitForObject(By.NAME, "ThemeZone", timeout: 10); }
        public AltObject LeaderBoardText { get => Driver.WaitForObject(By.PATH, "/UICamera/Leaderboard/Background/Text", timeout: 10); }
        public AltObject CloseLeaderBoardButton { get => Driver.WaitForObject(By.NAME, "Button", timeout: 10); }
        public AltObject MissionsText { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/MissionPopup/MissionBackground/Text", timeout: 10); }
        public List<AltObject> MissionsList { get => Driver.FindObjects(By.NAME, "MissionEntry(Clone)"); }
        public AltObject CloseMissionsButton { get => Driver.WaitForObject(By.NAME, "CloseButton", timeout: 10); }
        public AltObject SettingsText { get => Driver.WaitForObject(By.NAME, "Title", timeout: 10); }
        public AltObject CloseSettingsButton { get => Driver.WaitForObject(By.NAME, "CloseButton", timeout: 10); }
        public AltObject MasterSlider { get => Driver.WaitForObject(By.NAME, "MasterSlider", timeout: 10); }
        public AltObject MusicSlider { get => Driver.WaitForObject(By.NAME, "MusicSlider", timeout: 10); }
        public AltObject MasterSFXSlider { get => Driver.WaitForObject(By.NAME, "MasterSFXSlider", timeout: 10); }
        public AltObject ButtonRight { get => Driver.WaitForObject(By.NAME, "ButtonRight", timeout: 10); }
        public AltObject ButtonLeft { get => Driver.WaitForObject(By.NAME, "ButtonLeft", timeout: 10); }
        public AltObject ButtonTop { get => Driver.WaitForObject(By.NAME, "ButtonTop", timeout: 10); }
        public AltObject ButtonBottom { get => Driver.WaitForObject(By.NAME, "ButtonBottom", timeout: 10); }
        public AltObject AccessoriesSelector { get => Driver.WaitForObject(By.NAME, "AccessoriesSelector", timeout: 10); }
        public AltObject ThemeName { get => Driver.WaitForObject(By.NAME, "ThemeName", timeout: 10); }
        public AltObject DeleteDataButton { get => Driver.WaitForObject(By.NAME, "DeleteData", timeout: 10); }
        public AltObject YESButton { get => Driver.WaitForObject(By.NAME, "YESButton", timeout: 10); }
        public AltObject FindObjectByComponentLoadoutState { get => Driver.FindObject(By.COMPONENT, "LoadoutState"); }
        public AltObject StartButtonChild { get => Driver.FindObject(By.PATH, "//StartButton/Text"); }
        public AltObject AboutButton { get => Driver.FindObject(By.NAME, "About"); }
        public AltObject CloseStoreButton { get => Driver.FindObject(By.PATH, "/Canvas/Background/Button"); }
        public bool IsDisplayed()
        {
            if (StoreButton != null && LeaderBoardButton != null && SettingsButton != null && MissionButton != null 
            && RunButton != null && CharacterName != null && ThemeZone != null)
                return true;
            return false;
        }
        public void TapOnObject(AltObject objectTo)
        {
            objectTo.Tap();
        }
        public string GetLeaderboardText()
        {
            return LeaderBoardText.GetText();
        }
        public string GetMissionsText()
        {
            return MissionsText.GetText();
        }
        public string GetSettingsText()
        {
            return SettingsText.GetText();
        }
        public bool SettingsSlidersAreDisplayed()
        {
            if (MasterSlider != null && MusicSlider != null 
            && MasterSFXSlider != null)
                return true;
            return false;
        }
        public bool ButtonsLeftRightAreDisplayed()
        {
            if (ButtonRight != null && ButtonLeft != null )
                return true;
            return false;
        }
        public bool ButtonsTopBottomAreDisplayed()
        {
            if (ButtonTop != null && ButtonBottom != null )
                return true;
            return false;
        }
        public bool AccessoriesSelectorIsDisplayed()
        {
            if (AccessoriesSelector != null)
                return true;
            return false;
        }
        public string GetThemeNameText()
        {
            return ThemeName.GetText();
        }
        public string GetCharacterNameText()
        {
            return CharacterName.GetText();
        }
        public void DeleteData()
        {
            SettingsButton.Tap();
            DeleteDataButton.Tap();
            YESButton.Tap();
            CloseSettingsButton.Tap();
        }
        public string GetCharNameDisplay()
        {
            return FindObjectByComponentLoadoutState.GetComponentProperty<string>("LoadoutState", "charNameDisplay.text", "Assembly-CSharp");
        }
        public void SetCharNameDisplay(string valueToSet)
        {
            Assert.That(GetCharNameDisplay(), Is.EqualTo("Trash Cat"));
            FindObjectByComponentLoadoutState.SetComponentProperty("LoadoutState", "charNameDisplay.text", valueToSet, "Assembly-CSharp");
        }
        public string GetTextRunButton()
        {
            return StartButtonChild.GetText();
        }
        public void SetTextRunButton(string valueToSet)
        {
            StartButtonChild.SetText(valueToSet);
        }
        public string GetCurrentSelectionForObject(AltObject Object)
        {
            return Object.GetComponentProperty<string>("UnityEngine.UI.Button", "currentSelectionState", "UnityEngine.UI");
        }
        public short GetScreenWidth()
        {
            return Driver.CallStaticMethod<short>("UnityEngine.Screen", "get_width", "UnityEngine.CoreModule", new string[] { }, null);
        }
        public short GetScreenHeight()
        {
            return Driver.CallStaticMethod<short>("UnityEngine.Screen", "get_height", "UnityEngine.CoreModule", new string[] { }, null);
        }
        public dynamic GetCurrentResolutionUsingGetStaticProperty()
        {
            return Driver.GetStaticProperty<dynamic>("UnityEngine.Screen", "currentResolution", "UnityEngine.CoreModule");
        }
        public string GetScreenWidthFromProperty()
        {
            return Driver.GetStaticProperty<string>("UnityEngine.Screen", "width", "UnityEngine.CoreModule");
        }
        public string GetScreenHeightFromProperty()
        {
            return Driver.GetStaticProperty<string>("UnityEngine.Screen", "height", "UnityEngine.CoreModule");
        }
        public void SetScreenResolutionUsingCallStaticMethod(string widthSet, string heightSet)
        {
            string[] parameters = new[] { widthSet, heightSet, "false" };
            string [] typeOfParameters = new[] { "System.Int32", "System.Int32", "System.Boolean" };
            Driver.CallStaticMethod<string>("UnityEngine.Screen", "SetResolution", "UnityEngine.CoreModule", parameters, typeOfParameters );
        }
        public string UseGetSetStringKeyPlayerPref(string key, string setValue)
        {
            Driver.SetKeyPlayerPref(key, setValue);
            return Driver.GetStringKeyPlayerPref(key);
        }
        public int UseGetSetIntKeyPlayerPref(string key, int setValue)
        {
            Driver.SetKeyPlayerPref(key, setValue);
            return Driver.GetIntKeyPlayerPref(key);
        }
        public float UseGetSetFloatKeyPlayerPref(string key, float setValue)
        {
            Driver.SetKeyPlayerPref(key, setValue);
            return Driver.GetFloatKeyPlayerPref(key);
        }
    }
}