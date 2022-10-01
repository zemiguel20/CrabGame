using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    const int TIME_LIMIT = 30;
    const int STARTING_SEAGULL_POOL_SIZE = 20;

    [SerializeField] private Vector3 playerSpawnPoint;
    [SerializeField] private List<GameObject> seagullSpawnPoints;
    [SerializeField] private List<GameDifficulty> difficultyLevels;

    private GameObject playerInstance;
    private List<GameObject> seagullInstancePool;
    private GameDifficulty currentDifficulty;
    private Coroutine seagullSpawner;
    private Coroutine timeCount;

    public int time { get; private set; }

    public Action<bool> gameEnded;

    void Awake()
    {
        // Instance player
        GameObject playerPrefab = Resources.Load<GameObject>("Crab");
        playerInstance = Instantiate(playerPrefab);
        playerInstance.SetActive(false);

        // Instance pool of seagulls
        GameObject seagullPrefab = Resources.Load<GameObject>("Seagull");
        seagullInstancePool = new List<GameObject>();
        for (int i = 0; i < STARTING_SEAGULL_POOL_SIZE; i++)
        {
            GameObject instance = Instantiate(seagullPrefab);
            instance.SetActive(false);
            seagullInstancePool.Add(instance);
        }
    }

    public void StartEasyRun()
    {
        currentDifficulty = difficultyLevels[0];
        StartGame();
    }

    public void StartMediumRun()
    {
        currentDifficulty = difficultyLevels[1];
        StartGame();
    }

    public void StartHardRun()
    {
        currentDifficulty = difficultyLevels[2];
        StartGame();
    }

    void StartGame()
    {
        playerInstance.transform.position = playerSpawnPoint;
        playerInstance.SetActive(true);

        time = 0;

        seagullSpawner = StartCoroutine(SeagullSpawner());
        timeCount = StartCoroutine(TimeCount());
    }

    IEnumerator SeagullSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(currentDifficulty.seagullSpawnCooldown);
            SpawnSeagull();
        }
    }

    void SpawnSeagull()
    {
        GameObject spawnedInstance = null;

        // Find an inactive instance
        for (int i = 0; i < seagullInstancePool.Count; i++)
        {
            if (!seagullInstancePool[i].activeSelf)
            {
                spawnedInstance = seagullInstancePool[i];
                break;
            }
        }

        // If all already active, create a new one as fallback
        if (!spawnedInstance)
        {
            spawnedInstance = Instantiate(seagullInstancePool[0]);
            seagullInstancePool.Add(spawnedInstance);
        }

        spawnedInstance.SetActive(true);

        // Set random spawn point
        int randomInd = UnityEngine.Random.Range(0, seagullSpawnPoints.Count);
        spawnedInstance.transform.position = seagullSpawnPoints[randomInd].transform.position;

        SeagullController controller = spawnedInstance.GetComponent<SeagullController>();
        Vector2 target = new Vector2(playerInstance.transform.position.x, playerInstance.transform.position.z);
        controller.SetDirection(target);
        controller.speed = currentDifficulty.seagullSpeed;
    }

    IEnumerator TimeCount()
    {
        while (time < TIME_LIMIT)
        {
            yield return new WaitForSeconds(1.0f);
            time++;
        }

        EndGame(true);
    }

    public void EndGame(bool playerWon)
    {
        StopCoroutine(timeCount);
        StopCoroutine(seagullSpawner);

        playerInstance.SetActive(false);
        foreach (GameObject seagull in seagullInstancePool) seagull.SetActive(false);

        gameEnded?.Invoke(playerWon);
    }
}