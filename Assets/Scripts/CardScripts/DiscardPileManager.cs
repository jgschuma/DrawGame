using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DiscardPileManager : MonoBehaviour
{
    public List<Card> DiscardPile = new List<Card>();
    public static event Action<List<Card>> DiscardToDrawPile;
    // Start is called before the first frame update
    void Start()
    {
        HandManager.CardWasPlayed += HandToDiscard;
        DrawPileManager.DrawPileEmpty += ShuffleDiscard;
    }

    void HandToDiscard(Card PlayedCard)
    {
        DiscardPile.Add(PlayedCard);
        //Debug.Log(PlayedCard + " was added to the discard pile.");
    }

    void ShuffleDiscard()
    {
        // Pass the discard pile to our draw manager
        DiscardToDrawPile?.Invoke(DiscardPile);
    }
}
