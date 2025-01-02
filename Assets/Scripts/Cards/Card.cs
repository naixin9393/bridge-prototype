public class Card {
    public enum CardSuit { Spades, Hearts, Diamonds, Clubs }
    public enum CardRank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    public CardRank Rank { get; private set; }
    public CardSuit Suit { get; private set; }

    public Card(CardRank rank, CardSuit suit) {
        Rank = rank;
        Suit = suit;
    }
}
