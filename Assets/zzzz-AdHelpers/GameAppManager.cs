using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

[Serializable]
public class InAppItem
{
    public string iapItem_Name;
    public ProductType producttype;

}

public class GameAppManager : MonoBehaviour, IStoreListener
{
    public static GameAppManager instance_;
    public static GameAppManager instance
    {
        get
        {
            if (!instance_)
                instance_ = GameObject.FindObjectOfType<GameAppManager>();

            return instance_;
        }
    }
    public InAppItem[] iapitems = null;
    public static event EventHandler consumable_events;
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    public static bool check_Unlockall = false;
    public static string remove_AdsString = "remove_adds";
    public static string UnlockAll = "unlockall";
    //public static string UnlockCars="unlockcars";
    private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";
    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";
    [SerializeField] PetSelectionMainMenu petselection;
    [SerializeField] GrannySelection grannySelection;
    void Awake()
    {
        //		instance = this;
        DontDestroyOnLoad(instance);
    }
    void Start()
    {

        if (m_StoreController == null)
        {
            //			Invoke ("InitializePurchasing",3f);
            print("dasda");
            InitializePurchasing();
        }
    }

    public void Btn_Buy_Removeads()
    {
        Debug.Log("Btn_Buy_Removeads");
        Buy_Product(0);
    }

    public void Unlock_All_Pets()
    {
        Debug.Log("Btn_Buy_Pets");
        Buy_Product(1);

    }
    public void Unlock_All_Grans()
    {
        Debug.Log("Btn_Buy_Grans");
        Buy_Product(2);

    }
    public void Btn_Buy_Everything()
    {
        Debug.Log("Btn_Buy_Everything");
        Buy_Product(3);
    }
    public void BuySecondPet()
    {
        Buy_Product(4);
    }
    public void BuyThirdPet()
    {
        Buy_Product(5);

    }
    public void BuySecondGran()
    {
        Buy_Product(6);

    }
    public void BuyThirdGran()
    {
        Buy_Product(7);

    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {

            // ... we are done here.
            return;
        }


        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(remove_AdsString, ProductType.NonConsumable);
        //		builder.AddProduct(UnlockAll, ProductType.NonConsumable);
        for (int i = 0; i < GameAppManager.instance.iapitems.Length; i++)
        {
            builder.AddProduct(GameAppManager.instance.iapitems[i].iapItem_Name, GameAppManager.instance.iapitems[i].producttype);
        }
        builder.Configure<IGooglePlayConfiguration>().SetDeferredPurchaseListener(OnDeferredPurchase);
        UnityPurchasing.Initialize(this, builder);
    }
    void OnDeferredPurchase(Product product)
    {
        Debug.Log($"Purchase of {product.definition.id} is deferred");
        //btnGold.enabled = false;

    }
    public void OnPurchaseDeferred(Product product)
    {

        Debug.Log("Deferred product " + product.definition.id.ToString());
    }
    public bool IsInitialized()
    {
        print("Pass");
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    void Buy_noAds()
    {
        print("Buy_noAds");
        if (IsInitialized())
        {
            print("IsInitialized*****************");

            if (!CheckProductID_Status(remove_AdsString))
            {
                BuyProductID(remove_AdsString);
            }
        }
    }
    public void Buy_unlockall()
    {
        if (IsInitialized())
        {
            if (!CheckProductID_Status(UnlockAll))
            {
                BuyProductID(UnlockAll);
            }
        }

    }
    public void Buy_Product(int iapID)
    {
        if(AdmobAdsManager.Instance)
        AdmobAdsManager.Instance.IAP_Chk = true;

        if (IsInitialized())
        {
            if (GameAppManager.instance.iapitems[iapID].producttype == ProductType.NonConsumable)
            {
                if (!CheckProductID_Status(GameAppManager.instance.iapitems[iapID].iapItem_Name))
                {
                    BuyProductID(GameAppManager.instance.iapitems[iapID].iapItem_Name);
                }
            }
            else
            {
                BuyProductID(GameAppManager.instance.iapitems[iapID].iapItem_Name);
            }
        }
    }

    public bool CheckProductID_Status(string productId)
    {
        Product product = m_StoreController.products.WithID(productId);
        if (product != null && product.hasReceipt)
        {

            return true;
        }
        else
        {
            return false;
        }
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }

        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }



    public void RestorePurchases()
    {

        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }


        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {

            Debug.Log("RestorePurchases started ...");


            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions((result) =>
            {

                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }

        else
        {

            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }


    //  
    // --- IStoreListener
    //

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;

        m_StoreExtensionProvider = extensions;
        if (IsInitialized())
        {
            if (CheckProductID_Status(remove_AdsString))
            {
                //Tenlogiclocal.Ads_purchase = true;
                //	Debug.Log ("ads are purchase");
            }

            if (CheckProductID_Status(UnlockAll))
            {
                check_Unlockall = true;
                //	Debug.Log ("ads are purchase");
            }
        }
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {

        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {

        if (String.Equals(args.purchasedProduct.definition.id, remove_AdsString, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
        }
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[0].iapItem_Name, StringComparison.Ordinal))//unlock_all
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            PlayerPrefs.SetInt("noADS", 1);
            AdmobAdsManager.Instance.Skip_Appopen = true;
            AdmobAdsManager.Instance.Skip_Banner_Top = true;
            AdmobAdsManager.Instance.Skip_MedRec= true;
            Debug.Log("Removed all ads");

        }
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[1].iapItem_Name, StringComparison.Ordinal))//unlock_player
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            petselection.UnlockAllPets();
            Debug.Log("Unlocked all pets"); 

        }
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[2].iapItem_Name, StringComparison.Ordinal))//unlock_levels
        {

            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            grannySelection.UnlockAllGarns();   
            Debug.Log("Unlocked all Grans");

        }
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[3].iapItem_Name, StringComparison.Ordinal))//cars
        {

            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            petselection.UnlockAllPets();
            grannySelection.UnlockAllGarns();
            AdmobAdsManager.Instance.Skip_Appopen = true;
            AdmobAdsManager.Instance.Skip_Banner_Top = true;
            AdmobAdsManager.Instance.Skip_MedRec = true;
            PlayerPrefs.SetInt("noADS", 1);
            Debug.Log("Removed all ads");
        }
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[4].iapItem_Name, StringComparison.Ordinal))//cars
        {

            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            petselection.UnlockPetAtIndex(1);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[5].iapItem_Name, StringComparison.Ordinal))//cars
        {

            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            petselection.UnlockPetAtIndex(2);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[6].iapItem_Name, StringComparison.Ordinal))//cars
        {

            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            grannySelection.UnlockGranAtIndex(1);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, GameAppManager.instance.iapitems[7].iapItem_Name, StringComparison.Ordinal))//cars
        {

            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            grannySelection.UnlockGranAtIndex(2);
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        InAppPurchased();
        Invoke(nameof(LoadScene), 3f);

        //Data.SaveData();
        return PurchaseProcessingResult.Complete;
    }

    void InAppPurchased()
    { 
        if(AdmobAdsManager.Instance)
            AdmobAdsManager.Instance.Btn_InApp_Done();
    }

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ReLoad()
    {
        string xXx = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(xXx);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {

        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void give_CosumeEvent()
    {
        if (consumable_events != null)
            consumable_events(null, null);
    }

    public void removeall_ConsumeEvent()
    {
        consumable_events = null;
    }
    // Add New Line
    void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new NotImplementedException();
    }
    void IStoreListener.OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new NotImplementedException();
    }
}

