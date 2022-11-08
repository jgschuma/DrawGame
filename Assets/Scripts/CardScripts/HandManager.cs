using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class HandManager : MonoBehaviour
{
    public List<GameObject> HandList = new List<GameObject>();
    public GameObject CardPrefab;
    public Transform Canvas;

    public int MaxHandSize;
    // The general area our cards will rest
    public Transform HandPos;
    // The start and end points of our hand area
    public Transform HandStart;
    public Transform HandEnd;
    // What card is currently selected
    public int Selection;
    public Vector3 SelectedScale;
    public Vector3 DefaultScale;
    public float ScaleUpSpeed;
    public float ScaleDownSpeed;
    // How far apart should the cards be?
    private float CardGap;

    public static event Action<Card> CardWasPlayed;
    public static event Action DrawNewCard;

    public void Start()
    {
        DrawPileManager.Ready += StartingHand;
        DrawPileManager.Draw += DrawCardToHand;
        
    }

    void Update()
    {
        HightlightSelected();
        ScaleCards();
        PlayCard();
        
    }

    void PlayCard()
    {
        if(Input.GetKeyDown("f") && HandList.Count > 0){
            GameObject CardBeingPlayed = HandList[Selection];
            HandList[Selection].GetComponent<CardDisplay>().card.OnPlay();
            CardWasPlayed?.Invoke(HandList[Selection].GetComponent<CardDisplay>().card);
            HandList.RemoveAt(Selection);
            Destroy(CardBeingPlayed);
            DrawNewCard?.Invoke();
            if (Selection > HandList.Count - 1 && HandList.Count > 0){
                Selection = HandList.Count - 1;
            }
            FitCards();

        }
    }

    public void DrawCardToHand(Card CardToDraw)
    {
        // Create a new card prefab, give it the stats of the card at the top of the deck, 
        // and remove the card at the top of the deck.
        GameObject newCard = Instantiate(CardPrefab, HandPos.position, Quaternion.identity);
        newCard.GetComponent<CardDisplay>().card = CardToDraw;
        newCard.transform.SetParent(Canvas);
        newCard.transform.localScale = new Vector3(1, 1, 1);
        Outline toDisable = newCard.GetComponentInChildren(typeof(Outline)) as Outline;
        toDisable.enabled = false;
        // Add the card GameObject to the HandList
        HandList.Add(newCard);
        FitCards();
    }

    public void StartingHand(){
        Debug.Log("We are in the opening hand forLoop");
        for(int i = 0; i < MaxHandSize; i++)
        {
            DrawNewCard?.Invoke();
        }
    }

    // Adjusts the cards to they space evenly between our start and stop points
    public void FitCards()
    {
        float distance = Vector3.Distance(HandStart.position, HandEnd.position);
        // Need to divide by count+1 otherwise the numbers will be off
        CardGap = distance/(HandList.Count + 1);
        Vector3 LastPos = new Vector3(HandStart.position.x, HandStart.position.y, HandStart.position.z) ;
        Vector3 NextPos;
        for (int i = 0; i < HandList.Count; i++){
                NextPos = LastPos + new Vector3(CardGap, 0, 0) ;
            // Try using lerp here to make it smooth
            HandList[i].transform.position = NextPos;
            LastPos = NextPos;
        }
    }

    void HightlightSelected()
    {
        // Highlight the currently selected Card
        if(HandList.Count > 0){
            Outline CurrentOutline = HandList[Selection].GetComponentInChildren(typeof(Outline)) as Outline;
            if(CurrentOutline.enabled == false)
            {
                CurrentOutline.enabled = true;
            }
            // Disable the card as we select a new one

            // if(Input.GetAxis("Mouse ScrollWheel") > 0 && HandList[Selection+1] != null){
            if(Input.GetKeyDown("e") && Selection + 1 < HandList.Count){
                Outline oldOutline = HandList[Selection].GetComponentInChildren(typeof(Outline)) as Outline;
                oldOutline.enabled = false;
                Selection++;
            }
            // if(Input.GetAxis("Mouse ScrollWheel") < 0 && HandList[Selection-1] != null){
            if(Input.GetKeyDown("q") && Selection - 1 >= 0){
                Outline oldOutline = HandList[Selection].GetComponentInChildren(typeof(Outline)) as Outline;
                oldOutline.enabled = false;
                Selection--;
            }
        }
    }

    void ScaleCards()
    {
        if(HandList.Count > 0){
            GameObject CurrentCard = HandList[Selection];

            // While the current card is selected, ensure that it scales to the appropriate size
            if(CurrentCard.transform.localScale != SelectedScale){
                CurrentCard.transform.localScale = Vector3.Lerp(CurrentCard.transform.localScale, SelectedScale, ScaleUpSpeed);
            }
            GameObject OtherCard;
            // For every other card, ensure it returns to the defaul
            for(int i = 0; i < HandList.Count; i++)
            {
                OtherCard = HandList[i];
                if(i != Selection)
                {
                    // TODO See if a while loop is acceptable here
                    while(OtherCard.transform.localScale != DefaultScale){
                        OtherCard.transform.localScale = Vector3.Lerp(OtherCard.transform.localScale, DefaultScale, ScaleDownSpeed);
                    }
                }
            }
        }
    }
}
