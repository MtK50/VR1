using UnityEngine;

public class LaserShooter : MonoBehaviour
{
    public float laserRange = 50f;      // Maximum distance of the laser


    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            ShootLaser();
            Debug.Log("Laser shot!");
        }
    }

    void ShootLaser()
    {
        // Perform a raycast in the forward direction of the player
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, laserRange))
        {
            // Check if the hit object has the tag "Gems"
            if (hit.collider.CompareTag("Gems"))
            {
                // Get the Renderer of the hit object
                Renderer gemRenderer = hit.collider.GetComponent<Renderer>();
                if (gemRenderer != null)
                {
                    // Change the color of the gem to a random color
                    gemRenderer.material.color = new Color(Random.value, Random.value, Random.value);
                }
            }

            // Optional: Debug line to see where the laser hits in the scene view
            Debug.DrawLine(transform.position, hit.point, Color.red, 0.5f);
        }
        else
        {
            // Optional: Debug line for no hit
            Debug.DrawLine(transform.position, transform.position + transform.forward * laserRange, Color.green, 0.5f);
        }
    }
}
