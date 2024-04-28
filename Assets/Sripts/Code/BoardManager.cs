using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BoardManager : MonoBehaviour
{
    public GameObject tilePrefab;
    public float tileSize = 100.0f;
    public Vector2 leftUpperCorner = new Vector2(-150, 155);

    public readonly Dictionary<int, Tile> TileDict = new();
    private int _zeroIndex;
    public GameManager gameManager;
    public List<int> tiles;

    public void SetupBoard(List<int> tiles, GameManager gameManager)
    {
        this.tiles = tiles;
        this.gameManager = gameManager;
        SetBoardClear();
        TileDict.Clear();
        ForEachCell(CreateTile);
    }
    
    public void SwapTiles(int index)
    {
        var distance = TileDict[index]._position - TileDict[_zeroIndex]._position;
        var canMove = Math.Abs(Mathf.Abs(distance.x) + Mathf.Abs(distance.y) - 1) < Single.Epsilon;
        
        if(canMove == false) return;
        
        
        //set real pos
        var temp = TileDict[index].gameObject.transform.position;
        TileDict[index].gameObject.transform.SetPositionAndRotation(TileDict[_zeroIndex].gameObject.transform.position, Quaternion.identity);
        TileDict[_zeroIndex].transform.SetPositionAndRotation(temp, Quaternion.identity);
        
        //set data pos
        temp = TileDict[index]._position;
        TileDict[index]._position = TileDict[_zeroIndex]._position;
        TileDict[_zeroIndex]._position = temp;
    }

    public bool IsWin()
    {
        return ForEachCell(IsCorrectTilePosition);
    }

    private void SetBoardClear()
    {
        for (var i = tiles.Count - 1; i >= 0; i--) {
            if(!TileDict.ContainsKey(i)) return;
            Destroy(TileDict[i].gameObject);
        }
    }

    public bool ForEachCell(Func<int, int, bool> action)
    {
        for (int i = 0;i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                if (!action(i, j)) {
                    return false;
                }
            }
        }
        return true;
    }

    public bool CreateTile(int i, int j)
    {
        var tileIndex = i * 4 + j;
        var position = new Vector2(j * tileSize + leftUpperCorner.x, i * -tileSize + leftUpperCorner.y);

        var tileGO = Instantiate(tilePrefab, transform);
        tileGO.transform.localPosition = position;

        var tile = tileGO.GetComponent<Tile>();
        TileDict.TryAdd(tileIndex, tile);

        if (tiles[tileIndex] == 0) {
            _zeroIndex = tileIndex;
        }

        if (tile != null) {
            tile.Init(tiles[tileIndex], tileIndex, new Vector2(j, i), gameManager);
        } else {
            return false;
        }

        return true;
    }

    public bool IsCorrectTilePosition(int i, int j)
    {
        var tileIndex = i * 4 + j;
        if (tileIndex == _zeroIndex) return true;
        if (_zeroIndex == 0 && tileIndex == 1) return tiles[tileIndex] == 1;
        if (tileIndex == 0) return tiles[tileIndex] == 1;
        if (tileIndex >= tiles.Count) return tiles[tileIndex] == 15;
        
        var previewIndex = tileIndex - 1 == _zeroIndex ? 2 : 1;
        return tiles[tileIndex] - 1 == tiles[tileIndex - previewIndex];
    }
}