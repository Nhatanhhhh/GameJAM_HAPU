using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 destination)
    {
        if (agent.isOnNavMesh)
            agent.SetDestination(destination);
    }

    public bool IsMoving()
    {
        return agent.remainingDistance > agent.stoppingDistance;
    }
}
