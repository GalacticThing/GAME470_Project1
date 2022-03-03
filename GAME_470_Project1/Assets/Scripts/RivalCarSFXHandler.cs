using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalCarSFXHandler : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource carBrakeAudioSource;
    public AudioSource carEngineAudioSource;
    public AudioSource carCrashAudioSource;
    //public AudioSource carIdleAudioSource;

    //Local variable
    float desiredEnginePitch = 0.5f;
    float carBrakePitch = 0.5f;

    // Components
    PathFollow pathFollow;


    private void Awake()
    {
        pathFollow = GetComponent<PathFollow>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSFX();
        UpdateBrakingSFX();

    }

    void UpdateEngineSFX()
    {
        //Handle engine SFX
        float velocityMagnitude = pathFollow.moveSpeed;

        //Increases the engine volume as the car goes faster
        float desiredEngineVolume = velocityMagnitude * 0.05f;

        //keeps a minimum level playing even if the car is Idle
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1.0f);

        carEngineAudioSource.volume = Mathf.Lerp(carEngineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        //adds more variation to the engine sound by altering the pitch
        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        carEngineAudioSource.pitch = Mathf.Lerp(carEngineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    void UpdateBrakingSFX()
    {
        /* The Rival Cars won't need to brake so this can be ignored
        // Handle Braking SFX
        if (pathFollow.accelerationInput == -1) // When we Brake, increase the volume and pitch of the braking sound effect
        {
            carBrakeAudioSource.volume = Mathf.Lerp(carBrakeAudioSource.volume, 1.0f, Time.deltaTime * 10);
            carBrakePitch = Mathf.Lerp(carBrakePitch, 0.5f, Time.deltaTime * 10);
        }
        else // if car is not braking, fade out the braking sound effect
        {
            carBrakeAudioSource.volume = Mathf.Lerp(carBrakeAudioSource.volume, 0, Time.deltaTime * 10);
        }
        */
    }
}
