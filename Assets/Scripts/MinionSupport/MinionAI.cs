using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MinionAI : MonoBehaviour
{
    public enum AIState
    {
        Static,
        Moving
    };
    
    private NavMeshAgent agent;
    private Animator anim;
    public GameObject[] waypoints;
    public GameObject movingWaypoint;
    private int currWaypoint;
    public AIState aistate;
    public GameObject marker;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 5f;
        aistate = AIState.Static;
        currWaypoint = -1;
        setNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        switch (aistate)
        {
            case AIState.Static:
                if (agent.remainingDistance < 1.0 && !agent.pathPending)
                {
                    setNextWaypoint();
                }
                anim.SetFloat("vely", agent.velocity.magnitude / agent.speed);
                break;

            case AIState.Moving:
                if (Vector3.Distance(this.transform.position, movingWaypoint.transform.position) < 1.0)
                {
                    aistate = AIState.Static;
                    currWaypoint = -1;
                }
                else
                {
                    predictWaypoint();
                }
                break;

            default:
                break;
        }
    }

    private void setNextWaypoint()
    {
       if (waypoints.Length > 0)
        {
            if (currWaypoint < waypoints.Length - 1)
            {
                currWaypoint++;
            }
            else
            {
                aistate = AIState.Moving;
            }

            agent.SetDestination(waypoints[currWaypoint].transform.position);
            marker.transform.position = waypoints[currWaypoint].transform.position;
        }
        else
        {
            Debug.LogError("Waypoint array is empty");
        }
    }

    private void predictWaypoint()
    {
        float dist = Vector3.Distance(this.transform.position, movingWaypoint.transform.position);
        float lookAheadT = dist / agent.speed;
        Mathf.Clamp(lookAheadT, 0f, 10f);

        Vector3 target = movingWaypoint.transform.position + (lookAheadT * movingWaypoint.GetComponent<VelocityReporter>().velocity);
        NavMeshHit hit;
        if (agent.Raycast(target, out hit))
        {
            if (target.z > 0)
            {
                target.z = 17f;
            }
            else
            {
                target.z = -17f;
            }
        }

        agent.SetDestination(target);
        marker.transform.position = target;
    }
}
