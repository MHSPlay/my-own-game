using UnityEngine;
using UnityEngine.AI;

public class SimpleMobNavMesh : MonoBehaviour
{
    public NavMeshAgent agent;
    public float Radius = 10f;
    public float delayBetweenMoves = 5f;

    private float nextMoveTime = 0f;

    void Start()
    {
        if (agent == null)
            agent = GetComponent<NavMeshAgent>();
        

        MoveToRandomPoint();
    }

    void Update()
    {
        if (Time.time >= nextMoveTime)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                MoveToRandomPoint();
                nextMoveTime = Time.time + delayBetweenMoves;
            }
        }
    }

    void MoveToRandomPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * Radius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, Radius, NavMesh.AllAreas))
            agent.SetDestination(hit.position);
        
    }
}
