using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public static MenuController menuController;
    public GameObject menuRoom;
    public Button buttonReady;
    public AudioSource audioSelectGun;
    public AudioSource audioButton;
    public GameObject panelLobby;
    public AudioSource audioMenu;
    public AudioClip audioChest;
    public AudioClip audioLose;
    public GameObject gameWheel;
    public GameObject gameChest;


    private void Awake()
    {
        if (menuController == null)
        {
            menuController = this;

        }
    }

    private void Start()
    {
       
    }

    

    public void PlayAudioChest()
    {
        audioMenu.clip = audioChest;
        audioMenu.Play();
    }
    public void PlayAudioLose()
    {
        audioMenu.clip = audioLose;
        audioMenu.Play();
    }

    public void ActiveRoom()
    {
        menuRoom.SetActive(true);
    }
   


}
