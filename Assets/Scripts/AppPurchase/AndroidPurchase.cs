using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

using PlayFab;
using PlayFab.ClientModels;

public class AndroidPurchase : MonoBehaviour, IStoreListener
{
    public IStoreController storeController;
    public string oneDollarItem;
    public string fiveDolarItem;
    public int amountCoinsOneDollar;
    public int amountConsFiveDollar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void InitPurchase()
    {
        
       var module = StandardPurchasingModule.Instance(AppStore.GooglePlay);
       var builder = ConfigurationBuilder.Instance(module);
       builder.AddProduct(oneDollarItem, ProductType.Consumable);
       builder.AddProduct(fiveDolarItem, ProductType.Consumable);
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
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }


    public void BuyCoinOneDolar()
    {
        storeController.InitiatePurchase(oneDollarItem);// StartPurchase
    }
    public void BuyCoinFiveDolar()
    {
        storeController.InitiatePurchase(fiveDolarItem);
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

        // Deserialize receipt
        if (e.purchasedProduct.definition.id == oneDollarItem)
        {
            PlayFabAuth.playFabAuth.AddCurrency(amountCoinsOneDollar);// Grant Currency After Pay Item
            StoreObjects.storeObjects.panelResult.SetActive(true);
            return PurchaseProcessingResult.Complete;

        }
        if (e.purchasedProduct.definition.id == fiveDolarItem)
        {
            PlayFabAuth.playFabAuth.AddCurrency(amountConsFiveDollar);
            StoreObjects.storeObjects.panelResult.SetActive(true);
            return PurchaseProcessingResult.Complete;
        }

        return PurchaseProcessingResult.Complete;
        // Invoke receipt validation
        // This will not only validate a receipt, but will also grant player corresponding items
        // only if receipt is valid.


    }


    

    

}
