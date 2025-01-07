using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class UpdateTaxiSpeed : MonoBehaviour
{
    private Rigidbody taxiRigidbody;
    private CarController carController;
    public TextMeshProUGUI speedText; // Referencia al TextMeshPro en el Canvas

    // Start is called before the first frame update
    void Start()
    {
        // Obt�n la referencia al Rigidbody y al CarController
        taxiRigidbody = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();

        // Aseg�rate de que los componentes necesarios est�n presentes
        if (taxiRigidbody == null || carController == null)
        {
            Debug.LogError("Faltan componentes en el taxi. Aseg�rate de que este script est� en el taxi con los componentes necesarios.");
        }

        // Verifica que se haya asignado el TextMeshProUGUI desde el inspector
        if (speedText == null)
        {
            Debug.LogError("No se asign� un TextMeshProUGUI al script. Arrastra el componente desde el Canvas al inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (taxiRigidbody != null && speedText != null)
        {
            // Calcula la velocidad en km/h
            float speed = taxiRigidbody.velocity.magnitude * 3.6f;

            // Actualiza el texto del TextMeshProUGUI
            speedText.text = Mathf.RoundToInt(speed) + " km/h";
        }
    }
}
