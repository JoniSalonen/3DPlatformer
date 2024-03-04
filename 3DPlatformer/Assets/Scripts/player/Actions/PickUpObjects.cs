using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private Transform objectGrabPointTransform;

    [SerializeField]
    private LayerMask pickUpLayerMask;

    [SerializeField]
    private float pickUpDistance = 2f;

    private GrabbableObject currentGrabbedObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentGrabbedObject != null)
            {
                currentGrabbedObject.Release();
                currentGrabbedObject = null;
                return;
            }

            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
            {
                if (raycastHit.transform.TryGetComponent(out GrabbableObject grabbableObject))
                {
                    grabbableObject.Grab(objectGrabPointTransform); // Pass objectGrabPointTransform here
                    currentGrabbedObject = grabbableObject;
                }
            }
        }
    }
}
