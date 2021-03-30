using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum KindOfItem
{
    isWeapon,
    isCoin,
    isArmor,
    isItem
}


public class StoreItems : ScriptableObject
{
    public KindOfItem kindOfItem;
    public string nameItem;
    public Sprite imageItem;
    public string description;
    public int priceItem;
        //public GameObject prefabItem;
    
    
}


















