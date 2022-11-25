using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public List<GameObject> TargetList = new List<GameObject>();
    public GameObject TargetPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++){
            Vector3 targetPosition = new Vector3(Random.Range(-5,5), Random.Range(-5,5), Random.Range(-5,5)) + transform.position;
            GameObject CurrentTarget = Instantiate(TargetPrefab, targetPosition, Quaternion.identity);
            CurrentTarget.transform.Rotate(90,0,0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
