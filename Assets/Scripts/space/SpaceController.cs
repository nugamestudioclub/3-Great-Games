using System.Collections;
using UnityEngine;

public class SpaceController : MonoBehaviour
{
    [ReadOnly]
    [SerializeField]
    private float xVelocity;
    [ReadOnly]
    [SerializeField]
    private float yVelocity;

    private float xInput;
    private float yInput;

    private float maxSpeed = 3;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private Object bullet;
    [SerializeField]
    private float shootDelay;

    private bool inShoot;

    [SerializeField]
    private AudioClip shootSound;

    //private Rigidbody2D rigidbody2D;
    private AudioSource audioSource;


    void Start()
    {
        xVelocity = 0;
        yVelocity = 0;
        inShoot = false;
        //rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponentInChildren<AudioSource>();
    }


    private bool IsMovingX() => !Mathf.Approximately(xVelocity, 0);
    
    private bool IsLessThanMaxSpeedX() => Mathf.Abs(xVelocity) < maxSpeed;
    private bool IsInputX() => !Mathf.Approximately(xInput, 0);

    

    private bool IsMovingY() => !Mathf.Approximately(yVelocity, 0);
    private bool IsLessThanMaxSpeedY() => Mathf.Abs(yVelocity) < maxSpeed;
    private bool IsInputY() => !Mathf.Approximately(yInput, 0);

    private static bool IsSameDirection(float a, float b) => Mathf.Approximately(Mathf.Sign(a), Mathf.Sign(b));



    void Update()
    {
        float xCurrentDir = Mathf.Sign(xVelocity);
        float yCurrentDir = Mathf.Sign(yVelocity);
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        float xInputDir = Mathf.Sign(xInput);
        float yInputDir = Mathf.Sign(yInput);

        //if not moving, no input -> clamp to 0
        //calulate change in velocity

        //velocity > max -> clamp to 0

        //dir * acceleration * time

        // float 
        if (!IsMovingX() && !IsInputX())
        {
            xVelocity = 0;
        } else
        {
            if (IsSameDirection(xInput,xVelocity) && IsLessThanMaxSpeedX())
            {
                xVelocity += xCurrentDir * acceleration * Time.deltaTime;
            } else
            {
                xVelocity -= xCurrentDir * acceleration * Time.deltaTime;
            }
        }

        if (!IsMovingY() && !IsInputY())
        {
            yVelocity = 0;
        }
        else
        {
            if (IsSameDirection(yInput, yVelocity) && IsLessThanMaxSpeedY())
            {
                yVelocity += yCurrentDir * acceleration * Time.deltaTime;
            }
            else
            {
                yVelocity -= yCurrentDir * acceleration * Time.deltaTime;
            }
        }

        /*

        if (Mathf.Abs(xVelocity) < maxSpeed && !Mathf.Approximately(xDir, 0) && Mathf.Approximately(Mathf.Sign(xInput), xDir))
        {

           xVelocity += xDir * acceleration * Time.deltaTime;

        } 
         if (!Mathf.Approximately(xVelocity, 0))
        {
            xVelocity -= Mathf.Max(xDir * acceleration * Time.deltaTime, 0);
        }

        if (Mathf.Abs(yVelocity) < maxSpeed && !Mathf.Approximately(yDir, 0) && Mathf.Approximately(Mathf.Sign(yInput), yDir))
        {

            yVelocity += yDir * acceleration * Time.deltaTime;

        }
         if (!Mathf.Approximately(yVelocity, 0))
        {
            yVelocity -= Mathf.Max(yDir * acceleration * Time.deltaTime, 0);
        }
        */

        ////
        /*
        if (Input.GetKey(KeyCode.D))
        {
            horzSpeed += acceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            horzSpeed -= acceleration * Time.deltaTime;
        }
        else if (Mathf.Abs(vertSpeed) > Mathf.Epsilon)
        {
            if (horzSpeed < 0)
            {
                horzSpeed += acceleration * Time.deltaTime;
            }
            else if (horzSpeed > 0)
            {
                horzSpeed -= acceleration * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            vertSpeed -= acceleration * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            vertSpeed += acceleration * Time.deltaTime;
        }
        else if (Mathf.Abs(vertSpeed) > Mathf.Epsilon)
        {
            if (vertSpeed < 0)
            {
                vertSpeed += acceleration * Time.deltaTime;
            }
            else if (vertSpeed > 0)
            {
                vertSpeed -= acceleration * Time.deltaTime;
            }

        }
        */
        float newX = xVelocity * Time.deltaTime;
        float newY = yVelocity * Time.deltaTime;

        transform.Translate(new Vector2(newX, newY));// = new Vector2(newX + transform.position.x, newY + transform.position.y);

        if (Input.GetKey(KeyCode.Space) && !inShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        inShoot = true;
        audioSource.PlayOneShot(shootSound);
        Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.15f, this.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(shootDelay);
        inShoot = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HorzWall") || collision.gameObject.CompareTag("HorzWallTop"))
        {
            yVelocity = 0;
        }
        else if (collision.gameObject.CompareTag("VertWall"))
        {
            xVelocity = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameMemory.Instance.Corrupt();
            TransitionManager.ToMenu();
        }
    }
}