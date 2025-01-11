using UnityEngine;

public class Radar : MonoBehaviour
{
    [SerializeField] private float legalSpeed = 50.0f; // Velocidad máxima permitida en km/h
    [SerializeField] private float detectionRadius = 100.0f; // Radio de detección en metros

    private Taxi detectedTaxi;


    public Taxi DetectTaxi()
    {
        Taxi taxi = FindObjectOfType<Taxi>();
        if (taxi != null)
        {
            float distanceToTaxi = Vector3.Distance(transform.position, taxi.transform.position);
            if (distanceToTaxi <= detectionRadius)
            {
                return taxi;
            }
        }

        return null;
    }

    public bool MeasureSpeed(Taxi taxi)
    {
        Rigidbody taxiRigidbody = taxi.GetComponent<Rigidbody>();
        if (taxiRigidbody != null)
        {
            float speed = taxiRigidbody.velocity.magnitude * 3.6f; // Convierte de m/s a km/h
            return speed > legalSpeed;
        }

        Debug.LogWarning("El Taxi no tiene un Rigidbody asignado.");
        return false;
    }
}
