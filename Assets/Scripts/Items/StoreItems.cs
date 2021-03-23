using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum KindOfItem
{
    isWeapon,
    isCoin,
    isArmor,
    isHealth
}

[CreateAssetMenu(menuName = "StoreItems")]
public class StoreItems : ScriptableObject
{
    
    public string itemId;
    public string nameItem;
    public Sprite imageItem;
    public string description;
    public int priceItem;
        //public GameObject prefabItem;
    
    
}

public class WeaponItems : StoreItems
{
    public int damage;
    public bool isEquiped;


}

public class ArmorItems : StoreItems
{
    public int armor;
    public bool isEquiped;

}












