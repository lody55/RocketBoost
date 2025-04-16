using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
