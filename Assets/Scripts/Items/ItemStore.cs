using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.AdminModels;


public class ItemStore : MonoBehaviour
{
    public Text itemName;
    public Text itemTitle;
    public Text itemDescription;
    public Text itemPrice;
    public Image imageItem;
    public string itemId;
    public int amountCoins;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetItemsName(CatalogItem item)
    {
        itemName.text = item.DisplayName;
        //itemTitle.text = item.nameItem;
        itemDescription.text = item.Description;
        if (item.VirtualCurrencyPrices == null)
        {
            itemPrice.text = "0";
        }
        else
        {
            itemPrice.text = item.VirtualCurrencyPrices["US"].ToString();
        }
        
        imageItem.sprite = GetImageItem(item.ItemId);
    }

    private Sprite GetImageItem(string itemId)
    {
        var totalItems = Resources.LoadAll("Items", typeof(StoreItems));
        foreach (StoreItems item in totalItems)
        {
            if (item.itemId == itemId)
            {
                return item.imageItem;
            }
        }
        return null;
    }

    public void BuyItem()
    {
        //MenuController.menuController.audioButton.Play();
        //StoreObjects.storeObjects.BuyItem(itemId, "Others", amountCoins);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
