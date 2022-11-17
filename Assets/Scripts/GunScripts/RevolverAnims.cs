using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevolverAnims : MonoBehaviour
{
    Animator Anim;
    public PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
        Anim.SetBool("isShoot", false);
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown("mouse 0") && Anim.GetBool("isShoot") == false){
        //     Anim.SetBool("isShoot", true);
        // }
        if(Player.isSprint == true && Anim.GetBool("isShoot") == false){
            Anim.SetBool("isRun", true);
        }
        else{
            Anim.SetBool("isRun", false);
        }
    }


    public void ShootFalse(){
        Anim.SetBool("isShoot", false);
    }
}
