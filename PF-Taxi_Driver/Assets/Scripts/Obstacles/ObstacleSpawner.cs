using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Tipos de prefabs de los obst�culos
    [SerializeField] private GameObject weakenerPrefab;
    [SerializeField] private GameObject fencePrefab;
    private List<GameObject> obstacles = new List<GameObject>();

    // Posiciones v�lidas
    [SerializeField] private List<GameObject> validPositions;

    // L�mites
    [SerializeField] private float initialSpawnInterval = 15f; // Tiempo de aparici�n inicial
    [SerializeField] private float minSpawnInterval = 2f;      // Tiempo m�nimo de aparici�n
    [SerializeField] private float spawnIntervalReduction = 1f; // Reducci�n del tiempo de aparici�n cada 20 segundos

    private float spawnInterval;
    private float spawnTime;
    private float elapsedTime;

    // Variable de control para verificar posiciones v�lidas
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
            CreateObstacle();
        }
    }

    private void CreateObstacle()
    {
        // Verificar si quedan posiciones v�lidas
        if (validPositions.Count == 0)
        {
            hasValidPositions = false; // Actualizar la variable de control
            Debug.LogWarning("No hay m�s posiciones v�lidas disponibles.");
            return;
        }

        // Elegir aleatoriamente un �ndice en la lista de posiciones v�lidas
        int randomIndex = Random.Range(0, validPositions.Count);
        GameObject randomPositionObject = validPositions[randomIndex];
        Vector3 randomPosition = randomPositionObject.transform.position;

        // Eliminar la posici�n utilizada de la lista
        validPositions.RemoveAt(randomIndex);

        // Elegir aleatoriamente el objeto que se va a crear
        GameObject randomObstacle = obstacles[Random.Range(0, obstacles.Count)];

        // Crear el obst�culo en la escena
        Instantiate(randomObstacle, randomPosition, Quaternion.identity);
    }
}
