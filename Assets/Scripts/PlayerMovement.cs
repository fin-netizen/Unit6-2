using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public enum States // used by all logic
{
    None,
    Idle,
    Walk,
    Run,
    Dead,
}
public class PlayerMovement : MonoBehaviour
{
    States state;
    int playerHealth;
    public CharacterController controller;
    public Transform cam;
    public bool isWalking;
    public bool isRunning;
    public float speed = 6f;
    public float runSpeed = 8f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    InputAction moveAction;
    InputAction jumpAction;
    Rigidbody rb;
    float xvel, yvel, zvel;
    public Animator anim;
    public Transform respawnPoint;
    float timer;

    private void Start()
    {
        timer = 4;
        state = States.Idle;
        rb = GetComponent<Rigidbody>();

        playerHealth = 3;

        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;
        zvel = rb.linearVelocity.z;
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");

        


    }
    // Update is called once per frame
    void Update()
    {
        DoLogic();
    }
   
    public void DoLogic()
    {
        if (state == States.Idle)
        {
            DoIdle();
        }


        if (state == States.Walk)
        {
            DoMove();
            CheckForDeath();
        }
        if(state == States.Dead)
        {
            IsDead();
        }
        if(state == States.Run)
        {
            //DoRun();
            //CheckForDeath();
        }
    }

    /*
    public void DoRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            state = States.Walk;
            anim.SetBool("IsRunning", false);
        }
    }
    */

    void DoIdle()
    {
        if( moveAction.ReadValue<Vector2>().magnitude > 0.1f )
        {
            state = States.Walk;
        }


    }

    public void IsDead()
    {
        anim.SetBool("IsDying", true);
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            RespawnPlayer();
        }
        
    }
    public void DoMove()
    {
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
            rb.linearVelocity = new Vector3(vel.x, rb.linearVelocity.y, vel.z);
            anim.SetBool("IsWalking", true);
        }
        else
        {
           state = States.Idle;
           anim.SetBool("IsWalking", false);
        }
    }
    void RespawnPlayer()
    {
        transform.position = respawnPoint.position;
        state = States.Idle;
    }

    void CheckForDeath()
    {
        if (playerHealth == 0)
        {
            state = States.Dead;
            playerHealth = 3;
        }

    }

    void OnGUI()
    {
        string text = "state:" + state;
        text += "\n";

        GUI.Label(new Rect(10f, 10f, 200f, 200f), text);
    }
}
