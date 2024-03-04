using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MovableBox"))
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if(distance < 0.05f)
            {
                other.transform.position = transform.position;
                other.attachedRigidbody.isKinematic = true;
                other.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
    }
}
