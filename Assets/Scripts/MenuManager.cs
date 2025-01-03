using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _biddingMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnGameStateChanged(GameManager.GameState state) {
        _biddingMenu.SetActive(state == GameManager.GameState.Bidding);
    }
    
    void OnDestroy() {
        _gameManager.OnGameStateChanged -= OnGameStateChanged;
    }
}
