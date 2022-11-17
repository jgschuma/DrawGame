using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public int HitPoints;

    public void TakeDamage(int damage){
        HitPoints -= damage;

        if(HitPoints <= 0){
            Die();
        }
    }

    public void Die(){
        Destroy(this.gameObject);
    }
}
