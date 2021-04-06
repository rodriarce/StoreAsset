using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.AdminModels;
using System.IO;

public enum KindOfOperation
{
    isAdd,
    isUpdate,
    isDelete

}


[ExecuteAlways]
public class PlayFabItems : MonoBehaviour
{
    public GameObject prefabItem;
    public Transform parentWeapons;
    public Transform parentArmor;
    public Transform parentItems;
    public Transform parentCoins;
    public static List<CatalogItem> itemsToAdd = new List<CatalogItem>();
    public static CatalogItem currentItem;
    public static List<CatalogItem> shopItems = new List<CatalogItem>();
    private static bool hasItem;
    private static bool hasItemToDelete;
    private static CatalogItem itemToDelete;
    private static string nameDeleteItem;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public static void AddItems(CatalogItem newItem)
    {

        //var totalItems = Resources.LoadAll("Items", typeof(StoreItems));
        currentItem = newItem;
        GetCurrentItems(KindOfOperation.isAdd);
        //UpdateStoreItems(itemsToAdd);
        
       
    }

    public static void RemoveItem(string nameItem)
    {
        nameDeleteItem = nameItem;
        GetCurrentItems(KindOfOperation.isDelete);
    }


    public static void UpdateItem(CatalogItem newItem)
    {
        currentItem = newItem;
        GetCurrentItems(KindOfOperation.isUpdate);
    }


    public void GetShopItems()
    {
        GetCatalogItemsRequest request = new GetCatalogItemsRequest();
        request.CatalogVersion = "Items";
        PlayFabAdminAPI.GetCatalogItems(request, OnGetShopItems, error => { Debug.Log(error.GenerateErrorReport()); });

    }
    private void OnGetShopItems(GetCatalogItemsResult result)
    {
        shopItems = result.Catalog;
        Debug.Log("You Get an Item");
        GameLoader.gameLoader.isShopItems = true;
        CreateItem();
        
    }

    private void CreateItem()
    {
        // Fetch items in content panel
        //var totalItems = Resources.LoadAll("Items", typeof(StoreItems));
        foreach (CatalogItem item in shopItems)
        {
            switch (item.ItemClass)
            {
                case "Weapons":
                    GameObject newWeapon = Instantiate(prefabItem);
                    newWeapon.transform.SetParent(parentWeapons);
                    newWeapon.transform.localScale = Vector3.one;
                    newWeapon.GetComponent<ItemStore>().kindOfItem = KindOfItem.isWeapon;
                    newWeapon.GetComponent<ItemStore>().SetItemsName(item);

                    //weaponItems.Add(item);
                    break;
                case "Armor":
                    GameObject newArmor = Instantiate(prefabItem);
                    newArmor.transform.SetParent(parentArmor);
                    newArmor.transform.localScale = Vector3.one;
                    newArmor.GetComponent<ItemStore>().kindOfItem = KindOfItem.isArmor;
                    newArmor.GetComponent<ItemStore>().SetItemsName(item);

                    //armorItems.Add(item);
                    break;
                case "Items":
                    GameObject newOther = Instantiate(prefabItem);
                    newOther.transform.SetParent(parentItems);
                    newOther.transform.localScale = Vector3.one;
                    newOther.GetComponent<ItemStore>().kindOfItem = KindOfItem.isItem;
                    newOther.GetComponent<ItemStore>().SetItemsName(item);
                    //otherItems.Add(item);
                    break;
                case "Coins":
                    GameObject newCoins = Instantiate(prefabItem);
                    newCoins.transform.SetParent(parentCoins);
                    newCoins.transform.localScale = Vector3.one;
                    newCoins.GetComponent<ItemStore>().kindOfItem = KindOfItem.isCoin;
                    newCoins.GetComponent<ItemStore>().SetItemsName(item);

                    break;

                //case 
            }



         

        }

    }    

    public static void GetCurrentItems(KindOfOperation kindOfOperation)
    {
        itemsToAdd.Clear();
        GetCatalogItemsRequest request = new GetCatalogItemsRequest();
        request.CatalogVersion = "Items";
        switch (kindOfOperation)
        {
            case KindOfOperation.isAdd:
                PlayFabAdminAPI.GetCatalogItems(request, OnGetItems, error => { Debug.Log(error.GenerateErrorReport()); });
                break;
            case KindOfOperation.isUpdate:
                PlayFabAdminAPI.GetCatalogItems(request, OnUpdateNewItem, error => { Debug.Log(error.GenerateErrorReport()); });
                break;
            case KindOfOperation.isDelete:
                PlayFabAdminAPI.GetCatalogItems(request, OnDeleteItem, error => { Debug.Log(error.GenerateErrorReport()); });
                break;
        }
        
    }

    private static void OnGetItems(GetCatalogItemsResult result)
    {
        
        itemsToAdd = result.Catalog;
        itemsToAdd.Add(currentItem);
        UpdateStoreItems(itemsToAdd);
        
        
    }


    private static void OnDeleteItem(GetCatalogItemsResult result)
    {
        itemsToAdd = result.Catalog;
        foreach (var item in itemsToAdd)
        {
            if (item.ItemId == nameDeleteItem)
            {

                hasItemToDelete = true;
                itemToDelete = item;
            }
        }
        if (hasItemToDelete)
        {
            itemsToAdd.Remove(itemToDelete);
            UpdateStoreItems(itemsToAdd);
        }
       
    }

    private static void OnUpdateNewItem(GetCatalogItemsResult result)
    {
        itemsToAdd = result.Catalog;
        hasItem = false;
        foreach (var item in itemsToAdd)
        {
            if (item.ItemId == currentItem.ItemId)
            {
                hasItem = true;
              
                
            }
        }

        if (hasItem)
        {
            itemsToAdd.Add(currentItem);
        }

        UpdateStoreItems(itemsToAdd);


    }


    

    public static void UpdateStoreItems(List<CatalogItem> items)
    {
        UpdateCatalogItemsRequest request = new UpdateCatalogItemsRequest();
        request.CatalogVersion = "Items";
        request.Catalog = items;
        PlayFabAdminAPI.SetCatalogItems(request, OnUpdateItems, error => { Debug.Log(error.GenerateErrorReport()); });
        
        
    }
    private static void OnUpdateItems(UpdateCatalogItemsResult result)
    {
        Debug.Log("Succes Update Items");
    }
 
    




    public void GetStoreItems()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
