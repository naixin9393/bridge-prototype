using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _biddingMenu;
    [SerializeField] private Text _roundWinnerText;

    void Start() {
        _gameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    void OnDestroy() {
        _gameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameManager.GamePhase state) {
        _biddingMenu.SetActive(state == GameManager.GamePhase.Bidding);
        _roundWinnerText.enabled = state == GameManager.GamePhase.BetweenRounds;
    }
}
