using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Prefab;

    public GameObject CurrSpawn;

    public float ResetSpawnTime = 3f;
    public float SpawnTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrSpawn == null)
        {
            SpawnTime -= Time.deltaTime;
            if (SpawnTime < 0)
            {
                SpawnTime = ResetSpawnTime;
                CurrSpawn = GameObject.Instantiate(Prefab, this.transform);
            }
        }
    }
}
