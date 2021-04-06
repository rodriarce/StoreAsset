using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using PlayFab;
using PlayFab.AdminModels;





public class MenuExample : EditorWindow
{
    
    private string nameItem;
    public string descriptionItem;
    public string classItem;
    private int priceItem;
    public float amountGold;
    public Sprite newObject;
    public string resultItem;
    public SerializedProperty newProp;
    private string[] toolbar = new string[3] {"AddItem", "UpdateItem", "DeleteItem" };
    public int orderToolbar = 0;
    private string textState;
    public KindOfItem kindOfItem = KindOfItem.isWeapon;
    public GameObject prefabObject;
    public int damageWeapon;
    public int defenseWeapon;
    public int amountOfCoins;
    public int valueItem;
    private string sourceDeleteItem;
    private GameObject prefabToAdd;
    private GameObject prefabToDelete;
    private DataItem datatoDelete;
    public string productId;
    
    
    //private string pathAsset = "Assets/StoreItems.asset";
    
    
    [MenuItem("Window/CreateItems")]
    public static void ShowWindow()
    {
       var windowMenu = GetWindow<MenuExample>("Example");
        windowMenu.minSize = new Vector2(400f, 400f);
        windowMenu.maxSize = new Vector2(400f, 400f);
        

    }

    private void OnGUI()
    {
        
        
        orderToolbar = GUILayout.Toolbar(orderToolbar, toolbar);
        switch (orderToolbar)
        {
            case 0:
                OnCreateItem();
                break;
            case 1:
                OnUpdateItem();
                break;
            case 2:
                OnDeleteItem();
                break;
        }




        // Window Code
    }

