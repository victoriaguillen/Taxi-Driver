using UnityEngine;


public abstract class SolidObstacle : Obstacle
{
    protected void Awake()
    {
        string typeOfObstacle = FindTypeOfObstacle(); // Obtén el tipo de la subclase
        Initialize(typeOfObstacle, 20, 0.8f, 10, 100); // Usa ese tipo para inicializar
    }

    // Método virtual para que las subclases definan su tipo
    protected virtual string FindTypeOfObstacle()
    {
        return "SolidObstacle"; // Tipo predeterminado
    }
}
