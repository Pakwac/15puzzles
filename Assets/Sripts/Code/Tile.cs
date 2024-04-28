using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerClickHandler
{
    public Vector2 _position;

    public Text numberText;
    private GameManager _gameManager;
    private int _index;

    public void Init(int num, int index, Vector2 position, GameManager gameManager)
    {
        _index = index;
        numberText = GetComponentInChildren<Text>();
        _position = position;
        _gameManager = gameManager;
        if (numberText == null) return;

        if (_gameManager == null) {
            Debug.LogError("Game Manager is null");
            return;
        }
        
        numberText.text = num == 0 ? "" : num.ToString();
        gameObject.SetActive(num != 0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _gameManager.TrySwapTiles(_index);
    }
}