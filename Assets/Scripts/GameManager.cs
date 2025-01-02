using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private readonly Deck _deck = new();
    private readonly Player _northPlayer = new(Player.PlayerPosition.North);
    private readonly Player _eastPlayer = new(Player.PlayerPosition.East);
    private readonly Player _southPlayer = new(Player.PlayerPosition.South);
    private readonly Player _westPlayer = new(Player.PlayerPosition.West);

    void Start() {
        Debug.Log("GameManager started");
        DealCards();
        Debug.Log("Dealt cards");
    }

    void Update() {

    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("StartMenu");
    }

    private void DealCards() {
        for (int i = 0; i < 13; i++) {
            _northPlayer.AddCard(_deck.DrawCard());
            _eastPlayer.AddCard(_deck.DrawCard());
            _southPlayer.AddCard(_deck.DrawCard());
            _westPlayer.AddCard(_deck.DrawCard());
        }
    }
}
