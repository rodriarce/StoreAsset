using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ServerModels;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
    
    public GameObject panelEquipment;
    public List<WeaponItems> weaponsItems = new List<WeaponItems>();
    public List<ArmorItems> armorItems = new List<ArmorItems>();
    public List<OtherItems> otherItems = new List<OtherItems>();
    public GameObject prefabItem;
    public Transform parentWeapon;
    public Transform parentArmor;
    public Transform parentItems;
    public GameObject panelSuccesEquip;
    // Start is called before the first frame update
    private void Awake()
    {
        if (inventory == null)
        {
            inventory = this;
        }
    }
    void Start()
    {
        
    }

    public void GetUserInventory()
    {
        GetCatalogItemsRequest request = new GetCatalogItemsRequest();
        //request.CatalogVersion = "Items";

        PlayFabServerAPI.GetCatalogItems(request, OnGetCatalogItems, error => { Debug.Log(error.GenerateErrorReport()); });
    }

    private void OnGetCatalogItems(GetCatalogItemsResult result)
    {
        foreach (CatalogItem item in result.Catalog)
        {
            switch (item.ItemClass)
            {
                case "Weapons":
                    var newWeapon = GetWeapon(item.ItemId);
                   
                    if (newWeapon != null)
                    {
                        GameObject itemPanel = Instantiate(prefabItem, parentWeapon);
                        itemPanel.GetComponent<ItemInventory>().AddEvent(newWeapon);
                        itemPanel.SetActive(true);

                        weaponsItems.Add(newWeapon);

                    }

                    break;
                case "Armor":
                    var newArmor = GetArmor(item.ItemId);
                    if (newArmor != null)
                    {
                        GameObject itemArmor = Instantiate(prefabItem, parentArmor);
                        itemArmor.GetComponent<ItemInventory>().AddEvent(GetArmor(item.ItemId));
                        itemArmor.SetActive(true);
                        armorItems.Add(newArmor);

                    }

                   
                    break;
                case "Others":
                    var newItem = GetItems(item.ItemId);
                    if (newItem != null)
                    {
                        GameObject newItemPanel = Instantiate(prefabItem, parentItems);
                        newItemPanel.GetComponent<ItemInventory>().AddEvent(GetItems(item.ItemId));
                        newItemPanel.SetActive(true);

                        otherItems.Add(newItem);

                    }
                    

                    break;
            }
        }
        GameLoader.gameLoader.isGetInventory = true;
    }
    


    public WeaponItems GetWeapon(string nameItem)
    {
        foreach (WeaponItems weaponItem in Resources.LoadAll<WeaponItems>("Weapons"))
        {
            if (nameItem == weaponItem.nameItem)
            {
                return weaponItem;
            }
        }
        return null;
    }

    public ArmorItems GetArmor(string nameItem)
    {
        foreach (ArmorItems armorItem in Resources.LoadAll<ArmorItems>("Armor"))
        {
            if (nameItem == armorItem.nameItem)
            {
                return armorItem;
            }
        }
        return null;
    }

    public OtherItems GetItems(string nameItem)
    {
        foreach (OtherItems otherItems in Resources.LoadAll<OtherItems>("Others"))
        {
            if (nameItem == otherItems.nameItem)
            {
                return otherItems;
            }
        }
        return null;
    }


    private void SetGameObject()
    {

    }
    

    

    


    



    

  
}


