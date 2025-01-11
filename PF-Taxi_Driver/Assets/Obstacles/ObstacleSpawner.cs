using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Tipos de prefabs de los obst�culos
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] ObstacleFactory obstacleFactory;

    //[SerializeField] private GameObject weakenerPrefab;
    //[SerializeField] private GameObject fencePrefab;
    //private List<GameObject> obstacles = new List<GameObject>();

    //// Posiciones v�lidas
    private List<RoadTile> validPositions;

    // Referencia al RoadObject que gestiona las Tiles
    [SerializeField] RoadObject roadObject;

    // L�mites
    [SerializeField] private float initialSpawnInterval = 15f; // Tiempo de aparici�n inicial
    [SerializeField] private float minSpawnInterval = 2f;      // Tiempo m�nimo de aparici�n
    [SerializeField] private float spawnIntervalReduction = 1f; // Reducci�n del tiempo de aparici�n cada 20 segundos

    private float spawnInterval;
    private float spawnTime;
    private bool hasValidPositions;
    private float elapsedTime;

    // Variable de control para verificar posiciones v�lidas
    //private bool hasValidPositions = true;

    void Start()
    {
        // Inicializar variables
        validPositions = roadObject.RoadTiles();
        //obstacles.Add(weakenerPrefab);
        //obstacles.Add(fencePrefab);
        spawnInterval = initialSpawnInterval;
    }

    void Update()
    {
        // Si no hay posiciones v�lidas, detener el proceso de generaci�n
        if (!hasValidPositions)
        {
            Debug.Log("No hay posiciones v�lidas disponibles. Deteniendo generaci�n de obst�culos.");
            return;
        }

        // Actualizar tiempo transcurrido
        elapsedTime += Time.deltaTime;
        spawnTime += Time.deltaTime;

        // Reducir el intervalo de aparici�n progresivamente despu�s del primer minuto
        if (elapsedTime > 60f && elapsedTime % 20f < Time.deltaTime)
        {
            spawnInterval = Mathf.Max(spawnInterval - spawnIntervalReduction, minSpawnInterval);
        }

        // Generar un obst�culo si se cumple el tiempo de aparici�n
        if (spawnTime > spawnInterval)
        {
            spawnTime = 0;
            

        }
    }

    private void GenerateObstacle()
    {
        // Verificar si quedan posiciones v�lidas
        if (validPositions.Count == 0)
        {
            hasValidPositions = false; // Actualizar la variable de control
            Debug.LogWarning("No hay m�s posiciones v�lidas disponibles.");
            return;
        }

        // Elegir aleatoriamente una posici�n v�lida
        RoadTile tile = roadObject.GetRandomTile();
        while (!tile.isObstaclePlaceable)
        {
            tile = roadObject.GetRandomTile();
        }


        // Eliminar la posici�n utilizada de la lista
        tile.isObstaclePlaceable = false;

        // Elegir aleatoriamente el objeto que se va a crear
        GameObject randomObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Crear el obst�culo en la escena
        Instantiate(randomObstacle, tile.transform.position, Quaternion.identity);
    }
}

