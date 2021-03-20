using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public static class StoreDatabase 
{
    public static List<StoreItems> storeObjects = new List<StoreItems>();
    
    
    public static void AddNewObject(StoreItems newItem)
    {
        storeObjects.Add(newItem);
    }
    public static void RemoveItem(StoreItems itemToRemove)
    {
        storeObjects.Remove(itemToRemove);
    }
    public static void ClearItems()
    {
        storeObjects.Clear();
    }
    public static void UpdapteItem()
    {
        
    }

    
}
