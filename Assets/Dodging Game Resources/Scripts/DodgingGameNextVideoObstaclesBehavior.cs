using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingGameNextVideoObstaclesBehavior : DodgingGameObstaclesBehavior
{

    public override void PlayerCollisionEffect(DodgingGamePlayerBehavior player)
    {
        DodgingGameBehavior.instance.VideoTokenCollision();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("VisibleZone"))
        {
            Destroy(gameObject);
            DodgingGameBehavior.instance.ResetCurrentScoreStreak();
            DodgingGameBehavior.instance.videoObstacleIsPresent = false;
        }
    }
}
