using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Wave[] waves;
    public TextMeshProUGUI waveCountDownText;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countDown = 2f;
    private int waveIndex = 1;
    public static int enemyAlive = 0;

    private void Update()
    {
        if(enemyAlive > 0)
        {
            return;
        }

        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
            return;
        }

        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        waveCountDownText.text = string.Format( "{0:00.00}", countDown);

        IEnumerator SpawnWave()
        {

            Wave wave = waves[waveIndex];

            for (int i = 0; i < wave.amount; i++)
            {
                SpawnEnemy(wave.enemy);
                yield return new WaitForSeconds(1f/wave.rate);
            }

            waveIndex++;
            PlayerStats.rounds++;

            if(waveIndex == waves.Length)
            {
                Debug.Log("Level 1 Finished");
                this.enabled = false;
            }
        }
        
        void SpawnEnemy(GameObject enemy)
        {
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
            enemyAlive++;
        }
    }
}
