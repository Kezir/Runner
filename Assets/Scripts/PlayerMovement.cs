using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side
{
    left,
    middle,
    right
}
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRb;

    public Side side = Side.middle;

    private bool left, right, up;

    public Vector3 directionRay;
    private RaycastHit hit;

    float moveToX = 0f;

    public float moveWidth;

    private CharacterController player;

    private float tempX, tempY;

    public float dodgeSpeed;
    public float jumpForce;
    public float speed;
    private float speedAnim = 0.5f;
    private Vector3 moveVector;
    public bool alive;
    public ParticleSystem particle;
    public Animator anim;
    private ParticleSystem.MainModule main;

    private static PlayerMovement _instance;
    private Touch touch;
    private Vector2 initialPosition;

    public static PlayerMovement Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        alive = true;
        player = GetComponent<CharacterController>();
        transform.position = Vector3.zero;
        InvokeRepeating("SpeedUp", 10, 10);
        main = particle.main;
    }

    void Update()
    {
        if(alive)
        {
            Inputs();

            if (player.isGrounded)
            {
                MoveSides();
            }
            Jump();
            moveVector = new Vector3(tempX - transform.position.x, tempY * Time.deltaTime, speed * Time.deltaTime);
            tempX = Mathf.Lerp(tempX, moveToX, Time.deltaTime * dodgeSpeed);
            player.Move(moveVector);
        }
        
    }

    private void SpeedUp()
    {
        speed += 0.1f;
        main.startSpeedMultiplier += 0.5f;
        anim.SetFloat("Blend", speedAnim);
        if(speedAnim<1f)
        {
            speedAnim += 0.05f;
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Kill();
        }
    }

    public void Kill()
    {
        alive = false;
        Time.timeScale = 0;
        GameController.Instance.OpenLosePanel();
    }

    private void Jump()
    {
        if(player.isGrounded)
        {
            if (up)
            {
                tempY = jumpForce;              
            }
        }
        else
        {
            tempY -= jumpForce * 2 * Time.deltaTime;
        }
        
    }

    private void Inputs()
    {
        left = SwipeManager.swipeLeft;//Input.GetKeyDown(KeyCode.LeftArrow);
        right = SwipeManager.swipeRight;//Input.GetKeyDown(KeyCode.RightArrow);
        up = SwipeManager.swipeUp;//Input.GetKeyDown(KeyCode.UpArrow);
    }

    private bool CheckForCollisions(Side _side)
    {

        // Only move if we wouldn't hit something 
        if(_side == Side.left )
        {
            directionRay = Vector3.left;
        }
        else
        {
            directionRay = Vector3.right;
        }

        if (playerRb.SweepTest(directionRay, out hit, moveWidth))
        {
            //Debug.Log("true");
            directionRay = Vector3.zero;
            return true;
        }
        else
        {
            //Debug.Log("false");
            directionRay = Vector3.zero;
            return false;
        }

    }

    public void MoveToMiddle()
    {
        moveToX = 0;
        side = Side.middle;
    }

    public void MoveBack()
    {
        //gameObject.transform.position -= new Vector3(0, 0, 10);
    }

    private void MoveSides()
    {

        if (left)
        {
            if(CheckForCollisions(Side.left))
            {
                return;
            }

            if (side == Side.middle)
            {
                moveToX -= moveWidth;
                side = Side.left;
            }
            else if (side == Side.right)
            {
                moveToX = 0;
                side = Side.middle;
            }
        }
        else if (right)
        {
            if(CheckForCollisions(Side.right))
            {
                return;
            }

            if (side == Side.middle)
            {
                moveToX = moveWidth;
                side = Side.right;
            }
            else if (side == Side.left)
            {
                moveToX = 0;
                side = Side.middle;
            }
        }

    }

}
