using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgingGamePlayerBehavior : MonoBehaviour
{

    public float speed;

    [Tooltip("The higher this value, the smaller the offset is")]
    public int borderOffsetX = 30;

    [Tooltip("The higher this value, the smaller the offset is")]
    public int borderOffsetY = 60;

    private float previousXPosition = 0;
    private float previousYPosition = 0;

    private bool canMove = true;

    private bool canMoveRight = true;
    private bool canMoveLeft = true;
    private bool canMoveUp = true;
    private bool canMoveDown = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canMove && !DodgingGameBehavior.instance.gameIsOver)
        {
            if (collision.gameObject.tag == "DodgingGameObstacle")
            {
                Destroy(collision.gameObject);
                if (DodgingGameBehavior.instance.GetHealth() <= 0)
                {
                    
                    canMove = false;

                    int pointsEarned;

                    if(DodgingGameBehavior.instance.GetScore() > GMGlobalNumericVariables.gnv.MINIMUM_POINTS_KEPT_PER_DATE)
                    {
                        pointsEarned = GMGlobalNumericVariables.gnv.MINIMUM_POINTS_KEPT_PER_DATE;
                    }
                    else if(DodgingGameBehavior.instance.GetScore() > GMGlobalNumericVariables.gnv.MINIMUM_POINTS_EARNED_PER_DATE)
                    {
                        pointsEarned = DodgingGameBehavior.instance.GetScore();
                    }
                    else
                    {
                        pointsEarned = GMGlobalNumericVariables.gnv.MINIMUM_POINTS_EARNED_PER_DATE;
                    }

                    DodgingGameBehavior.instance.GameOver("Your gaze slipped one too many times. She left the date...\nYou only earned " +
                       pointsEarned + " points.", pointsEarned);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "VisibleZone")
        {
            BoxCollider2D collider = collision as BoxCollider2D;

            if (transform.position.x >= ((collider.bounds.center.x + collider.size.x / 2 - collider.size.x / borderOffsetX) * collision.transform.localScale.x))
                canMoveRight = false;
            else
                canMoveRight = true;

            if (transform.position.x <= (collider.bounds.center.x - collider.size.x / 2 + collider.size.x / borderOffsetX) * collision.transform.localScale.x)
                canMoveLeft = false;
            else
                canMoveLeft = true;

            if (transform.position.y >= (collider.bounds.center.y + collider.size.y / 2 - collider.size.y / borderOffsetY) * collision.transform.localScale.y)
                canMoveUp = false;
            else
                canMoveUp = true;

            if (transform.position.y <= (collider.bounds.center.y - collider.size.y / 2 + collider.size.y / borderOffsetY) * collision.transform.localScale.y)
                canMoveDown = false;
            else
                canMoveDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "VisibleZone")
        {
            BoxCollider2D collider = collision as BoxCollider2D;

            Debug.Log(collider.size.x / 2);

            if (transform.position.x >= ((collider.bounds.center.x + collider.size.x / 2) * collision.transform.localScale.x))
                canMoveRight = false;
            else if (transform.position.x <= (collider.bounds.center.x - collider.size.x / 2) * collision.transform.localScale.x)
                canMoveLeft = false;

            if (transform.position.y >= (collider.bounds.center.y + collider.size.y / 2) * collision.transform.localScale.y)
                canMoveUp = false;
            else if (transform.position.y <= (collider.bounds.center.y - collider.size.y / 2) * collision.transform.localScale.y)
                canMoveDown = false;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove && !DodgingGameBehavior.instance.gameIsOver)
        {
            previousXPosition = transform.position.x;
            previousYPosition = transform.position.y;

            if (Input.GetAxisRaw("Horizontal") < 0 && canMoveLeft)
            {
                    transform.position = new Vector3(transform.position.x - speed,
                        transform.position.y, transform.position.z);
            }
            if (Input.GetAxisRaw("Horizontal") > 0 && canMoveRight)
            {
                transform.position = new Vector3(transform.position.x + speed,
                    transform.position.y, transform.position.z);
            }
            if (Input.GetAxisRaw("Vertical") > 0 && canMoveUp)
            {
                transform.position = new Vector3(transform.position.x,
                    transform.position.y + speed, transform.position.z);
            }
            if (Input.GetAxisRaw("Vertical") < 0 && canMoveDown)
            {
                transform.position = new Vector3(transform.position.x,
                    transform.position.y - speed, transform.position.z);
            }
        }
    }

}
