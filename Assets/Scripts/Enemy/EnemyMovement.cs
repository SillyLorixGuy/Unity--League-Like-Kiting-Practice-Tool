using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    public bool isStopped;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isStopped = !isStopped;
        }
         agent.destination = player.transform.position;
         agent.isStopped = isStopped;
    }
}
