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
    private void OnEnable()//obje sahneye ilk girdi�inde 
    {
        playerHealth.Shielded = true;
    }

    private void OnDisable()//obje kapat�ld���nda(yok olunca) �al���r
    {
        playerHealth.Shielded = false;
    }


}
