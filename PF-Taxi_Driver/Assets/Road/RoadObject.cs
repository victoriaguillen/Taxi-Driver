using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoadObject : MonoBehaviour
{
    private List<RoadTile> roadTiles = new List<RoadTile>(); // Lista de Tiles de la carretera
    

    void Awake()
    {
        InitializeRoadTiles();
        InitializeTileNeighbors(); 
    }


    // Inicializa la lista de Tiles al inicio
    void InitializeRoadTiles()
    {
        foreach (Transform child in transform)
        {
            RoadTile tile = child.GetComponent<RoadTile>();
            if (tile != null)
            {
                roadTiles.Add(tile);
            }
        }
        Debug.Log(roadTiles.Count);

        if (roadTiles.Count == 0)
        {
            Debug.LogError("No se encontraron Tiles en el objeto Road.");
        }
    }


    // Devuelve una Tile aleatoria
    public RoadTile GetRandomTile()
    {
        if (roadTiles.Count == 0)
        {
            throw new System.Exception("No hay Tiles disponibles en el RoadObject.");
        }

        return roadTiles[UnityEngine.Random.Range(0, roadTiles.Count)];
    }

    void InitializeTileNeighbors()
    {
        foreach (RoadTile tile in roadTiles)
        {
            List<RoadTile> neighbors = new List<RoadTile>();

            foreach (RoadTile otherTile in roadTiles)
            {
                if (tile != otherTile && IsAdjacent(tile, otherTile))
                {
                    neighbors.Add(otherTile);
                }
            }
            if (neighbors.Count == 0) 
            { 
                //Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaa");
            }
            tile.SetNeighbors(neighbors);
        }
    }

    bool IsAdjacent(RoadTile tile1, RoadTile tile2)
    {
        Vector3 pos1 = tile1.transform.position;
        Vector3 pos2 = tile2.transform.position;

        float dx = Mathf.Abs(pos1.x - pos2.x);
        float dz = Mathf.Abs(pos1.z - pos2.z);

        // Los tiles son adyacentes si están a una unidad en un solo eje y en la misma fila/columna
        return (dx <= tile1.CustomNeighborDistance+tile2.CustomNeighborDistance && dz <= 0.2f) || (dz <= tile1.CustomNeighborDistance+tile2.CustomNeighborDistance && dx <= 0.2f);
    }




    // ruta entre 2 tiles usando enfoque BFS
    public List<RoadTile> FindPath(RoadTile startTile, RoadTile endTile)
    {
        Queue<RoadTile> frontier = new Queue<RoadTile>(); // Cola para la búsqueda
        Dictionary<RoadTile, RoadTile> cameFrom = new Dictionary<RoadTile, RoadTile>(); // Para reconstruir el camino
        frontier.Enqueue(startTile);
        cameFrom[startTile] = null;

        while (frontier.Count > 0)
        {
            RoadTile currentTile = frontier.Dequeue();

            // Si llegamos al destino, reconstruimos el camino
            if (currentTile == endTile)
            {
                List<RoadTile> path = new List<RoadTile>();
                while (currentTile != startTile)
                {
                    path.Insert(0, currentTile); // Insertamos al principio para invertir el camino
                    currentTile = cameFrom[currentTile];
                }
                path.Insert(0, startTile); // Insertamos el punto de inicio
                HighlightPath(path); // Iluminamos las tiles del camino
                Debug.Log(path);
                return path;
            }

            // Recorremos los vecinos de los tiles
            foreach (RoadTile neighbor in currentTile.neighbors)
            {
                if (!cameFrom.ContainsKey(neighbor)) // Si no hemos visitado este tile
                {
                    frontier.Enqueue(neighbor);
                    cameFrom[neighbor] = currentTile;
                }
            }
        }
        Debug.Log("¡Hola, consola!");
        return null; // Si no hay camino
    }


    //public RoadTile GetRoadTileAtPosition(Vector3 position)
    //{
    //    RoadTile closestTile = null;
    //    float closestDistance = Mathf.Infinity;

    //    // Iteramos sobre todos los RoadTiles
    //    foreach (RoadTile roadTile in roadTiles)
    //    {
    //        // Obtenemos la posición central del RoadTile (su transform.position)
    //        Vector3 tilePosition = roadTile.transform.position;

    //        // Calculamos la distancia entre la posición dada y la posición de la RoadTile
    //        float distance = Vector3.Distance(position, tilePosition);

    //        // Si encontramos una RoadTile más cercana, la actualizamos como la más cercana
    //        if (distance < closestDistance)
    //        {
    //            closestTile = roadTile;
    //            closestDistance = distance;
    //        }
    //    }

    //    return closestTile;
    //}

    public RoadTile GetRoadTileAtPosition(Vector3 position)
    {
        RoadTile closestTile = null;
        float closestDistance = Mathf.Infinity;

        foreach (RoadTile roadTile in roadTiles)
        {
            // Verificar si el roadTile tiene un collider inicializado
            if (roadTile.GetMeshCollider() != null && roadTile.GetMeshCollider().bounds.Contains(position))
            {
                Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
                return roadTile; // Devuelve directamente si la posición está dentro del bounds del collider
            }

            // Si no está en los bounds, calcular distancia como respaldo
            float distance = Vector3.Distance(position, roadTile.transform.position);
            if (distance < closestDistance)
            {
                closestTile = roadTile;
                closestDistance = distance;
            }
        }
        

        return closestTile; // Devuelve el más cercano si no se encuentra uno en los bounds
    }

    //public RoadTile GetRoadTileAtPosition(Vector3 position)
    //{
    //    Ray ray = new Ray(position + Vector3.up * 10f, Vector3.down); // Rayo desde arriba hacia abajo
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    //    {
    //        RoadTile roadTile = hit.collider.GetComponentInParent<RoadTile>();
    //        if (roadTile != null)
    //        {
    //            return roadTile;
    //        }
    //        else
    //        {
    //            //Debug.LogWarning($"El objeto impactado ({hit.collider.name}) no tiene un RoadTile asociado.");
    //        }
    //    }
    //    else
    //    {
    //        //Debug.LogWarning($"No se detectó ningún collider en la posición: {position}");
    //    }

    //    return null; // Si no se encuentra un RoadTile, retorna null
    //}

    public void HighlightPath(List<RoadTile> path)
    {
        foreach (RoadTile roadTile in path)
        {
            roadTile.HighlightTile();
        }
    }

}