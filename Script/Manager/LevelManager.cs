using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    int currentExp;
    int level;
    int expToNextLevel;
    public static LevelManager instance;

    public Image expBar;
    public Text levelText;
    public int Getlevel { get { return level+1; } }

    public GameObject effectLevelUp;
    Transform player;

    private void Awake()
    {
        level = 0;
        currentExp = 0;
        expToNextLevel = 100;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//sahne geçiþinde o nesyneyi yok etme
        }
        else
        {
            Destroy(gameObject);
        }
        expBar.fillAmount = 0;
        UpdateLevelText();

        player = GameObject.Find("Player").gameObject.transform;

    }
    
    

    public void AddExp(int amount)
    {
        currentExp += amount;
        expBar.fillAmount =(float) currentExp / expToNextLevel;
        if(currentExp >=expToNextLevel)
        {
            level++;
            UpdateLevelText();
            currentExp -= expToNextLevel;
            expBar.fillAmount = 0;
            GameObject levelupEffect = Instantiate(effectLevelUp, player.position, Quaternion.identity);
            levelupEffect.transform.SetParent(player);
        }
    }

    void UpdateLevelText()
    {
        levelText.text = Getlevel.ToString();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            AddExp(25);
            print(level);
        }
    }
    private void OnEnable()
    {
        EnemyHealt.onDeath += AddExp;//onDeath eventini addExp metoduna baðladýk
    }

    private void OnDisable()
    {
        EnemyHealt.onDeath -= AddExp;//onDeath eventini addExp metodundan veri istemiyecek

    }
}
