using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public NavMeshAgent agent;
    public float rotateSpeedMovement = 0.05f;
    public float rotateVelocity;

    public GameObject greenIndicatorPrefab;
    public GameObject redIndicatorPrefab;
    public LayerMask groundLayer;

    private HeroCombat heroCombatScript;
    private GameObject currentIndicator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        heroCombatScript = GetComponent<HeroCombat>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right-click to move
        {
            HandleClick(greenIndicatorPrefab, clearTarget: true);
        }

        if (Input.GetKeyDown(KeyCode.A)) // Attack move (A)
        {
            HandleClick(redIndicatorPrefab, clearTarget: false);
        }

        CleanupEnemyIfDead();
    }

    void HandleClick(GameObject indicatorPrefab, bool clearTarget)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                // Move the agent
                agent.SetDestination(hit.point);
                agent.stoppingDistance = 0;

                if (clearTarget)
                {
                    heroCombatScript.targetedEnemy = null;
                }

                // Rotate towards movement point
                Quaternion rotationToLookAt = Quaternion.LookRotation(hit.point - transform.position);
                float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * Time.deltaTime * 5);
                transform.eulerAngles = new Vector3(0, rotationY, 0);

                // Spawn indicator
                if (currentIndicator != null) Destroy(currentIndicator);
                currentIndicator = Instantiate(indicatorPrefab, hit.point + Vector3.up * 0.1f, Quaternion.identity);
            }
        }
    }

    void CleanupEnemyIfDead()
    {
        if (heroCombatScript.targetedEnemy != null)
        {
            HeroCombat enemyCombat = heroCombatScript.targetedEnemy.GetComponent<HeroCombat>();
            if (enemyCombat != null && !enemyCombat.isHeroAlive)
            {
                heroCombatScript.targetedEnemy = null;
            }
        }
    }
}
