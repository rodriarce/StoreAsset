using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ItemInventory : MonoBehaviour
{
    public Text nameItem;
    public Text statName;
    public Text amountStat;
    public Image imageItem;
    public Button buttonEquip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddEvent<T>(T eventItem)
    {
        if (eventItem.GetType() == typeof(WeaponItems))
        {
            var newObject = (object)eventItem;
            WeaponItems newItem = (WeaponItems)newObject;
            SetDataItem(newItem.nameItem, "Damage", newItem.damage.ToString(), newItem.imageItem);
        }
        if (eventItem.GetType() == typeof(ArmorItems))
        {
            var newObject = (object)eventItem;
            ArmorItems newItem = (ArmorItems)newObject;
            SetDataItem(newItem.nameItem, "Armor", newItem.armor.ToString(), newItem.imageItem);
        }
        if (eventItem.GetType() == typeof(OtherItems))
        {
            var newObject = (object)eventItem;
            ArmorItems newItem = (ArmorItems)newObject;
            SetDataItem(newItem.nameItem, "Armor", newItem.armor.ToString(), newItem.imageItem);
        }



        buttonEquip.onClick.AddListener(() => { SetItem(eventItem); });
    }


    public void SetItem<T>(T scriptableObject)
    {

        if (scriptableObject.GetType() == typeof(WeaponItems))
        {
            var newObject = (object)scriptableObject;
            WeaponItems newIItem = (WeaponItems) newObject;
            
            foreach (WeaponItems weapon in Inventory.inventory.weaponsItems)
            {

                if (weapon.nameItem == newIItem.nameItem)
                {
                    weapon.isEquiped = true;
                    Inventory.inventory.panelSuccesEquip.SetActive(true);
                }
                    
            }
        }
        if (scriptableObject.GetType() == typeof(ArmorItems))
        {
            var newObject = (object)scriptableObject;
            ArmorItems newIItem = (ArmorItems)newObject;
            SetDataItem(newIItem.nameItem, "Armor", newIItem.armor.ToString(), newIItem.imageItem);
            foreach (ArmorItems armor in Inventory.inventory.armorItems)
            {

                if (armor.nameItem == newIItem.nameItem)
                {
                    armor.isEquiped = true;
                }

            }
        }
        if (scriptableObject.GetType() == typeof(OtherItems))
        {
            var newObject = (object)scriptableObject;
            OtherItems newIItem = (OtherItems)newObject;
            SetDataItem(newIItem.nameItem, "Value", newIItem.valueItem.ToString(), newIItem.imageItem);
            foreach (ArmorItems armor in Inventory.inventory.armorItems)
            {

                if (armor.nameItem == newIItem.nameItem)
                {
                    armor.isEquiped = true;
                }

            }

        }

    }
    public void SetDataItem(string nameItem, string statName, string amountStat, Sprite spriteImage)
    {
        this.nameItem.text = nameItem;
        this.statName.text = statName;
        this.amountStat.text = amountStat;
        imageItem.sprite = spriteImage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
