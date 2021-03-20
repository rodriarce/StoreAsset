using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repeat : MonoBehaviour
{

    public GameObject repetidor = null;

    void Start()
    {
        InvokeRepeating("Repeat", 0, 5);
    }

    void Repeat()
    {
        //Enable or Disable the GameObject
    }
}
