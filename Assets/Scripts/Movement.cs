using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //PARAMETERS - for tuning, typically set in the editor
    //CACHE - e.g. references for readability or speed
    //STATE - private instance (member) variables

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem LeftThrusterParticle;
    [SerializeField] ParticleSystem RightThrusterParticle;
    [SerializeField] ParticleSystem mainEngineParticle;
    
    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            LeftRotation();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RightRotation();
        }
        else
        {
            StopRotation();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
    }

    void LeftRotation()
    {
        ApplyRotation(rotationThrust);
        if (!RightThrusterParticle.isPlaying)
        {
            RightThrusterParticle.Play();
        }
    }

    void RightRotation()
    {
        ApplyRotation(-rotationThrust);
        if (!LeftThrusterParticle.isPlaying)
        {
            LeftThrusterParticle.Play();
        }
    }

    void StopRotation()
    {
        LeftThrusterParticle.Stop();
        RightThrusterParticle.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreeze the rotation so the physics rotation continues
    }
}
