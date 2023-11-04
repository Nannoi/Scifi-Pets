using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.AI;

public class MoveToDestination : MonoBehaviour
{
    // Start is called before the first frame update
    private NavMeshAgent ThisAgent = null;
    public Transform Dest =null;
    void Start()
    {
        ThisAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ThisAgent.SetDestination(Dest.position);
    }
}
