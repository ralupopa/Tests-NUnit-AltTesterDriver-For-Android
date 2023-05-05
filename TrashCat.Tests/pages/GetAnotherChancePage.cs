namespace TrashCat.Tests.pages
{
    public class GetAnotherChancePage : BasePage
    {
        public GetAnotherChancePage(AltDriver driver) : base(driver)
        {
        }

        public AltObject GameOverButton { get => Driver.WaitForObject(By.NAME, "GameOver", timeout: 2); }
        public AltObject PremiumButton { get => Driver.WaitForObject(By.NAME, "Premium Button", timeout: 2); }
        public AltObject AvailableCurrency { get => Driver.WaitForObject(By.NAME, "PremiumOwnCount", timeout: 2); }

        public bool IsDisplayed()
        {
            if (GameOverButton != null && PremiumButton != null && AvailableCurrency != null)
                return true;
            return false;
        }
        public void PressGameOver()
        {
            GameOverButton.Tap();
        }
        public void PressPremiumButton()
        {
            PremiumButton.Tap();
        }
    }
}