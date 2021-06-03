using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingGameObstaclesSpawnerBehavior : MonoBehaviour
{

    public ObstacleHorizontalDirection directionX;
    public ObstacleVerticalDirection directionY;

    public void SpawnObstacle(GameObject toSpawn)
    {
        GameObject instance = Instantiate(toSpawn, transform.position, Quaternion.identity);

        DodgingGameObstaclesBehavior instanceBehavior = instance.GetComponent<DodgingGameObstaclesBehavior>();

        instanceBehavior.directionX = directionX;
        instanceBehavior.directionY = directionY;
    }

}
