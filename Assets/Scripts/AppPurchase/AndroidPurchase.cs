using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using PlayFab;
using PlayFab.ClientModels;

public class AndroidPurchase : MonoBehaviour, IStoreListener
{
    public static AndroidPurchase androidPurchase;
    public IStoreController storeController;
    public List<string> itemsIds = new List<string>();
    private CoinsItems[] currentItems;
   
    
    
    // Start is called before the first frame update

    private void Awake()
    {
        if (androidPurchase == null)
        {
            androidPurchase = this;
        }
    }


    void Start()
    {
        
    }

    public void InitPurchase()
    {
        
       var module = StandardPurchasingModule.Instance(AppStore.GooglePlay);
       var builder = ConfigurationBuilder.Instance(module);
       currentItems = Resources.LoadAll<CoinsItems>("Coins");
        if (currentItems == null)
        {
            GameLoader.gameLoader.isGoogleInit = true;
            return;
        }
        foreach (CoinsItems item in currentItems)
        {
            builder.AddProduct(item.productId, ProductType.Consumable);
            itemsIds.Add(item.productId);
        }


       //builder.AddProduct(oneDollarItem, ProductType.Consumable);
       //builder.AddProduct(fiveDolarItem, ProductType.Consumable);
       UnityPurchasing.Initialize(this, builder);

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        GameLoader.gameLoader.isGoogleInit = true;
        //m_StoreController = controller;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log(error);
        GameLoader.gameLoader.isGoogleInit = true;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }


    public void BuyCoin(string productId)
    {
        storeController.InitiatePurchase(productId);// StartPurchase
        
    }
    

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {
        if (e.purchasedProduct == null)
        {
            Debug.LogWarning("Attempted to process purchase with unknown product. Ignoring");
            return PurchaseProcessingResult.Complete;
        }
        
        // Test edge case where purchase has no receipt
        if (string.IsNullOrEmpty(e.purchasedProduct.receipt))
        {
            Debug.LogWarning("Attempted to process purchase with no receipt: ignoring");
            return PurchaseProcessingResult.Complete;
        }

        Debug.Log("Processing transaction: " + e.purchasedProduct.transactionID);
        foreach (CoinsItems items in currentItems)
        {
            if (e.purchasedProduct.definition.id == items.productId)
            {
                PlayFabAuth.playFabAuth.AddCurrency(items.amountOfCoins);// Grant Currency After Pay Item
                StoreObjects.storeObjects.panelResult.SetActive(true);
                return PurchaseProcessingResult.Complete;

            }



        }
        // Deserialize receipt
               
        return PurchaseProcessingResult.Complete;
        // Invoke receipt validation
        // This will not only validate a receipt, but will also grant player corresponding items
        // only if receipt is valid.


    }


    

    

}
