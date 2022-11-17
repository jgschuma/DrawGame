using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunScript : MonoBehaviour
{   
    Animator Anim;
    public GunCard GunStats;
    public Camera PlayerCam;
    public TMP_Text AmmoDisplay;
    public ParticleSystem MuzzleFlash;

    public float FireRate;
    public int Damage;
    public float MaxRange;
    public int AmmoCount;
    public int CurrentAmmo;
    public FireMode FireType;
    private float FireDelay;
    private float LastShootTime;

    // Start is called before the first frame update
    void Start()
    {
        FireRate = GunStats.FireRate;
        Damage = GunStats.Damage;
        MaxRange = GunStats.MaxRange;
        AmmoCount = GunStats.AmmoCount;
        FireType = GunStats.FireType;

        FireDelay = 1/FireRate;
    
        Anim = GetComponent<Animator>();
        Anim.SetBool("isShoot", false);
        CurrentAmmo = AmmoCount;
        AmmoDisplay.text = CurrentAmmo + "/" + AmmoCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("mouse 0") && FireType == FireMode.Single){
            // If the player has ammo, shoot the gun
            if(CurrentAmmo > 0){
                Shoot();
            }
        }
        else if( FireType == FireMode.Auto && Input.GetKey("mouse 0") ){
            if(CurrentAmmo > 0){
                Shoot();
            }
        }

        if(CurrentAmmo <= 0){
            AmmoDisplay.text = "0/0";
            Destroy(this.gameObject);
        }
    }


    private void Shoot(){
        if(Time.time > LastShootTime + FireDelay){
            RaycastHit hit;
            if(Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out hit, MaxRange)){
                Debug.Log(hit.transform.name);
                Damageable damageable;
                damageable = hit.transform.GetComponent<Damageable>();
                if(damageable != null){
                    damageable.TakeDamage(Damage);
                }
            }
            Anim.Play("Shoot", 0, 0);
            MuzzleFlash.Play();
            CurrentAmmo--;
            AmmoDisplay.text = CurrentAmmo + "/" + AmmoCount;
            
            LastShootTime = Time.time;
        }
    }
}
