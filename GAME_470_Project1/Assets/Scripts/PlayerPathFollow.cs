using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathFollow : MonoBehaviour

{    // Local Variables
    public Transform[] waypoints; // the array containing the waypoints of the car
    public float moveSpeed = 0f; // controls the rate at which the car moves along the path
    private int waypointIndex = 0; // used to identify waypoints and keep the car on the path
    protected float accelerationInput = 0; // checks it the player is pressing forward or back

    protected float velocityVsUp = 0;

    public float maxSpeed; // the maximum speed the car can travel
    public float minSpeed; // value used to prevent the car from totally reversing



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
            print("Accelerate" + moveSpeed);
        }
        else if(accelerationInput == 0)
        {
            moveSpeed -= 0.1f;
        }
        else if(accelerationInput == -1)
        {
            //Apply Brakes
            moveSpeed -= 0.1f;
            print("Deccelerate" + moveSpeed);
        }

        
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
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                //print("Rival reached checkpoint" + waypointIndex);
                waypointIndex += 1;
            }
        }
    }


}
