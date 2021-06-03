using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingGameBetterScoreIncreaseObstaclesBehavior : DodgingGameObstaclesBehavior
{
    public override void PlayerCollisionEffect(DodgingGamePlayerBehavior player)
    {
        DodgingGameBehavior.instance.IncreaseScore(GMGlobalNumericVariables.gnv.DATE_GAME_BETTER_SCORE_OBSTACLE_VALUE);
    }
}
