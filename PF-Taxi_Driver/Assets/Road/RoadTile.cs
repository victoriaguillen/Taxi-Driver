using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RoadTile : MonoBehaviour
{
    public float gridSize = 0.5f; // Tama�o de las subdivisiones dentro de la Tile
    public Vector3 tileSize = new Vector3(1, 1, 1); // Tama�o de la Tile
    //public LayerMask pavementLayer; // Capa que identifica la acera
    private List<Vector3> validPositions; // Lista de posiciones v�lidas dentro de la Tile
    public List<RoadTile> neighbors; // Lista de vecinos a este RoadTile
    private bool isHighlighted = false; // Estado del resaltado
    [SerializeField] float customNeighborDistance = 10.0f;

    private MeshCollider meshCollider;
    private BoxCollider[] boxColliders;
    public bool HasObstacle { get; set; } = false;

    public MeshCollider GetMeshCollider()
    {
        return meshCollider;
    }

    public float CustomNeighborDistance
    {
        get { return customNeighborDistance; }
        set { customNeighborDistance = value; }
    }

    void Start()
    {
        meshCollider = GetComponentInChildren<MeshCollider>();
        boxColliders = GetComponentsInChildren<BoxCollider>();
        //PrecomputeValidPositions();

    }

    //Precomputar las posiciones v�lidas dentro de la Tile
    void PrecomputeValidPositions()
    {
        validPositions = new List<Vector3>();

        // Iterar sobre todas las subdivisiones de la Tile
        for (float x = -tileSize.x / 2; x <= tileSize.x / 2; x += gridSize)
        {
            for (float z = -tileSize.z / 2; z <= tileSize.z / 2; z += gridSize)
            {
                Vector3 testPosition = transform.position + new Vector3(x, 0, z);

                if (CheckIsPlaceable(testPosition))
                {
                    validPositions.Add(testPosition);
                }
            }
        }
    }



    public Vector3 GetRandomPositionWithinCollider()
    {
        // Obtenemos el centro y los extents del MeshCollider
        Bounds bounds = meshCollider.bounds;

        // Generamos una posici�n aleatoria dentro de los l�mites del collider
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        // Creamos la posici�n aleatoria
        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        if (CheckIsPlaceable(randomPosition)) { return randomPosition; }
        return GetRandomPositionWithinCollider();
    }


    // M�todo para verificar si una posici�n espec�fica es v�lida
    public bool CheckIsPlaceable(Vector3 position)
    {
        foreach (BoxCollider boxCollider in boxColliders)
        {
            if (boxCollider.bounds.Contains(position))
            { return false; }

        }
        return true;
    }

    // Resaltar la tile en dorado
    public void HighlightTile()
    {
        if (!isHighlighted)
        {
            // Activa el halo asociado al GameObject del tile
            Behaviour halo = (Behaviour)GetComponent("Halo");
            if (halo != null)
            {
                halo.enabled = true; // Activa el halo
            }

            isHighlighted = true;
        }
    }


    // Apagar el resaltado
    public void UnhighlightTile()
    {
        if (isHighlighted)
        {
            // Activa el halo asociado al GameObject del tile
            Behaviour halo = (Behaviour)GetComponent("Halo");
            if (halo != null)
            {
                halo.enabled = false; // Desactiva el halo
            }

            isHighlighted = false;
        }
    }


    // Devuelve una posici�n aleatoria v�lida dentro de la Tile
    public Vector3 GetRandomPlaceablePosition()
    {
        if (validPositions.Count == 0)
        {
            throw new System.Exception("No hay posiciones v�lidas en esta Tile.");
        }

        return validPositions[Random.Range(0, validPositions.Count)];
    }


    // M�todo para establecer los vecinos de este tile (puedes hacer este c�lculo desde RoadObject)
    public void SetNeighbors(List<RoadTile> adjacentTiles)
    {
        neighbors = new List<RoadTile>(adjacentTiles);

    }

}
