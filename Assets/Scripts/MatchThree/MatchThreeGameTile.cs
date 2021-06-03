using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatchThreeGameTile : MonoBehaviour
{ 
    public int x;
    public int y;

    public bool isSelected = false;

    public SpriteRenderer spriteRenderer;

    private void OnMouseDown()
    {
        if (MatchThreeBehavior.matchThreeBehavior.isShifting <= 0)
        {
            if (isSelected)
            {
                isSelected = false;
                MatchThreeBehavior.selected = null;
                spriteRenderer.color = Color.white;
            }
            else
            {
                if (MatchThreeBehavior.selected != null)
                {
                    SwapSprites();
                    isSelected = false;
                    spriteRenderer.color = Color.white;
                }
                else
                {
                    isSelected = true;
                    MatchThreeBehavior.selected = this;
                    spriteRenderer.color = Color.red;
                }
            }
        }
    }

    public void SwapSprites()
    {
        if (MatchThreeBehavior.selected.x == x || MatchThreeBehavior.selected.y == y)
        {
            Sprite aux = spriteRenderer.sprite;
            spriteRenderer.sprite = MatchThreeBehavior.selected.spriteRenderer.sprite;
            MatchThreeBehavior.selected.spriteRenderer.sprite = aux;

            DeleteMatches();
            MatchThreeBehavior.selected.DeleteMatches();

            MatchThreeBehavior.selected.isSelected = false;
            MatchThreeBehavior.selected.spriteRenderer.color = Color.white;
            MatchThreeBehavior.selected = null;
        }

        MatchThreeBehavior.matchThreeBehavior.ShiftTiles();

    }

    public bool DeleteMatches()
    {
        bool hasDeleted = false;
        if (spriteRenderer.sprite != null)
        {
            List<GameObject> matchesUp = FindMatch(Vector2.up);
            List<GameObject> matchesDown = FindMatch(Vector2.down);

            List<GameObject> matchesLeft = FindMatch(Vector2.left);
            List<GameObject> matchesRight = FindMatch(Vector2.right);

            if (matchesUp.Count + matchesDown.Count > 1)
            {
                hasDeleted = true;
                List<GameObject> matches = new List<GameObject>(matchesUp);
                matches.AddRange(matchesDown);
                for (int i = 0; i < matches.Count; i++)
                {
                    spriteRenderer.sprite = null;
                    matches[i].GetComponent<SpriteRenderer>().sprite = null;
                }
            }
            if (matchesLeft.Count + matchesRight.Count > 1)
            {
                hasDeleted = true;
                List<GameObject> matches = new List<GameObject>(matchesLeft);
                matches.AddRange(matchesRight);
                for (int i = 0; i < matches.Count; i++)
                {
                    spriteRenderer.sprite = null;
                    matches[i].GetComponent<SpriteRenderer>().sprite = null;
                }
            }
        }
        return hasDeleted;
    }

    private List<GameObject> FindMatch(Vector2 castDir)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        //We change the layer to ensure that the raycast does not hit the object itself, avoiding an infinite loop
        //gameObject.layer = 2;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir);
        
        while (hit.collider != null 
            && hit.collider.GetComponent<SpriteRenderer>().sprite == spriteRenderer.sprite)
        {
            GameObject prevCollider = hit.collider.gameObject;
            //We change the layer to ensure that the raycast does not it the object itself, avoiding an infinite loop
            //prevCollider.layer = 2;
            matchingTiles.Add(prevCollider);
            hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
            //prevCollider.layer = 0;
            
        }
        //gameObject.layer = 0;
        
        return matchingTiles; 
    }

}
