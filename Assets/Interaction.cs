using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class Interaction : MonoBehaviour
{
    public Camera PlayerCam;
    public float InteractionDistance;
    public static event Action<Collider> InteractionEvent;
    public GameObject TextDisplay;
    public TMP_Text TextContents;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out hit, InteractionDistance)){
            if(hit.transform.CompareTag("Interactable")){
                // if(hit.transform.GetComponent<InteractionText>() != null){
                //     TextDisplay.SetActive(true);
                //     TextContents.text = hit.transform.GetComponent<InteractionText>().TextPopup;
                // }
                if(Input.GetKeyDown("f")){
                    // I'm calling an interaction and passing the collider, there has to be
                    // a better way than having every actionable item check an if statement
                    // to see if it is the one being interacted with.
                    InteractionEvent?.Invoke(hit.collider);
                    Debug.Log(hit.transform.name + " was activated");
                }
            }
        // }else{
        //     TextDisplay.SetActive(false);
        //     TextContents.text = "Amongus";
        }
        
    }
}
