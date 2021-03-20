using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PurchaseItemRequest request = new PurchaseItemRequest();
        request.CatalogVersion = "Weapons";
        request.VirtualCurrency = "CO";
        request.ItemId = "StarterKit";
        request.Price = 0;
        PlayFabClientAPI.PurchaseItem(request,
            result => { Debug.Log("You have claimed the Starter kit");},
            error => { Debug.Log(error.ErrorMessage);});

                
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
