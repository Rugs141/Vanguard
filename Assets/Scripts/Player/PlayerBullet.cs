using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public GameObject impactEffect; 
    public int damage = 50;         
    public float speed = 70f;       

    private Vector3 direction;      

    public void Initialize(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (impactEffect != null)
        {
            GameObject effectInstance = Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(effectInstance, 2f);
        }

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); 
        }

        Destroy(gameObject);
    }
}
