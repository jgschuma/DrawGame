using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int Energy;
    public float EnergyRegenTime;
    private float LastIncrementTime;
    [SerializeField] private Image EnergyBar;
    

    public int Health;
    public int Credits;

    public TMP_Text EnergyDisplay;
    public TMP_Text HealthDisplay;
    public TMP_Text CreditsDisplay;

    void Start()
    {
        LastIncrementTime = Time.time  ;
    }

    void Update()
    {
        EnergyDisplay.text = "Energy: " + Energy.ToString();
        EnergyRegen();
    }

    void EnergyRegen()
    {
        if(Time.time > LastIncrementTime + EnergyRegenTime){
            LastIncrementTime = Time.time;
            Energy++;
        }
        UpdateEnergyBar();
    }

    public void UpdateEnergyBar(){
        EnergyBar.fillAmount = (Time.time - LastIncrementTime)/EnergyRegenTime;
    }
}
