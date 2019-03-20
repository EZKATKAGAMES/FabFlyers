using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcaEnemyPool : MonoBehaviour
{
    public int enemyPoolSize = 5;
    public GameObject orcaPrefab;
    public float spawnRate = 4f;
    public float coluumnMin = -1f;
    public float columnMax = 3.5f;

    private GameObject[] orcas;
    private Vector2 orcaPoolPosition = new Vector2(-15f, -25f);
    private float timeSinceLastSpawned;
    private float spawnXPosition = 10f;
    private int currentColumn = 0;

    // Start is called before the first frame update
    void Start()
    {
        // new object pool for the orcas
        orcas = new GameObject[enemyPoolSize];

        // spawning of the orcas
        for (int i = 0; i < enemyPoolSize; i++)
        {
            orcas[i] = (GameObject)Instantiate(orcaPrefab, orcaPoolPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GM.gameStateStarted == true)
        {
            // making it smooth for spawning
            timeSinceLastSpawned += Time.deltaTime;

            // if the last time it spawned was more than 4 seconds ago then respawn
            if (timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0;
                float spawnYPosition = Random.Range(coluumnMin, columnMax);
                //setting fixed x position and random y position for our orcas
                orcas[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                currentColumn++;

                if (currentColumn >= enemyPoolSize)
                {
                    currentColumn = 0;
                }
            }
        }
    }
}
