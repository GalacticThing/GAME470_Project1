using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    //Components
    CarController carController;
    PlayerPathFollow playerPathFollow;

    private void Awake()
    {
        carController = GetComponent<CarController>(); // refrences the CarController Script
        playerPathFollow = GetComponent<PlayerPathFollow>(); // refrences player path follow script
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        
        inputVector.y = Input.GetAxis("Vertical"); 

        carController.SetInputVector(inputVector);
    }
}
