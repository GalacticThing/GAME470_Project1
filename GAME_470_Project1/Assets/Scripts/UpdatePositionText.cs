using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdatePositionText : MonoBehaviour
{
    public CarLapCounter carLapCounter;
    public PlayerPathFollow playerPathFollow;
    public Text text;

    public Text lapText;
   
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = carLapCounter.carPositionText.text;
        print(carLapCounter.carPositionText);

        lapText.text = carLapCounter.lapCounterText.text;
        lapText.text = playerPathFollow.lapCounterText.text;
    }
}
