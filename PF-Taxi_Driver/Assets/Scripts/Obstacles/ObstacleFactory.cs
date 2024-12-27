using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleFactory
{
    public abstract Obstacle BuiltObstacle();
}


class FenceFactory : ObstacleFactory
{
    public override Obstacle BuiltObstacle()
    {
        return new Fence();
    }
}

class WeakenerFactory : ObstacleFactory
{
    public override Obstacle BuiltObstacle()
    {
        return new Weakener();
    }
}
