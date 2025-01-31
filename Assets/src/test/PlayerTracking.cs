using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerTracking : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;

    [Header("Tracking Settings")]
    [Tooltip("Speed at which the object rotates to face the player.")]
    public float rotationSpeed = 3f;

    [Tooltip("Duration for how long the object tracks the player.")]
    public float trackingDuration = 0.4f;

    private bool isTracking = false;
    private float trackingTimer = 0f;

    private void Update()
    {
        if (isTracking && player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Keep the rotation only on the horizontal plane

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smoothly interpolate the rotation
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // Update tracking timer
                trackingTimer += Time.deltaTime;
                if (trackingTimer >= trackingDuration)
                {
                    ResetTracking();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            StartTracking();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            StartTracking();
        }
    }

    private void StartTracking()
    {
        isTracking = true;
        trackingTimer = 0f;
    }

    private void ResetTracking()
    {
        isTracking = false;
        trackingTimer = 0f;
    }

}
