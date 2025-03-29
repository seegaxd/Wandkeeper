using UnityEngine;
using UnityEngine.AI;

public class AvoidMove : MonoBehaviour
{
    private float stopDistance;
    private Vector3 avoidDirection;
    private bool isAvoiding = false;

    public AvoidMove()
    {
        stopDistance = Random.Range(3f, 7f);
    }

    public void Execute(NavMeshAgent agent, Transform target)
    {
        float distance = Vector3.Distance(agent.transform.position, target.position);
        
        if (distance > stopDistance)
        {
            agent.SetDestination(target.position);
            isAvoiding = false;
        }
        else
        {
            if (!isAvoiding)
            {
                Vector3 offset = (agent.transform.position - target.position).normalized * 3f;
                avoidDirection = agent.transform.position + Quaternion.Euler(0, Random.Range(-60, 60), 0) * offset;
                isAvoiding = true;
            }
            agent.SetDestination(avoidDirection);
        }
    }
}
