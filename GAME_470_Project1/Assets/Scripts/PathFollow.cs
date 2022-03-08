using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollow : MonoBehaviour
{
    [SerializeField]
    //private Transform[] waypoints;
    public Transform[] waypoints;

    [SerializeField]
    //private float moveSpeed = 2f;
    public float moveSpeed = 2f;

   

    //Index of curr waypoint from which Rival moves
    private int waypointIndex = 0;

    // Lap counter
    private int lapCounter = 1; //keeps track of a cars current Lap

    public int totalLaps = 3;



    void Start()
    {
        // Set position of Rival as position of first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
  
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // Move Rival
        Move();

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
            if ( transform.position == waypoints[waypointIndex].transform.position && waypointIndex != waypoints.Length)
            {
                //print(" Rival reached checkpoint" + waypointIndex);
                waypointIndex += 1;
                if (waypointIndex == waypoints.Length && lapCounter != totalLaps) // this prevents an issue with the array size
                {
                    waypointIndex = 0;
                }
                transform.up = waypoints[waypointIndex].position - transform.position; // forces car to always face checkpoint

            }
           if(waypointIndex == waypoints.Length -1 && lapCounter != totalLaps)
            {
                
                waypointIndex = 0;               
                lapCounter++;
                moveSpeed += 0.2f; // a slight boost in difficulty to make the race seem close
                print("This Cars Current Lap is " + lapCounter);
            }

            
        }

    }
}
