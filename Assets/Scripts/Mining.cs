using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mining : MonoBehaviour
{
    private Tilemap tilemap;
    private Vector3 mousePosition;
    private float distanceToTile;

    private Vector3Int tilePosition;
    private string tileName;

    private int score = 0;


    [SerializeField] GameObject player;
    [SerializeField] GridLayout grid;

    private class TileRemoveInfo
    {
        public Vector3Int position;
        public int hitsToDestroy;
        public int numGems;

        public TileRemoveInfo(Vector3Int position, int hitsToDestroy, int numGems)
        {
            this.position = position;
            this.hitsToDestroy = hitsToDestroy;
            this.numGems = numGems;
        }

        public void DecrimentHits()
        {
            hitsToDestroy--;
        }
    }

    private List<TileRemoveInfo> HitTiles = new List<TileRemoveInfo>();
    private TileRemoveInfo hitTile;


    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonUp(0))
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            distanceToTile = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.y), new Vector2(mousePosition.x, mousePosition.y));
            tilePosition = grid.WorldToCell(mousePosition);

            if (tilemap.HasTile(tilePosition))
            {
                tileName = tilemap.GetTile(tilePosition).name;
            }
            else
            {
                tileName = "";
            }

            if (distanceToTile <= 3.5 && tileName.Contains("Ore"))
            {
                hitTile = HitTiles.Find(curTile => curTile.position.x == tilePosition.x && curTile.position.y == tilePosition.y);

                if (hitTile == null)
                {
                    int randomNum = Random.Range(1, 4);

                    if (randomNum == 1)
                    {
                        DestroyOre(1);
                    }
                    else
                    {
                        HitTiles.Add(new TileRemoveInfo(tilePosition, randomNum - 1, randomNum));
                    }
                }
                else
                {
                    hitTile.DecrimentHits();

                    if (hitTile.hitsToDestroy <= 0)
                    {
                        DestroyOre(hitTile.numGems);
                    }

                }
            }
        }

    }

    private void DestroyOre(int numGems)
    {
        score += GetScoreByTileName() * numGems;

        tilemap.SetTile(tilePosition, null);

        if (hitTile != null)
        {
            HitTiles.Remove(hitTile);
        }

        Debug.Log(score);
    }

    private int GetScoreByTileName()
    {
        if (tileName.Contains("Diamond"))
        {
            return 500;
        }
        else if (tileName.Contains("Emerald"))
        {
            return 400;
        }
        else if (tileName.Contains("Gold"))
        {
            return 300;
        }
        else if (tileName.Contains("Silver"))
        {
            return 200;
        }
        else if (tileName.Contains("Copper"))
        {
            return 100;
        }
        else
        {
            return 0;
        }
    }
}
