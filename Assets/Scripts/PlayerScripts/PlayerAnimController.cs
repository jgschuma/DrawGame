using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    public GameObject Arms;
    public PlayerController guy;
    public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(guy.isSprint == true)
        {
            //Debug.Log("We out here sprinting");
            //Anim.Play("Run_Start");
            Anim.SetBool("IsRunning", true);
        }
        else{
            Anim.SetBool("IsRunning", false);
        }
    }



}
