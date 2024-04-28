using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Fields and components
    
    [SerializeField]
    private BoardManager boardManager;

    [SerializeField] 
    private GameObject _curtain;

    public Action<int> TrySwapTilesAction;

    #endregion

    #region Public interface
    
    public void TrySwapTiles(int index)
    {
        boardManager.SwapTiles(index);
        TrySwapTilesAction?.Invoke(index);
        if (!boardManager.IsWin()) return;
        SetCurtain(true);
    }

    public void OnButtonClick()
    {
        StartNewGame();
    }

    #endregion

    #region Unity Handlers

    private void Start()
    {
        if (boardManager == null)
        {
            Debug.LogError("BoardManager is not set in GameManager.");
            return;
        }
        SetCurtain(true);
    }

    #endregion

    #region Internal logic
    
    private void StartNewGame()
    {
        var tilesList = GenerateInitialTiles();
        while (!IsSolvable(tilesList))
        {
            tilesList = GenerateInitialTiles();
        }
        boardManager.SetupBoard(tilesList, this);
        
        SetCurtain(false);
    }

    public List<int> GenerateInitialTiles()
    {
        var tilesList = new List<int>();
        for (int i = 0; i < 16; i++)
        {
            tilesList.Add(i);
        }

        var n = tilesList.Count;
        while (n > 1)
        {
            n--;
            var k = Random.Range(0, n + 1);
            (tilesList[k], tilesList[n]) = (tilesList[n], tilesList[k]);
        }

        return tilesList;
    }

    public bool IsSolvable(List<int> tiles)
    {
        var inversionCount = 0;
        for (int i = 0; i < tiles.Count; i++)
        {
            for (int j = i + 1; j < tiles.Count; j++)
            {
                if (tiles[i] > tiles[j] && tiles[i] != 0 && tiles[j] != 0)
                {
                    inversionCount++;
                }
            }
        }
        return inversionCount % 2 == 0;
    }
    
    private void SetCurtain(bool value)
    {
        if (_curtain == null) return;
        if (value) {
            _curtain.SetActive(true);
            _curtain.transform.SetAsLastSibling();
        } else {
            _curtain.SetActive(false);
        }
    }
    #endregion
}
