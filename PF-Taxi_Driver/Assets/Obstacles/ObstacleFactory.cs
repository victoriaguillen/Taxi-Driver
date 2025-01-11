using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFactory: MonoBehaviour
{
    public GameObject CreateObstacle(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(prefab, position, rotation);
    }
}


//class FenceFactory : ObstacleFactory
//{
//    public override Obstacle BuiltObstacle()
//    {
//        return new Fence();
//    }
//}

//class WeakenerFactory : ObstacleFactory
//{
//    public override Obstacle BuiltObstacle()
//    {
//        return new Weakener();
//    }
//}
