using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "StoreItems")]
public class StoreItems : ScriptableObject
{
    public string itemId;
    public string nameItem;
    public Sprite imageItem;
    public string description;
    public int priceItem;
    
    
}
