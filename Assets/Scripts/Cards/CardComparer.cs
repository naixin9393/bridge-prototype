using System.Collections.Generic;
using UnityEngine;

public class CardComparer : IComparer<Card> {
    private readonly Card.CardSuit _biddingSuit;

    public CardComparer(Card.CardSuit biddingSuit) {
        _biddingSuit = biddingSuit;
    }

    public int Compare(Card x, Card y) {
        return x.CompareTo(y, _biddingSuit);
    }
}
