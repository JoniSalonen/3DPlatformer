using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform position1, position2;
    [SerializeField]
    private float _speed;
    private bool _switch = false;
    private Vector3 lastPlatformPosition;
    private Vector3 platformMovement;

    private void Start()
    {
        lastPlatformPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (_switch == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, position1.position,
                _speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, position2.position,
                _speed * Time.deltaTime);
        }

        if (transform.position == position1.position)
        {
            _switch = true;
        }
        else if (transform.position == position2.position)
        {
            _switch = false;
        }

        // Calculate platform movement since last frame
        platformMovement = transform.position - lastPlatformPosition;
        lastPlatformPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Move the player along with the platform
            other.transform.parent = transform;
        }

        if (other.CompareTag("Box"))
        {
            // Move the player along with the platform
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop moving the player with the platform
            other.transform.parent = null;
        }

        if (other.CompareTag("Box"))
        {
            // Stop moving the player with the platform
            other.transform.parent = null;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // If a player is on the platform, move the player along with the platform's movement
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position += platformMovement;
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            collision.transform.position += platformMovement;
        }
    }
}
