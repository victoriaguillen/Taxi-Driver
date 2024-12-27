using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle
{
    //atributos
    private string typeOfObstacle;
    private int negLivePoints;
    private double multiplyFactor;
    private int secAcffectedHighSpeed;

    //constructor
    public Obstacle(string typeOfObstacle, int negLivePoints, double multiplyFactor, int secAcffectedHighSpeed)
    {
        this.typeOfObstacle = typeOfObstacle;
        this.negLivePoints = negLivePoints;
        this.multiplyFactor = multiplyFactor;
        this.secAcffectedHighSpeed = secAcffectedHighSpeed;
    }

    public int GetTime()
    {
        return secAcffectedHighSpeed;
    }

    public int GetLivePoints()
    {
        return negLivePoints;
    }

    public double GetMultiplyFactor()
    {
        return multiplyFactor;
    }

    public string GetTypeOfObstacle()
    {
        return typeOfObstacle;
    }

}