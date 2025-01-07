using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : Vehicle
{
    private bool isChasing;
    private bool isPatrolling = false;


    void Awake()
    {
        // Inicialización en lugar del constructor
        Initialize("Police Car");
    }

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
