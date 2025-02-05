using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Taxi : Vehicle
{
    //constant string as TypeOfVehicle wont change allong PoliceCar instances.
    private bool isCarryingPassengers = false;
    private bool isLooking = true;
    private List<RoadTile> path = null; // Ruta a seguir
    private RoadTile tile;

    private Passenger objective;
    private RoadObject roadObject;
    private Bank bank;


    Passenger pickUp = null;

    void Awake()
    {
        bank = FindObjectOfType<Bank>();
        Initialize("Taxi");
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(45.0f);
        roadObject = FindObjectOfType<RoadObject>();

        
    }

    void Update()
    {
        tile = roadObject.GetRoadTileAtPosition(transform.position);
        //tile.HighlightTile();
        if (!isCarryingPassengers)
        {
            objective = FindClosestPassenger();
            if (objective != null && isLooking)
            {
                path = roadObject.FindPath(tile, objective.Tile);
                isLooking = false;
            }
            if (pickUp != null)
            {
                //roadObject.FindPath(tile, pickUp.Tile);
                Debug.Log("encontrado");
                roadObject.UnhighlightAll();
                InitiateJourney();
            }
        }

        else
        {
            CheckFinishedJourney();
        }
        tile.UnhighlightTile();

    }


    Passenger FindClosestPassenger()
    {
        Passenger[] passengers = FindObjectsOfType<Passenger>();
        Passenger closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Passenger passenger in passengers)
        {
            if (passenger.isActive)
            {
                float targetDistance = Vector3.Distance(transform.position, passenger.transform.position);

                if (targetDistance < maxDistance)
                {
                    closestTarget = passenger;
                    maxDistance = targetDistance;
                }
            }

        }
        if (maxDistance <= Threshold)
        {
            pickUp = closestTarget;
        }
        return closestTarget;
    }

    void InitiateJourney()
    {
        path = roadObject.FindPath(roadObject.GetRoadTileAtPosition(transform.position), roadObject.GetRoadTileAtPosition(pickUp.Destination)); // Usar las coordenadas del taxi y el pasajero

        isCarryingPassengers = true;
        pickUp.gameObject.SetActive(false);
        NoticeEvents.RaiseNotice($"Has recogido a un pasajero. ¡Destino: {pickUp.Destination}!");



    }

    void CheckFinishedJourney()
    {
        float destinationDistance = Vector3.Distance(transform.position, pickUp.Destination);
        if (destinationDistance <= Threshold)
        {
            FinishRide();
        }

    }

    void FinishRide()
    {
        pickUp.transform.position = pickUp.Destination;
        Debug.Log("Finalizada");
        pickUp.gameObject.SetActive(true);
        isCarryingPassengers = false;
        isLooking = true;
        pickUp.isActive = false;
        pickUp.SwitchHalo();

        // Se recibe el pago
        bank.Deposit(pickUp.Precio);

        // Mostrar mensaje de llegada
        NoticeEvents.RaiseNotice($"Has dejado al pasajero en su destino. Ganaste ${pickUp.Precio}!");

        // Se reinicia
        pickUp = null;
        roadObject.UnhighlightAll();


    }

}
