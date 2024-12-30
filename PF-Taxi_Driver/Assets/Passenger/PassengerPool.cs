using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PassengerPool : MonoBehaviour
{
    // Array para almacenar los prefabs
    [SerializeField] List<GameObject> passengerPrefabs = new List<GameObject>();

    // Número de pasajeros a generar
    [SerializeField] int poolSize = 5;

    [SerializeField][Range(0.1f, 30)] float spawnTimer = 1f;

    // Rango de posiciones aleatorias
    [SerializeField] Vector3 minSpawnPosition;
    [SerializeField] Vector3 maxSpawnPosition;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnPassengers());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            // Instancia un prefab aleatorio del array
            int randomPrefabIndex = Random.Range(0, passengerPrefabs.Count);
            pool[i] = Instantiate(passengerPrefabs[randomPrefabIndex], transform);
            pool[i].SetActive(false);
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                // Asigna una posición aleatoria al objeto activado
                Vector3 posicion = GetRandomSpawnPosition();
                pool[i].transform.position = posicion;
                pool[i].SetActive(true);
                return;
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(
            Random.Range(minSpawnPosition.x, maxSpawnPosition.x),
            Random.Range(minSpawnPosition.y, maxSpawnPosition.y),
            Random.Range(minSpawnPosition.z, maxSpawnPosition.z)
        );
    }

    IEnumerator SpawnPassengers()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
