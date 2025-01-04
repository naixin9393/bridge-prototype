using UnityEngine;
using UnityEngine.UI;

public class GameInfoManager : MonoBehaviour {
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Text _playerScoreText;
    [SerializeField] private Text _roundWinnerText;

    void Start() {
        _gameManager.OnPlayerScoreChanged += HandlePlayerScoreChanged;
        _gameManager.OnRoundFinished += HandleRoundFinished;
    }
    
    void OnDestroy() {
        _gameManager.OnPlayerScoreChanged -= HandlePlayerScoreChanged;
        _gameManager.OnRoundFinished -= HandleRoundFinished;
    }

    private void HandlePlayerScoreChanged(int score) {
        _playerScoreText.text = "Your score is " + score;
    }

    private void HandleRoundFinished(string winner) {
        _roundWinnerText.text = "Round winner is " + winner;
    }
}
