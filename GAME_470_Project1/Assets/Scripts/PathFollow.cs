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
           if( transform.position == waypoints[waypointIndex].transform.position)
            {
                //print("Rival reached checkpoint" + waypointIndex);
                waypointIndex += 1;
            }
        }
    }
}
