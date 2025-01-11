using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleFactory
{
    public abstract GameObject CreateObstacle();
}


//class FenceFactory : ObstacleFactory
//{
//    public override Obstacle CreateObstacle()
//    {
//        return new Fence();
//    }
//}

//class WeakenerFactory : ObstacleFactory
//{
//    public override Obstacle CreateObstacle()
//    {
//        return new Weakener();
//    }
//}
