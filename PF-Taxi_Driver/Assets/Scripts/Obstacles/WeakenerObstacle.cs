using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakener : Obstacle
{
    [SerializeField] private string typeOfObstacle = "Weakener";
    [SerializeField] private int NegLivePoints = 0;
    [SerializeField] private float MultiplyFactor = 0.5f;
    [SerializeField] private int SecAcffectedHighSpeed = 5;

    void Awake()
    {
        Initialize(typeOfObstacle, NegLivePoints, MultiplyFactor, SecAcffectedHighSpeed);
    }
}
