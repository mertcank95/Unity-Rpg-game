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
    private void OnEnable()//obje sahneye ilk girdiğinde 
    {
        playerHealth.Shielded = true;
    }

    private void OnDisable()//obje kapatıldığında(yok olunca) çalışır
    {
        playerHealth.Shielded = false;
    }


}
