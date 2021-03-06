using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CarLapCounter : MonoBehaviour
{
    public Text carPositionText;
    public Text lapCounterText;

    //public bool playerCar;

    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckPoint = 0;

    int numberOfPassedCheckpoints = 0;

    int lapsCompleted = 0; // this is for position Handling, not the navigation
    const int lapsToComplete = 3;
    bool isRaceCompleted = false;
    int carPosition = 0;


    //Events
    public event Action<CarLapCounter> OnPassCheckpoint;


    public void SetCarPosition(int position)
    {
        
        carPosition = position;
        //print("Position: " + position);

    }

    public int GetNumberOfCheckpointsPassed()
    {
        return numberOfPassedCheckpoints;
    }

    public float GetTimeAtLastCheckPoint()
    {
        return timeAtLastPassedCheckPoint;
    }

    IEnumerator ShowPositionCO(float delayUntilHidePosition)
    {
        carPositionText.text = carPosition.ToString();
        print("Position" + carPosition);

        //carPositionText.gameObject.SetActive(true);

        yield return new WaitForSeconds(delayUntilHidePosition);

        carPositionText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.CompareTag("CheckPoint"))
        {
            //Once a car has completed the race, stop checking checkpoints and conting laps
            if (isRaceCompleted)
            {
                StartCoroutine(ShowPositionCO(100));
                return;
            }

            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();

            // Make sure we are passing checkpoints in the right order
            if (passedCheckPointNumber + 1 == checkPoint.checkPointNumber)
            {
                passedCheckPointNumber = checkPoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                // Records the Time at the checkpoint
                timeAtLastPassedCheckPoint = Time.time;

                if (checkPoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;
                    lapCounterText.text = lapsCompleted.ToString();

                    if (lapsCompleted >= lapsToComplete)
                    {
                        isRaceCompleted = true;
                        print("race over");
                        SceneManager.LoadScene("TitleScene");
                    }
                }

                // Invoke the passed checkpoint event
                OnPassCheckpoint?.Invoke(this);

                // Now show the cars position as it has been calculated
                if (isRaceCompleted)
                {
                    StartCoroutine(ShowPositionCO(10));

                }
                else
                {
                    StartCoroutine(ShowPositionCO(1.5f));
                }
            }
        }
    }
    
}
