using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{
    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckPoint = 0;

    int numberOfPassedCheckpoints = 0;

    //Events
    public event Action<CarLapCounter> OnPassCheckpoint;

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("CheckPoint"))
        {
            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();

            // Make sure we are passing checkpoints in the right order
            if (passedCheckPointNumber +1 == checkPoint.checkPointNumber)
            {
                passedCheckPointNumber = checkPoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                // Records the Time at the checkpoint
                timeAtLastPassedCheckPoint = Time.time;

                // Invoke the passed checkpoint event
                OnPassCheckpoint?.Invoke(this);
            }
        }
    }
    
}
