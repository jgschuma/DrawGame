using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    
    public TMP_Text NameText;
    public TMP_Text CardTypeText;
    public TMP_Text DescriptionText;
    public TMP_Text CardCost;

    // Start is called before the first frame update
    void Start()
    {
        NameText.text = card.Name;
        CardTypeText.text = card.CardType;
        DescriptionText.text = card.Description;
        CardCost.text = card.Cost.ToString();
    }
}
