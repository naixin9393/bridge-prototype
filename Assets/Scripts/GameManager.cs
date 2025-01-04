using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public enum GamePhase { Bidding, Playing, Finished, BetweenRounds };
    public enum GameTurn { North, East, South, West };
    public enum GameBidding { NT, Spades, Hearts, Diamonds, Clubs };
    public event Action<GamePhase> OnGameStateChanged;
    public event Action<GameTurn, Dictionary<GameTurn, List<Card>>> OnRoundStarted;
    public event Action<int> OnPlayerScoreChanged;
    public event Action<string> OnRoundFinished;

    public GamePhase PlayingPhase { get; private set; }

    [SerializeField] private BiddingManager _biddingManager;
    [SerializeField] private RoundManager _roundManager;

    // Temporal objects
    [SerializeField] private PlayerHandUI _playerHandView;
    [SerializeField] private PlayerHandUI _dummyHandView;

    private readonly Player _northPlayer = new(Player.PlayerPosition.North);
    private readonly Player _eastPlayer = new(Player.PlayerPosition.East);
    private readonly Player _southPlayer = new(Player.PlayerPosition.South);
    private readonly Player _westPlayer = new(Player.PlayerPosition.West);
    private GameTurn _currentTurn;
    private int _currentRound = 0;
    private string _biddingSuit;
    private int _playerScore = 0;

    void Start() {
        // TODO: random player
        StartGame();
        _biddingManager.OnBiddingFinished += HandleBiddingFinished;
        _roundManager.OnRoundFinished += HandleRoundFinished;
    }

    void OnDestroy() {
        _biddingManager.OnBiddingFinished -= HandleBiddingFinished;
        _roundManager.OnRoundFinished -= HandleRoundFinished;
    }

    private void HandleCardPlayed(GameTurn turn, Card card) {
        _currentTurn = (GameTurn)(((int)_currentTurn + 1) % 4);

    }

    private void HandleRoundFinished(Dictionary<GameTurn, Card> cards) {
        _currentRound++;
        GameTurn winner = DetermineRoundWinner(cards);
        OnGameStateChanged?.Invoke(GamePhase.BetweenRounds);
        if (winner == GameTurn.South || winner == GameTurn.North)
            OnPlayerScoreChanged?.Invoke(++_playerScore);
        OnRoundFinished?.Invoke(winner.ToString());
        Debug.Log("Round winner is " + winner);
        foreach (var pair in cards)
            GetPlayer(pair.Key).PlayCard(pair.Value);
        if (_currentRound == 13) {
            OnGameStateChanged?.Invoke(GamePhase.Finished);
            return;
        }

        WaitingForSeconds(2, winner);
    }

    private async void WaitingForSeconds(int seconds, GameTurn winner) {
        await Task.Delay(seconds * 1000);
        _dummyHandView.Initialize(GameTurn.North, _northPlayer.Hand);
        _playerHandView.Initialize(GameTurn.South, _southPlayer.Hand);
        OnRoundStarted?.Invoke(winner, GetPlayersHands());
        OnGameStateChanged?.Invoke(GamePhase.Playing);
    }

    private GameTurn DetermineRoundWinner(Dictionary<GameTurn, Card> cards) {
        GameTurn winner = GameTurn.North;
        Card winningCard = cards[GameTurn.North];

        CardComparer cardComparer = _biddingSuit == "NT" ? new(cards[_currentTurn].Suit) : GetSuit(_biddingSuit);
        foreach (var pair in cards) {
            bool isCardStrongerThanWinningCard = cardComparer.Compare(pair.Value, winningCard) == 1;
            if (isCardStrongerThanWinningCard) {
                winner = pair.Key;
                winningCard = pair.Value;
            }
        }
        return winner;
    }

    private Player GetPlayer(GameTurn turn) {
        return turn switch {
            GameTurn.North => _northPlayer,
            GameTurn.East => _eastPlayer,
            GameTurn.South => _southPlayer,
            GameTurn.West => _westPlayer,
            _ => throw new ArgumentException("Invalid turn")
        };
    }

    private CardComparer GetSuit(string biddingSuit) {
        return biddingSuit switch {
            "spades" => new(Card.CardSuit.Spades),
            "hearts" => new(Card.CardSuit.Hearts),
            "diamonds" => new(Card.CardSuit.Diamonds),
            "clubs" => new(Card.CardSuit.Clubs),
            _ => new(Card.CardSuit.Clubs)
        };
    }

    private void HandleBiddingFinished(int biddingValue, string biddingSuit) {
        _biddingSuit = biddingSuit;
        OnGameStateChanged?.Invoke(GamePhase.Playing);
        _northPlayer.Hand.Sort((x, y) => y.CompareTo(x, Card.CardSuit.Spades));
        _dummyHandView.Initialize(GameTurn.North, _northPlayer.Hand);
        OnRoundStarted?.Invoke(GameTurn.West, GetPlayersHands());
    }

    private Dictionary<GameTurn, List<Card>> GetPlayersHands() {
        Dictionary<GameTurn, List<Card>> playersHands = new() {
            { GameTurn.North, _northPlayer.Hand },
            { GameTurn.East, _eastPlayer.Hand },
            { GameTurn.South, _southPlayer.Hand },
            { GameTurn.West, _westPlayer.Hand }
        };
        return playersHands;
    }

    private void StartGame() {
        Debug.Log("Game started");
        DealCards();
        Debug.Log("Dealt cards");
        InitializePlayerHand();
        StartBidding();
    }

    private void StartBidding() {
        OnGameStateChanged?.Invoke(GamePhase.Bidding);
    }

    private void DealCards() {
        Deck deck = new();
        deck.Shuffle();
        for (int i = 0; i < 13; i++) {
            _northPlayer.AddCard(deck.DrawCard());
            _eastPlayer.AddCard(deck.DrawCard());
            _southPlayer.AddCard(deck.DrawCard());
            _westPlayer.AddCard(deck.DrawCard());
        }
    }

    private void InitializePlayerHand() {
        _southPlayer.Hand.Sort((x, y) => y.CompareTo(x, Card.CardSuit.Spades));
        _playerHandView.Initialize(GameTurn.South, _southPlayer.Hand);
    }
}
