using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MatchThreeBehavior : MonoBehaviour
{
    public GameObject board;

    public GameObject tilePrefab;

    private int firstClickedX;
    private int firstClickedY;

    private GameObject firstClicked;
    private GameObject secondClicked;

    private bool isButtonClicked = false;

    //private GameObject[][] boardData = null;
    private bool isBoardCreated = false;

    public static GameObject[,] tiles;

    public int sizeX = 8;
    public int sizeY = 8;

    public float startX;
    public float startY;

    public Sprite[] tileSprites;

    public static MatchThreeGameTile selected = null;

    public static MatchThreeBehavior matchThreeBehavior;

    //private bool tilesShifted = false;

    public int isShifting = 0;

    private void CreateBoard()
    {
        matchThreeBehavior = this;
        if (!isBoardCreated)
        {
            /*
            boardData = new GameObject[8][];
            boardData[0] = new GameObject[8];
            boardData[1] = new GameObject[8];
            boardData[2] = new GameObject[8];
            boardData[3] = new GameObject[8];
            boardData[4] = new GameObject[8];
            boardData[5] = new GameObject[8];
            boardData[6] = new GameObject[8];
            boardData[7] = new GameObject[8];
            */

            tiles = new GameObject[sizeX , sizeY];

            Vector2 offset = tilePrefab.GetComponent<SpriteRenderer>().bounds.size;

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    int auxX = x;
                    int auxY = y;

                    /*
                    tiles[auxX,auxY] = new MatchThreeGameTile(auxX, auxY,
    auxY > 0 ? tiles[auxX,auxY - 1] : null, auxX < sizeX - 1 ? tiles[auxX + 1,auxY] : null,
    auxY < sizeY - 1 ? tiles[auxX,auxY + 1] : null, auxX > 0 ? tiles[auxX - 1,auxY] : null,
    (TileType)Random.Range(0, 6));
    */
                    GameObject instance = Instantiate(tilePrefab, 
                        new Vector3(startX + x*offset.x,startY + y*offset.y,0), Quaternion.identity);



                    List<Sprite> possibleSprites = new List<Sprite>(tileSprites);

                    if(x > 0)
                    {
                        possibleSprites.Remove(tiles[x-1,y].GetComponent<SpriteRenderer>().sprite);
                    }
                    if (y > 0)
                    {
                        possibleSprites.Remove(tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite);
                    }

                    int tileType = Random.Range(0, possibleSprites.Count);

                    MatchThreeGameTile gameTile = instance.GetComponent<MatchThreeGameTile>();

                    instance.GetComponent<SpriteRenderer>().sprite = possibleSprites[tileType];
                    gameTile.x = x;
                    gameTile.y = y;

                    /*
                instance.GetComponent<MatchThreeGameTile>().tile = new Tile(auxX, auxY,
                    auxY > 0 ? tiles[auxX, auxY - 1].GetComponent<MatchThreeGameTile>().tile : null,
                    auxX < sizeX - 1 ? tiles[auxX + 1, auxY].GetComponent<MatchThreeGameTile>().tile : null,
                    auxY < sizeY - 1 ? tiles[auxX, auxY + 1].GetComponent<MatchThreeGameTile>().tile : null,
                    auxX > 0 ? tiles[auxX - 1, auxY].GetComponent<MatchThreeGameTile>().tile : null,
                    (TileType)tileType);

                    */
                    tiles[x, y] = instance;
                    //instance.GetComponent<MatchThreeGameTile>() = tiles[auxX][auxY];

                    /*
                    Button b = instance.GetComponentInChildren<Button>();
                    b.onClick.AddListener(delegate { OnButtonClick(auxX, auxY); });
                    




                    tiles[auxX][auxY].tileButton.GetComponentInChildren<Text>().text = tiles[auxX][auxY].tileType.ToString();
                    */
                    //tiles[auxX][auxY].tileButton.transform.SetParent(board.transform, false);

                    isBoardCreated = true;
                }
            }
        }
        else
        {
            //DestroyAllChildren(board.transform);
            //board.transform.DetachChildren();
            for(int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    //tiles[x][y].RefreshTile();
                }
            }
        }
        
    }


    private void DestroyAllChildren(Transform t)
    {
        for(int i = 0; i < t.childCount; i++)
        {
            //Debug.Log(t.GetChild(i).name);
            Destroy(t.GetChild(i).gameObject);
        }
    }

    private void OnButtonClick(int x, int y)
    {
        if (isButtonClicked)
        {
            //tiles[x][y].SwitchWithTile(tiles[firstClickedX][firstClickedY]);
            isButtonClicked = false;
            CreateBoard();
            //CheckDestruction();
        }
        else
        {
            firstClickedX = x;
            firstClickedY = y;
            isButtonClicked = true;
        }
    }

    public static List<MatchThreeGameTile> SearchNullTile()
    {
        List<MatchThreeGameTile> result = new List<MatchThreeGameTile>();
        foreach (GameObject go in tiles)
        {
            if(go.GetComponent<SpriteRenderer>().sprite == null)
            {
                result.Add(go.GetComponent<MatchThreeGameTile>());
            }
        }

            return result;
    }

    public static void RefillBoard()
    {
        for (int x = 0; x < matchThreeBehavior.sizeX; x++)
        {
            for (int y = 0; y < matchThreeBehavior.sizeY; y++)
            {
                if (tiles[x, y].GetComponent<SpriteRenderer>().sprite == null)
                {
                    List<Sprite> possibleSprites = new List<Sprite>(matchThreeBehavior.tileSprites);
                    if (x > 0)
                    {
                        possibleSprites.Remove(tiles[x - 1, y].GetComponent<SpriteRenderer>().sprite);
                    }
                    if (x < matchThreeBehavior.sizeX - 1)
                    {
                        possibleSprites.Remove(tiles[x + 1, y].GetComponent<SpriteRenderer>().sprite);
                    }
                    if (y > 0)
                    {
                        possibleSprites.Remove(tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite);
                    }

                    int tileType = Random.Range(0, possibleSprites.Count);
                    Sprite test = possibleSprites[tileType];
                    tiles[x, y].GetComponent<SpriteRenderer>().sprite = possibleSprites[tileType];
                }
            }
        }
    }

    private IEnumerator ShiftDown(float delay, int numberOfTiles, MatchThreeGameTile tileToShift)
    {
        isShifting++;
        for(int i = 0; i < numberOfTiles; i++)
        {
            tiles[tileToShift.x, tileToShift.y - 1].GetComponent<SpriteRenderer>().sprite = tileToShift.spriteRenderer.sprite;
            tileToShift.spriteRenderer.sprite = null;
            tileToShift = tiles[tileToShift.x, tileToShift.y - 1].GetComponent<MatchThreeGameTile>();
            yield return new WaitForSeconds(delay);
        }
        //tileToShift.DeleteMatches();
        isShifting--;
    }
    
    private IEnumerator WaitForShiftingFinished()
    {
        bool hasDeleted = false;
        do
        {
            //Debug.Log("Pouet");
            for (int x = 0; x < matchThreeBehavior.sizeX; x++)
            {
                ShiftColumn(x);
            }

            yield return new WaitUntil(delegate { return isShifting <= 0; });

            hasDeleted = false;
            for (int x = 0; x < sizeX; x++)
            {
                Debug.Log(hasDeleted);
                for (int y = 0; y < sizeY; y++)
                {
                    if (tiles[x, y].GetComponent<MatchThreeGameTile>().DeleteMatches())
                    {
                        hasDeleted = true;
                    }
                }
            }
        }
        while (hasDeleted);

        RefillBoard();
    }

    private void ShiftColumn(int column)
    {
        int numberOfTiles = 0;
        for(int y = 0; y < sizeY; y++)
        {
            if(tiles[column,y].GetComponent<SpriteRenderer>().sprite == null)
            {
                //tilesShifted = true;
                numberOfTiles++;
            }
            else if(numberOfTiles > 0)
            {
                StartCoroutine(ShiftDown(0.3f, numberOfTiles, tiles[column, y].GetComponent<MatchThreeGameTile>()));
            }
        }
    }
    

    public void ShiftTiles()
    {
        StartCoroutine(WaitForShiftingFinished());
        //for(int x = 0; x < matchThreeBehavior.sizeX; x++)
        //{
        //for (int y = 0; y < matchThreeBehavior.sizeY; y++)
        //{
        /*
            MatchThreeGameTile tile = tiles[x,y].GetComponent<MatchThreeGameTile>();
            if (tile.spriteRenderer.sprite == null)
            {
                tilesShifted = true;
                MatchThreeGameTile currentTile = tile;
                int numberOftiles = 0;
                while (currentTile.spriteRenderer.sprite == null && currentTile.y < matchThreeBehavior.sizeY - 1)
                {
                    numberOftiles++;
                    currentTile = tiles[currentTile.x, currentTile.y + 1].GetComponent<MatchThreeGameTile>();
                }
                */
        //The current tile is the first one with a sprite
        //ShiftColumn(x);
        //currentTile = tiles[currentTile.x, currentTile.y + 1].GetComponent<MatchThreeGameTile>();
        /*
    tile.spriteRenderer.sprite = currentTile.spriteRenderer.sprite;
    currentTile.spriteRenderer.sprite = null;
    */
        /*
        for (int i = 0; i < nullCounter; i++)
        {
            if (tile.y + nullCounter < sizeY)
            {
                currentTile = tiles[tile.x, tile.y + i].GetComponent<MatchThreeGameTile>();
                tiles[tile.x, tile.y + i].GetComponent<SpriteRenderer>().sprite =
                    tiles[tile.x, tile.y + i + nullCounter].GetComponent<SpriteRenderer>().sprite;
                tiles[tile.x, tile.y + i + nullCounter].GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        */

        //}
        //}
        //}
    }

    private void Start()
    {
        CreateBoard();
    }

}
