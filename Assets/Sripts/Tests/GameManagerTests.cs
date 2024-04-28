using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class GameManagerTests
{
    private GameManager gameManager;
    
        [SetUp]
        public void SetUp()
        {
            GameObject gameManagerObject = new GameObject();
            gameManager = gameManagerObject.AddComponent<GameManager>();
        }
    
        [Test]
        public void TestGenerateInitialTiles()
        {
            //Arrange
            var sampleCollection = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15};
            
            //Act
            var tiles = gameManager.GenerateInitialTiles();
            
            //Assert
            Assert.AreEqual(16, tiles.Count);
            CollectionAssert.AllItemsAreUnique(tiles);
            CollectionAssert.AreNotEqual(sampleCollection, tiles);
        }
    
        [Test]
        public void TestIsSolvable()
        {
            //Arrange
            var correctList = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0};
            var incorrectLis = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 15, 14, 0};

            //Act
            var solvableResult = gameManager.IsSolvable(correctList);
            var insolvableResult = gameManager.IsSolvable(incorrectLis);
            
            //Assert
            Assert.IsTrue(solvableResult);
            Assert.IsFalse(insolvableResult);
        }
        
    
        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(gameManager.gameObject);
        }
    
}
