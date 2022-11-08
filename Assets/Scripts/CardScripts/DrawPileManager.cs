using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DrawPileManager : MonoBehaviour
{
    public List<Card> DrawPile;
    public bool InitDone = false;
    public static event Action<Card> Draw;
    public static event Action Ready;

    public void Start(){
        Deck.InitDraw += InitDraw;
        HandManager.DrawNewCard += DrawCardFromPile;
    }

    public void DrawCardFromPile(){
        if(DrawPile.Count >= 1){
            Draw?.Invoke(DrawPile[0]);
            DrawPile.RemoveAt(0);
        }
        // Else Shuffle Discard into DrawPile and attempt to draw again
    }

    // Use the knuth shuffle to randomize the deck list
    public void ShuffleDrawPile()
    {
        for(int i = 0; i < DrawPile.Count; i++)
        {
            Card temp = DrawPile[i];
            int r = UnityEngine.Random.Range(i, DrawPile.Count);
            DrawPile[i] = DrawPile[r];
            DrawPile[r] = temp;
        }

        for(int i = 0; i < DrawPile.Count; i++){
            Debug.Log(DrawPile[i].Name);
        }
    }

    public void InitDraw(List<Card> Decklist)
    {
        DrawPile = new List<Card>(Decklist);
        ShuffleDrawPile();
        Ready?.Invoke();
        Debug.Log("What the fuck why isn't this working?????");
    }


}
