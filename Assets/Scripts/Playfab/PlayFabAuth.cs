using PlayFab;
using PlayFab.ServerModels;
using PlayFab.ProfilesModels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;



[System.Serializable]
public class PlayersLeaderboard
{
    public string posPlayer;
    public string namePlayer;
    public string xpPlayer;
    public string totalKills;
    public string coinsPlayer;
}

public class PlayFabAuth : MonoBehaviour
{

    public static PlayFabAuth playFabAuth;// SIngletone
    public string levelToLoad;
    public int levelPlayer;
    public int gameLevel;
    public int totalKills;
    public int xpPlayer;
    public int bullets;
    public int ammoGun;
    public int ammoRifle;
    public int ammoShotGun;
    public int ammoMagnum;
    public ScriptableWeapons selectGun;
    public ScriptableWeapons selectShotGUn;
    public ScriptableWeapons selectRifle;
    public ScriptableWeapons selectMagnum;
    
    public string userNameId;
    public string playFabId;
    public int amountCurrency;
    public List<StatisticUpdate> playerList = new List<StatisticUpdate>();
    private List<PlayerLeaderboardEntry> leaderBoardPlayers;
    private List<StatisticModel> statsPlayer;
    public List<PlayersLeaderboard> playersInLeaderboard = new List<PlayersLeaderboard>();
    public PlayFabStore playFabStore;
    public ItemInstance medicKit;
    public ItemInstance grenadeItem;
    public ItemInstance reward;
    public ItemInstance rewardItem;
    public bool hasReward;
    public bool hasRewardItem;
    public bool getReward;
    //public List<BoardPlayer> boardsPlayers;


    

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "F4844";
        }
            
        
    }
    private void Awake()
    {
        if (playFabAuth == null)
        {
            playFabAuth = this;
        }
                    
        DontDestroyOnLoad(this.gameObject);
    }

    public void GetLeaderBoard()
    {
        GetLeaderboardRequest request = new GetLeaderboardRequest();
        request.MaxResultsCount = 10;
        request.StatisticName = "xpPlayer";
        PlayerProfileViewConstraints playerConst = new PlayerProfileViewConstraints();
        playerConst.ShowStatistics = true;
        playerConst.ShowDisplayName = true;
        request.ProfileConstraints = playerConst;
       
        PlayFabServerAPI.GetLeaderboard(request, OnResultLeaderBoard, OnErrorLeaderBoard);
    }

    
    public void DestroyScript()
    {
        Destroy(gameObject);
    }


    private void OnResultLeaderBoard(GetLeaderboardResult result)
    {
        leaderBoardPlayers = result.Leaderboard; 
        
        foreach (PlayerLeaderboardEntry player in leaderBoardPlayers)
        {

            
            Debug.Log(player.DisplayName);
            Debug.Log(player.StatValue);
            Debug.Log(player.Position);
            PlayersLeaderboard playerData = new PlayersLeaderboard();
            playerData.namePlayer = player.DisplayName;
            
            playerData.xpPlayer = player.StatValue.ToString();
            int posPlayer = player.Position + 1;
            playerData.posPlayer = posPlayer.ToString();
            
            playersInLeaderboard.Add(playerData);
                      
            
            foreach (var playerStats in player.Profile.Statistics)
            {
                if (playerStats.Name == "xpPlayer")
                {
                    playerData.xpPlayer = playerStats.Value.ToString();
                }
                if (playerStats.Name == "totalKills")
                {
                    playerData.totalKills = playerStats.Value.ToString();
                }
                
            }
             
        }
        
        GameLoader.gameLoader.isGetLeaderBoard = true;
    }

    
    private void OnErrorLeaderBoard(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }



    public void SetListStats(string name, int amount)
    {
       
        StatisticUpdate item = new StatisticUpdate();
        item.StatisticName = name;
        item.Value = amount;
        playerList.Add(item);

    }
    
    public void SetStats()
    {

        UpdatePlayerStatisticsRequest request = new UpdatePlayerStatisticsRequest();
        request.Statistics = playerList;
        request.PlayFabId = playFabId;
                       
        PlayFabServerAPI.UpdatePlayerStatistics(request,OnGetStats,
    error => { Debug.LogError(error.GenerateErrorReport()); });
        playerList.Clear();// Clear list when finish 
    }


    private void OnGetStats(UpdatePlayerStatisticsResult result)
    {
        GameLoader.gameLoader.isInitialAmmo = true;
    }


    public void OnResultChangeStats(UpdatePlayerStatisticsResult result)
    {
        if (SceneManager.GetActiveScene().name == "Launcher")
        {
            Debug.Log("Change Statistics");

        }
    }



    public void GetStats()
    {
        GetPlayerStatisticsRequest request = new GetPlayerStatisticsRequest();
        request.PlayFabId = playFabId;
        

        PlayFabServerAPI.GetPlayerStatistics(
            request,
            OnGetStatistics,
            error => Debug.LogError(error.GenerateErrorReport())
        );
    }

    private void OnGetStatistics(GetPlayerStatisticsResult result)
    {
        Debug.Log("Received the following Statistics:");
        foreach (var eachStat in result.Statistics)
        {
                switch (eachStat.StatisticName)
            {
                case "xpPlayer":
                    xpPlayer = eachStat.Value;
                    Register.register.textXp.text = xpPlayer.ToString();
                    break;
                case "totalKills":
                    totalKills = eachStat.Value;
                    Register.register.textKills.text = totalKills.ToString();
                    break;
                case "ammoGun":
                    ammoGun = eachStat.Value;
                    Register.register.ammoGun.text = ammoGun.ToString();
                    break;
                case "ammoShotGun":
                    ammoShotGun = eachStat.Value;
                    Register.register.ammoShotGun.text = ammoShotGun.ToString();
                    break;
                case "ammoRifle":
                    ammoRifle = eachStat.Value;
                    Register.register.ammoRifle.text = ammoRifle.ToString();
                    break;
                case "ammoMagnum":
                    ammoMagnum = eachStat.Value;
                    //Register.register.ammoRifle.text = ammoMagnum.ToString();
                    break;
                                   
            }
        }
        //playFabStore.GetUserWeapons();
        GameLoader.gameLoader.isGetStats = true;
        
    }

    public void AddCurrency(int amount) // Add Currency Player
    {
        AddUserVirtualCurrencyRequest request = new AddUserVirtualCurrencyRequest();
        request.VirtualCurrency = "CO";
        request.Amount = amount;
        request.PlayFabId = playFabId;
        PlayFabServerAPI.AddUserVirtualCurrency(request, OnCurrencyResult, error => { Debug.Log(error.GenerateErrorReport()); });
        
    }

    public void SubstractCurrency(int amount)
    {
        SubtractUserVirtualCurrencyRequest request = new SubtractUserVirtualCurrencyRequest();
        request.VirtualCurrency = "CO";
        request.Amount = amount;
        request.PlayFabId = playFabId;
        PlayFabServerAPI.SubtractUserVirtualCurrency(request, OnCurrencyResult, error => { Debug.Log(error.GenerateErrorReport()); });

    }

    public void OnCurrencyResult(ModifyUserVirtualCurrencyResult result)
    {
        if (SceneManager.GetActiveScene().name == "Launcher")// CHeck if is in the Menu
        {
            amountCurrency = result.Balance;
            Register.register.textCurrency.text = result.Balance.ToString();// Total Currency Value
                        
        }
        
    }   







    public void ShowCurrency()
    {
        GetPlayerCombinedInfoRequest request = new GetPlayerCombinedInfoRequest();
        request.PlayFabId = playFabId;

        request.InfoRequestParameters = new GetPlayerCombinedInfoRequestParams();
        request.InfoRequestParameters.GetUserVirtualCurrency = true;

        PlayFabServerAPI.GetPlayerCombinedInfo(request, GetUserData, error => { Debug.Log(error.GenerateErrorReport()); });
    }



    public void GetUserData(GetPlayerCombinedInfoResult result)
    {
        Dictionary<string, int> myCurrency = new Dictionary<string, int>();
        myCurrency = result.InfoResultPayload.UserVirtualCurrency;
        int currency; 
        myCurrency.TryGetValue("CO", out currency);
        amountCurrency = currency;
        Register.register.textCurrency.text = amountCurrency.ToString();
        Register.register.textCurrency.text = amountCurrency.ToString();
        GameLoader.gameLoader.isShowCurrency = true;

    }
    public void GrantItem(string itemName, string catalogVersion)
    {

       

        GrantItemsToUserRequest request = new GrantItemsToUserRequest();
        request.PlayFabId = playFabId;
        request.ItemIds = new List<string>() { itemName };
        request.CatalogVersion = catalogVersion;
        

        PlayFabServerAPI.GrantItemsToUser(request, OnResultItem, error => Debug.Log(error.GenerateErrorReport()));
    }

    private void OnResultItem(GrantItemsToUserResult result)
    {
        foreach (var item in result.ItemGrantResults)
        {
            if (item.ItemId == "RewardItem")
            {
                Register.register.amountRewards.text = (item.RemainingUses).ToString();
                Register.register.miniGamesButton.interactable = true;
                Debug.Log("Get Reward Item");
            }
        }
    }

    public void GrantCharacter(int kindCharacter, string name)
    {
      
            
            GrantCharacterToUserRequest request = new GrantCharacterToUserRequest();
            request.CharacterName = name;
            request.CharacterType = kindCharacter.ToString();
            request.PlayFabId = playFabId;
            PlayFabServerAPI.GrantCharacterToUser(request, result => { Debug.Log("Grant Character"); }, error => { Debug.Log(error.GenerateErrorReport()); });
               
    }
    
    public void UseItem(ItemInstance newItem)
    {
        
        ConsumeItemRequest request = new ConsumeItemRequest();
        request.ItemInstanceId = newItem.ItemInstanceId;
        request.ConsumeCount = 1;
        request.PlayFabId = playFabId;
        PlayFabServerAPI.ConsumeItem(request, OnUseItem, error => { Debug.Log(error.GenerateErrorReport()); });
        

        
    }

    public void UseRewardItem()
    {
        UseItem(rewardItem);
    }


    public void UseReward()
    {
        UseItem(reward);
        
        StartCoroutine(GrantReward());
        Register.register.nextReward.text = "No More Rewards";
            
        //Register.register.amountRewards.text = (rewardItem.RemainingUses + 1).ToString();
        //Register.register.miniGamesButton.interactable = true;

        // Grant Item
    }

    private IEnumerator GrantReward()
    {
        yield return new WaitForSecondsRealtime(1f);
        GrantItem("RewardItem", "Others");
        Debug.Log("Grant Item");
        //yield return null;
        
    }


    public void GetGameData()
    {
        GetTitleDataRequest request = new GetTitleDataRequest();
        request.Keys = new List<string>() { "News", "MiniGame" };
        PlayFabServerAPI.GetTitleData(request, OnGetTitleData, error => { Debug.Log(error.GenerateErrorReport()); });
    }
    private void OnGetTitleData(GetTitleDataResult result)
    {
        Register.register.newsInfo.text = result.Data["News"];// Set News Info
        string miniGame = result.Data["MiniGame"];
        if (miniGame == "1")
        {
            MenuController.menuController.gameWheel.SetActive(true);
        }
        if (miniGame == "2")
        {
            MenuController.menuController.gameChest.SetActive(true);
        }

        GameLoader.gameLoader.hasTitleData = true;
    }


    private void OnUseItem(ConsumeItemResult result)
    {
        if (result.ItemInstanceId == rewardItem.ItemInstanceId)
        {
            Register.register.amountRewards.text = result.RemainingUses.ToString();
            if (result.RemainingUses <= 0)
            {
                Register.register.miniGamesButton.interactable = false;
                Register.register.playAgainButton.interactable = false;
                Register.register.playWheel.interactable = false;

            }

        }

        if (result.ItemInstanceId == reward.ItemInstanceId)
        {
            Register.register.rewardButton.interactable = false;
            Register.register.nextReward.text = Register.register.nextRewardTime;
        }
        
        Debug.Log("Succes Consume Item");
    }

   

    

  


}

