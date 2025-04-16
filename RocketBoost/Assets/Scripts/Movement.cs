using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    AudioSource audioSource;
    Rigidbody rb;
    #region Field
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] AudioClip mainEngineSFX;
    [SerializeField] ParticleSystem mainEnginParticles;
    [SerializeField] ParticleSystem rigthtTrustParticles;
    [SerializeField] ParticleSystem leftTrustParticles;



    [SerializeField] float rotationStrength = 100f;
    [SerializeField] float thrustStrength = 100f;
    #endregion
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.inProgress)
        {
            //Debug.Log("스페이스바 누름");
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSFX);
                
                
            }
            if (!mainEnginParticles.isPlaying)
            {
                mainEnginParticles.Play();
            }
            else
            {
                audioSource.Stop();
                mainEnginParticles.Stop();
            }
        }
    }
    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        //Debug.Log(rotationInput);
        if(rotationInput < 0)
        {
            ApplyRotation(rotationStrength);
        }
        if (!rigthtTrustParticles.isPlaying)
        {
            leftTrustParticles.Stop();
            rigthtTrustParticles.Play();
        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationStrength);
            if (!leftTrustParticles.isPlaying)
            {
                rigthtTrustParticles.Stop();
                leftTrustParticles.Play();
            }
        }
        else
        {
            rigthtTrustParticles.Stop();
            leftTrustParticles.Stop();
        }

    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }

}
