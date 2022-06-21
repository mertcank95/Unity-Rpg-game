using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    PlayerHealth playerHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        
    }
    private void OnEnable()//obje sahneye ilk girdiðinde 
    {
        playerHealth.Shielded = true;
    }

    private void OnDisable()//obje kapatýldýðýnda(yok olunca) çalýþýr
    {
        playerHealth.Shielded = false;
    }


}
