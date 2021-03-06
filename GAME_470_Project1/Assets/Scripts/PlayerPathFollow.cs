using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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
    public int lapCounter = 1; //keeps track of a cars current Lap

    public int totalLaps = 3;

    public Text lapCounterText;



    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) && spinOut == false) // Acceleration Input
        {
            accelerationInput = 1;
            //print("accelerate");
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
            //print("Brake");
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
            moveSpeed += 0.06f;
            //print("Accelerate" + moveSpeed);
        }
        else if(accelerationInput == 0)
        {
            moveSpeed = 0.8f;
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
        //rotZ +=  Time.deltaTime * rotationSpeed;

        moveSpeed = 0.8F; // slow down car
        accelerationInput = 0;

        float t = 0;
        while (t < 0.8)
        {
            //print("RotZ is: " + rotZ);
            rotZ += (Time.deltaTime * rotationSpeed) / 10;
            if (rotZ >= 15) rotZ = 15;
            transform.Rotate(Vector3.forward, rotZ);
            yield return new WaitForSeconds(0.01f);
            t += Time.deltaTime;
        }

        //yield return null;
        //yield return new WaitForSeconds(32F);
        accelerationInput = 1;
        spinOut = false;

        
    }

    private void Move()
    {
        //if (spinOut == true) return;

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
                //print("Player reached checkpoint" + waypointIndex);
                
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
                lapCounterText.text = lapCounter.ToString();
                print("Player is on Lap " + lapCounter);
            }

            //Spinout protocols
            //checks the players moveSpeed at certain points to see if they are going too fast

            if((waypointIndex == 2 || waypointIndex == 10 || waypointIndex == 22 || waypointIndex == 29 || waypointIndex == 34 || waypointIndex == 40 || waypointIndex == 45 || waypointIndex == 50 || waypointIndex == 56) && moveSpeed > turnSpeedLimit)
            {
                StartCoroutine(SpinOut());
            }
        }
    }


}
