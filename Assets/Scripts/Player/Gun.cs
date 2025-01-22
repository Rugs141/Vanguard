using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera mainCamera;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextTimeToFire = 0f;

    public Transform turret;
    public ParticleSystem muzzleFlash;

    void Update()
    {
        RotateTurretToMouse();

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            Shoot();
            nextTimeToFire = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        PlayerBullet bullet = bulletGO.GetComponent<PlayerBullet>();

        if (bullet != null)
        {
            bullet.Initialize(firePoint.forward);
        }
    }

    void RotateTurretToMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetPoint = hit.point;
            Vector3 direction = targetPoint - turret.position;
            direction.y = 0;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            turret.rotation = Quaternion.Lerp(turret.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
}