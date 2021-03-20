using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using PlayFab;
using PlayFab.AdminModels;

public class MenuExample : EditorWindow
{
    public string itemID;
    private string nameItem;
    public string descriptionItem;
    public string classItem;
    private int priceItem;
    public float amountGold;
    public Sprite newObject;
    public string resultItem;
    public SerializedProperty newProp;
    private string[] toolbar = new string[2] {"AddItem", "UpdateItem" };
    public int orderToolbar = 0;
    private string textState;
    
    //private string pathAsset = "Assets/StoreItems.asset";
    
    
    [MenuItem("Window/Example")]
    public static void ShowWindow()
    {
       var windowMenu = GetWindow<MenuExample>("Example");
        windowMenu.maxSize = new Vector2(400f, 300f);

    }

    private void OnGUI()
    {
        
        
        orderToolbar = GUILayout.Toolbar(orderToolbar, toolbar);
        switch (orderToolbar)
        {
            case 0:
                
                GUILayout.Label("Add New Item", EditorStyles.boldLabel);
                nameItem = EditorGUILayout.TextField("NameItem", nameItem);
                GUILayout.Space(5f);
                itemID = EditorGUILayout.TextField("ItemId", itemID);

                GUILayout.Space(5f);
                descriptionItem = EditorGUILayout.TextField("Description", descriptionItem);
                GUILayout.Space(5f);
                priceItem = EditorGUILayout.IntField("Price Item", priceItem);
                GUILayout.Space(5f);
                newObject = (Sprite)EditorGUILayout.ObjectField(newObject, typeof(Sprite));
                GUILayout.Space(5f);
                if (GUILayout.Button("Press Button"))
                {
                    OnClickCreate();
                    Debug.Log("You Press a Button");
                    //newObject = null;
                }
                GUILayout.Space(5f);
                if (GUILayout.Button("Clear List"))
                {
                    StoreObjects.ClearList(); GUILayout.Label("Add New Item", EditorStyles.boldLabel);
        nameItem = EditorGUILayout.TextField("NameItem", nameItem);
        GUILayout.Space(5f);
        itemID = EditorGUILayout.TextField("ItemId", itemID);
       
        GUILayout.Space(5f);
        descriptionItem = EditorGUILayout.TextField("Description", descriptionItem);
        GUILayout.Space(5f);
        priceItem = EditorGUILayout.IntField("Price Item", priceItem);
        GUILayout.Space(5f);
        newObject = (Sprite)EditorGUILayout.ObjectField(newObject, typeof(Sprite));
        GUILayout.Space(5f);
        if (GUILayout.Button("Press Button"))
        {
            OnClickCreate();
            Debug.Log("You Press a Button");
            //newObject = null;
        }
        GUILayout.Space(5f);
        if (GUILayout.Button("Clear List"))
        {
            StoreObjects.ClearList();
        }
        GUILayout.Space(5f);
                }
                GUILayout.Space(5f);
                
                break;
            case 1:

                GUILayout.Label("Update Item", EditorStyles.boldLabel);
                GUILayout.Space(5f);
                nameItem = EditorGUILayout.TextField("Name Item", nameItem);
                GUILayout.Space(5f);
                priceItem = EditorGUILayout.IntField("Price Item", priceItem);
                GUILayout.Space(5f);
                descriptionItem = EditorGUILayout.TextField("Item Description", descriptionItem);
                GUILayout.Space(5f);
                newObject = (Sprite)EditorGUILayout.ObjectField(newObject, typeof(Sprite));
                GUILayout.Space(5f);
                StoreItems newItem = ScriptableObject.CreateInstance<StoreItems>();
                newItem.itemId = nameItem;
                newItem.priceItem = priceItem;
                newItem.description = descriptionItem;
                newItem.imageItem = newObject;

                if (GUILayout.Button("Update Player Stats"))
                {
                    if (OnClickUpdate(nameItem, newItem))
                    {
                        textState = "Succes Update Items";
                        Debug.Log("Succes Update Object");
                    }
                    else
                    {
                        textState = "Object Not Find";
                        Debug.Log("Object not find");
                    }
                }

                GUILayout.Space(10f);
                GUILayout.Label(textState, EditorStyles.boldLabel);
               

                break;
        }




        // Window Code
    }

    private void OnClickCreate()
    {                      
        StoreItems item = ScriptableObject.CreateInstance<StoreItems>();
        item.itemId = itemID;
        item.nameItem = nameItem;
        item.description = descriptionItem;
        item.imageItem = newObject;
        CatalogItem playFabItem = new CatalogItem();
        playFabItem.ItemId = itemID;
        playFabItem.DisplayName = nameItem;
        playFabItem.CatalogVersion = "Items";
        playFabItem.Description = descriptionItem;
        Dictionary<string, uint> currencyItem = new Dictionary<string, uint>();
                   
        currencyItem.Add("US", (uint)priceItem);
        playFabItem.VirtualCurrencyPrices = currencyItem;
        //
                
        AssetDatabase.CreateAsset(item, "Assets/Resources/Items/" + itemID + ".asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        PlayFabItems.AddItems(playFabItem);
       
    }
    private bool OnClickUpdate(string nameItem, StoreItems newItem)
    {
        var storeItems = Resources.LoadAll("Items");
        
                         
        foreach (var itemObject in storeItems)
        {
            StoreItems item = (StoreItems)itemObject;

            if (item.itemId == newItem.itemId)
            {
                Debug.Log("This is the Item to Update");
                item.nameItem = newItem.nameItem;
                item.priceItem = newItem.priceItem;
                item.imageItem = newItem.imageItem;
                AssetDatabase.DeleteAsset("Assets/Resources/Items/" + nameItem + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                StoreItems itemToUpdate = ScriptableObject.CreateInstance<StoreItems>();
                itemToUpdate.itemId = itemID;
                itemToUpdate.nameItem = nameItem;
                itemToUpdate.description = descriptionItem;
                itemToUpdate.imageItem = newObject;
                CatalogItem playFabItem = new CatalogItem();
                playFabItem.ItemId = itemID;
                playFabItem.DisplayName = nameItem;
                playFabItem.CatalogVersion = "Items";
                playFabItem.Description = descriptionItem;

                AssetDatabase.CreateAsset(itemToUpdate, "Assets/Resources/Items" + nameItem + ".assset");
                PlayFabItems.UpdateItem(playFabItem);
                return true;
            }
        }
        return false;
    }

}
