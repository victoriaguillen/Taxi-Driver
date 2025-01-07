using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : SolidObstacle
{
    [SerializeField] private string TypeOfObstacle = "Fence";

    protected new void Awake()
    {
        base.Awake();
    }
}
