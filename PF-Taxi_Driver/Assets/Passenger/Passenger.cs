using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Passenger : MonoBehaviour
{
    // destino del pasajero
    [SerializeField] private Vector3 destination = new Vector3(0,0,0);

    private RoadTile tile;
    private Vector3 initialPosition;

    public Vector3 Destination
    {
        get { return destination; }
    }
    public RoadTile Tile
    {
        get { return tile; }
        set { tile = value; }
    }


    private Vector3 origin;
    private RoadObject roadObject;
    public bool isActive { get; set; }



    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        initialPosition = transform.position;

        roadObject = FindObjectOfType<RoadObject>();
        tile = roadObject.GetRoadTileAtPosition(initialPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
