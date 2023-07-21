using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MinionAI1 : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    public GameObject[] waypoints;
    private int currWaypoint;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currWaypoint = -1;
        setNextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1.0 && !agent.pathPending)
        {
            setNextWaypoint();
        }
        anim.SetFloat("vely", agent.velocity.magnitude / agent.speed);
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
                currWaypoint = 0;
            }

            agent.SetDestination(waypoints[currWaypoint].transform.position);
        }
        else
        {
            Debug.LogError("Waypoint array is empty");
        }
    }
}
