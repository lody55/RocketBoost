using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip succes;
    AudioSource audioSource;


    bool isControllable = true;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable) return;
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("±Â");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("¿¬·á");
                break;
            default:
                StartCrashSequence();
                Invoke("ReloadLevel", levelLoadDelay);
                break;
        }
    }
    private void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    private void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        
        int nextScene = currentScene + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        SceneManager.LoadScene(nextScene);

    }
    private void StartCrashSequence()
    {
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        isControllable = false;
        audioSource.Stop();
    }
    private void StartSuccessSequence()
    {
        audioSource.PlayOneShot(succes);
        Invoke("LoadNextLevel", levelLoadDelay);
        GetComponent<Movement>().enabled = false;
        isControllable = false;
        audioSource.Stop();
    }
}
