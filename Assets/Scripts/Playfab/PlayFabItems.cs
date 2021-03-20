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
    public Transform parentItem;
    public static List<CatalogItem> itemsToAdd = new List<CatalogItem>();
    public static CatalogItem currentItem;
    public static List<CatalogItem> shopItems = new List<CatalogItem>();
    private static bool hasItem;


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
        //var totalItems = Resources.LoadAll("Items", typeof(StoreItems));
        foreach (CatalogItem item in shopItems)
        {
            GameObject newItem = Instantiate(prefabItem);
            newItem.transform.SetParent(parentItem);
            newItem.transform.localScale = Vector3.one;
            newItem.GetComponent<ItemStore>().SetItemsName(item);

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
        }
        
    }

    private static void OnGetItems(GetCatalogItemsResult result)
    {
        
        itemsToAdd = result.Catalog;
        itemsToAdd.Add(currentItem);
        UpdateStoreItems(itemsToAdd);
        
        
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
