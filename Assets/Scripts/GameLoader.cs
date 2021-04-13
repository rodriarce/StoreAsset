using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class GameLoader : MonoBehaviour
{
    public static GameLoader gameLoader;
    public PlayFabStore playFabStore;
    public AndroidPurchase androidPurchase;
    public PlayFabReward playFabReward;
    public PlayFabItems playFabItems;
    public bool isRegisted;
    public Action registerPlayer;
    public bool isInitialAmmo;
    public Action setInitialAmmo;
    public bool isInitalWeapons;
    public Action setInitialWeapons;

       
    public bool isLogin;
    public Action loginPlayer;
    public bool isShowCurrency;
    public Action showCurrency;
    public bool isGetStats;
    public Action getStats;
    public bool isGetInventory;
    public Action getInventory;
    public bool isGetLeaderBoard;
    public Action GetLeaderBoard;
    public bool hasTitleData;
    public Action getTitleData;
    public bool isScheduleTask;
    public Action getScheduleTask;
    public bool isGoogleInit;
    public Action initGoogle;
    public bool isConnectToPhoton;
    public Action connectToPhoton;
    public Button[] buttonList;
    public bool isShopItems;
    public Action GetShopItems;
    
    


    private void Awake()
    {
        if (gameLoader == null)
        {
            gameLoader = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadScripts());
    }


    public IEnumerator LoadScripts()
    {
        while (!isRegisted) 
        {
            if (registerPlayer == null)
            {
                registerPlayer = Register.register.CheckPlayer;
                registerPlayer.Invoke();
            }
            yield return null;
        }
       
             while (!isLogin)
        {
            if (loginPlayer == null)
            {
                loginPlayer = Register.register.LoginPlayer;
                loginPlayer.Invoke();
            }
            yield return null;
        }
        while (!isShowCurrency)
        {
            if (showCurrency == null)
            {
                showCurrency = PlayFabAuth.playFabAuth.ShowCurrency;
                showCurrency.Invoke();
            }
            yield return null;
        }
        while (!isGetStats)
        {
            if (getStats == null)
            {
                getStats = PlayFabAuth.playFabAuth.GetStats;
                getStats.Invoke();
            }
            yield return null;
        }

        while (!isShopItems)
        {
            if (GetShopItems == null)
            {
                GetShopItems = playFabItems.GetShopItems;
                GetShopItems.Invoke();
            }
            yield return null;
        }


        while (!isGetInventory)
        {
            if (getInventory == null)
            {
                getInventory = Inventory.inventory.GetUserInventory;
                getInventory.Invoke();
            }
            yield return null;
        }
        
        
        


        while (!isGoogleInit)
        {
            if (initGoogle == null)
            {
                initGoogle = androidPurchase.InitPurchase;
                initGoogle.Invoke();

            }
            yield return null;

        }

       
        foreach (Button button in buttonList)
        {
            button.interactable = true;
        }
        yield return null;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
