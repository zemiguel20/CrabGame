using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float playerSpeed;
    private GameObject playerInstance;

    [SerializeField] private GameObject seagull;
    [SerializeField] private float seagullSpeed;
    [SerializeField] private List<Vector3> seagullSpawnPoints;
    [SerializeField] private float seagullSpawnDelay;
    private List<GameObject> instancePool;

    private bool gameover;

    void Awake()
    {
        // Instance player
        playerInstance = Instantiate(player);
        playerInstance.GetComponent<CrabController>().collisionEvent.AddListener(PlayerHitCallback);

        // Instance pool of seagulls
        instancePool = new List<GameObject>();
        for (int i = 0; i < 15; i++)
        {
            GameObject instance = Instantiate(seagull);
            instance.SetActive(false);
            instancePool.Add(instance);
        }
    }

    void Start()
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

        gameover = false;

        StartCoroutine(SeagullSpawnLoop());
    }

    IEnumerator SeagullSpawnLoop()
    {
        while (!gameover)
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
            spawnedInstance = Instantiate(seagull);
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
            gameover = true;

            playerInstance.SetActive(false);
        }
    }
}