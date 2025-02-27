using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movemente variables
    public Rigidbody ricktusRB;
    public float movementSpeed;
    public float staminaLevel = 1;
    private Vector2 movementInput;

    //Dash variables

    public float dashSpeed;
    public float dashLength = 0.3F;
    public float dashBufferLength = 0.1F;
    private float dashBufferCounter;
    private bool isDashing;
    private bool hasDashed;
    private bool canDash; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



    }

    private void FixedUpdate()
    {
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
        //movementInput.Normalize();

        ricktusRB.linearVelocity = new Vector3(movementInput.x * movementSpeed, ricktusRB.linearVelocity.y, movementInput.y * movementSpeed);

        if (dashBufferCounter > 0F && !hasDashed) 
        {
            canDash = true;
        } 
        

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            dashBufferCounter = dashBufferLength;
            StartCoroutine(Dash(movementInput.x,movementInput.y));
        }
        else
        {
            dashBufferCounter = Time.deltaTime;
        }
    }

    IEnumerator Dash(float dashDirectionX, float dashDirectionZ)
    {
        float dashStartTime = Time.time;
        hasDashed = true;
        isDashing = true;
        Vector3 dashDirection;


        if (Input.GetKey(KeyCode.A))
        {
            dashDirection = new Vector3(-1F, 0F, 0F);
            Debug.Log("space key was pressed");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dashDirection = new Vector3(1F, 0F, 0F);
            Debug.Log("space key was pressed");
        }
        else if (Input.GetKey(KeyCode.S))
        {
            dashDirection = new Vector3(0F, 0F, -1F);
            Debug.Log("space key was pressed");
        }
        else if (Input.GetKey(KeyCode.W))
        {
            dashDirection = new Vector3(0F, 0F, 1F);
            Debug.Log("space key was pressed");
        }
        else
        {
            dashDirection = new Vector3(dashDirectionX, 0F, dashDirectionZ);
        }

        while (Time.deltaTime < dashStartTime + dashLength)
        {
            ricktusRB.linearVelocity = dashDirection * dashSpeed;

            yield return null;
        }

        isDashing = false;
    }
}
