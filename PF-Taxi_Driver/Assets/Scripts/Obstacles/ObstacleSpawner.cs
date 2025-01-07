using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Tipos de prefabs de los obstáculos
    [SerializeField] private GameObject weakenerPrefab;
    [SerializeField] private GameObject fencePrefab;
    private List<GameObject> obstacles = new List<GameObject>();

    // Posiciones válidas
    [SerializeField] private List<GameObject> validPositions;

    // Límites
    [SerializeField] private float initialSpawnInterval = 15f; // Tiempo de aparición inicial
    [SerializeField] private float minSpawnInterval = 2f;      // Tiempo mínimo de aparición
    [SerializeField] private float spawnIntervalReduction = 1f; // Reducción del tiempo de aparición cada 20 segundos

    private float spawnInterval;
    private float spawnTime;
    private float elapsedTime;

    // Variable de control para verificar posiciones válidas
    private bool hasValidPositions = true;

    void Start()
    {
        // Inicializar variables
        obstacles.Add(weakenerPrefab);
        obstacles.Add(fencePrefab);
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
            CreateObstacle();
        }
    }

    private void CreateObstacle()
    {
        // Verificar si quedan posiciones válidas
        if (validPositions.Count == 0)
        {
            hasValidPositions = false; // Actualizar la variable de control
            Debug.LogWarning("No hay más posiciones válidas disponibles.");
            return;
        }

        // Elegir aleatoriamente un índice en la lista de posiciones válidas
        int randomIndex = Random.Range(0, validPositions.Count);
        GameObject randomPositionObject = validPositions[randomIndex];
        Vector3 randomPosition = randomPositionObject.transform.position;

        // Eliminar la posición utilizada de la lista
        validPositions.RemoveAt(randomIndex);

        // Elegir aleatoriamente el objeto que se va a crear
        GameObject randomObstacle = obstacles[Random.Range(0, obstacles.Count)];

        // Crear el obstáculo en la escena
        Instantiate(randomObstacle, randomPosition, Quaternion.identity);
    }
}
