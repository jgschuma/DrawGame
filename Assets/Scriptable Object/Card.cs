using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    public string Name;
    public string CardType;
    public string Description;
    //public Sprite CardTypeIcon;
    //public Sprite CardImage;
    public int Cost;

    public virtual void OnPlay(){
        Debug.Log(Name + " was played.");
    }
}
