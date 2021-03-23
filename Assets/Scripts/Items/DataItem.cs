using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class DataItem : MonoBehaviour
{
    public string itemId;
    public bool itemState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    public void SetDataItem(string itemId)
    {
        this.itemId = itemId;
    }
}
