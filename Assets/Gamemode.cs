using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    private GameObject playerInstance;

    [SerializeField] private float seagullSpeed;
    [SerializeField] private List<Vector3> seagullSpawnPoints;
    [SerializeField] private float seagullSpawnDelay;
    private List<GameObject> instancePool;

    public enum GameState
    {
        START,
        RUNNING,
        GAMEOVER
    }
    public GameState state { get; private set; }

    public float time { get; private set; }

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
            instance.SetActive(false);
            instancePool.Add(instance);
        }

        state = GameState.START;
    }

    private void Update()
    {
        if ((state == GameState.START || state == GameState.GAMEOVER)
            && Input.GetKey(KeyCode.Space))
        {
            StartGame();
        }

        if (state == GameState.RUNNING)
        {
            time += Time.deltaTime;
        }

        // APPLICATION QUIT
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    void StartGame()
    {
        // Activate and position player
        playerInstance.transform.position = new Vector3(0.0f, 0.5f, 0.0f);
        playerInstance.GetComponent<CrabController>().speed = playerSpeed;
        playerInstance.SetActive(true);

        // Set all seagull instances inactive
        foreach (GameObject instance in instancePool)
        {
            instance.SetActive(false);
        }

        state = GameState.RUNNING;

        time = 0.0f;

        StartCoroutine(SeagullSpawnLoop());
    }

    IEnumerator SeagullSpawnLoop()
    {
        while (state == GameState.RUNNING)
        {
            yield return new WaitForSeconds(seagullSpawnDelay);
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
        sgcomponent.speed = seagullSpeed;
        Vector2 seagullDirection = new Vector2(
            playerInstance.transform.position.x,
            playerInstance.transform.position.z);
        sgcomponent.SetDirection(seagullDirection);
    }

    private void PlayerHitCallback(Collision collision)
    {
        if (collision.collider.CompareTag("Seagull"))
        {
            Debug.Log("GAME OVER");
            state = GameState.GAMEOVER;

            playerInstance.SetActive(false);
        }
    }
}