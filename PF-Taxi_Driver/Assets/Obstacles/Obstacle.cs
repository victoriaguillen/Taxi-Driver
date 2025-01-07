using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    private string typeOfObstacle;
    private int negLivePoints;
    private float multiplyFactor;
    private int secAcffectedHighSpeed;

    // Inicializa las variables
    public void Initialize(string typeOfObstacle, int negLivePoints, float multiplyFactor, int secAcffectedHighSpeed)
    {
        this.typeOfObstacle = typeOfObstacle;
        this.negLivePoints = negLivePoints;
        this.multiplyFactor = multiplyFactor;
        this.secAcffectedHighSpeed = secAcffectedHighSpeed;
    }

    // Métodos getter
    public int GetTime() => secAcffectedHighSpeed;
    public int GetLivePoints() => negLivePoints;
    public float GetMultiplyFactor() => multiplyFactor;
    public string GetTypeOfObstacle() => typeOfObstacle;
}