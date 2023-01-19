using System;
using System.Collections.Generic;
using Sources.Logic.Ball;
using UnityEngine;

namespace Sources.Spawner
{
    public class BallSpawner : Spawner<Ball>
    {
        public Ball GetBall() => GetObject();

        public void DisableBalls()
        {
            DisableAll();
        }
    }
}