    private void OnCreateItem()
    {
        GUILayout.Label("Add New Item", EditorStyles.boldLabel);
        GUILayout.Space(5f);
        nameItem = EditorGUILayout.TextField("NameItem", nameItem);
        GUILayout.Space(5f);
        GUILayout.Label("Kind Of Item", EditorStyles.boldLabel);
        GUILayout.Space(5f);
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
            case KindOfItem.isCoin:
                GUILayout.Space(5f);
                amountOfCoins = EditorGUILayout.IntField("Amount of Coins", amountOfCoins);
                GUILayout.Space(5f);
                productId = EditorGUILayout.TextField("Google Product Id", productId);
                break;
            case KindOfItem.isItem:
                GUILayout.Space(5f);
                GUILayout.Label("Add the Item Prefab", EditorStyles.boldLabel);
                prefabObject = (GameObject)EditorGUILayout.ObjectField(prefabObject, typeof(GameObject), true);
                GUILayout.Space(5f);
                valueItem = EditorGUILayout.IntField("Item Stat Value", valueItem);
                break;



        }

       
       

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

        

    }


    private void OnUpdateItem()
    {
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
            case KindOfItem.isCoin:
               
                GUILayout.Space(5f);

                break;


        }



        if (GUILayout.Button("Update Player Stats"))
        {
            if (!CheckValues())
            {
                textState = "You Have Empty Fields";
                return;
            }
            if (OnClickUpdate(nameItem, kindOfItem))
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


    }

    private void OnDeleteItem()
    {
        GUILayout.Label("Delete Item", EditorStyles.boldLabel);
        GUILayout.Space(5f);
        nameItem = EditorGUILayout.TextField("NameItem", nameItem);
        GUILayout.Space(5f);
        GUILayout.Label("Kind Of Item", EditorStyles.boldLabel);
        GUILayout.Space(5f);
        kindOfItem = (KindOfItem)EditorGUILayout.EnumPopup(kindOfItem);

       switch (kindOfItem)
        {
            case KindOfItem.isArmor:
                prefabObject = (GameObject)EditorGUILayout.ObjectField(prefabObject, typeof(GameObject), true);
                sourceDeleteItem = AssetDatabase.GetAssetPath(prefabObject);
                break;

        }



        if (GUILayout.Button("Delete Item"))
        {
            OnButtonDelete();
        }

    }



    private void OnButtonDelete()
    {
        switch (kindOfItem)
        {


            case KindOfItem.isWeapon:




                GUILayout.Space(5f);
                prefabObject = (GameObject)EditorGUILayout.ObjectField(prefabObject, typeof(GameObject), true);
                string sourceOtherItem = AssetDatabase.GetAssetPath(prefabObject);
                GUILayout.Space(5f);

                // Destroy added component to Item
                GameObject assetOtherItem = PrefabUtility.LoadPrefabContents(sourceOtherItem);

                DataItem otherComponent = assetOtherItem.GetComponent<DataItem>();
                DestroyImmediate(otherComponent);

                PrefabUtility.SaveAsPrefabAssetAndConnect(assetOtherItem, sourceOtherItem, InteractionMode.AutomatedAction);

                break;

            case KindOfItem.isArmor:


                //Object prefabInstance = PrefabUtility;
                var armorItems = Resources.LoadAll("Armor", typeof(ArmorItems));
                foreach (ArmorItems armorItem in armorItems)
                {
                    if (nameItem == armorItem.nameItem)
                    {
                        GUILayout.Space(5f);
                     
                        GUILayout.Space(5f);

                        // Destroy added component to Item
                        prefabToDelete = PrefabUtility.LoadPrefabContents(sourceDeleteItem);

                        datatoDelete = prefabToDelete.GetComponent<DataItem>();
                        DestroyImmediate(datatoDelete);
                        AssetDatabase.DeleteAsset("Assets/Resources/Armor/" + nameItem + ".asset");// Deleta Scriptable
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        PrefabUtility.SaveAsPrefabAssetAndConnect(prefabToDelete, sourceDeleteItem, InteractionMode.AutomatedAction);
                        PlayFabItems.RemoveItem(nameItem);// Delete PlayFabItem



                        return;
                    }
                }

                //var newGameObject = new GameObject();
                //newGameObject.AddComponent(typeof(ItemScript));
                //var newComponent =  newPrefab.AddComponent<ItemScript>();
                //prefabObject = newGameObject;
                //PrefabUtility.
                //PrefabUtility.R

                //PlayFabItems.RemoveItem

                break;
            case KindOfItem.isCoin:
                break;
            case KindOfItem.isItem:
                break;

        }

    }
    




    private bool CheckValues()
    {

        if ((kindOfItem == KindOfItem.isArmor) || (kindOfItem == KindOfItem.isWeapon))
        {
            if ((string.IsNullOrEmpty(nameItem)) || (prefabObject == null))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        else if (string.IsNullOrEmpty(nameItem))
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
                weaponItem.kindOfItem = kindOfItem;
                weaponItem.nameItem = nameItem;
                weaponItem.description = descriptionItem;
                weaponItem.imageItem = newObject;
                weaponItem.damage = damageWeapon;
                CatalogItem playFabWeapon = new CatalogItem();
                playFabWeapon.ItemId = nameItem;
                playFabWeapon.DisplayName = nameItem;
                playFabWeapon.CatalogVersion = "Items";
                playFabWeapon.Description = descriptionItem;
                playFabWeapon.ItemClass = "Weapons";
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
                ArmorItems armorItem = ScriptableObject.CreateInstance<ArmorItems>();
                armorItem.kindOfItem = kindOfItem;
                armorItem.nameItem = nameItem;
                armorItem.description = descriptionItem;
                armorItem.imageItem = newObject;
                armorItem.armor = damageWeapon;
                CatalogItem playFabArmor = new CatalogItem();
                playFabArmor.ItemId = nameItem;
                playFabArmor.DisplayName = nameItem;
                playFabArmor.CatalogVersion = "Items";
                playFabArmor.Description = descriptionItem;
                playFabArmor.ItemClass = "Armor";
                //var newItemScript = prefabObject.AddComponent<Rigidbody>();
                string sourceArmor = AssetDatabase.GetAssetPath(prefabObject);
                //Object prefabInstance = PrefabUtility;


                //var newGameObject = new GameObject();
                //newGameObject.AddComponent(typeof(ItemScript));
                //var newComponent =  newPrefab.AddComponent<ItemScript>();
                //prefabObject = newGameObject;
                //PrefabUtility.
                //PrefabUtility.R
                GameObject assetArmor = PrefabUtility.LoadPrefabContents(sourceArmor);
                DataItem armorComponent = assetArmor.AddComponent<DataItem>();
                armorComponent.SetDataItem(nameItem);

                PrefabUtility.SaveAsPrefabAssetAndConnect(assetArmor, sourceArmor, InteractionMode.AutomatedAction);
                //PrefabUtility.ApplyAddedComponent(componentToAdd, prefabSource, InteractionMode.AutomatedAction);
                //AssetDatabase.CreateAsset(newGameObject, "Assets/Resources/Weapons/" + nameItem + ".assset");
                //prefabObject.GetComponent<ItemScript>().SetDataItem(nameItem);


                Dictionary<string, uint> currencyArmor = new Dictionary<string, uint>();

                currencyArmor.Add("US", (uint)priceItem);
                playFabArmor.VirtualCurrencyPrices = currencyArmor;
                //

                AssetDatabase.CreateAsset(armorItem, "Assets/Resources/Armor/" + nameItem + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                PlayFabItems.AddItems(playFabArmor);




                break;
            case KindOfItem.isCoin:
                CoinsItems coinItem = ScriptableObject.CreateInstance<CoinsItems>();
                coinItem.kindOfItem = kindOfItem;
                coinItem.nameItem = nameItem;
                coinItem.description = descriptionItem;
                coinItem.imageItem = newObject;
                coinItem.amountOfCoins = amountOfCoins;
                CatalogItem playFabCoins = new CatalogItem();
                playFabCoins.ItemId = nameItem;
                playFabCoins.DisplayName = nameItem;
                playFabCoins.CatalogVersion = "Items";
                playFabCoins.Description = descriptionItem;
                playFabCoins.ItemClass = "Coins";
                playFabCoins.CustomData = productId;

                Dictionary<string, uint> currencyCoins = new Dictionary<string, uint>();

                currencyCoins.Add("US", (uint)priceItem);
                playFabCoins.VirtualCurrencyPrices = currencyCoins;
                //

                AssetDatabase.CreateAsset(coinItem, "Assets/Resources/Coins/" + nameItem + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                PlayFabItems.AddItems(playFabCoins);


                break;
            case KindOfItem.isItem:
                OtherItems otherItem = ScriptableObject.CreateInstance<OtherItems>();
                otherItem.kindOfItem = kindOfItem;
                otherItem.nameItem = nameItem;
                otherItem.description = descriptionItem;
                otherItem.imageItem = newObject;
                otherItem.valueItem = valueItem;
                CatalogItem playFabOtherItem = new CatalogItem();
                playFabOtherItem.ItemId = nameItem;
                playFabOtherItem.DisplayName = nameItem;
                playFabOtherItem.CatalogVersion = "Items";
                playFabOtherItem.Description = descriptionItem;
                playFabOtherItem.ItemClass = "Items";
                //var newItemScript = prefabObject.AddComponent<Rigidbody>();
                string sourceOtherItem = AssetDatabase.GetAssetPath(prefabObject);
                //Object prefabInstance = PrefabUtility;


                //var newGameObject = new GameObject();
                //newGameObject.AddComponent(typeof(ItemScript));
                //var newComponent =  newPrefab.AddComponent<ItemScript>();
                //prefabObject = newGameObject;
                //PrefabUtility.
                //PrefabUtility.R
                GameObject assetOtherItem = PrefabUtility.LoadPrefabContents(sourceOtherItem);
                                                               
                DataItem otherComponent = assetOtherItem.AddComponent<DataItem>();
                otherComponent.SetDataItem(nameItem);

                PrefabUtility.SaveAsPrefabAssetAndConnect(assetOtherItem, sourceOtherItem, InteractionMode.AutomatedAction);
                //PrefabUtility.ApplyAddedComponent(componentToAdd, prefabSource, InteractionMode.AutomatedAction);
                //AssetDatabase.CreateAsset(newGameObject, "Assets/Resources/Weapons/" + nameItem + ".assset");
                //prefabObject.GetComponent<ItemScript>().SetDataItem(nameItem);


                Dictionary<string, uint> currencyOther = new Dictionary<string, uint>();

                currencyOther.Add("US", (uint)priceItem);
                playFabOtherItem.VirtualCurrencyPrices = currencyOther;
                //

                AssetDatabase.CreateAsset(otherItem, "Assets/Resources/Others/" + nameItem + ".asset");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                PlayFabItems.AddItems(playFabOtherItem);


                break;


        }

        return;
       
       
    }
    private bool OnClickUpdate(string nameItem, KindOfItem kindOfItem)
    {
        switch (kindOfItem)
        {

            case KindOfItem.isArmor:
                var armorItems = Resources.LoadAll("Armor", typeof(ArmorItems));
                foreach (ArmorItems itemObject in armorItems)
                {
                    
                    if (nameItem == itemObject.nameItem)
                    {
                        Debug.Log("This is the Item to Update");
                        
                        
                        AssetDatabase.DeleteAsset("Assets/Resources/Armor/" + nameItem + ".asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        ArmorItems item = ScriptableObject.CreateInstance<ArmorItems>();
                        item.nameItem = itemObject.nameItem;
                        item.priceItem = itemObject.priceItem;
                        item.imageItem = itemObject.imageItem;
                                                                   
                        item.description = descriptionItem;
                        
                        
                        CatalogItem playFabItem = new CatalogItem();
                        playFabItem.ItemId = nameItem;
                        playFabItem.DisplayName = nameItem;
                        playFabItem.CatalogVersion = "Items";
                        playFabItem.Description = descriptionItem;

                        AssetDatabase.CreateAsset(item, "Assets/Resources/Armor/" + nameItem + ".asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        PlayFabItems.UpdateItem(playFabItem);
                        return true;
                    }
                }
                return false;
                              
            case KindOfItem.isCoin:
                var coinsItems = Resources.LoadAll("Coins", typeof(CoinsItems));
                foreach (CoinsItems itemObject in coinsItems)
                {

                    if (nameItem == itemObject.nameItem)
                    {
                        Debug.Log("This is the Item to Update");


                        AssetDatabase.DeleteAsset("Assets/Resources/Coins/" + nameItem + ".asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        CoinsItems item = ScriptableObject.CreateInstance<CoinsItems>();
                        item.nameItem = itemObject.nameItem;
                        item.priceItem = itemObject.priceItem;
                        item.imageItem = itemObject.imageItem;

                        item.description = descriptionItem;


                        CatalogItem playFabItem = new CatalogItem();
                        playFabItem.ItemId = nameItem;
                        playFabItem.DisplayName = nameItem;
                        playFabItem.CatalogVersion = "Items";
                        playFabItem.Description = descriptionItem;

                        AssetDatabase.CreateAsset(item, "Assets/Resources/Coins/" + nameItem + ".asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        PlayFabItems.UpdateItem(playFabItem);
                        return true;
                    }
                }
                return false;


            case KindOfItem.isItem:

                var otherItems = Resources.LoadAll("Others", typeof(OtherItems));
                foreach (OtherItems itemObject in otherItems)
                {

                    if (nameItem == itemObject.nameItem)
                    {
                        Debug.Log("This is the Item to Update");


                        AssetDatabase.DeleteAsset("Assets/Resources/Others/" + nameItem + ".asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        OtherItems item = ScriptableObject.CreateInstance<OtherItems>();
                        item.nameItem = itemObject.nameItem;
                        item.priceItem = itemObject.priceItem;
                        item.imageItem = itemObject.imageItem;

                        item.description = descriptionItem;


                        CatalogItem playFabItem = new CatalogItem();
                        playFabItem.ItemId = nameItem;
                        playFabItem.DisplayName = nameItem;
                        playFabItem.CatalogVersion = "Items";
                        playFabItem.Description = descriptionItem;

                        AssetDatabase.CreateAsset(item, "Assets/Resources/Others/" + nameItem + ".asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        PlayFabItems.UpdateItem(playFabItem);
                        return true;
                    }
                }
                return false;
                                
            case KindOfItem.isWeapon:
                var weaponItems = Resources.LoadAll("Weapons", typeof(WeaponItems));
                foreach (WeaponItems itemObject in weaponItems)
                {

                    if (nameItem == itemObject.nameItem)
                    {
                        Debug.Log("This is the Item to Update");


                        AssetDatabase.DeleteAsset("Assets/Resources/Weapons/" + nameItem + ".asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        WeaponItems item = ScriptableObject.CreateInstance<WeaponItems>();
                        item.nameItem = itemObject.nameItem;
                        item.priceItem = itemObject.priceItem;
                        item.imageItem = itemObject.imageItem;

                        item.description = descriptionItem;


                        CatalogItem playFabItem = new CatalogItem();
                        playFabItem.ItemId = nameItem;
                        playFabItem.DisplayName = nameItem;
                        playFabItem.CatalogVersion = "Items";
                        playFabItem.Description = descriptionItem;

                        AssetDatabase.CreateAsset(item, "Assets/Resources/Weapons/" + nameItem + ".asset");
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                        PlayFabItems.UpdateItem(playFabItem);
                        return true;
                    }
                }





                break;
        }




        return false;            
        
    }

}
