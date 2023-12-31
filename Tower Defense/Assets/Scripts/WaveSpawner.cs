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
    //public float timeOffsetBetweenWaves = 5.5f;
    public float timeOffsetBetweenWaves = 3.5f;

    public Wave[] waves;
    public static int currPathIndx = 0;
    public static int ActiveEnemies;
    private float countdown = 2f;
    private int waveNumber = 0;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        countdownText.text = Mathf.Round(timeOffsetBetweenWaves).ToString();
        ActiveEnemies= 0;
        gameManager = GameManager.Instance;
    }

    IEnumerator SpawnNextWave()
    {
        Wave wave = waves[waveNumber];
        currPathIndx = wave.pathIndx;
        ActiveEnemies = wave.count;
        for (int i = 0; i < wave.count; i++)
        {
            spawnEnemy(wave.enemy, wave.pathIndx);
            yield return new WaitForSeconds(1 / wave.spawningRate); 
        }
        Debug.Log($"wave incoming! wave number: {waveNumber}");
        waveNumber++;
        GameManager.roundsPlayed++;
    }

    private void spawnEnemy(GameObject enemy, int pathId)
    {
        //enemy.GetComponent<Enemy>().setPath(pathId);
        Instantiate(enemy, startPosition.position, startPosition.rotation);

        //ActiveEnemies++;
    }

    // Update is called once per frame
    void Update()
    {
        countdownText.text = $"Time till next wave: {Mathf.Round(countdown).ToString()}";
        if (ActiveEnemies <= 0)
        {
            // there are more waves to go
            if (waveNumber < waves.Length)
            {
                if (countdown <= 0)
                {
                    StartCoroutine(SpawnNextWave());
                    //reset timer between waves
                    countdown = timeOffsetBetweenWaves;
                }
                else
                {
                    countdown -= Time.deltaTime;
                }
            }
            // all waves completed
            else
            {
                //move to next level or win screen
                Debug.Log("level finsihed!");
                gameManager.WinLevel();
                this.enabled= false;
            }

        }
    }
}
