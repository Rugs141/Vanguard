using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    public int health = 100;
    public int giveMoneyOnDeath = 75;

    private Transform target;
    private int wavePointIndex = 0;

    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
    }
    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerStats.money += giveMoneyOnDeath;

        GameObject Effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(Effect, 5f);

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }

    void GetNextWayPoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            PathEnded();
            return;
        }

        wavePointIndex++;
        target = Waypoints.points[wavePointIndex];
    }

    void PathEnded()
    {
        PlayerStats.lives--;
        Destroy(gameObject);
    }

}
