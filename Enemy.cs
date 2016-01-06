using UnityEngine;
using System.Collections;

public class Enemy : Entity {

    public float attackInterval = 2f;

    float attackThreshold = 1.5f;

    public float rayLength;
    public LayerMask layerMask;

    public enum MyState { Chasing, Attacking };

    Material skinMaterial;
    Color originalColour;

    MyState currentState;

    Transform target;
    NavMeshAgent nav;


    float nextAttackTime;
    float myCollisionRadius;
    float targetCollisionRadius;

    protected override void Start()
    {
        base.Start();

        skinMaterial = GetComponent<Renderer>().material;
        originalColour = skinMaterial.color;

        currentState = MyState.Chasing;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        nav = GetComponent<NavMeshAgent>();

        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
    }

    void Update()
    {
        if(Time.time > nextAttackTime)
        {
            float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
            if(sqrDstToTarget < Mathf.Pow(attackThreshold + myCollisionRadius + targetCollisionRadius, 2))
            {
                nextAttackTime = Time.time + attackInterval;
                StartCoroutine(Attack());
            }
        }

        FindTarget();

        if (health <= 0) {
            base.Die();
        }
    }

    IEnumerator UpdatePath()
    {
        float refreshRate = 0.25f;

        while(target != null)
        {
            if(currentState == MyState.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPos = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackThreshold/2);
                nav.SetDestination(targetPos);
            }
        }
        yield return new WaitForSeconds(refreshRate);

        
    }

    IEnumerator Attack()
    {
        skinMaterial.color = Color.yellow;

        Vector3 originalPos = transform.position;
        Vector3 attackPos = target.position;

        float attackSpeed = 3f;
        float percent = 0f;

        while(percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;

            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;

            transform.position = Vector3.Lerp(originalPos, attackPos, interpolation);

            yield return null;
        }

        skinMaterial.color = originalColour;
    }

    void FindTarget()
    {
        if (currentState == MyState.Chasing)
        {
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            Vector3 targetPos = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackThreshold/2);
            nav.destination = targetPos;
        }
        

        //RaycastHit hit;

        //Ray ray = new Ray(transform.position, Vector3.forward);

        //if(Physics.Raycast(transform.position , transform.forward, out hit, rayLength, layerMask))
        //{
        //    if(hit.collider.CompareTag("Player"))
        //    {
        //        nav.destination = hit.transform.position;
        //    }
        //}
        //else
        //{
        //    nav.destination = new Vector3(0, 0, 0);
        //}
    }
}
