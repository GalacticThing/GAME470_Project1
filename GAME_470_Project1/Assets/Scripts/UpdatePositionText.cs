using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UpdatePositionText : MonoBehaviour
{
    public PositionHandler positionHandler;
    public string textValue;
    public Text positionNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //positionNumber.text = positionHandler.carPosition.ToString(); 
    }
}
