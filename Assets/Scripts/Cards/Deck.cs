using System;
using System.Collections.Generic;

public class Deck {
    private readonly List<Card> _cards = new();

    public Deck() {
        foreach (Card.CardSuit suit in Enum.GetValues(typeof(Card.CardSuit))) {
            foreach (Card.CardRank rank in Enum.GetValues(typeof(Card.CardRank))) {
                _cards.Add(new Card(rank, suit));
            }
        }
    }

    public void shuffle() {
        // TODO: Implement shuffling
    }

    public Card DrawCard() {
        Card lastCard = _cards[^1];
        _cards.RemoveAt(_cards.Count - 1);
        return lastCard;
    }
}
