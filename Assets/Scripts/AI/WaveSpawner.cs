using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public TextMeshProUGUI waveCountDownText;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countDown = 2f;
    private int waveIndex = 1;

    private void Update()
    {
        if (countDown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countDown = timeBetweenWaves;
        }

        countDown -= Time.deltaTime;

        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        waveCountDownText.text = string.Format( "{0:00.00}", countDown);

        IEnumerator SpawnWave()
        {
            for (int i = 0; i < waveIndex; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.5f);
            }

            waveIndex++;
            PlayerStats.rounds++;
        }
        
        void SpawnEnemy()
        {
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
