using UnityEngine;
using System.Collections;
using System.Runtime.CompilerServices;
//------------------------------------------
public class AI_Enemy : MonoBehaviour
{
    //------------------------------------------
    public enum ENEMY_STATE { PATROL, CHASE, ATTACK };
    //------------------------------------------
    public ENEMY_STATE CurrentState
    {
        get { return currentstate; }

        set
        {
            //Update current state
            currentstate = value;

            //Stop all running coroutines
            StopAllCoroutines();

            switch (currentstate)
            {
                case ENEMY_STATE.PATROL:
                    StartCoroutine(AIPatrol());
                    break;

                case ENEMY_STATE.CHASE:
                    StartCoroutine(AIChase());
                    break;

                case ENEMY_STATE.ATTACK:
                    StartCoroutine(AIAttack());
                    break;
            }
        }
    }
    //------------------------------------------
    [SerializeField]
    private ENEMY_STATE currentstate = ENEMY_STATE.PATROL;

    //Reference to line of sight component
    private LineSight ThisLineSight = null;

    //Reference to nav mesh agent
    private UnityEngine.AI.NavMeshAgent ThisAgent = null;

    //Reference to player health
    private Health PlayerHealth = null;

    //Reference to player transform
    private Transform PlayerTransform = null;

    //Reference to patrol destination
    private Transform PatrolDestination = null;

    //Damage amount per second
    public float MaxDamage = 5f;
    public float attackInterval = 1f; // Set the attack interval to 5 seconds
    private float timeSinceLastAttack = 0f;

public GameObject effect;
    //------------------------------------------
    void Awake()
    {
        ThisLineSight = GetComponent<LineSight>();
        ThisAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        FindAndSetPlayerHealth();
    }

    private void FindAndSetPlayerHealth()
    {
        if (PlayerHealth == null || PlayerHealth.gameObject == null|| !PlayerHealth.gameObject.activeSelf)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                PlayerHealth = playerObject.GetComponent<Health>();
                if (PlayerHealth != null)
                {
                    PlayerTransform = PlayerHealth.transform;
                }
                else
                {
                    Debug.LogError("Player object does not have the Health component.");
                }
            }
            else
            {
                Debug.LogError("No GameObject with the 'Player' tag found.");
            }
        }
    }

    void Update()
    {
        // Check if the existing player is null or destroyed during runtime
        if (PlayerHealth == null || PlayerHealth.gameObject == null|| !PlayerHealth.gameObject.activeSelf)
        {
            FindAndSetPlayerHealth();
        }
    }

    //------------------------------------------
    void Start()
    {
        //Get random destination
        GameObject[] Destinations = GameObject.FindGameObjectsWithTag("Dest");
        PatrolDestination = Destinations[Random.Range(0, Destinations.Length)].GetComponent<Transform>();

        //Configure starting state
        CurrentState = ENEMY_STATE.PATROL;
    }
    //------------------------------------------
    public IEnumerator AIPatrol()
    {
        //Loop while patrolling
        while (currentstate == ENEMY_STATE.PATROL)
        {
            if (!ThisAgent.isOnNavMesh)
            {
                // Handle the case when the agent is no longer on the NavMesh
                yield break;
            }
            //Set strict search
            ThisLineSight.Sensitity = LineSight.SightSensitivity.STRICT;

            //Chase to patrol position
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(PatrolDestination.position);
            
            Animator anim = GetComponent<Animator>();
            anim.SetBool("Fight", false);
            effect.SetActive(false);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            //If we can see the target then start chasing
            if (ThisLineSight.CanSeeTarget)
            {
                ThisAgent.isStopped = true;
                CurrentState = ENEMY_STATE.CHASE;
                yield break;
            }

            //Have we arrived at dest, get new dest
            if (Vector3.Distance(transform.position, PatrolDestination.position) <= ThisAgent.stoppingDistance * 1.2f)
            {
                GameObject[] Destinations = GameObject.FindGameObjectsWithTag("Dest");
                PatrolDestination = Destinations[Random.Range(0, Destinations.Length)].GetComponent<Transform>();
            }

            //Wait until next frame
            yield return null;
        }
    }
    //------------------------------------------


    public IEnumerator AIChase()
    {
        //Loop while chasing
        while (currentstate == ENEMY_STATE.CHASE)
        {
            if (!ThisAgent.isOnNavMesh)
            {
                // Handle the case when the agent is no longer on the NavMesh
                yield break;
            }
            //Set loose search
            ThisLineSight.Sensitity = LineSight.SightSensitivity.LOOSE;

            //Chase to last known position
            ThisAgent.isStopped = false;
            ThisAgent.SetDestination(ThisLineSight.LastKnowSighting);

            Animator anim = GetComponent<Animator>();
            anim.SetBool("Fight", false);

            //Wait until path is computed
            while (ThisAgent.pathPending)
                yield return null;

            

            //Have we reached destination?
            if (ThisAgent.remainingDistance <= ThisAgent.stoppingDistance)
            {
                //Stop agent
                ThisAgent.isStopped = true;

                
                //Reached destination but cannot see player
                if (!ThisLineSight.CanSeeTarget)
                {
                    CurrentState = ENEMY_STATE.PATROL;
                }
                else //Reached destination and can see player. Reached attacking distance
                {
                    CurrentState = ENEMY_STATE.ATTACK;
                }

                yield break;
            }

            //Wait until next frame
            yield return null;
        }
    }
    //------------------------------------------

    public IEnumerator AIAttack()
    {
        // Loop while chasing and attacking
        while (currentstate == ENEMY_STATE.ATTACK && ThisAgent != null && ThisAgent.isOnNavMesh)
        {
            if (!ThisAgent.isOnNavMesh)
            {
                // Handle the case when the agent is no longer on the NavMesh
                yield break;
            }
            // Chase to player position
            ThisAgent.isStopped = false;

            if (ThisAgent != null)
            {
                ThisAgent.SetDestination(PlayerTransform.position);
                float distanceToPlayer = Vector3.Distance(transform.position, PlayerTransform.position);

                Animator anim = GetComponent<Animator>();
                anim.SetBool("Fight", true);
                effect.SetActive(true);
                // Wait until path is computed
                while (ThisAgent.pathPending)
                    yield return null;

                // Has the player run away?
                if (distanceToPlayer > 1f)
                // if (ThisAgent.remainingDistance > ThisAgent.stoppingDistance)
                {
                    // Change back to chase
                    CurrentState = ENEMY_STATE.PATROL;
                    yield break;
                }
                else
                {
                    // Check if enough time has passed since the last attack
                    if (timeSinceLastAttack >= attackInterval)
                    {
                        // Attack
                        PlayerHealth.HealthPoints -= MaxDamage;
                        timeSinceLastAttack = 0f; // Reset the timer
                    }
                }

                // Add the time passed since the last frame to the timer
                timeSinceLastAttack += Time.deltaTime;

                // Wait until the next frame
                yield return null;
            }
        }

        yield break;
    }

    //------------------------------------------
}
//------------------------------------------