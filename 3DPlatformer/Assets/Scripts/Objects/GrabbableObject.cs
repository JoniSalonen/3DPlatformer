using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    private Rigidbody rb;
    private Transform objectGrabPointTransform;
    private bool isGrabbed = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
        isGrabbed = true;
        rb.isKinematic = true;
    }

    public void Release()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        isGrabbed = false;
    }

    private void FixedUpdate()
    {
        if (isGrabbed && objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPosition);
        }
    }
}
