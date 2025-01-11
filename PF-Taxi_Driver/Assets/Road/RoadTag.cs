using UnityEngine;

public class AssignTagToChildren : MonoBehaviour
{
    public string tagName = "Road"; // Nombre de la tag que deseas asignar

    void Start()
    {
        // Asignar la tag al GameObject principal
        gameObject.tag = tagName;

        // Asignar la tag a todos los hijos
        foreach (Transform child in transform)
        {
            child.gameObject.tag = tagName;
        }
    }
}
