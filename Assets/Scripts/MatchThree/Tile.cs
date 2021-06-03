using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile
{
    public int x;
    public int y;

    public Tile upNeighbour;
    public Tile rightNeighbour;
    public Tile leftNeighbour;
    public Tile downNeighbour;

    public TileType tileType;

    //public Button tileButton;

    public Tile(int x, int y, Tile upNeighbour, Tile rightNeighbour, 
        Tile leftNeighbour, Tile downNeighbour, TileType tileType)
    {
        this.x = x;
        this.y = y;
        this.upNeighbour = upNeighbour;
        this.rightNeighbour = rightNeighbour;
        this.leftNeighbour = leftNeighbour;
        this.downNeighbour = downNeighbour;
        this.tileType = tileType;
    }

    public void SwitchWithTile(Tile otherTile)
    {
        if (otherTile.x == x || otherTile.y == y)
        {
            TileType aux = otherTile.tileType;

            otherTile.tileType = tileType;
            tileType = aux;
        }
    }

    public void RefreshTile()
    {
        //tileButton.GetComponentInChildren<Text>().text = tileType.ToString();
    }

    public int CheckNeighbours(TileType tileType, TileDirection direction)
    {
        if(tileType != this.tileType)
            return 0;
        else
        {
            Tile neighbour = GetNeighbour(direction);
            if (neighbour != null)
            {
                return 1 + neighbour.CheckNeighbours(tileType, direction);
            }
            else
            {
                return 1;
            }
        }
    }

    private Tile GetNeighbour(TileDirection direction)
    {
        switch(direction){
            case TileDirection.UP: return upNeighbour;
            case TileDirection.RIGHT: return rightNeighbour;
            case TileDirection.DOWN: return downNeighbour;
            case TileDirection.LEFT: return leftNeighbour;
        }
        return null;
    }

}
