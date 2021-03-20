using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponStats : MonoBehaviour
{
    public Text nameWeapon;
    public Text coinsWeapon;
    public Text damageWeapon;
    public Text capacityWeapon;
    public Text amountDaysWeapon;
    public Text speed;
    public Image imageObject;
    public Text priceWeapon;
    public ScriptableWeapons selectWeapon;
    public string classWeapon;
    public Slider sliderDamage;
    public Slider sliderCapacity;
    public Slider sliderSpeed;
    public Slider sliderAmountDaysWeapon;
    public Button buttonBuy;
    public bool isPrefabStore;
        
    
    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    public void SetCurrentWeapon()
    {
        switch (classWeapon)
        {
            case "Rifle":
                PlayFabAuth.playFabAuth.selectRifle = selectWeapon;
                PlayerPrefs.SetString("rifleName", selectWeapon.name);
                
                break;
            case "ShotGun":
                PlayFabAuth.playFabAuth.selectShotGUn = selectWeapon;
                PlayerPrefs.SetString("shotGunName", selectWeapon.name);
               
                break;
            case "Gun":
                PlayFabAuth.playFabAuth.selectGun = selectWeapon;
                PlayerPrefs.SetString("gunName", selectWeapon.name);
                
                break;
            case "Magnum":
                PlayFabAuth.playFabAuth.selectMagnum = selectWeapon;
                PlayerPrefs.SetString("magnunName", selectWeapon.name);
                break;

        }
        MenuController.menuController.audioSelectGun.Play();
        //Inventory.inventory.SetEquipItems();
        StoreObjects.storeObjects.resultEquipItem.SetActive(true);
        
    }

    public void SetSliders(ScriptableWeapons weapon)
    {
        sliderDamage.value = weapon.damage / StoreObjects.storeObjects.totalDamage;
        sliderCapacity.value = weapon.capacity / StoreObjects.storeObjects.totalCapacity;
        sliderSpeed.value = weapon.rateShoot  / StoreObjects.storeObjects.totalSpeed;
        sliderAmountDaysWeapon.value = (float)weapon.amountDays / (float)StoreObjects.storeObjects.totalDays;
        

    }
    public void SetNewWeapon(ScriptableWeapons weapon)
    {
        nameWeapon.text = weapon.name;
        damageWeapon.text = weapon.damage.ToString();
        capacityWeapon.text = weapon.capacity.ToString();
        speed.text = weapon.rateShoot.ToString();
        sliderDamage.value = weapon.damage / 500f;
        sliderCapacity.value = weapon.capacity / 100f;
        sliderSpeed.value = weapon.rateShoot / 100f;
        imageObject.sprite = weapon.imageObject;
        amountDaysWeapon.text = weapon.amountDays.ToString();

    }

    public void BuyItem()
    {
        MenuController.menuController.audioButton.Play();
        StoreObjects.storeObjects.BuyItem(selectWeapon.name, "Weapons", selectWeapon.priceWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
