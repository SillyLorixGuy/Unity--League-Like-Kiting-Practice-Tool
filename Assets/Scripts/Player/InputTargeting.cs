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

    // Update is called once per frame
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
    }
}
