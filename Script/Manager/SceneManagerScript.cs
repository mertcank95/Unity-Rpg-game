using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneManagerScript : MonoBehaviour
{

    BossState bossState;
    public GameObject bossHealtBar;

    private void Awake()
    {
        bossState = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossState>();
        
    
    }
    private void Start()
    {
        AudioManager.instance.PlayGameMusic();
    }

    void Update()
    {
        if(bossState.state == BossState.State.SLEEP || bossState.state == BossState.State.DEATH)
        {
            if(bossHealtBar !=null)
            bossHealtBar.SetActive(false);
        }
        else
        {
            if (bossHealtBar != null)
                bossHealtBar.SetActive(true);
        }

        if(Boss.bossDeath == true)
        {
            Invoke("RestartScene", 5);
        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            Invoke("RestartScene", 3);

        }


    }


    void RestartScene()
    {
        SceneManager.LoadScene(0);
    }



}
