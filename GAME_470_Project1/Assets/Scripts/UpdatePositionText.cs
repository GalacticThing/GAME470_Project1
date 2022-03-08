using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdatePositionText : MonoBehaviour
{
    public CarLapCounter carLapCounter;
    public Text text;
   
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = carLapCounter.carPositionText.text;
        print(carLapCounter.carPositionText);
    }
}
