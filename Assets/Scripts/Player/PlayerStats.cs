using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public int startingMoney = 500;

    public static int lives;
    public int startingLives = 100;

    public static int rounds;

    private void Start()
    {
        lives = startingLives;
        money = startingMoney;
        rounds = 0;
    }
}
