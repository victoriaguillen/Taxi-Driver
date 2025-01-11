using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaxiLifeController : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public float damagePerCollision = 10f;
    private Slider healthBar;
    private Text lifeText;
    public string groundTag = "Road";

    void Start()
    {
        currentHealth = maxHealth;

        healthBar = GetComponentInChildren<Slider>();
        lifeText = GetComponentInChildren<Text>();

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (lifeText != null)
        {
            UpdateLifeText();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(groundTag))
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

        // Actualizar el texto del Slider
        if (lifeText != null)
        {
            UpdateLifeText();
        }

        // Si la vida llega a 0, destruir el objeto Taxi
        if (currentHealth <= 0)
        {
            Debug.Log("El Taxi ha sido destruido.");
            Destroy(gameObject);
        }
    }

    void UpdateLifeText()
    {
        lifeText.text = $"{currentHealth}/{maxHealth}";
    }
}
