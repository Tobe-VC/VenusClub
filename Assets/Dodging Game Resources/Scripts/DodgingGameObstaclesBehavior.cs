using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DodgingGameObstaclesBehavior : MonoBehaviour
{
    public float speedX = 0.01f;
    public float speedY = 0.005f;

    [HideInInspector]
    public ObstacleHorizontalDirection directionX; //Either 1 or -1
    [HideInInspector]
    public ObstacleVerticalDirection directionY; //Either 1 or -1

    public SpriteRenderer spriteRenderer;

    

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.clear;
        speedX *= Random.Range(0.5f,1.5f) * GMGlobalNumericVariables.gnv.DATE_GAME_SPEED_MULTIPLIER;
        speedY *= Random.Range(0.5f,1.5f) * GMGlobalNumericVariables.gnv.DATE_GAME_SPEED_MULTIPLIER;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        spriteRenderer.sprite = newSprite;
        collider.size = newSprite.bounds.size;
        */
    }

    private void FixedUpdate()
    {
        if (!DodgingGameBehavior.instance.timeFreeze)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && !DodgingGameBehavior.instance.gameIsOver)
        {
            PlayerCollisionEffect(collision.gameObject.GetComponent<DodgingGamePlayerBehavior>());
        }

        if (collision.CompareTag("VisibleZone"))
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("VisibleZone"))
        {
                Destroy(gameObject);
        }
    }

    public abstract void PlayerCollisionEffect(DodgingGamePlayerBehavior player);

    public virtual void Move()
    {
        transform.position = new Vector3(transform.position.x + (speedX * ((int)directionX)),
            transform.position.y + (speedY *((int)directionY)),
            transform.position.z);
    }
}
