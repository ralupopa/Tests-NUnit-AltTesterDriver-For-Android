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
        public AltObject CoinFindObjectByName { get => Driver.FindObject(By.NAME, "Coin"); }
        public AltObject CoinFindObjectByPath { get => Driver.FindObject(By.PATH, "/Canvas/Background/Coin"); }
        public List<AltObject> FindObjectsByTagUntagged { get => Driver.FindObjects(By.TAG, "Untagged"); }
        public AltObject FindObjectByComponentNIS { get => Driver.FindObject(By.COMPONENT, "Altom.AltTester.NewInputSystem"); }
        public List<AltObject> FindObjectsByComponentShopList { get => Driver.FindObjects(By.COMPONENT, "ShopItemListItem"); }
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
        public AltObject MagnetFindObjectByText { get => Driver.FindObject(By.TEXT, "Magnet"); }
        public List<AltObject> FindObjectsByTextBuy { get => Driver.FindObjects(By.TEXT, "BUY"); }
        public AltObject FindObjectWhichContainsPremium { get => Driver.FindObjectWhichContains(By.NAME, "Premium"); }
        public List<AltObject> FindObjectsWhichContainItemEntry { get => Driver.FindObjectsWhichContain(By.NAME, "ItemEntry(Clone"); }
        public AltObject StoreTitleFindObjectAtCoordinates { get => Driver.FindObjectAtCoordinates(new AltVector2(548, 568)); }
        public List<AltObject> GetAllEnabledObjects { get => Driver.GetAllElements(enabled: true); }
        public List<AltObject> GetAllDisabledObjects { get => Driver.GetAllElements(enabled: false); }
        public AltObject WaitForObjectWhichContainsPremium { get => Driver.WaitForObjectWhichContains(By.NAME, "Premi", timeout: 5); }
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
        public void TapStoreToIncreaseCoins()
        {
            Store.Tap();
        }
        public string GetCoinsCounterText()
        {
            return CoinsCounter.GetText();
        }
        public void PressBuy()
        {
            var parentButton = BuyButtonText.GetParent();
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
        public float GetColorOfObject()
        {
            return ItemsTab.GetComponentProperty<float>("UnityEngine.UI.Button", "colors.highlightedColor.r", "UnityEngine.UI");
        }

        public object CallComponentMethodGetColor()
        {
            return ItemsTab.CallComponentMethod<object>("UnityEngine.UI.Button", "get_colors", "UnityEngine.UI", new object[] { });
        }

        public string GetCurrentSelectionForObject(AltObject Object)
        {
            return Object.GetComponentProperty<string>("UnityEngine.UI.Button", "currentSelectionState", "UnityEngine.UI");
        }
    }
}