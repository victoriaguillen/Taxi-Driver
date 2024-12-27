using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Weakener : Obstacle
{
    private const string typeOfVehicle = "Weakener";
    private const int negLivePoints = 0;
    private const double multiplyFactor = 0.5;
    private const int secAcffectedHighSpeed = 30;

    public Weakener() : base(typeOfVehicle, negLivePoints, multiplyFactor, secAcffectedHighSpeed)
    {
    }
}