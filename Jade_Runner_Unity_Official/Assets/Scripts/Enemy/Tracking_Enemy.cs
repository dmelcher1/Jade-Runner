using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Tracking_Enemy : BaseEnemy
{
    public bool randomPatrol;
    public bool strictPatrol;
    public bool randPatrolTime;
    //For pause menu???
    public bool reset;

    public Transform resetPos;
    public Transform[] patrolPoints;
    private NavMeshAgent statuePuppet;
    //private GameObject playerLoc;
    
    private float followDist;
    [SerializeField]
    private float resetTimer = 30.0f;

    private int currentPatrolPoint = 0;
    public float patrolRadius;
    public float patrolTimerStart;
    private float patrolTimer;
    private float timerCount;
    public float attackTimer;
    private float statueDist;
    private float startTimer;
    //private float hurtLayerWeight;
    private float playerProximity;

    public bool chase;
    public bool patroling;
    public bool attack;
    public bool idle;

    private void Awake()
    {
        
        statuePuppet = GetComponent<NavMeshAgent>();

        //if(!randomPatrol && strictPatrol && patroling)
            //SetPatrolPoint();
        //else if(!randomPatrol && !strictPatrol && patroling)
            //SetRandomPatrolPoint();
    }

    // Start is called before the first frame update
    void Start()
    {
        patrolTimer = patrolTimerStart;
        timerCount = patrolTimer;
        patroling = true;
        startTimer = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        startTimer -= 0.1f;
        statueAnim.SetBool("Chase", chase);
        statueAnim.SetBool("Idle", idle);
        statueAnim.SetBool("Patrol", patroling);
        attackTimer -= 0.1f;

        statueDist = statuePuppet.remainingDistance;
        //Debug.Log(remainingDistance);
        playerProximity = Vector3.Distance(playerLocation.transform.position, transform.position);

        if(playerLocomotion.dead)
        {
            resetTimer -= 0.1f;
            if(resetTimer < 0)
            {
                transform.position = resetPos.position;
                attack = false;
                chase = false;
                patroling = true;
            }
        }
        else
        {
            resetTimer = 30.0f;
        }
        //From pause menu???
        if (reset)
        {
            this.transform.position = resetPos.transform.position;
        }
        
        if(statueDist == 0)
        {
            idle = true;
        }
        else
        {
            idle = false;
        }

        //Reduntant???
        //float playerDist = Vector3.Distance(playerLocation.transform.position, this.transform.position);

        if(!chase)
        {
            statuePuppet.stoppingDistance = 0;
        }

        if(playerProximity < followDist)
        {
            //PursuePlayer();
            patroling = false;
            chase = true;
            //Debug.Log("Chasing!")
        }
        else if (playerProximity > followDist)
        {
            patroling = true;
            chase = false;
        }

        if (patroling)
            timerCount += Time.deltaTime;

        if (timerCount > patrolTimer)
        {
            if (randomPatrol && patroling)
            {
                //NextPatrolPoint();
            }
            else if (!randomPatrol && strictPatrol && patroling)
            {
                //if (!statuePuppet.pathPending && statuePuppet.remainingDistance < 0.5f)
                    //SetPatrolPoint();
            }
            else if (!randomPatrol && !strictPatrol && patroling)
            {
                //if (!statuePuppet.pathPending && statuePuppet.remainingDistance < 0.5f)
                    //SetRandomPatrolPoint();
            }

            if (randPatrolTime)
            {
                patrolTimer = Random.Range(patrolTimerStart / 2, patrolTimerStart);
            }
            else
            {
                patrolTimer = patrolTimerStart;
            }
            timerCount = 0f;
        }


        //attack order code, needs modifying for this
        //if(playerProximity < 2.0f && !playerLocomotion.dead)
        //{
        //    statueAnim.SetBool("Attack", attack);
        //    if(attackTimer <= 0)
        //    {
        //        attack = true;
        //        attackTimer = 10.0f;
        //    }
        //    else
        //    {
        //        attack = false;
        //    }
        //    if(attack)
        //    {
        //        playerLocomotion.health -= 1;
        //    }
        //}

    }

    //modify as needed for rhino, may pull over to that script
    void NextPatrolPoint()
    {
        Vector3 newPatrolPoint;

        newPatrolPoint = RandomNavSphere(transform.position, patrolRadius, -1);
        statuePuppet.SetDestination(newPatrolPoint);

        //else if(!randomPatrol)
        //{
        //    newPatrolPoint = SetPatrolPoint();
        //    enemyAgent.SetDestination(newPatrolPoint);
        //}
    }

    //modify as needed for rhino, may pull over to that script
    Vector3 RandomNavSphere(Vector3 originPos, float radiusMult, int layerMask)
    {
        Vector3 randDir = Random.insideUnitSphere * radiusMult;
        randDir += originPos;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, radiusMult, layerMask);

        return navHit.position;
    }

    void SetPatrolPoint()
    {

    }

    void PursuePlayer()
    {

    }
}
