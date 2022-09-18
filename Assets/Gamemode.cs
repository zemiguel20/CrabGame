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
        // Spawn player
        playerInstance = Instantiate(player);
        playerInstance.
            GetComponent<CharacterCollisionEvent>().
            collisionEvent.AddListener(PlayerHitCallback);

        // Create pool of seagulls
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
        playerInstance.transform.position = new Vector3(0.0f, 2.0f, 0.0f);

        gameover = false;
        StartCoroutine(SeagullSpawnLoop());
        SpawnSeagull();
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
        SeagullMovement sgcomponent = spawnedInstance.GetComponent<SeagullMovement>();
        sgcomponent.speed = seagullSpeed;
        sgcomponent.SetDirection(playerInstance.transform);
    }

    private void PlayerHitCallback(ControllerColliderHit hit)
    {
        Debug.Log(hit.collider.tag + "aaa");
        //if (hit.collider.CompareTag("Seagull"))
        //{
        //    Debug.Log("GAME OVER");
        //    gameover = true;
        //}
    }
}