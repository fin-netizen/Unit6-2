using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public enum States // used by all logic
{
    None,
    Idle,
    Walk,
}
public class PlayerMovement : MonoBehaviour
{
    States state;
    public CharacterController controller;
    public Transform cam;
    public bool IsWalking;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody rb;
    float xvel, yvel, zvel;
    public Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = States.Idle;

        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;
        zvel = rb.linearVelocity.z;
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

    }
    // Update is called once per frame
    void Update()
    {
        DoMove();
        DoJump();

    }
    public void DoJump()
    {
        

        /*
        if(Input.GetKey("w"))
        {
            zvel = 5f;
        }
        if (Input.GetKey("s"))
        {
            zvel = -5f;
        }
        if (Input.GetKey("a"))
        {
            xvel = 5f;
        }
        if (Input.GetKey("d"))
        {
            zvel = -5f;
        }
        */
        if (jumpAction.IsPressed())
        {
  
        }
        
    }
    public void DoMove()
    {
        //old input system
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");

        //new input system
        float horizontal = moveAction.ReadValue<Vector2>().x;
        float vertical = moveAction.ReadValue<Vector2>().y;

        //Vector3 playerVelocity;

        Vector2 direction = moveAction.ReadValue<Vector2>().normalized;// new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            Vector3 vel = moveDir.normalized * speed;
            //direction *= speed;
            //Vector3 vel = moveDir.normalized * speed;

            //playerVelocity.x = vel.x;
            //playerVelocity.z = vel.z;
            //anim.SetBool("IsWalking", true);
            //controller.Move(moveDir.normalized * speed * Time.deltaTime); 
            rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);
        }
        else
        {
            //state = States.Idle;
        }
    }
}
