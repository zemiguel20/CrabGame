using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameMode : MonoBehaviour
{
    private GameObject playerInstance;
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

    // Make spawnpoints a GameObject
    [SerializeField] private List<Vector3> seagullSpawnPoints;
    private List<GameObject> instancePool;

    public UnityEvent gameLost;
    public UnityEvent gameWon;

    private Coroutine timeCount;
    public int time { get; private set; }

    void Awake()
    {
        // Instance player
        GameObject playerPrefab = Resources.Load<GameObject>("Player");
        playerInstance = Instantiate(playerPrefab);
        playerInstance.GetComponent<CrabController>().collisionEvent.AddListener(PlayerHitCallback);
        playerInstance.SetActive(false);

        // Instance pool of seagulls
        GameObject seagullPrefab = Resources.Load<GameObject>("Seagull");
        instancePool = new List<GameObject>();
        for (int i = 0; i < 15; i++)
        {
            GameObject instance = Instantiate(seagullPrefab);
            instancePool.Add(instance);
        }
        SetAllSeagullsInactive();
    }

    public void StartEasyRun()
    {
        selectedSeagullSpeed = seagullSpawnCooldownEasy;
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
        // Activate and position player
        playerInstance.transform.position = new Vector3(0.0f, 0.5f, 0.0f);
        playerInstance.GetComponent<CrabController>().speed = playerSpeed;
        playerInstance.SetActive(true);

        SetAllSeagullsInactive();

        time = 0;

        seagullSpawner = StartCoroutine(SeagullSpawnLoop());
        timeCount = StartCoroutine(TimeCount());
    }

    IEnumerator SeagullSpawnLoop()
    {
        while (true)
        {
            yield return selectedSpawnCooldown;
            SpawnSeagull();
        }
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
        spawnedInstance.transform.position = seagullSpawnPoints[randomInd];
        // Set movement
        SeagullController sgcomponent = spawnedInstance.GetComponent<SeagullController>();
        sgcomponent.speed = selectedSeagullSpeed;
        Vector2 seagullDirection = new Vector2(
            playerInstance.transform.position.x,
            playerInstance.transform.position.z);
        sgcomponent.SetDirection(seagullDirection);
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

    private void PlayerHitCallback(Collision collision)
    {
        if (collision.collider.CompareTag("Seagull"))
        {
            EndGame(false);
        }
    }

    public void EndGame(bool playerWon)
    {
        StopCoroutine(timeCount);
        StopCoroutine(seagullSpawner);
        SetAllSeagullsInactive();
        playerInstance.SetActive(false);

        if (playerWon)
            gameWon.Invoke();
        else
            gameLost.Invoke();
    }

    public void SetAllSeagullsInactive()
    {
        foreach (GameObject seagull in instancePool)
        {
            seagull.SetActive(false);
        }
    }
}