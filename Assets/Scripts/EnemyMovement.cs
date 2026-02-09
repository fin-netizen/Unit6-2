using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float minDist = 1f;
    public Transform target;
    Rigidbody rb;
    public bool isPartrolling;
    public Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target == null)
        {
            if(GameObject.FindWithTag("Player")!=null)
            {
                target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            }
        }
        anim.SetBool("isPartrolling", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;
        transform.LookAt(target);
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance > minDist)
            transform.position += transform.forward * speed * Time.deltaTime;
        anim.SetBool("isPartrolling", true);
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
