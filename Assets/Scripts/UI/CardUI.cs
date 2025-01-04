using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour {
    private Image _cardImage;

    public void Initialize(Card card) {
        _cardImage = GetComponent<Image>();
        string rank;
        if ((int)card.Rank > 10) {
            rank = card.Rank.ToString();
        }
        else {
            int value = (int)card.Rank;
            rank = value.ToString();
        }
        string spriteName = rank.ToLower() + "_" + card.Suit.ToString().ToLower();
        _cardImage.sprite = Resources.Load<Sprite>("Cards/" + spriteName);
    }
}
