using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory inventory;
    
        
  
    public GameObject panelEquipment;
    // Start is called before the first frame update
    private void Awake()
    {
        if (inventory == null)
        {
            inventory = this;
        }
    }
    void Start()
    {
        
    }

    private void SetGameObject()
    {

    }
    

    

    


    



    

  
}


