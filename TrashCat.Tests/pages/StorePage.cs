namespace TrashCat.Tests.pages
{
    public class StorePage : BasePage
    {
        public StorePage(AltDriver driver) : base(driver)
        {
        }
        public void LoadScene()
        {
            Driver.LoadScene("Main");
        }
        public AltObject Store { get => Driver.WaitForObject(By.NAME, "StoreTitle", timeout: 10); }
        public AltObject StoreText { get => Driver.WaitForObject(By.NAME, "StoreTitle", timeout: 10); }
        public List<AltObject> StoreTabsList { get => Driver.FindObjects(By.PATH, "/Canvas/Background/TabsSwitch/*"); }
        public AltObject OwnedButtonText { get => Driver.WaitForObject(By.PATH, "//BuyButton/Text[@text=Owned]", timeout: 10); }
        public AltObject BuyButtonText { get => Driver.WaitForObject(By.PATH, "//BuyButton/Text[@text=BUY]", timeout: 10); }
        public List<AltObject> BuyButtonList { get => Driver.FindObjects(By.NAME, "BuyButton"); }
        public AltObject CoinElement { get => Driver.WaitForObject(By.NAME, "Coin", timeout: 10); }
        public AltObject CoinsCounter { get => Driver.WaitForObject(By.NAME, "CoinsCounter", timeout: 10); }
        public AltObject PremiumCounter { get => Driver.WaitForObject(By.NAME, "Premium", timeout: 10); }
        public AltObject ThemesTab { get => Driver.WaitForObject(By.NAME, "Themes", timeout: 10); }
        public AltObject ItemsTab { get => Driver.WaitForObject(By.NAME, "Item", timeout: 10); }
        public AltObject CharactersTab { get => Driver.WaitForObject(By.NAME, "Character", timeout: 10); }
        public AltObject AccessoriesTab { get => Driver.WaitForObject(By.NAME, "Accesories", timeout: 10); }
        public AltObject CloseStoreButton { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/Button", timeout: 10); }
        public List<AltObject> ItemsList { get => Driver.FindObjects(By.PATH, "/Canvas/Background/ItemsList/Container/ItemEntry(Clone)/NamePriceButtonZone/Name"); }
        public List<AltObject> CharactersList { get => Driver.FindObjects(By.PATH, "/Canvas/Background/CharacterList/Container/ItemEntry(Clone)/NamePriceButtonZone/Name");}
        public List<AltObject> AccessoriesList { get => Driver.FindObjects(By.PATH, "/Canvas/Background/CharacterAccessoriesList/Container/ItemEntry(Clone)"); }
        public List<AltObject> AccessoriesHeaderCharacter { get => Driver.FindObjects(By.PATH, "/Canvas/Background/CharacterAccessoriesList/Container/Header(Clone)"); }
        public List<AltObject> ThemesList { get => Driver.FindObjects(By.PATH, "/Canvas/Background/ThemeList/Container/ItemEntry(Clone)/NamePriceButtonZone/Name"); }

        public bool IsDisplayed()
        {
            if (Store != null && CoinElement != null 
                && CoinsCounter != null && PremiumCounter != null)
                return true;
            return false;
        }
        public string GetStoreText()
        {
            return StoreText.GetText();
        }
        public void TapCloseStore()
        {
            CloseStoreButton.Tap();
        }
        public void PressStoreToIncreaseCoins()
        {
            Store.Tap();
        }
        public string GetCoinsCounterText()
        {
            return CoinsCounter.GetText();
        }
        public void PressBuy()
        {
            var parentButton = BuyButtonText.getParent();
            parentButton.Tap();
        }

        public void PressCharacters()
        {
            CharactersTab.Tap();
        }

        public void PressAccessories()
        {
            AccessoriesTab.Tap();
        }
        public void PressThemes()
        {
            ThemesTab.Tap();
        }

        public bool StoreTabsAreDisplayed()
        {
            if (ItemsTab != null && CharactersTab != null 
            && AccessoriesTab != null && ThemesTab != null)
                return true;
            return false;
        }
    }
}