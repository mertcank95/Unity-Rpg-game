using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillCast : MonoBehaviour
{
    [Header("Mana Settings")]
    public float totalMana = 100;
    public float manaRegenSpeed = 2f;
    public Image manaBar;

    [Header("Cooldown Icons")]
    public Image[] coolDownIcon;

    [Header("Out Of Mana Icons")]
    public Image[] outOfManaIcons;

    [Header("Cooldown Times")]
    [HideInInspector] public List<float> coolDownTimes = new List<float>();

    //Mana Degerleri
    [Header("Mana Amounts")]
    public float skill1ManaAmount = 20f;
    public float skill2ManaAmount = 20f;
    public float skill3ManaAmount = 20f;
    public float skill4ManaAmount = 20f;
    public float skill5ManaAmount = 20f;
    public float skill6ManaAmount = 20f;

    [Header("Requi level")]
    public int skill1 = 2;
    public int skill2 = 3;
    public int skill3 = 4;
    public int skill4 = 5;
    public int skill5 = 6;
    public int skill6 = 7;


    List<int> levelList = new List<int>();

    List<float> manaAmountList = new List<float>();
    bool faded = false;
    int[] fadeImages = new int[] { 0, 0, 0, 0, 0, 0 };
    Animator anim;
    bool canAttack = true;
    PlayerOnClick playerOnClick;

    LevelManager levelManager;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerOnClick = GetComponent<PlayerOnClick>();
        manaBar = GameObject.Find("ManaOrb").GetComponent<Image>();
        levelManager = FindObjectOfType<LevelManager>();

    }
    void Start()
    {
        AddList();
    }

    void AddList()
    {
        //coolDown
        coolDownTimes.Add(0.1f);
        coolDownTimes.Add(0.3f);
        coolDownTimes.Add(0.15f);
        coolDownTimes.Add(0.05f);
        coolDownTimes.Add(0.05f);
        coolDownTimes.Add(0.1f);
        //Mana
        manaAmountList.Add(skill1ManaAmount);
        manaAmountList.Add(skill2ManaAmount);
        manaAmountList.Add(skill3ManaAmount);
        manaAmountList.Add(skill4ManaAmount);
        manaAmountList.Add(skill5ManaAmount);
        manaAmountList.Add(skill6ManaAmount);
        //level
        levelList.Add(skill1);
        levelList.Add(skill2);
        levelList.Add(skill3);
        levelList.Add(skill4);
        levelList.Add(skill5);
        levelList.Add(skill6);



    }


    void Update()
    {
        //anim.IsInTransition(0) = geçiþde deðilse
        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }

        if (anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            TrunThePlayer();
        }

        if (totalMana < 100f)
        {
            totalMana += Time.deltaTime * manaRegenSpeed;
            manaBar.fillAmount = totalMana / 100;
        }

        CheckLevel();
        CheckMana();
        CheckToFade();
        CheckInput();

    }


    void CheckInput()
    {
        if (anim.GetInteger("Attack") == 0)
        {
            playerOnClick.FinishedMovent = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                playerOnClick.FinishedMovent = true;
            }

        }
        //Skil Input
        if (Input.GetKeyDown(KeyCode.Alpha1) && totalMana >= skill1ManaAmount && levelManager.Getlevel >= skill1)
        {
            playerOnClick.TargetPosion = transform.position;
            if (playerOnClick.FinishedMovent && fadeImages[0] != 1 && canAttack)
            {
                totalMana -= skill1ManaAmount;
                fadeImages[0] = 1;
                anim.SetInteger("Attack", 1);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && totalMana >= skill2ManaAmount && levelManager.Getlevel >= skill2)
        {
            playerOnClick.TargetPosion = transform.position;
            if (playerOnClick.FinishedMovent && fadeImages[1] != 1 && canAttack)
            {
                totalMana -= skill2ManaAmount;
                fadeImages[1] = 1;
                anim.SetInteger("Attack", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && totalMana >= skill3ManaAmount && levelManager.Getlevel >= skill3)
        {
            playerOnClick.TargetPosion = transform.position;
            if (playerOnClick.FinishedMovent && fadeImages[2] != 1 && canAttack)
            {
                totalMana -= skill3ManaAmount;
                fadeImages[2] = 1;
                anim.SetInteger("Attack", 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && totalMana >= skill4ManaAmount && levelManager.Getlevel >= skill4)
        {
            playerOnClick.TargetPosion = transform.position;
            if (playerOnClick.FinishedMovent && fadeImages[3] != 1 && canAttack)
            {
                totalMana -= skill4ManaAmount;
                fadeImages[3] = 1;
                anim.SetInteger("Attack", 4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && totalMana >= skill5ManaAmount && levelManager.Getlevel >= skill5)
        {
            playerOnClick.TargetPosion = transform.position;
            if (playerOnClick.FinishedMovent && fadeImages[4] != 1 && canAttack)
            {
                totalMana -= skill5ManaAmount;
                fadeImages[4] = 1;
                anim.SetInteger("Attack", 5);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && totalMana >= skill6ManaAmount && levelManager.Getlevel >= skill6)
        {
            playerOnClick.TargetPosion = transform.position;
            if (playerOnClick.FinishedMovent && fadeImages[5] != 1 && canAttack)
            {
                totalMana -= skill6ManaAmount;
                fadeImages[5] = 1;
                anim.SetInteger("Attack", 6);
            }
        }
        else
        {
            anim.SetInteger("Attack", 0);
        }


    }
    void CheckToFade()
    {

        for (int i = 0; i < fadeImages.Length; i++)
        {
            if (fadeImages[i] == 1)
            {
                if (FadeAndWait(coolDownIcon[i], coolDownTimes[i]))
                {
                    fadeImages[i] = 0;
                }
            }
        }

    }



    void CheckMana()
    {
        for (int i = 0; i < outOfManaIcons.Length; i++)
        {
            if (levelManager.Getlevel >= levelList[i])
            {
                if (totalMana < manaAmountList[i])
                {
                    outOfManaIcons[i].gameObject.SetActive(true);
                }
                else
                {
                    outOfManaIcons[i].gameObject.SetActive(false);
                }
            }
            
        }

    }

    void CheckLevel()
    {
        for (int i = 0; i < outOfManaIcons.Length; i++)
        {
            if(levelManager.Getlevel< levelList[i])
            {
                outOfManaIcons[i].gameObject.SetActive(true);
            }
        }
    }
    bool FadeAndWait(Image fadeImage, float fadeTime)
    {
        faded = false;
        if (fadeImage == null)
        {
            return faded;
        }

        if (!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }

        fadeImage.fillAmount -= fadeTime * Time.deltaTime;
        //Mathf.Epilon = en küçük float sayý
        if (fadeImage.fillAmount <= 0)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }
        return faded;

    }



    void TrunThePlayer()
    {
        Vector3 targetPos = Vector2.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);//skil atýðýmýz yerin kordinatý
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position),
            playerOnClick.trunSpeed * Time.deltaTime);
    }



}
