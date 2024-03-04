using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLocking : MonoBehaviour
{
   
   
    void Update()
    {
        //Press the space bar to apply no locking to the Cursor
        if (Input.GetKey(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
