using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingGameScoreIncreaseObstaclesBehavior : DodgingGameObstaclesBehavior
{
    public override void PlayerCollisionEffect(DodgingGamePlayerBehavior player)
    {
        DodgingGameBehavior.instance.IncreaseScore(1);
    }
}
