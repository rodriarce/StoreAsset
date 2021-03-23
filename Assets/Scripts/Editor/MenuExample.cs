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
    public KindOfItem kindOfItem = KindOfItem.isWeapon;
    public GameObject prefabObject;
    public int damageWeapon;
    public int defenseWeapon;
    
    
    //private string pathAsset = "Assets/StoreItems.asset";
    
    
    [MenuItem("Window/CreateItems")]
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
                kindOfItem = (KindOfItem)EditorGUILayout.EnumPopup(kindOfItem);
                switch (kindOfItem)
                {
                    case KindOfItem.isWeapon:
                        GUILayout.Space(5f);
                        GUILayout.Label("Add the Item Prefab", EditorStyles.boldLabel);
                        prefabObject = (GameObject)EditorGUILayout.ObjectField(prefabObject, typeof(GameObject), true);
                        GUILayout.Space(5f);
                        damageWeapon = EditorGUILayout.IntField("Damage Weapon", damageWeapon);
                        break;
                    case KindOfItem.isArmor:
                        GUILayout.Space(5f);
                        GUILayout.Label("Add the Item Prefab", EditorStyles.boldLabel);
                        //GUILayout.Space(5f);
                        
                        prefabObject = (GameObject)EditorGUILayout.ObjectField(prefabObject, typeof(GameObject), true);
                        GUILayout.Space(5f);
                        defenseWeapon = EditorGUILayout.IntField("Defense Weapon", defenseWeapon);
                        break;


                }
                
                nameItem = EditorGUILayout.TextField("NameItem", nameItem);
                GUILayout.Space(5f);
                itemID = EditorGUILayout.TextField("ItemId", itemID);

                GUILayout.Space(5f);
                descriptionItem = EditorGUILayout.TextField("Description", descriptionItem);
                GUILayout.Space(5f);
                priceItem = EditorGUILayout.IntField("Price Item", priceItem);
                GUILayout.Space(5f);
                newObject = (Sprite)EditorGUILayout.ObjectField(newObject, typeof(Sprite), true);

              
                GUILayout.Space(5f);
                if (GUILayout.Button("Create Item"))
                {
                    if (!CheckValues())
                    {
                        textState = "You Have Empty Fields";
                        return;
                    }
                    OnClickCreate();
                    textState = "Succes Create Item";
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
        newObject = (Sprite)EditorGUILayout.ObjectField(newObject, typeof(Sprite), true);
        GUILayout.Space(5f);
       
                }
                GUILayout.Label(textState, EditorStyles.boldLabel);
                GUILayout.Space(5f);
                
                break;
            case 1:

                GUILayout.Label("Update Item", EditorStyles.boldLabel);
                GUILayout.Space(5f);
                nameItem = EditorGUILayout.TextField("nameItem", nameItem);
                GUILayout.Space(5f);
                priceItem = EditorGUILayout.IntField("Price Item", priceItem);
                
                
             
                GUILayout.Space(5f);
                descriptionItem = EditorGUILayout.TextField("Item Description", descriptionItem);
                GUILayout.Space(5f);
                newObject = (Sprite)EditorGUILayout.ObjectField(newObject, typeof(Sprite), true);
                GUILayout.Space(5f);
                StoreItems newItem = ScriptableObject.CreateInstance<StoreItems>();
                newItem.itemId = nameItem;
                newItem.priceItem = priceItem;
                newItem.description = descriptionItem;
                newItem.imageItem = newObject;
                GUILayout.Space(5f);
                
                kindOfItem = (KindOfItem)EditorGUILayout.EnumPopup(kindOfItem);
                switch (kindOfItem)
                {
                    case KindOfItem.isWeapon:
                        GUILayout.Space(5f);
                        prefabObject = (GameObject)EditorGUILayout.ObjectField(prefabObject, typeof(GameObject), true);
                        break;
                    case KindOfItem.isArmor:
                        GUILayout.Space(5f);
                        prefabObject = (GameObject)EditorGUILayout.ObjectField(prefabObject, typeof(GameObject), true);
                        break;


                }

                

                if (GUILayout.Button("Update Player Stats"))
                {
                    if (!CheckValues())
                    {
                        textState = "You Have Empty Fields";
                        return;
                    }
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

    private bool CheckValues()
    {

        if ((kindOfItem == KindOfItem.isArmor) || (kindOfItem == KindOfItem.isWeapon))
        {
            if ((string.IsNullOrEmpty(itemID)) || (string.IsNullOrEmpty(nameItem)) || (prefabObject == null))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        else if ((string.IsNullOrEmpty(itemID)) || (string.IsNullOrEmpty(nameItem)))
        {
            return false;
        }
        return true;
    }

    private void OnClickCreate()
    {        
        switch (kindOfItem)
        {
            case KindOfItem.isWeapon:
                WeaponItems weaponItem = ScriptableObject.CreateInstance<WeaponItems>();
                weaponItem.itemId = nameItem;
                weaponItem.nameItem = nameItem;
                weaponItem.description = descriptionItem;
                weaponItem.imageItem = newObject;
                weaponItem.damage = damageWeapon;
                CatalogItem playFabWeapon = new CatalogItem();
                playFabWeapon.ItemId = itemID;
                playFabWeapon.DisplayName = nameItem;
                playFabWeapon.CatalogVersion = "Items";
                playFabWeapon.Description = descriptionItem;
                //var newItemScript = prefabObject.AddComponent<Rigidbody>();
                string sourceItem = AssetDatabase.GetAssetPath(prefabObject);
                //Object prefabInstance = PrefabUtility;


                //var newGameObject = new GameObject();
                //newGameObject.AddComponent(typeof(ItemScript));
                //var newComponent =  newPrefab.AddComponent<ItemScript>();
                //prefabObject = newGameObject;
                //PrefabUtility.
                //PrefabUtility.R
                GameObject assetWeapon = PrefabUtility.LoadPrefabContents(sourceItem);
                DataItem weaponComponent = assetWeapon.AddComponent<DataItem>();
                weaponComponent.SetDataItem(nameItem);

                PrefabUtility.SaveAsPrefabAssetAndConnect(assetWeapon, sourceItem, InteractionMode.AutomatedAction);
                //PrefabUtility.ApplyAddedComponent(componentToAdd, prefabSource, InteractionMode.AutomatedAction);
                //AssetDatabase.CreateAsset(newGameObject, "Assets/Resources/Weapons/" + nameItem + ".assset");
                //prefabObject.GetComponent<ItemScript>().SetDataItem(nameItem);
                

                Dictionary<string, uint> currencyWeapon = new Dictionary<string, uint>();

                currencyWeapon.Add("US", (uint)priceItem);
                playFabWeapon.VirtualCurrencyPrices = currencyWeapon;
                //

                AssetDatabase.CreateAsset(weaponItem, "Assets/Resources/Weapons/" + nameItem + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                PlayFabItems.AddItems(playFabWeapon);


                break;
            case KindOfItem.isArmor:
                break;
            
        }

        return;
        StoreItems item = ScriptableObject.CreateInstance<StoreItems>();
        item.itemId = nameItem;
        item.nameItem = nameItem;
        item.description = descriptionItem;
        item.imageItem = newObject;
        
        
        //item.prefabItem = Instantiate(prefabObject);
        CatalogItem playFabItem = new CatalogItem();
        playFabItem.ItemId = itemID;
        playFabItem.DisplayName = nameItem;
        playFabItem.CatalogVersion = "Items";
        playFabItem.Description = descriptionItem;
        //var newItemScript = prefabObject.AddComponent<Rigidbody>();
        string prefabSource = AssetDatabase.GetAssetPath(prefabObject);
        //Object prefabInstance = PrefabUtility;


        //var newGameObject = new GameObject();
        //newGameObject.AddComponent(typeof(ItemScript));
        //var newComponent =  newPrefab.AddComponent<ItemScript>();
        //prefabObject = newGameObject;
        //PrefabUtility.
        //PrefabUtility.R
        GameObject assetPrefab = PrefabUtility.LoadPrefabContents(prefabSource);
        DataItem componentToAdd = assetPrefab.AddComponent<DataItem>();
        componentToAdd.SetDataItem(nameItem);
        
        PrefabUtility.SaveAsPrefabAssetAndConnect(assetPrefab, prefabSource, InteractionMode.AutomatedAction);
        //PrefabUtility.ApplyAddedComponent(componentToAdd, prefabSource, InteractionMode.AutomatedAction);
        //AssetDatabase.CreateAsset(newGameObject, "Assets/Resources/Weapons/" + nameItem + ".assset");
        //prefabObject.GetComponent<ItemScript>().SetDataItem(nameItem);

        
        Dictionary<string, uint> currencyItem = new Dictionary<string, uint>();
                   
        currencyItem.Add("US", (uint)priceItem);
        playFabItem.VirtualCurrencyPrices = currencyItem;
        //
                
        AssetDatabase.CreateAsset(item, "Assets/Resources/Items/" + nameItem + ".asset");
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
                itemToUpdate.itemId = nameItem;
                itemToUpdate.nameItem = nameItem;
                itemToUpdate.description = descriptionItem;
                itemToUpdate.imageItem = newObject;
                //itemToUpdate.kindOfItem = kindOfItem;
                
                CatalogItem playFabItem = new CatalogItem();
                playFabItem.ItemId = nameItem;
                playFabItem.DisplayName = nameItem;
                playFabItem.CatalogVersion = "Items";
                playFabItem.Description = descriptionItem;

                AssetDatabase.CreateAsset(itemToUpdate, "Assets/Resources/Items/" + nameItem + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                PlayFabItems.UpdateItem(playFabItem);
                return true;
            }
        }
        return false;
    }

}
