using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;


public class Register : MonoBehaviour
{
    public static Register register;
    public InputField userNameInput;
    public InputField passwordInput;
    public InputField email;
    public InputField loginUser;
    public InputField loginPassword;
    public GameObject panelRegister;
    public string userName;
    public string password;
    public Text textCurrency;
    public Text textBullets;
    public Text textXp;
    public Text textKills;
    public Text ammoGun;
    public Text ammoShotGun;
    public Text ammoRifle;
    public Text ammoMagnum;
    public Text amountMedicKits;
    public Text amountGrenades;
    public int bullets;
    public Button enterGame;
    public GameObject panelResultBuy;
    public Text textResultBuy;
    public Text textResultLogin;
    public GameObject loginPanel;
    public InputField userNameError;
    public InputField passwordError;
    //public List<BoardPlayer> boardsPlayers;
    public Text errorLogin;
    public Button rewardButton;
    public Button miniGamesButton;
    public Text newsInfo;
    public Text playerName;
    public Text amountRewards;
    public Text nextReward;
    public string nextRewardTime;
    public Button playAgainButton;
    public Button playWheel;
    public GameObject panelWinReward;
    public string selectMiniGame;
    public GameObject panelLoading;



    private void Awake()
    {
        if (register == null)
        {
            register = this;
        }
        // PlayerPrefs.DeleteAll();
    }
    private void Start()
    {
        //PlayFabAuth.playFabAuth.GetLeaderBoard();
       
    }

    public void CheckPlayer()
    {
        if (!PlayerPrefs.HasKey("UserName"))
        {
            panelRegister.SetActive(true);
        }
        else
        {
            userName = PlayerPrefs.GetString("UserName");
            password = PlayerPrefs.GetString("Password");
            GameLoader.gameLoader.isRegisted = true;
            GameLoader.gameLoader.isInitalWeapons = true;
            GameLoader.gameLoader.isInitialAmmo = true;
            //LoginPlayer();
        }

    }



    public void CreateAccount()// Method to create Account
    {
               
            RegisterPlayFabUserRequest request = new RegisterPlayFabUserRequest();
            request.Username = userNameInput.text;
            request.Password = passwordInput.text;
            request.RequireBothUsernameAndEmail = false;
            request.DisplayName = userNameInput.text;
            PlayFabClientAPI.RegisterPlayFabUser(request,OnPlayerRegister,
                OnErrorLogin);
        
    }
    
    
    private void OnErrorLogin(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
        textResultLogin.text = "The Password must be at least 6 characters or try another User Name";

    }

    private void OnPlayerRegister(RegisterPlayFabUserResult result)
    {
        textResultLogin.text = "Succes Create Account";
        panelRegister.SetActive(false);
        PlayerPrefs.SetString("UserName", userNameInput.text);
        PlayerPrefs.SetString("Password", passwordInput.text);
        userName = userNameInput.text;// Set UserName
        password = passwordInput.text;// Set Password
        GameLoader.gameLoader.isRegisted = true;


        PlayFabAuth.playFabAuth.playFabId = result.PlayFabId;
        PlayFabAuth.playFabAuth.ShowCurrency();
        PlayFabAuth.playFabAuth.SetListStats("bullets", 500);// Set PlayFab Stats 
        PlayFabAuth.playFabAuth.SetListStats("xpPlayer", 0);
        PlayFabAuth.playFabAuth.SetListStats("totalKills", 0);
        PlayFabAuth.playFabAuth.SetStats();
        //Invoke("SetNewWeapons", 2f);
        //Invoke("SetInitialAmmo", 3f);
        
        
        
        //LoginPlayer();// Login Player After Register


        //PlayFabAuth.playFabAuth.SetStats("WeaponAmmo");
    }
    public void SetNewWeapons()
    {
        List<string> standardWepaons = new List<string>()
        {
            "AK47",
            "M1911",
            "Bennelli",
        };
        StoreObjects.storeObjects.SetInitialWeapons(standardWepaons);
    }
    public void SetInitialAmmo()
    {
        PlayFabAuth.playFabAuth.SetListStats("ammoGun", 50);
        PlayFabAuth.playFabAuth.SetListStats("ammoMagnum", 100);
        PlayFabAuth.playFabAuth.SetListStats("ammoRifle", 100);
        PlayFabAuth.playFabAuth.SetListStats("ammoShotGun", 30);
        PlayFabAuth.playFabAuth.SetStats();
    }


    public void OnLoginClick()
    {
        userName = loginUser.text;
        password = loginPassword.text;
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        request.Username = userName;
        request.Password = password;
        PlayFabClientAPI.LoginWithPlayFab(request, LoginResultPlayFab, error => {errorLogin.text = "Wrong User or Password"; });

    }




    public void LoginPlayer()
    {
        LoginWithPlayFabRequest request = new LoginWithPlayFabRequest();
        if(PlayerPrefs.HasKey("UserName"))
        {
            request.Username = userName;
            request.Password = password;
            PlayFabClientAPI.LoginWithPlayFab(
            request, LoginResultPlayFab, LoginError);

        }
        else
        {
            loginPanel.SetActive(true);
            //userName = userName;
            //password = loginPassword.text;
        }
        
        //playerName = request.Username;
        
       
    }

