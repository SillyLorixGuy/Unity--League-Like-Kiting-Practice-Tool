using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{

    public NavMeshAgent agent;
    public float rotateSpeedMovement = 0.05f;
    public float rotateVelocity;

    private HeroCombat heroCombatScript;
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        heroCombatScript = GetComponent<HeroCombat>();
    }

    void Update()
    {
        Move();
    }

    public void Animation()
    {
        
    }

    public void Move()
    {
        if (heroCombatScript.targetedEnemy != null)
        {
            if (heroCombatScript.targetedEnemy.GetComponent<HeroCombat>() != null)
            {
                if (!heroCombatScript.targetedEnemy.GetComponent<HeroCombat>().isHeroAlive)
                {
                    heroCombatScript.targetedEnemy = null;
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if(hit.collider.tag == "Ground")
                {
                    agent.SetDestination(hit.point);
                    heroCombatScript.targetedEnemy = null;
                    agent.stoppingDistance = 0;

                    Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                    float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 5));

                    transform.eulerAngles = new Vector3(0, rotationY, 0);
                }
            }
        }
    }
}
