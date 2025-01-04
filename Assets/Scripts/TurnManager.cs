using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

using GameTurn = GameManager.GameTurn;

public class TurnManager : MonoBehaviour {
    public event Action<GameTurn, Card> OnCardPlayed;

    [SerializeField] private PlayerHandUI _playerHandView;
    [SerializeField] private PlayerHandUI _dummyHandView;
    [SerializeField] private RoundManager _roundManager;
    private GameManager.GameBidding _bidding;

    void Start() {
        _roundManager.OnTurnStarted += HandleTurnStarted;
        _playerHandView.OnCardClicked += HandleCardClicked;
        _dummyHandView.OnCardClicked += HandleCardClicked;
    }

    void OnDestroy() {
        _roundManager.OnTurnStarted -= HandleTurnStarted;
        _playerHandView.OnCardClicked -= HandleCardClicked;
        _dummyHandView.OnCardClicked -= HandleCardClicked;
    }

    private void HandleCardClicked(GameTurn turn, Card card) {
        OnCardPlayed?.Invoke(turn, card);
    }

    private void HandleTurnStarted(Card.CardSuit? suit, List<Card> hand, GameManager.GameTurn turn) {
        List<Card> possibleCards = DeterminePossibleCards(suit, hand);
        if (turn == GameTurn.West || turn == GameTurn.East) {
            PlayCardWithDelay(1, turn, possibleCards[0]);
        }
    }

    private List<Card> DeterminePossibleCards(Card.CardSuit? suit, List<Card> hand) {
        if (suit == null) {
            return hand;
        }
        bool handContainsSuit = hand.Any(card => card.Suit == suit);
        if (!handContainsSuit) {
            return hand;
        }
        return hand.Where(card => card.Suit == suit).ToList();
    }
    private async void PlayCardWithDelay(int seconds, GameTurn turn, Card card) {
        await Task.Delay(seconds * 1000);
        OnCardPlayed?.Invoke(turn, card);
    }
}
