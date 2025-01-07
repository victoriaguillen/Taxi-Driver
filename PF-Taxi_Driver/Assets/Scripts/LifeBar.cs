using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TaxiLifeController : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100f;
    private float currentHealth;
    public float damagePerCollision = 10f;
    private Slider healthBar;
    private Text lifeText;

    void Start()
    {

        currentHealth = maxHealth;

        healthBar = GetComponentInChildren<Slider>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        GameObject collidedObject = collision.gameObject;

        //comparo etiquetas para saber si se trata de un obstaculo
        if (collidedObject.CompareTag("Obstacle"))
        {
            Debug.Log("entre");
            Obstacle obstacle = collision.gameObject.GetComponent<Obstacle>();
            SpeedControler speedControler = GetComponent<SpeedControler>();

            float damage = obstacle.GetLivePoints();
            float reduction = obstacle.GetMultiplyFactor();
            float time = obstacle.GetTime();

            TakeDamage(damage);
            speedControler.SetReductionTime(time);
            speedControler.ReduceSpeed(reduction);
        }
        else
        {
           TakeDamage(damagePerCollision);
        }    
    }

    void TakeDamage(float damage)
    {
        currentHealth -= damage;

        // Asegurarse de que la salud no baje de 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Actualizar la barra de salud si está asignada
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Si la vida llega a 0, destruir el objeto Taxi
        if (currentHealth <= 0)
        {
            Debug.Log("El Taxi ha sido destruido.");
            Destroy(gameObject);
        }
    }
}
