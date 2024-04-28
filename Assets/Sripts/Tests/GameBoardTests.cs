using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class GameBoardTests
{
    private BoardManager _boardManager;
    private List<int> _tilesList;
    private GameManager _gameManager;

    [SetUp]
    public void SetUp()
    {
        var boardManagerObject = new GameObject();
        _boardManager = boardManagerObject.AddComponent<BoardManager>();
        _boardManager.tilePrefab = SetUpTilePrefab();
        _gameManager = new GameObject().AddComponent<GameManager>();
        _tilesList = _gameManager.GenerateInitialTiles();
    }

    private GameObject SetUpTilePrefab()
    {
        var tilePrefab = new GameObject().AddComponent<Tile>();
        tilePrefab.numberText = tilePrefab.gameObject.AddComponent<Text>();
        return tilePrefab.gameObject;
    }

    [Test]
    public void TestIsCorrectTilePosition()
    {
        //Arrange
        
        //Act
        var result = _boardManager.IsCorrectTilePosition(0, 0);
        
        //Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void TestForEachCell()
    {
        //Arrange
        _boardManager.SetupBoard(_tilesList, _gameManager);
        
        //Act
        var result = _boardManager.ForEachCell((i, j) =>  _boardManager.CreateTile(i, j));
        
        //Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void TestSwapTiles()
    {
        //Arrange
        _boardManager.gameManager = _gameManager;
        _boardManager.tiles = new List<int>{0, 1};
        _boardManager.CreateTile(0, 0);
        _boardManager.CreateTile(0, 1);
        var first = _boardManager.TileDict[0]._position;
        var second = _boardManager.TileDict[1]._position;
        
        //Act
        _boardManager.SwapTiles(1);
        
        //Assert
        Assert.IsTrue(first == _boardManager.TileDict[1]._position && second == _boardManager.TileDict[0]._position);
    }

    [Test]
    public void TestIsWin()
    {
        //Arrange
        _tilesList.Sort();
        _boardManager.SetupBoard(_tilesList, _gameManager);
        
        //Act
        var result = _boardManager.IsWin();
        
        //Assert
        Assert.IsTrue(result);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_boardManager.gameObject);
    }
}
