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
        
        imageItem.sprite = GetImageItem(item.ItemId, item.ItemClass);
    }

    private Sprite GetImageItem(string itemId, string kindOfItem)
    {
        switch (kindOfItem)
        {
            case "Weapons":
                var totalWeapons = Resources.LoadAll("Weapons", typeof(WeaponItems));
                foreach (WeaponItems weapon in totalWeapons)
                {
                    if (weapon.nameItem == itemId)
                    {
                        return weapon.imageItem;
                    }
                }
                break;
            case "Armor":
                var totalArmor = Resources.LoadAll("Armor", typeof(ArmorItems));
                foreach (ArmorItems armor in totalArmor)
                {
                    if (armor.nameItem == itemId)
                    {
                        return armor.imageItem;
                    }
                }



                break;
            case "Others":
                var totalOthers = Resources.LoadAll("Others", typeof(OtherItems));
                foreach (OtherItems item in totalOthers)
                {
                    if (item.nameItem == itemId)
                    {
                        return item.imageItem;
                    }
                }

                break;
            case "Coins":
                var totalCoinsItems = Resources.LoadAll("Coins", typeof(CoinsItems));
                foreach (CoinsItems coinItem in totalCoinsItems)
                {
                    if (coinItem.nameItem == itemId)
                    {
                        return coinItem.imageItem;
                    }
                }

                break;

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
