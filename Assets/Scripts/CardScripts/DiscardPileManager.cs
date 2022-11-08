using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiscardPileManager : MonoBehaviour
{
    public List<Card> DiscardPile = new List<Card>();
    // Start is called before the first frame update
    void Start()
    {
        HandManager.CardWasPlayed += HandToDiscard;
    }

    void HandToDiscard(Card PlayedCard)
    {
        DiscardPile.Add(PlayedCard);
        Debug.Log(PlayedCard + " was added to the discard pile.");
    }
}
