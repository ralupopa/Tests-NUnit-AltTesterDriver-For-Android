namespace TrashCat.Tests.pages
{
    public class PauseOverlayPage : BasePage
    {
        public PauseOverlayPage(AltDriver driver) : base(driver)
        {
        }

        public AltObject ResumeButton { get => Driver.WaitForObject(By.NAME, "Resume", timeout: 2); }
        public AltObject MainMenuButton { get => Driver.WaitForObject(By.NAME, "Exit", timeout: 2); }
        public AltObject Title { get => Driver.WaitForObject(By.NAME, "Text", timeout: 2); }

        public bool IsDisplayed()
        {
            if (ResumeButton != null && MainMenuButton != null && Title != null)
                return true;
            return false;
        }
        public void TapResume()
        {
            ResumeButton.Tap();
        }
        public void TapMainMenu()
        {
            MainMenuButton.Tap();
        }
    }
}