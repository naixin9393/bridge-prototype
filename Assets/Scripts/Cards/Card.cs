public class Card {
    public enum CardSuit { Clubs, Diamonds, Hearts, Spades }
    public enum CardRank { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }

    public CardRank Rank { get; private set; }
    public CardSuit Suit { get; private set; }

    public Card(CardRank rank, CardSuit suit) {
        Rank = rank;
        Suit = suit;
    }

    public int CompareTo(Card other, CardSuit biddingSuit) {
        if (Suit == other.Suit) {
            return Rank.CompareTo(other.Rank);
        }
        if (Suit == biddingSuit) return 1;
        if (other.Suit == biddingSuit) return -1;
        return Suit.CompareTo(other.Suit);
    }
}
