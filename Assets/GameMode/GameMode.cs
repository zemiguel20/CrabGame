using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    const int TIME_LIMIT = 30;
    const int STARTING_SEAGULL_POOL_SIZE = 20;

    [SerializeField] private Vector3 playerSpawnPoint;
    [SerializeField] private List<Vector3> seagullSpawnPoints;

    public GameDifficulty currentDifficulty;
    public int time { get; private set; }
    public bool running { get; private set; }
    public event Action<bool> gameEnded;

    private GameObject playerInstance;
    private List<GameObject> seagullInstancePool;
    private Coroutine seagullSpawner;
    private Coroutine timeCount;

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

        time = 0;
        running = false;
    }

    public void StartGame()
    {
        playerInstance.transform.position = playerSpawnPoint;
        playerInstance.SetActive(true);

        time = 0;

        seagullSpawner = StartCoroutine(SeagullSpawner());
        timeCount = StartCoroutine(TimeCount());

        running = true;
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
        spawnedInstance.transform.position = seagullSpawnPoints[randomInd];

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

        running = false;

        gameEnded?.Invoke(playerWon);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawIcon(playerSpawnPoint, "icon-egg");
        Gizmos.DrawWireSphere(playerSpawnPoint, 2.0f);

        Gizmos.color = Color.red;
        foreach (Vector3 point in seagullSpawnPoints)
        {
            Gizmos.DrawIcon(point, "icon-egg");
            Gizmos.DrawWireSphere(point, 2.0f);
        }
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (running)
        {
            GUI.skin.button.fontSize = 15;
            GUI.skin.button.fontStyle = FontStyle.Bold;
            GUILayout.BeginArea(new Rect(20, 130, 100, 100));
            if (GUILayout.Button("Win Game")) { EndGame(true); time = 30; } // set time just for info purposes
            if (GUILayout.Button("Lose Game")) { EndGame(false); }
            GUILayout.EndArea();
        }
    }
#endif
}