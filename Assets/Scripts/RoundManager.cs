using System;
using System.Collections.Generic;
using UnityEngine;

using GameTurn = GameManager.GameTurn;

public class RoundManager : MonoBehaviour {
    public event Action<Card.CardSuit?, List<Card>, GameTurn> OnTurnStarted;
    public event Action<Dictionary<GameTurn, Card>> OnRoundFinished;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TurnManager _turnManager;

    private Card.CardSuit? _leadSuit;
    private Dictionary<GameTurn, Card> _playedCards = new();
    private GameTurn _currentTurn;
    private Dictionary<GameTurn, List<Card>> _playersHands;

    void Start() {
        _turnManager.OnCardPlayed += HandleCardPlayed;
        _gameManager.OnRoundStarted += HandleRoundStarted;
    }

    void OnDestroy() {
        _turnManager.OnCardPlayed -= HandleCardPlayed;
        _gameManager.OnRoundStarted -= HandleRoundStarted;
    }

    private void HandleRoundStarted(GameTurn turn, Dictionary<GameTurn, List<Card>> playersHands) {
        _playedCards.Clear();
        _leadSuit = null;
        _currentTurn = turn;
        _playersHands = playersHands;
        OnTurnStarted?.Invoke(_leadSuit, playersHands[turn], turn);
    }

    private void HandleCardPlayed(GameTurn turn, Card card) {
        if (_currentTurn != turn) {
            return;
        }
        _playedCards.Add(turn, card);
        Debug.Log(turn + " played " + card.Rank + " " + card.Suit);
        if (_playedCards.Count == 4) {
            OnRoundFinished?.Invoke(_playedCards);
            return;
        }
        if (_playedCards.Count == 1) {
            _leadSuit = card.Suit;
        }
        GameTurn nextTurn = (GameTurn)(((int)_currentTurn + 1) % 4);
        _currentTurn = nextTurn;
        OnTurnStarted?.Invoke(_leadSuit, _playersHands[nextTurn], nextTurn);
    }
}
