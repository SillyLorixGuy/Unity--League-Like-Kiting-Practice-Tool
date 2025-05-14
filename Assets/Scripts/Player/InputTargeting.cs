using UnityEngine;

public class InputTargeting : MonoBehaviour
{
    

    public GameObject selectedHero;
    public bool heroPlayer;
    RaycastHit hit;
    
    void Start()
    {
        selectedHero = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if(hit.collider.GetComponent<IsTargetable>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<IsTargetable>().enemyType == IsTargetable.EnemyType.Enemy)
                    {
                        selectedHero.GetComponent<HeroCombat>().targetedEnemy = hit.collider.gameObject;
                    }
                }
                else if(hit.collider.gameObject.GetComponent<IsTargetable>() == null)
                {
                    selectedHero.GetComponent<HeroCombat>().targetedEnemy = null;
                }
            }
        }
        else if(Input.GetKeyDown("a"))
        {
            IsTargetable[] trgts = FindObjectsByType<IsTargetable>(FindObjectsSortMode.None);
            GameObject closest = trgts[0].gameObject;
            Vector3 currentMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            foreach (var item in trgts)
            {
                if(Vector3.Distance(currentMousePos, item.transform.position) < Vector3.Distance(currentMousePos, closest.transform.position))
                {
                    closest = item.gameObject;
                }
            }
            Debug.DrawRay(closest.transform.position, closest.transform.position + Vector3.up * 10, Color.red, 5);
            if (closest.GetComponent<IsTargetable>() != null)
            {
                if (closest.GetComponent<IsTargetable>().enemyType == IsTargetable.EnemyType.Enemy) //fix this BS
                {
                    selectedHero.GetComponent<HeroCombat>().targetedEnemy = closest;
                }
            }
            else if (closest.GetComponent<IsTargetable>() == null)
            {
                selectedHero.GetComponent<HeroCombat>().targetedEnemy = null;
            }
        }
    }
}
