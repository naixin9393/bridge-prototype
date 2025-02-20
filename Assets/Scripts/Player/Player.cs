using System.Collections.Generic;

public class Player {
    public enum PlayerPosition { North, East, South, West }

    public PlayerPosition Position { get; private set; }
    public List<Card> Hand { get; private set; }

    public Player(PlayerPosition position) {
        Position = position;
        Hand = new List<Card>();
    }

    public void AddCard(Card card) {
        Hand.Add(card);
    }

    public void PlayCard(Card card) {
        Hand.Remove(card);
    }
}
