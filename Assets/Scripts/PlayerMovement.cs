using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public bool IsWalking;
    public float speed = 6f;
    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody rb;
    float xvel, yvel, zvel;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();


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


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            direction *= speed;
            //controller.Move(direction * speed * Time.deltaTime); 
            rb.linearVelocity = new Vector3(direction.x, rb.linearVelocity.y, direction.z);
        }
    }
}
