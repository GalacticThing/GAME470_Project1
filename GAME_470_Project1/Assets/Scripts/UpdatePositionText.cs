using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdatePositionText : MonoBehaviour
{
    public CarLapCounter carLapCounter;
    public GameObject positionNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        positionNumber.GetComponent<Text>().text = carLapCounter.carPositionText.ToString(); 
    }
}
