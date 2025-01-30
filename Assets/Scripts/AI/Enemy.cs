using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int giveMoneyOnDeath = 75;

    public GameObject deathEffect;

    public Image hpBar;


    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        hpBar.fillAmount = health / startHealth;

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

        WaveSpawner.enemyAlive--;

        Destroy(gameObject);
    }

    public void Slow(float amount)
    {
        speed = startSpeed * (1f - amount);
    }
}
