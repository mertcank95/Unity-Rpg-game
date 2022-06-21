using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnClick : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float trunSpeed = 15f;
    Animator anim;
    CharacterController controller;
    CollisionFlags collisionFlags = CollisionFlags.None;s
    //CollisionFlags karakterin move methodu için
    Vector3 playerMove = Vector3.zero;
    Vector3 targetMovePoint = Vector3.zero;

    float currentSpeed;
    float playerToPointDistance;
    float gravity = 9.8f;
    float height;


    bool canMove;
    bool finishedMovement = true;
    Vector3 newMovePoint;

    //attack iþlemleri
    Vector3 targetAttackPoint = Vector3.zero;
    Vector3 newAttackPoint;
    bool canAttack;
    public float attackRange=2f;
    GameObject enemy;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        currentSpeed = maxSpeed;
    }
    void Start()
    {

    }


    void Update()
    {
        CalculateHeight();
        CheckIfFinishedMovement();
        AttackMove();
    }

    bool IsGrounded()
    {
        return collisionFlags == CollisionFlags.CollidedBelow ? true : false;

    }
    void AttackMove()
    {
        if (canAttack)
        {
            targetAttackPoint = enemy.gameObject.transform.position;
            newAttackPoint = new Vector3(targetAttackPoint.x, transform.position.y, targetAttackPoint.z);
        }
        if(anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Base Attack"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newAttackPoint - transform.position), trunSpeed *2 * Time.deltaTime);
        }
    }
    void CalculateHeight()
    {
        if (IsGrounded())
        {
            height = 0f;
        }
        else
        {
            //eðer yerde deðilsek yerçeki mi uygulancak
            height -= gravity * Time.deltaTime;
        }

    }

    void CheckIfFinishedMovement()
    {
        if (!finishedMovement)
        {
            if (!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f) //IsInTransition = geçiþ sürcecinde deðilse//base layer = 0.layer
            {//GetCurrentAnimatorStateInfo=0.layerdaki animator bilgileri
             //normalizedTime :0 animasyon baþý 0.5 animasyon ortasý 1 animasyon sonu

                finishedMovement = true;

            }
        }
        else
        {
            MovePlayer();
            playerMove.y = height * Time.deltaTime;
            collisionFlags = controller.Move(playerMove);
        }
    }

    void MovePlayer()
    {
        if (Input.GetMouseButtonDown(1))//maouse sað tuþ
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;//rayin bize döndürdüðü deðeri yakalýyacaðýz
            if (Physics.Raycast(ray, out hit))//objeye deðdikten sonra hit bize veriyi getirir
            {
                
                playerToPointDistance = Vector3.Distance(transform.position, hit.point);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                   
                    if (playerToPointDistance >= 1.0f)
                    {
                        canMove = true;
                        canAttack = false;
                        targetMovePoint = hit.point;
                        
                    }
                }
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
                {
                    //hit.collider.gameObject:týklanan colliderin gameObjecti
                    enemy = hit.collider.gameObject.GetComponentInParent<EnemyHealt>().gameObject;
                    CanMove = true;
                    canAttack = true;

                }
            }

        }
        if (canMove)
        {
            anim.SetFloat("Speed", 1.0f);
            if (!canAttack)
            {
                newMovePoint = new Vector3(targetMovePoint.x, transform.position.y, targetMovePoint.z);

                //baktýðýmýz yöne dönüþ;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newMovePoint - transform.position), trunSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newAttackPoint - transform.position), trunSpeed * Time.deltaTime);

            }
            //transform.forward=karakterin önü
            playerMove = transform.forward * currentSpeed * Time.deltaTime;

            //sonsuza kadar gitmiyecek karakter
            if (Vector3.Distance(transform.position, newMovePoint) <= 0.6f &&  !canAttack)
            {
                canMove = false;
                canAttack = false;
            }
            else if (canAttack)
            {
                if (Vector3.Distance(transform.position, newAttackPoint) <= attackRange)
                {
                    playerMove.Set(0f, 0f, 0f);
                    anim.SetFloat("Speed", 0f);
                    targetAttackPoint = Vector3.zero;
                    anim.SetTrigger("AttackMove");
                    canAttack = false;
                    canMove = false;
                }
            }
        }
        else
        {
            playerMove.Set(0f, 0f, 0f);
            anim.SetFloat("Speed", 0f);

        }

    }


    public bool FinishedMovent
    {
        get
        {
          return finishedMovement;
        }

        set
        {
            finishedMovement = value;
        }

    }

    public bool CanMove
    {
        get
        {
            return canMove;
            
        }
        set
        {
            canMove = value;
            
        }
    }

    public Vector3 TargetPosion
    {
        get
        {
            return targetMovePoint;
        }
        set
        {
            targetMovePoint = value;
        }
    }

    
 


}
