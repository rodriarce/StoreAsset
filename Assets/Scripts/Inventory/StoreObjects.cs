using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ServerModels;
using UnityEditor;


[ExecuteAlways]
public class StoreObjects : MonoBehaviour
{ 
    public static StoreObjects storeObjects;
    public List<StoreItems> storeItems = new List<StoreItems>();
       
    
    private int amountToSubstract;
    
    private string itemGranted;
    public GameObject panelResult;
    public GameObject panelError;
    public PlayFabStore playFabStore;
    public GameObject resultEquipItem;
    



    private void Awake()
    {
        if (storeObjects == null)
        {
            storeObjects = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public  void CreateList()
    {
        //storeItems.Clear();
        storeItems = new List<StoreItems>();
    }

    public static void ClearList()
    {
        //storeItems.Clear();
        
    
    }



    public void BuyItem(string itemId, string catalogVersion, int amount)
    {
        if (PlayFabAuth.playFabAuth.amountCurrency < amount)
        {
            panelError.SetActive(true);
            return;
        }
        GrantItemsToUserRequest request = new GrantItemsToUserRequest();
        request.ItemIds = new List<string> {itemId};
        request.CatalogVersion = catalogVersion;
        request.PlayFabId = PlayFabAuth.playFabAuth.playFabId;
        itemGranted = itemId;
        PlayFabServerAPI.GrantItemsToUser(request, OnResultBuyItem, error => { Debug.Log(error.GenerateErrorReport()); });
        amountToSubstract = amount;
    }
    public void SetInitialWeapons(List<string> nameItems)
    {
        GrantItemsToUserRequest request = new GrantItemsToUserRequest();
        request.ItemIds = nameItems;
        request.CatalogVersion = "Weapons";
        request.PlayFabId = PlayFabAuth.playFabAuth.playFabId;
        PlayFabServerAPI.GrantItemsToUser(request, OnSetWeaponsResult, error => { Debug.Log(error.GenerateErrorReport()); });


    }

    private void OnSetWeaponsResult(GrantItemsToUserResult result)
    {
        GameLoader.gameLoader.isInitalWeapons = true;
        Debug.Log(result.ItemGrantResults.Count);
    }

    private void OnResultBuyItem(GrantItemsToUserResult result)
    {
        
        if (itemGranted == "MedicKit")
        {
            int amountUses;
            foreach (var item in result.ItemGrantResults)
            {
                if (item.ItemId == "MedicKit")
                {
                    amountUses = (int)PlayFabAuth.playFabAuth.medicKit.RemainingUses;
                    Register.register.amountMedicKits.text = amountUses.ToString();
                }
            }
           
             
          
            //PlayFabAuth.playFabAuth.medicKit = result.ItemGrantResults.)
            //ItemInstance newItem = new ItemInstance();
            //newItem.ItemId = "MedicKit";
            //newItem.RemainingUses = result.ItemGrantResults.Find(x => x.ItemId == "MedicKit").Re;


        }
        Debug.Log("Succes Buy Item");
        PlayFabAuth.playFabAuth.SubstractCurrency(amountToSubstract);
        panelResult.SetActive(true);
        playFabStore.GetUserWeapons();
        
    }




   
     
    

    
   
    

}
