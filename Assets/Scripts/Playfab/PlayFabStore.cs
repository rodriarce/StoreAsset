using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ServerModels;

public class PlayFabStore : MonoBehaviour
{
    public static PlayFabStore playFabStore;
    public StoreObjects storeObjects;
    // Start is called before the first frame update


    private void Awake()
    {
        if (playFabStore == null)
        {
            playFabStore = this;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BuyWeapon(string itemId)
    {
        
        GrantItemsToUserRequest request = new GrantItemsToUserRequest();
        request.ItemIds = new List<string>() { itemId };
        request.CatalogVersion = "Weapons";
        request.PlayFabId = PlayFabAuth.playFabAuth.playFabId;
        PlayFabServerAPI.GrantItemsToUser(request, OnBuyItem, error => Debug.Log(error.GenerateErrorReport()));   
    }


    public void BuyArmor(string itemId)
    {
        GrantItemsToUserRequest request = new GrantItemsToUserRequest();
        request.ItemIds = new List<string>() { itemId };
        request.CatalogVersion = "Armor";
        request.PlayFabId = PlayFabAuth.playFabAuth.playFabId;
        PlayFabServerAPI.GrantItemsToUser(request, OnBuyItem, error => Debug.Log(error.GenerateErrorReport()));

    }



    public void BuyOtherItem(string itemId)
    {
        GrantItemsToUserRequest request = new GrantItemsToUserRequest();
        request.ItemIds = new List<string>() { itemId };
        request.CatalogVersion = "Others";
        request.PlayFabId = PlayFabAuth.playFabAuth.playFabId;
        PlayFabServerAPI.GrantItemsToUser(request, OnBuyItem, error => Debug.Log(error.GenerateErrorReport()));

    }

    private void OnBuyItem(GrantItemsToUserResult result)
    {
        Debug.Log("Succes Buy Item");
        StoreObjects.storeObjects.panelResult.SetActive(true);
    }

    public void GetUserWeapons()// Get Player Inventory
    {
        GetUserInventoryRequest request = new GetUserInventoryRequest();
        request.PlayFabId = PlayFabAuth.playFabAuth.playFabId;
        PlayFabServerAPI.GetUserInventory(request, OnResultInventory, error => { Debug.Log(error.GenerateErrorReport()); });
    }
    private void OnResultInventory(GetUserInventoryResult result)
    {
        //Inventory.inventory.rifleWeapons.Clear();
        //Inventory.inventory.shotGunWeapons.Clear();
        //Inventory.inventory.gunWeapons.Clear();
        //Inventory.inventory.magnumWeapons.Clear();

        List<ItemInstance> items = result.Inventory;
        foreach (ItemInstance item in items)
        {
            

            Debug.Log(item.ItemClass);
            Dictionary<string, string> customData = item.CustomData;
            
            //Debug.Log(customData["Damage"]);
            //Debug.Log(customData["Speed"]);
            //Debug.Log(customData["Capacity"]);
        }
        //Inventory.inventory.ShowWeapons();
        //Inventory.inventory.SetEquipItems();
        GameLoader.gameLoader.isGetInventory = true;

    }
    
}
