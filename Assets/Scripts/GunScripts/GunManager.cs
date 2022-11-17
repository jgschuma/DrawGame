using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunManager : MonoBehaviour
{
    public Camera PlayerCam;
    public TMP_Text AmmoDisplay;
    private GameObject CurrentGun;
    // Start is called before the first frame update
    void Start()
    {
        GunCard.GunCardPlayed += SpawnNewGun;
    }

    void SpawnNewGun(GameObject GunPrefab)
    {
        if(CurrentGun != null){
            Destroy(CurrentGun);
        }
        CurrentGun = Instantiate(GunPrefab, transform.position, Quaternion.identity, this.transform);
        CurrentGun.GetComponent<GunScript>().PlayerCam = PlayerCam;
        CurrentGun.GetComponent<GunScript>().AmmoDisplay = AmmoDisplay;
        CurrentGun.transform.forward = PlayerCam.transform.forward;
    }
}
