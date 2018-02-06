using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit_Movement : MonoBehaviour {

   public float attack = 0;
   public float fatigue = 0;
   public float disapline = 0;
   public float health = 1;
   
   public int morale = 0;

   private Vector3 goal ;

   private GameObject enemyPos;

   private bool isAttacking = false;
   private bool isDead = false;
   public bool isRunningAway = false;
   private bool isExhausted = false;

   private Vector3 avgEnemyPos;
    private Vector3 direction;

    public List<GameObject> engagedUnits; 

   private float time = 0;

    NavMeshAgent agent;

    // private Vector3 goal;
    // public bool unitSelected = false;

    // Use this for initialization
    void Start ()
    {
      goal = this.transform.position;

      agent = GetComponent<NavMeshAgent>();

       avgEnemyPos = Vector3.zero;
        direction = Vector3.zero;
       //agent.destination = goal.position;
    }

    // Update is called once per frame
    void Update()
    {
       // if (!isExhausted)
       // {
            agent.destination = goal;
        //}

        if (health <= 0)
        {
            isDead = true;

            Destroy(this);
        }

        time += Time.deltaTime;

        //  Debug.Log(time);

        if (time > 1.0f)
        {
            if //(agent.remainingDistance !=0) 
            (agent.hasPath)
            {
                fatigue += 1.0f;
            }
            else 
            {
                if (fatigue != 0)
                {
                    fatigue -= 1.0f;
                }
            }
            time = 0;
        }
        if (fatigue >= disapline)
        {
            //  agent.ResetPath();
            // if (goal != this.transform.position)
            //{
            //goal = this.transform.position;
            // }
            goal = transform.position;
            isExhausted = true;
            //agent.Stop();
            agent.ResetPath();
        }

        if(isExhausted == true && !isRunningAway)
        {
            //agent.Stop()
            agent.ResetPath(); 
            if(fatigue <= disapline + (disapline/10))
            {
                isExhausted = false;
            }
        }

        if (engagedUnits.Count > 0)
        {
            for (int i = 0; i < engagedUnits.Count; i++)
            {
                if (engagedUnits[i].gameObject.GetComponent<Enemy>().attack >= health)
                {
                    isRunningAway = true;
                }
            }
        }

        if (isRunningAway)
        {
            for (int i = 0; i < engagedUnits.Count ; i++)
            {
                avgEnemyPos += engagedUnits[i].transform.position;
                
            }
            avgEnemyPos /= engagedUnits.Count;

            direction = Vector3.Normalize(transform.position - avgEnemyPos);
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position + direction, out hit, 1.0f, NavMesh.AllAreas))
            {
                    
                goal = hit.position;
            }
            Debug.Log(gameObject.name + "is doing the run away function");

            direction = Vector3.zero;
            avgEnemyPos= Vector3.zero;

        }

        if(engagedUnits.Count == 0 && time >= 0.5)
        {
            isRunningAway = false;
            isAttacking = false;
        }
       // if (isAttacking)
       // {
            
         //   Debug.Log("stop should work here");
       // }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            isAttacking = true;
            Debug.Log("is attacking " + isAttacking + " an enemy called " + collision.gameObject.name);

            
            engagedUnits.Add(collision.gameObject);



          //  agent.Stop(true);

            // if(attack > collision.gameObject.GetComponent<Enemy>().amour)
            // {

            // }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            engagedUnits.Remove(collision.gameObject);

            isAttacking = false;
            Debug.Log(gameObject.name +"are you attacking" + isAttacking);
            enemyPos = null;
        }
    }

    public void SetUnitGoal(Vector3 pos)
    {
        goal = pos;
        Debug.Log("unit was given goal "+ goal);
    }

    public void SetIsRetreating(bool doRetreat)
    {
        //isRunningAway = doRetreat;
        Debug.Log("unit is running away " );
    }


    public int GetMorale()
    {

        return morale;

    }

    public bool GetIsDead()
    {

        return isDead;

    }
}
