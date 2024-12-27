using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class SolidObstacle : Obstacle
{
    public SolidObstacle(string typeOfObstacle, int negLivePoints, double multiplyFactor, int secAcffectedHighSpeed)
        : base(typeOfObstacle, negLivePoints, multiplyFactor, secAcffectedHighSpeed)
    {
    }
}
