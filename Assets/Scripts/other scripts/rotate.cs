using UnityEngine;
using System.Collections;

public class rotate : MonoBehaviour
{

	public float speed = 5f;

    private void Start()
    {
        
    }
    private void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}
