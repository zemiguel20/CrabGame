using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float playerSpeed;

    [SerializeField] private GameObject seagull;
    [SerializeField] private float seagullSpeed;
    [SerializeField] private List<Vector3> spawnPoints;
    private List<GameObject> instancePool;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO: IMPLEMENT 

    //void Awake()
    //{
    //    // Create pool of inactive objects
    //    instancePool = new List<GameObject>();
    //    for (int i = 0; i < 15; i++)
    //    {
    //        GameObject instance = Instantiate(obj);
    //        instance.SetActive(false);
    //        instancePool.Add(instance);
    //    }
    //}

    //public GameObject Spawn()
    //{
    //    GameObject spawnedInstance = null;

    //    // Find an inactive instance
    //    for (int i = 0; i < instancePool.Count; i++)
    //    {
    //        if (!instancePool[i].activeSelf)
    //        {
    //            spawnedInstance = instancePool[i];
    //            break;
    //        }
    //    }

    //    // If all already active, create a new one as fallback
    //    if (!spawnedInstance)
    //    {
    //        spawnedInstance = Instantiate(obj);
    //        instancePool.Add(spawnedInstance);
    //    }


    //    // Set active and position
    //    spawnedInstance.SetActive(true);
    //    int randomInd = Random.Range(0, spawnPoints.Count);
    //    spawnedInstance.transform.position = spawnPoints[randomInd];

    //    return spawnedInstance;
    //}

    //public void Despawn(GameObject instance)
    //{
    //    instancePool.Find(obj => instance.Equals(obj))?.SetActive(false);
    //}
}
