using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    //Components
    CarController carController;

    private void Awake()
    {
        carController = GetComponent<CarController>(); // refrences the CarController Script
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        
        inputVector.y = Input.GetAxis("Vertical"); 

        carController.SetInputVector(inputVector);
    }
}
