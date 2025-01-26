using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float health = 100;
    public int giveMoneyOnDeath = 75;

    public GameObject deathEffect;


    private void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
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

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }
}
