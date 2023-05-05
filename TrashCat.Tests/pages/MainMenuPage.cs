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

        public bool IsDisplayed()
        {
            if (StoreButton != null && LeaderBoardButton != null && SettingsButton != null && MissionButton != null 
            && RunButton != null && CharacterName != null && ThemeZone != null)
                return true;
            return false;
        }
        public void PressRun()
        {
            RunButton.Tap();
        }
        public void PressLeaderboard()
        {
            LeaderBoardButton.Tap();
        }
        public string GetLeaderboardText()
        {
            return LeaderBoardText.GetText();
        }
        public void PressCloseLeaderboard()
        {
            CloseLeaderBoardButton.Tap();
        }
        public void PressMissions()
        {
            MissionButton.Tap();
        }
        public string GetMissionsText()
        {
            return MissionsText.GetText();
        }
        public void PressCloseMissions()
        {
            CloseMissionsButton.Tap();
        }
        public void PressStore()
        {
            StoreButton.Tap();
        }
        
        public void PressSettings()
        {
            SettingsButton.Tap();
        }
        public string GetSettingsText()
        {
            return SettingsText.GetText();
        }
        public void PressCloseSettings()
        {
            CloseSettingsButton.Tap();
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
        public void PressButtonRight()
        {
            ButtonRight.Tap();
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
            PressSettings();
            DeleteDataButton.Tap();
            YESButton.Tap();
            PressCloseSettings();
        }

    }
}