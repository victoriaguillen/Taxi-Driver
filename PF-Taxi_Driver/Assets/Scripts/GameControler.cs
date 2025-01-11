using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] int fineAmount = 200; // Monto de la multa
    [SerializeField] Bank bank;
    [SerializeField] Taxi taxi;
    [SerializeField] PoliceCar policeCar;
    TaxiLifeController taxiLifeController;

    private void Awake()
    {
        // Suscribirse al evento OnTaxiCaught
        policeCar.OnTaxiCaught += HandlePoliceCatch;
        taxiLifeController = taxi.GetComponent<TaxiLifeController>();
    }
   

    private void OnDestroy()
    {
        // Cancelar la suscripci�n al evento cuando el objeto se destruye
        policeCar.OnTaxiCaught -= HandlePoliceCatch;
    }

    public void HandlePoliceCatch(Taxi taxi)
    {
        Debug.Log("ACTION");
        if (bank.CurrentBalance >= fineAmount)
        {
            // El taxi tiene suficiente dinero para pagar la multa
            bank.Withdraw(fineAmount);
            Debug.Log("El Taxi pag� la multa al coche de polic�a. El juego contin�a.");
        }
        else
        {
            // El taxi no tiene suficiente dinero, termina el juego
            Debug.Log("El Taxi no tiene suficiente dinero para pagar la multa. Fin del juego.");
            EndGame();
        }
    }

    private void Update()
    {
        // Verifica si el taxi se queda sin vida
        if (taxiLifeController != null && taxiLifeController.GetCurrentHealth() <= 0)
        {
            Debug.Log("El Taxi se ha quedado sin vida. Fin del juego.");
            EndGame();
        }
    }

 

    private void EndGame()
    {
        // Opcional: Mostrar mensaje de fin del juego en pantalla
        Debug.Log("Recargando escena...");

        // Reinicia la escena actual
        ReloadScene();
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
