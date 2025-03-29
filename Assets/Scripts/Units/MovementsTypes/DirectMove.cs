using UnityEngine;
using UnityEngine.AI;
public class DirectMove : MonoBehaviour
{
    public void Execute(NavMeshAgent agent, Transform target)
    {
        agent.SetDestination(target.position);
    }
}
