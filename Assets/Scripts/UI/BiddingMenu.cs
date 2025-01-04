using System;
using UnityEngine;
using UnityEngine.UI;

public class BiddingMenu : MonoBehaviour {
    public Text biddingText;
    private string _biddingSuit = "NT";
    private int _biddingValue = 1;
    public event Action<int, string> OnBiddingPlaced;

    void Start() {
        UpdateText();
    }

    public void OnSuitClicked(string suitName) {
        _biddingSuit = suitName;
        UpdateText();
    }

    public void OnValueClicked(int value) {
        _biddingValue = value;
        UpdateText();
    }

    private void UpdateText() {
        biddingText.text = "Your bidding is " + _biddingValue + " " + _biddingSuit;
    }

    public void OnConfirmClicked() {
        OnBiddingPlaced?.Invoke(_biddingValue, _biddingSuit);
    }

    public void OnPassClicked() {
        OnBiddingPlaced?.Invoke(0, "pass");
    }
}
