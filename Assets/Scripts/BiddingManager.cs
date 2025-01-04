using System;
using UnityEngine;

public class BiddingManager : MonoBehaviour {
    public event Action<int, string> OnBiddingPlaced;
    public event Action<int, string> OnBiddingFinished;

    [SerializeField] private BiddingMenu _biddingMenu;

    void Start() {
        _biddingMenu.OnBiddingPlaced += HandleBiddingPlaced;
    }

    void OnDestroy() {
        _biddingMenu.OnBiddingPlaced -= HandleBiddingPlaced;
    }

    private void HandleBiddingPlaced(int value, string suit) {
        OnBiddingPlaced?.Invoke(value, suit);

        // TODO: call when theres three consecutive passes
        OnBiddingFinished?.Invoke(value, suit);
    }
}
