using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform startPosition;
    public Transform enemyPrefab;
    public TextMeshProUGUI countdownText;
    public float timeOffsetBetweenWaves = 5.5f;
    private float countdown = 2f;
    private int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        countdownText.text = Mathf.Round(timeOffsetBetweenWaves).ToString();

    }

    IEnumerator SpawnNextWave()
    {
        for (int i = 0; i < waveNumber; i++)
        {
            spawnEnemy();
            yield return new WaitForSeconds(0.5f); //wait half a second between each enemy spawn
        }
        Debug.Log($"wave incoming! wave number: {waveNumber}");
        waveNumber++;
    }

    private void spawnEnemy()
    {
        Instantiate(enemyPrefab, startPosition.position, startPosition.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = $"Time till next wave: {Mathf.Round(countdown).ToString()}";
        if (countdown <= 0)
        {
            StartCoroutine(SpawnNextWave());
            countdown = timeOffsetBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }
}
