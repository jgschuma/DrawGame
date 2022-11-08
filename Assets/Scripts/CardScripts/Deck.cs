using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This script will handle the persistent deck kept between rounds
public class Deck : MonoBehaviour
{
    // deck is an arraylist of cards, this allows the decklist to change size
    public List<Card> deck = new List<Card>();
    public Card grenade;

    public static event Action<List<Card>> InitDraw;

    void Start()
    {
        
    }

    void Update(){
        if(Input.GetKeyDown("l"))
        {
            InitDraw?.Invoke(deck);
        }
    }

    

    // Testing method for adding a card. We should end up using an event to listen for someone adding a card
    // This is beneficial because they'll already have the scriptable object we want to pass
    public void AddCard(Card card)
    {
        deck.Add(card);
        ShuffleDeck();
    }

    // Use the knuth shuffle to randomize the deck list
    public void Shuffle()
    {
        for(int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int r = UnityEngine.Random.Range(i, deck.Count);
            deck[i] = deck[r];
            deck[r] = temp;
        }

        for(int i = 0; i < deck.Count; i++){
            Debug.Log(deck[i].Name);
        }
    }

    // These methods are used by the buttons in Card scene to test functionality
    public void ShuffleDeck()
    {
        Shuffle();
    }
    public void AddGrenade()
    {
        AddCard(grenade);
    }
}
