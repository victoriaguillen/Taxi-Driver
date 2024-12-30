using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Passenger : MonoBehaviour
{
    // destino del pasajero
    [SerializeField] private Vector3 destination = new Vector3(0,0,0);

    public Vector3 Destination
    {
        get { return destination; }
    }


    private Vector3 origin;
    public bool isActive { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        isActive = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
