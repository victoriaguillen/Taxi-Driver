using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Tipos de prefabs de los obstáculos
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] ObstacleFactory obstacleFactory;

    //[SerializeField] private GameObject weakenerPrefab;
    //[SerializeField] private GameObject fencePrefab;
    //private List<GameObject> obstacles = new List<GameObject>();

    //// Posiciones válidas
    private List<RoadTile> validPositions;

    // Referencia al RoadObject que gestiona las Tiles
    [SerializeField] RoadObject roadObject;

    // Límites
    [SerializeField] private float initialSpawnInterval = 15f; // Tiempo de aparición inicial
    [SerializeField] private float minSpawnInterval = 2f;      // Tiempo mínimo de aparición
    [SerializeField] private float spawnIntervalReduction = 1f; // Reducción del tiempo de aparición cada 20 segundos

    private float spawnInterval;
    private float spawnTime;
    private bool hasValidPositions;
    private float elapsedTime;

    // Variable de control para verificar posiciones válidas
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
        // Si no hay posiciones válidas, detener el proceso de generación
        if (!hasValidPositions)
        {
            Debug.Log("No hay posiciones válidas disponibles. Deteniendo generación de obstáculos.");
            return;
        }

        // Actualizar tiempo transcurrido
        elapsedTime += Time.deltaTime;
        spawnTime += Time.deltaTime;

        // Reducir el intervalo de aparición progresivamente después del primer minuto
        if (elapsedTime > 60f && elapsedTime % 20f < Time.deltaTime)
        {
            spawnInterval = Mathf.Max(spawnInterval - spawnIntervalReduction, minSpawnInterval);
        }

        // Generar un obstáculo si se cumple el tiempo de aparición
        if (spawnTime > spawnInterval)
        {
            spawnTime = 0;
            

        }
    }

    private void GenerateObstacle()
    {
        // Verificar si quedan posiciones válidas
        if (validPositions.Count == 0)
        {
            hasValidPositions = false; // Actualizar la variable de control
            Debug.LogWarning("No hay más posiciones válidas disponibles.");
            return;
        }

        // Elegir aleatoriamente una posición válida
        RoadTile tile = roadObject.GetRandomTile();
        while (!tile.isObstaclePlaceable)
        {
            tile = roadObject.GetRandomTile();
        }


        // Eliminar la posición utilizada de la lista
        tile.isObstaclePlaceable = false;

        // Elegir aleatoriamente el objeto que se va a crear
        GameObject randomObstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Crear el obstáculo en la escena
        Instantiate(randomObstacle, tile.transform.position, Quaternion.identity);
    }
}

