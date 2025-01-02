using UnityEngine;
using System.Collections.Generic;

public class PlayerHandView : MonoBehaviour {
    public GameObject cardPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    
    public void Initialize(List<Card> hand) {
        float width = AjustWidth(hand.Count);

        int numberOfCards = hand.Count;
        
        float separation = width / numberOfCards;
        
        for (int i = 0; i < numberOfCards; i++) {
            GameObject cardObject = Instantiate(cardPrefab, this.transform);
            RectTransform cardRect = cardObject.GetComponent<RectTransform>();
            cardRect.anchoredPosition = new Vector2(i * separation, 0);
            cardObject.GetComponent<CardView>().Initialize(hand[i]);
        }
    }
    
    private float AjustWidth(int numberOfCards) {
        RectTransform parentRect = this.transform.parent as RectTransform;
        RectTransform panelRect = this.transform as RectTransform;
        float parentWidth = parentRect.rect.width;
        float neededWidth = numberOfCards * cardPrefab.GetComponent<RectTransform>().rect.width;
        float newWidth = Mathf.Min(parentWidth, neededWidth);
        
        panelRect.sizeDelta = new Vector2(newWidth, panelRect.sizeDelta.y);
        return newWidth;
    }
}
