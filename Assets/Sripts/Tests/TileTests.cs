using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;


public class TileTests
{
    private Tile _tile;
    private GameManager _gameManagerMock;
    private Text _text;

    [SetUp]
    public void SetUp()
    {
        var tileObject = new GameObject();
        _tile = tileObject.AddComponent<Tile>();
        _text = tileObject.AddComponent<Text>();
        _gameManagerMock = tileObject.AddComponent<GameManager>();
    }

    [Test]
    public void TestInit()
    {
        //Arrange
        
        //Act
        _tile.Init(5, 0, new Vector2(1, 2), _gameManagerMock);
        
        //Assert
        Assert.AreEqual(5.ToString(), _tile.numberText.text);
        Assert.AreEqual(new Vector2(1, 2), _tile._position);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(_tile.gameObject);
    }
}
