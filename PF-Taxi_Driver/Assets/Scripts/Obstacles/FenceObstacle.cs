using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Fence : SolidObstacle
{
    private const string typeOfVehicle = "Fence";
    private const int negLivePoints = 10;
    private const double multiplyFactor = 0.8;
    private const int secAcffectedHighSpeed = 1;

    public Fence() : base(typeOfVehicle, negLivePoints, multiplyFactor, secAcffectedHighSpeed)
    {
    }
}
