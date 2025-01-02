using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour {
    private Image _cardImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

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
        Debug.Log(spriteName);
        _cardImage.sprite = Resources.Load<Sprite>("Cards/" + spriteName);
    }
}
