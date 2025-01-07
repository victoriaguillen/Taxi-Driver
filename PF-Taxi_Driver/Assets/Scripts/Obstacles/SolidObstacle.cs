using UnityEngine;

public class SolidObstacle : Obstacle
{
    [SerializeField] private int NegLivePoints = 20;
    [SerializeField] private float MultiplyFactor = 0.8f;
    [SerializeField] private int SecAcffectedHighSpeed = 10;

    protected void Awake()
    { 
       string typeOfObstacle = this.GetTypeOfObstacle();
       Initialize(typeOfObstacle, NegLivePoints, MultiplyFactor, SecAcffectedHighSpeed);
    }
}