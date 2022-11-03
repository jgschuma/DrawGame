using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Card grenade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShuffleDeck(){
        Shuffle(deck);
    }
    public void AddGrenade(){
        AddCard(grenade);
    }

    public void Shuffle(List<Card> deck){
        for(int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int r = Random.Range(i, deck.Count);
            deck[i] = deck[r];
            deck[r] = temp;
        }

        for(int i = 0; i < deck.Count; i++){
            Debug.Log(deck[i].Name);
        }
    }

    public void AddCard(Card card){
        deck.Add(card);
        ShuffleDeck();
    }
}
