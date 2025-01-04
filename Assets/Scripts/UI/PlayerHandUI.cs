using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

using GameTurn = GameManager.GameTurn;
using System.Linq;

public class PlayerHandUI : MonoBehaviour {
    public GameObject cardPrefab;
    public event Action<GameTurn, Card> OnCardClicked;
    private List<GameObject> _cardObjects = new();
    [SerializeField] private RoundManager _roundManager;
    private GameTurn _turn;

    void Start() {
        _roundManager.OnTurnStarted += HandleTurnStarted;
    }

    void OnDestroy() {
        _roundManager.OnTurnStarted -= HandleTurnStarted;
    }

    private void HandleTurnStarted(Card.CardSuit? suit, List<Card> hand, GameTurn turn) {
        if (_turn != turn) {
            SetAllCardsInteractable(false);
            return;
        }
        if (suit == null) {
            SetAllCardsInteractable(true);
            return;
        }
        if (!ContainsSuit(suit, hand)) {
            SetAllCardsInteractable(true);
            return;
        }
        for (int i = 0; i < _cardObjects.Count; i++) {
            if (_cardObjects[i] != null)
                _cardObjects[i].GetComponent<Button>().interactable = hand[i].Suit == suit;
        }
    }

    private bool ContainsSuit(Card.CardSuit? suit, List<Card> hand) {
        return hand.Any(card => card.Suit == suit);
    }

    private void SetAllCardsInteractable(bool interactable) {
        for (int i = 0; i < _cardObjects.Count; i++) {
            if (_cardObjects[i] != null)
                _cardObjects[i].GetComponent<Button>().interactable = interactable;
        }
    }

    public void Initialize(GameTurn turn, List<Card> hand) {
        _cardObjects.ForEach(cardObject => {
            if (cardObject != null)
                Destroy(cardObject);
        });
        _cardObjects.Clear();
        _turn = turn;
        float width = AdjustWidth(hand.Count);

        int numberOfCards = hand.Count;

        float separation = width / numberOfCards;

        RectTransform parentRect = this.transform.parent as RectTransform;

        for (int i = 0; i < numberOfCards; i++) {
            GameObject cardObject = Instantiate(cardPrefab, this.transform);
            _cardObjects.Add(cardObject);
            RectTransform cardRect = cardObject.GetComponent<RectTransform>();
            Button button = cardObject.GetComponent<Button>();
            button.interactable = false;
            Card card = hand[i];
            button.onClick.AddListener(() => {
                OnCardClicked?.Invoke(turn, card);
                Destroy(cardObject);
            });
            cardRect.anchoredPosition = new Vector2(parentRect.position.x + (i + 1) * separation, 0);
            cardObject.GetComponent<CardUI>().Initialize(hand[i]);
        }
    }

    private float AdjustWidth(int numberOfCards) {
        RectTransform parentRect = this.transform.parent as RectTransform;
        RectTransform panelRect = this.transform as RectTransform;
        float parentWidth = parentRect.rect.width;
        float neededWidth = numberOfCards * cardPrefab.GetComponent<RectTransform>().rect.width;
        float newWidth = Mathf.Min(parentWidth, neededWidth);

        panelRect.sizeDelta = new Vector2(newWidth, panelRect.sizeDelta.y);
        return newWidth;
    }
}
