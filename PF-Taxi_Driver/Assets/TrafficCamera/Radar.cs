using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : PoliceCar
{
    [SerializeField] float legalSpeed = 50.0f;
    Taxi objective = null;
    bool notify = false;

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestTaxi();
        if (objective != null)
        {
            Debug.Log("Triggered");
            notify = TriggerRadar(objective);
            if (notify)
            {

            }
        }
        
    }

    void FindClosestTaxi()
    {
        Taxi[] taxis = FindObjectsOfType<Taxi>();
        Taxi closestTaxi = null;
        float maxDistance = Mathf.Infinity;

        foreach (Taxi taxi in taxis)
        {

            float targetDistance = Vector3.Distance(transform.position, taxi.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTaxi = taxi;
                maxDistance = targetDistance;
            }

        }
        if (maxDistance <= Threshold)
        {
            objective = closestTaxi;
        }
    }

    public bool TriggerRadar(Vehicle vehicle)
    {
        float legalSpeed = 0f;
        string plate = vehicle.GetPlate();
        float speed = vehicle.GetSpeed();

        if (speed > legalSpeed) { return true; }
        else { return false; }
    }
}
