using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingGameBasicObstaclesBehavior : DodgingGameObstaclesBehavior
{
    public override void PlayerCollisionEffect(DodgingGamePlayerBehavior player)
    {
        DodgingGameBehavior.instance.DamagingCollision(1);
    }
}

