using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    [Header("Car Settings")]
    public float accelerationFactor = 15.0f; // the acceleration rate of the car
    public float driftFactor = 0.0f; // controls the car's ability to turn // being at zero means the car will behave as if on rails
                                     // as far as it's input is concerned
    public float dragFactor = 1.0f; // controls how much the car will slow down when the player lets gp of accelerate

    public float maxSpeed = 20.0f;




    //local variables
    protected float accelerationInput = 0; // checks it the player is pressing forward or back

    protected float velocityVsUp = 0;

    

    //Components
    Rigidbody2D carRigidbody2D;

    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>(); // refrences the rigidbody component of the car
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyEngineForce(); 

        KillHorizontalVelocity();

        ApplySteering();
    }

    protected void ApplyEngineForce() //applies a force that pushes the car either forward or backwards
    {
        //Calculates how much forward we are going in a given direction
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        //Sets the limit of our max speed
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }
        //Sets the limit of our max speed in reverse to 0% of our forward max speed // preventing us from reversing when at a stop
        if (velocityVsUp < -maxSpeed * 0.0f && accelerationInput < 0)
        {
            return;
        }
        //Sets the limit of our maxspeed in any diagonal direction
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //Apply drag if there is no accelerationInput so the car slows down when the player lets go of accelerate
        if (accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, dragFactor, Time.deltaTime * 3);
            // dragFactor controls the magnitude of this drag
        }
        else
        {
            carRigidbody2D.drag = 0;
        }
        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //Apply force and push the car forward
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering() // when the car comes in contact with rails that will force it to turn
                            // this function should control it
    {
        //Limits the cars ability to turn when moving slowly // prevents the car from infinetly spinning in place
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

    }



    public void SetInputVector(Vector2 inputVector)
    {
        accelerationInput = inputVector.y;
    }

    void KillHorizontalVelocity() // limits the cars ability to move Horizontaly
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
}
