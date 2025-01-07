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

    // Referencia al RoadObject que gestiona las Tiles
    [SerializeField] RoadObject roadObject;

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
                Vector3 position = GetRandomSpawnPosition();
                // Vector3 position = new Vector3(5, 0, 60);

                pool[i].transform.position = position;
                pool[i].SetActive(true);
                return;
            }
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        if (roadObject == null)
        {
            throw new System.Exception("RoadObject no está asignado en PassengerPool.");
        }

        // Selecciona aleatoriamente una Tile del RoadObject
        RoadTile selectedTile = roadObject.GetRandomTile();

        // Obtiene una posición aleatoria válida dentro de la Tile seleccionada
        //Vector3 randomPoint = selectedTile.GetRandomPlaceablePosition();
        Vector3 randomPoint = selectedTile.GetRandomPositionWithinCollider();

        return randomPoint;
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
