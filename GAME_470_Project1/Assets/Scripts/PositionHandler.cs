using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PositionHandler : MonoBehaviour
{
    public List<CarLapCounter> carLapCounters = new List<CarLapCounter>();

    // Start is called before the first frame update
    void Start()
    {
        // Find all Car lap counters in the scene
        CarLapCounter[] carLapCounterArray = FindObjectsOfType<CarLapCounter>();

        //store the lap counters in a list
        carLapCounters = carLapCounterArray.ToList<CarLapCounter>();

        //Connect the passed checkpoint event
        foreach (CarLapCounter lapCounters in carLapCounters)
        {
            lapCounters.OnPassCheckpoint += OnPassCheckpoint;
        }
    }

    void OnPassCheckpoint(CarLapCounter carLapCounter)
    {

    }

    
    
}
