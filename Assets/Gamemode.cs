using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameObject playerInstance;
    [SerializeField] private float playerSpeed;

    [Space(10)]

    [Header("Easy Difficulty")]
    [SerializeField] private float seagullSpeedEasy;
    [SerializeField] private float seagullSpawnCooldownEasy;

    [Header("Medium Difficulty")]
    [SerializeField] private float seagullSpeedMedium;
    [SerializeField] private float seagullSpawnCooldownMedium;

    [Header("Hard Difficulty")]
    [SerializeField] private float seagullSpeedHard;
    [SerializeField] private float seagullSpawnCooldownHard;

    [Space(10)]

    [SerializeField] private int timeLimit;

    [Space(10)]

    private float selectedSeagullSpeed;
    private WaitForSeconds selectedSpawnCooldown;
    private Coroutine seagullSpawner;

    [SerializeField] private List<GameObject> seagullSpawnPoints;
    private List<GameObject> instancePool;

    public UnityEvent gameLost;
    public UnityEvent gameWon;

    private Coroutine timeCount;
    public int time { get; private set; }

    void Awake()
    {
        DespawnPlayer();

        // Instance pool of seagulls
        GameObject seagullPrefab = Resources.Load<GameObject>("Seagull");
        instancePool = new List<GameObject>();
        for (int i = 0; i < 15; i++)
        {
            GameObject instance = Instantiate(seagullPrefab);
            instancePool.Add(instance);
        }

        DespawnAllSeagull();
    }

    public void StartEasyRun()
    {
        selectedSeagullSpeed = seagullSpeedEasy;
        selectedSpawnCooldown = new WaitForSeconds(seagullSpawnCooldownEasy);
        StartGame();
    }

    public void StartMediumRun()
    {
        selectedSeagullSpeed = seagullSpeedMedium;
        selectedSpawnCooldown = new WaitForSeconds(seagullSpawnCooldownMedium);
        StartGame();
    }

    public void StartHardRun()
    {
        selectedSeagullSpeed = seagullSpeedHard;
        selectedSpawnCooldown = new WaitForSeconds(seagullSpawnCooldownHard);
        StartGame();
    }

    void StartGame()
    {
        SpawnPlayer();
        time = 0;

        seagullSpawner = StartCoroutine(SeagullSpawner());
        timeCount = StartCoroutine(TimeCount());
    }

    IEnumerator SeagullSpawner()
    {
        while (true)
        {
            yield return selectedSpawnCooldown;
            SpawnSeagull();
        }
    }

    IEnumerator TimeCount()
    {
        while (time < timeLimit)
        {
            yield return new WaitForSeconds(1.0f);
            time++;
        }

        EndGame(true);
    }

    public void PlayerHitCallback(Collision collision)
    {
        if (collision.collider.CompareTag("Seagull"))
        {
            EndGame(false);
        }
    }

    void EndGame(bool playerWon)
    {
        StopCoroutine(timeCount);
        StopCoroutine(seagullSpawner);
        DespawnAllSeagull();
        playerInstance.SetActive(false);

        if (playerWon)
            gameWon.Invoke();
        else
            gameLost.Invoke();
    }

    void SpawnSeagull()
    {
        GameObject spawnedInstance = null;

        // Find an inactive instance
        for (int i = 0; i < instancePool.Count; i++)
        {
            if (!instancePool[i].activeSelf)
            {
                spawnedInstance = instancePool[i];
                break;
            }
        }

        // If all already active, create a new one as fallback
        if (!spawnedInstance)
        {
            spawnedInstance = Instantiate(instancePool[0]);
            instancePool.Add(spawnedInstance);
        }

        // Set active
        spawnedInstance.SetActive(true);
        // Set random spawn point
        int randomInd = Random.Range(0, seagullSpawnPoints.Count);
        spawnedInstance.transform.position = seagullSpawnPoints[randomInd].transform.position;
        // Set movement
        SeagullController sgcomponent = spawnedInstance.GetComponent<SeagullController>();
        sgcomponent.speed = selectedSeagullSpeed;
        Vector2 seagullDirection = new Vector2(
            playerInstance.transform.position.x,
            playerInstance.transform.position.z);
        sgcomponent.SetDirection(seagullDirection);
    }

    void DespawnAllSeagull()
    {
        foreach (GameObject seagull in instancePool)
        {
            seagull.SetActive(false);
        }
    }

    void SpawnPlayer()
    {
        playerInstance.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        playerInstance.GetComponent<CrabController>().speed = playerSpeed;
        playerInstance.SetActive(true);
    }

    void DespawnPlayer()
    {
        playerInstance.SetActive(false);
    }
}