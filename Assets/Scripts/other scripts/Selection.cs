using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
    Vector3 targetRot;

    Vector3 currentAngle;

    int currentSelection;
    int totalCharacteres = 3;

    private void Start()
    {
        currentSelection = 2;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentSelection < totalCharacteres)
        {
            currentAngle = transform.eulerAngles;
            targetRot = targetRot + new Vector3(0, 90, 0);
            currentSelection++;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentSelection > 1)
        {
            currentAngle = transform.eulerAngles;
            targetRot = targetRot - new Vector3(0, 90, 0);
            currentSelection--;
        }
        
        currentAngle = new Vector3(0, Mathf.LerpAngle(currentAngle.y, targetRot.y, 2.0f * Time.deltaTime),0);
        transform.eulerAngles = currentAngle;
    }

    public void OnClickLeft()
    {   
        if(currentSelection > 1)
        {
            currentAngle = transform.eulerAngles;
            targetRot = targetRot + new Vector3(0, 90, 0);
            currentSelection--;

        }
    }
    public void OnRightClick()
    {
        if(currentSelection < totalCharacteres)
        {
            currentAngle = transform.eulerAngles;
            targetRot = targetRot - new Vector3(0, 90, 0);
            currentSelection++;
        }

    }


}

