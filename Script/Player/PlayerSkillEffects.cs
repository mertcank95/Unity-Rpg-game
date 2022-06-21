using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillEffects : MonoBehaviour
{
    [Header("Skill Effects")]
    public GameObject hammerSkill;
    public GameObject kickSkill;
    public GameObject spellCastSkill;
    public GameObject healSkill;
    public GameObject shiedlSkill;
    public GameObject comboSkill;
    [Header("Skill Transforms")]
    public Transform kickTransform;
    public Transform spellTransform;
    public Transform hammerTransform;
    public Transform comboTransform;
    
    void HammerSkillCast()
    {
        Instantiate(hammerSkill, hammerTransform.position,transform.rotation);
    }

    void KickSpellCast()
    {
        Instantiate(kickSkill, kickTransform.position, Quaternion.identity);

    }

    void SpellCast()
    {
        Instantiate(spellCastSkill, spellTransform.position, Quaternion.identity);

    }

    void SlashComboCast()
    {
        Instantiate(comboSkill, comboTransform.position, Quaternion.identity);

    }

    void ShieldCast()
    {
        Vector3 pos = transform.position;
        
        GameObject shieldClone = Instantiate(shiedlSkill, pos, Quaternion.identity);
        shieldClone.transform.SetParent(transform);
    }

    void HealCast()
    {
        Vector3 pos = transform.position;
        GameObject healClone =  Instantiate(healSkill, pos, Quaternion.identity);
        healClone.transform.SetParent(transform);//healskill effecti bize takip edicek(bir çocuðu olarak)
    }

}
