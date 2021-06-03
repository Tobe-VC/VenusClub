using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingGameNextPortraitObstaclesBehavior : DodgingGameObstaclesBehavior
{

    public override void PlayerCollisionEffect(DodgingGamePlayerBehavior player)
    {
        DodgingGameBehavior.instance.NextPortrait();
    }
}