    public void LoginResultPlayFab(LoginResult result)
    {
        panelRegister.SetActive(false);
        loginPanel.SetActive(false);
        PlayFabAuth.playFabAuth.playFabId = result.PlayFabId;
        Debug.Log("Login In PlayFab");
        //PlayFabAuth.playFabAuth.ShowCurrency();
        PlayFabAuth.playFabAuth.userNameId = userName;
        playerName.text = userName;
        //Invoke("GetNewStats", 1f);
        //PhotonLobby.photonLobby.SetPlayerName(PlayerPrefs.GetString("UserName"));// Set PHoton NIckName
        LockCursor();
        PlayerPrefs.SetString("UserName", userName);
        PlayerPrefs.SetString("Password", password);
        GameLoader.gameLoader.isRegisted = true;
        GameLoader.gameLoader.isInitalWeapons = true;
        GameLoader.gameLoader.isInitialAmmo = true;
        GameLoader.gameLoader.isLogin = true;
       
        
    }
    private void GetNewStats()
    {
        PlayFabAuth.playFabAuth.GetStats();
    }
    public void LoginError(PlayFabError error)
    {
        loginPanel.SetActive(true);
    }


    public void LoginErrorPlayer()
    {
        userName = userNameError.text;
        password = passwordError.text;
        LoginPlayer();
    }




    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


    public void OnBuyGunAmmo(int amount)
    {
        if (PlayFabAuth.playFabAuth.amountCurrency >= amount)
        {
            PlayFabAuth.playFabAuth.SubstractCurrency(amount);
            int currentAmmo = PlayFabAuth.playFabAuth.ammoGun + 200;
            PlayFabAuth.playFabAuth.ammoGun = currentAmmo;
            PlayFabAuth.playFabAuth.SetListStats("ammoGun", currentAmmo);
            PlayFabAuth.playFabAuth.SetStats();
            ammoGun.text = currentAmmo.ToString();
            StoreObjects.storeObjects.panelResult.SetActive(true);
            //textResultBuy.text = "You Have It";
            //panelResultBuy.SetActive(true);
        }
        else
        {
            //textResultBuy.text = "You Dont have Money";
            StoreObjects.storeObjects.panelError.SetActive(true);
        }

    }

    public void OnBuyShotGunAmmo(int amount)
    {
        if (PlayFabAuth.playFabAuth.amountCurrency >= amount)
        {
            PlayFabAuth.playFabAuth.SubstractCurrency(amount);
            int currentAmmo = PlayFabAuth.playFabAuth.ammoShotGun + 200;
            PlayFabAuth.playFabAuth.ammoShotGun = currentAmmo;
            PlayFabAuth.playFabAuth.SetListStats("ammoShotGun", currentAmmo);
            PlayFabAuth.playFabAuth.SetStats();
            ammoShotGun.text = currentAmmo.ToString();
            StoreObjects.storeObjects.panelResult.SetActive(true);
            //textResultBuy.text = "You Have It";
            //panelResultBuy.SetActive(true);
        }
        else
        {
            //textResultBuy.text = "You Dont have Money";
            //panelResultBuy.SetActive(true);
            StoreObjects.storeObjects.panelError.SetActive(true);
        }

    }

    public void OnBuyRifleAmmo(int amount)
    {
        if (PlayFabAuth.playFabAuth.amountCurrency >= amount)
        {
            PlayFabAuth.playFabAuth.SubstractCurrency(amount);
            int currentAmmo = PlayFabAuth.playFabAuth.ammoRifle + 200;
            PlayFabAuth.playFabAuth.ammoRifle = currentAmmo;
            PlayFabAuth.playFabAuth.SetListStats("ammoRifle", currentAmmo);
            PlayFabAuth.playFabAuth.SetStats();
            ammoRifle.text = currentAmmo.ToString();
            StoreObjects.storeObjects.panelResult.SetActive(true);
            //textResultBuy.text = "You Have It";
            //panelResultBuy.SetActive(true);
        }
        else
        {
            //textResultBuy.text = "You Dont have Money";
            //panelResultBuy.SetActive(true);
            StoreObjects.storeObjects.panelError.SetActive(true);
        }

    }

    public void OnBuyMagnum(int amount)
    {
        if (PlayFabAuth.playFabAuth.amountCurrency >= amount)
        {
            PlayFabAuth.playFabAuth.SubstractCurrency(amount);
            int currentAmmo = PlayFabAuth.playFabAuth.ammoMagnum + 200;
            PlayFabAuth.playFabAuth.ammoMagnum = currentAmmo;
            PlayFabAuth.playFabAuth.SetListStats("ammoMagnum", currentAmmo);
            PlayFabAuth.playFabAuth.SetStats();
            ammoMagnum.text = currentAmmo.ToString();
            StoreObjects.storeObjects.panelResult.SetActive(true);
            //textResultBuy.text = "You Have It";
            //panelResultBuy.SetActive(true);
        }
        else
        {
            //textResultBuy.text = "You Dont have Money";
            //panelResultBuy.SetActive(true);
            StoreObjects.storeObjects.panelError.SetActive(true);
        }

    }

    public void OnBuyBullets(int amount)
    {
        if(PlayFabAuth.playFabAuth.amountCurrency >= amount)
        {
            PlayFabAuth.playFabAuth.SubstractCurrency(amount);
            int currentBullets = PlayFabAuth.playFabAuth.bullets + 200;
            PlayFabAuth.playFabAuth.bullets = currentBullets;
            PlayFabAuth.playFabAuth.SetListStats("bullets", currentBullets);
            PlayFabAuth.playFabAuth.SetStats();
            textBullets.text = currentBullets.ToString();
            textResultBuy.text = "You Have It";
            panelResultBuy.SetActive(true);
        }
        else
        {
            textResultBuy.text = "You Dont have Money";
            panelResultBuy.SetActive(true);
        }
        



        //PlayFabAuth.playFabAuth.
    }


    

   

}
