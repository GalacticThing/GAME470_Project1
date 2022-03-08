using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathFollow : MonoBehaviour

{    // Local Variables
    public Transform[] waypoints; // the array containing the waypoints of the car
    public float moveSpeed = 0f; // controls the rate at which the car moves along the path
    private int waypointIndex = 0; // used to identify waypoints and keep the car on the path
    public float accelerationInput = 0; // checks it the player is pressing forward or back
    public float turnSpeedLimit; // when does a player spinout during a turn
    private float rotZ;
    public float rotationSpeed; // for when the car spins out
    public float stunTime;
    public bool spinOut;

    protected float velocityVsUp = 0;

    public float maxSpeed; // the maximum speed the car can travel
    public float minSpeed; // value used to prevent the car from totally reversing

    // Lap counter
    private int lapCounter = 1; //keeps track of a cars current Lap

    public int totalLaps = 3;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A)) // Acceleration Input
        {
            accelerationInput = 1;
            print("accelerate");
        }
        /*
        else if(Input.GetKeyUp(KeyCode.A))
        {
            //accelerationInput = 0;
            print("slow down");
        }
        */
        else if (Input.GetKey(KeyCode.D)) // Decceleration Input
        {
            accelerationInput = -1;
            print("Brake");
        }
        ApplyForwardMovement();
        Move();
    }

    void ApplyForwardMovement()
    {
        //Limits how fast the car can drive forward
        if (moveSpeed > maxSpeed && accelerationInput > -1)
        {
            return;
        }

        //Limits how fast the car can move in reverse
        if (moveSpeed < minSpeed && accelerationInput <= 0)
        {
            return;
        }
        
        if(accelerationInput == 1)
        {
            moveSpeed += 0.08f;
            //print("Accelerate" + moveSpeed);
        }
        else if(accelerationInput == 0)
        {
            moveSpeed += 0.01f;
            if(moveSpeed >= 1 && accelerationInput == 0)
            {
                rotZ += Time.deltaTime * rotationSpeed;
                transform.rotation = Quaternion.Euler(0, 0, rotZ);
            }
            
            
        }
        else if(accelerationInput == -1)
        {
            //Apply Brakes
            moveSpeed -= 0.05f;
            //print("Deccelerate" + moveSpeed);
        }

        
    }

    public IEnumerator SpinOut()
    {
        spinOut = true;
        rotZ +=  Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        moveSpeed = 0.1F; // slow down car
        accelerationInput = 0;

        yield return new WaitForSeconds(32F);
        accelerationInput = 1;
        spinOut = false;

        
    }

    private void Move()
    {
        //print("Rival Start Moving");

        // If Rival didn't reach the last waypoint it can move
        // If Rival reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            // Move Rival from current waypoint to the next one
            // using Move Towards method

            transform.position = Vector2.MoveTowards(transform.position,
                                  waypoints[waypointIndex].transform.position,
                                   moveSpeed * Time.deltaTime);


            //print("Rival continue to move" + waypointIndex);

            // If Rival reaches position of waypoint he walked towards
            //The waypointIndex is increased by 1
            // And Rival starts to walk to next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position && waypointIndex != waypoints.Length)
            {
                //print("Rival reached checkpoint" + waypointIndex);
                
                waypointIndex += 1;
                if(waypointIndex == waypoints.Length && lapCounter != totalLaps) // this prevents an issue with the array size
                {
                    waypointIndex = 0;
                }
                transform.up = waypoints[waypointIndex].position - transform.position; // forces car to always face checkpoint
            }
            if( waypointIndex == waypoints.Length -1 && lapCounter != totalLaps )
            {
                
                waypointIndex = 0;                
                lapCounter++;
                print("Player is on Lap " + lapCounter);
            }

            //Spinout protocols
            //checks the players moveSpeed at certain points to see if they are going too fast

            if(waypointIndex == 2 && moveSpeed > turnSpeedLimit)
            {
                StartCoroutine(SpinOut());
            }
        }
    }


}
