
using System.Collections;
using UnityEngine;

public class PassengerPool : MonoBehaviour
{
    // Array para almacenar los prefabs
    [SerializeField] GameObject[] passengerPrefabs;

    // Número de pasajeros a generar
    [SerializeField] int poolSize = 5;

    [SerializeField][Range(0.1f, 120)] float spawnTimer = 120f;

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
        if (passengerPrefabs.Length == 0)
        {
            Debug.LogWarning("PassengerPrefabs está vacío. Por favor, agrega prefabs al listado.");
            return;
        }

        pool = new GameObject[passengerPrefabs.Length];

        for (int i = 0; i < passengerPrefabs.Length; i++)
        {
            pool[i] = Instantiate(passengerPrefabs[i], transform);
            pool[i].SetActive(false);
            Passenger p = pool[i].GetComponent<Passenger>();
            p.isActive = false;
        }
    }

    void EnableObjectInPool()
    {
        for (int i = 0; i < passengerPrefabs.Length; i++)
        {
            Passenger p = pool[i].GetComponent<Passenger>();
            if (!p.isActive)
            {
                // Asigna una posición aleatoria al objeto activado
                RoadTile selectedTile = GetRandomSpawnTile();
                if (selectedTile != null) 
                {
                    Vector3 position = selectedTile.GetRandomPositionWithinCollider();

                    Debug.Log("blanca");
                    //Vector3 position = new Vector3(5, 0, 60);

                    pool[i].transform.position = position;
                    p.Tile = selectedTile;
                    pool[i].SetActive(true);
                }
                // No hay posiciones validas, se sale.
                return;
            }
        }
    }

    RoadTile GetRandomSpawnTile()
    {
        if (roadObject == null)
        {
            throw new System.Exception("RoadObject no está asignado en PassengerPool.");
        }

        // Selecciona aleatoriamente una Tile del RoadObject
        RoadTile selectedTile = roadObject.GetRandomTile();

        // Obtiene una posición aleatoria válida dentro de la Tile seleccionada
        //Vector3 randomPoint = selectedTile.GetRandomPlaceablePosition();


        return selectedTile;
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