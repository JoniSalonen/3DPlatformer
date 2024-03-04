using UnityEngine;

public class ObjectPushing : MonoBehaviour
{
    [SerializeField] 
    private float forceMagnitude = 1;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody _rigidBody = hit.collider.attachedRigidbody;

        if (_rigidBody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            _rigidBody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);

        }
        
    }
}
