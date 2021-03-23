using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager itemsManager;
    public List<WeaponItems> weaponItems = new List<WeaponItems>();
    public Dictionary<string, WeaponItem> weaponsPlayer = new Dictionary<string, WeaponItem>();

    private void Awake()
    {
        if (itemsManager == null)
        {
            itemsManager = this;
        }
    }

    public void GetPlayerInventory()
    {
        //weaponsPlayer.Add("Weapon")
    }

    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
