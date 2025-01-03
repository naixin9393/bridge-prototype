using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public enum GameState { Bidding, Playing, Finished };
    public event Action<GameState> OnGameStateChanged;

    [SerializeField] private MenuManager _menuManager;
    [SerializeField] private BiddingManager _biddingManager;
    [SerializeField] private PlayerHandView _playerHandView;

    private readonly Player _northPlayer = new(Player.PlayerPosition.North);
    private readonly Player _eastPlayer = new(Player.PlayerPosition.East);
    private readonly Player _southPlayer = new(Player.PlayerPosition.South);
    private readonly Player _westPlayer = new(Player.PlayerPosition.West);
    private Player _currentPlayer;

    void Start() {
        // TODO: random player
        _currentPlayer = _southPlayer;
        StartGame();
    }

    private void OnBiddingFinished(int biddingValue, string biddingSuit) {
        _biddingManager.OnBiddingFinished -= OnBiddingFinished;
        OnGameStateChanged?.Invoke(GameState.Playing);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("StartMenu");
    }
    
    private void StartGame() {
        Debug.Log("Game started");
        DealCards();
        Debug.Log("Dealt cards");
        InitializePlayerHand();
        StartBidding();
    }

    private void StartBidding() {
        OnGameStateChanged?.Invoke(GameState.Bidding);
        _biddingManager.OnBiddingFinished += OnBiddingFinished;
    }

    private void DealCards() {
        Deck deck = new();
        for (int i = 0; i < 13; i++) {
            _northPlayer.AddCard(deck.DrawCard());
            _eastPlayer.AddCard(deck.DrawCard());
            _southPlayer.AddCard(deck.DrawCard());
            _westPlayer.AddCard(deck.DrawCard());
        }
    }

    private void InitializePlayerHand() {
        _currentPlayer.Hand.OrderByDescending(card => card, new CardComparer(Card.CardSuit.Spades));
        _playerHandView.Initialize(_currentPlayer.Hand);
    }
}
