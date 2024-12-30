using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taxi : Vehicle
{
    //constant string as TypeOfVehicle wont change allong PoliceCar instances.
    private bool isCarryingPassengers = false;

    Passenger pickUp = null;

    void Awake()
    {
        // Inicialización en lugar del constructor
        Initialize("Taxi");
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSpeed(45.0f);
    }

    void Update()
    {

        if (!isCarryingPassengers)
        {
            FindClosestPassenger();
            if (pickUp != null)
            {
                Debug.Log("encontrado");
                InitiateJourney();
            }
        }

        else
        {
            CheckFinishedJourney();
        }
    }


    void FindClosestPassenger()
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
    }

    void InitiateJourney()
    {
        Debug.Log("Iniciando");
        isCarryingPassengers = true;
        Debug.Log($"pickUp: {pickUp.name}, ActiveSelf: {pickUp.gameObject.activeSelf}");
        pickUp.gameObject.SetActive(false);
        Debug.Log($"pickUp: {pickUp.name}, ActiveSelf: {pickUp.gameObject.activeSelf}");

        pickUp.isActive = false;

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

        pickUp = null;
    }

}
