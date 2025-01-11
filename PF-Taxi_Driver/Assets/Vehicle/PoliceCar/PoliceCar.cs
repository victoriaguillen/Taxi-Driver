using UnityEngine;
using UnityEngine.AI;

public class PoliceCar : Vehicle
{
    private bool isChasing = false;
    private Taxi targetTaxi;
    private NavMeshAgent navMeshAgent; // Referencia al NavMeshAgent

    private void Awake()
    {
        Initialize("Police Car");
        navMeshAgent = GetComponent<NavMeshAgent>(); // Obtener el componente NavMeshAgent
    }

    private void OnEnable()
    {
        SpeedManager.OnSpeedViolationDetected += StartChase;
    }

    private void OnDisable()
    {
        SpeedManager.OnSpeedViolationDetected -= StartChase;
    }

    private void Update()
    {
        if (isChasing && targetTaxi != null)
        {
            ChaseTaxi();
        }
    }

    private void StartChase(Taxi taxi)
    {
        if (!isChasing)
        {
            targetTaxi = taxi;
            isChasing = true;
            Debug.Log($"{GetPlate()} comienza a perseguir al taxi {taxi.GetPlate()}.");

            // Establecer el destino del NavMeshAgent
            navMeshAgent.SetDestination(targetTaxi.transform.position);
        }
    }

    private void ChaseTaxi()
    {
        if (targetTaxi == null) return;

        // Establecer el destino nuevamente si el taxi se mueve
        if (navMeshAgent.destination != targetTaxi.transform.position)
        {
            navMeshAgent.SetDestination(targetTaxi.transform.position);
        }

        // Suavizar la rotación del PoliceCar para que mire en la dirección del movimiento
        Vector3 direction = targetTaxi.transform.position - transform.position;
        if (direction.sqrMagnitude > 0.1f) // Asegurarse de que no esté detenido
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
        }

        // Comprueba si el coche de policía está suficientemente cerca del taxi
        float distanceToTaxi = Vector3.Distance(transform.position, targetTaxi.transform.position);
        if (distanceToTaxi < 2.0f) // Distancia mínima para detener la persecución
        {
            Debug.Log($"{GetPlate()} ha alcanzado al taxi {targetTaxi.GetPlate()}.");
            StopChase();
        }
    }

    private void StopChase()
    {
        isChasing = false;
        targetTaxi = null;
        navMeshAgent.ResetPath(); // Detener el agente de navegación
    }
}
