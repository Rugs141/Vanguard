using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera mainCamera;
    public float range = 100f;
    public float fireRate = 0.5f;
    private float nextTimeToFire = 0f;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) 
        {
            Shoot();
            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            // Log the object hit
            Debug.Log("Hit " + hit.collider.name);

        }
    }
}
