using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayedCardsManager : MonoBehaviour {
    [SerializeField] private GameObject northCardObject;
    [SerializeField] private GameObject eastCardObject;
    [SerializeField] private GameObject southCardObject;
    [SerializeField] private GameObject westCardObject;
    [SerializeField] private TurnManager _turnManager;
    [SerializeField] private GameManager _gameManager;

    void Start() {
        _turnManager.OnCardPlayed += HandleCardPlayed;
        _gameManager.OnRoundStarted += HandleRoundStarted;
        SetAllCardsInvisible();
    }

    void OnDestroy() {
        _turnManager.OnCardPlayed -= HandleCardPlayed;
        _gameManager.OnRoundStarted -= HandleRoundStarted;
    }

    private void HandleRoundStarted(GameManager.GameTurn turn, Dictionary<GameManager.GameTurn, List<Card>> dictionary) {
        SetAllCardsInvisible();
    }

    private void SetAllCardsInvisible() {
        northCardObject.GetComponent<Image>().color = Color.clear;
        eastCardObject.GetComponent<Image>().color = Color.clear;
        southCardObject.GetComponent<Image>().color = Color.clear;
        westCardObject.GetComponent<Image>().color = Color.clear;
    }

    private void HandleCardPlayed(GameManager.GameTurn turn, Card card) {
        GameObject cardObject = GetCardObject(turn);
        cardObject.GetComponent<CardUI>().Initialize(card);
        cardObject.GetComponent<Image>().color = Color.white;
    }

    private GameObject GetCardObject(GameManager.GameTurn turn) {
        return turn switch {
            GameManager.GameTurn.North => northCardObject,
            GameManager.GameTurn.East => eastCardObject,
            GameManager.GameTurn.South => southCardObject,
            GameManager.GameTurn.West => westCardObject,
            _ => throw new ArgumentException("Invalid turn")
        };
    }
}
