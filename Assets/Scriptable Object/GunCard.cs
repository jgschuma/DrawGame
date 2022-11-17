using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "New Gun Card", menuName = "Gun Card")]
public class GunCard : Card
{
    // public enum FireMode {Single, Burst, Auto};
    
    public float FireRate;
    public int Damage;
    public float MaxRange;
    public int AmmoCount;
    public FireMode FireType;

    
    

    public static event Action<GameObject> GunCardPlayed;
    
    // We'll need to call an event when a gunCard is played to have the gunHolder spawn the prefab;
    public GameObject GunPrefab;

    public override void OnPlay(){
        GunCardPlayed?.Invoke(GunPrefab);
        Debug.Log(Name + " was played. FireRate: " + FireRate + ", Damage: " + Damage + ", AmmoCount: " + AmmoCount);
    }
}
