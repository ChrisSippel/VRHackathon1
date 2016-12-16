using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class FinishLineScript : MonoBehaviour
{

    public string LevelName;
    public AudioClip LevelCompletedAudioClip;

    private AudioSource audioSource;

    [SerializeField] private VRCameraFade m_VRCameraFade;           // Reference to the script that fades the scene to black.

    void OnCollisionEnter(Collision col)
    {
        audioSource = GetComponent<AudioSource>();
        if (col.gameObject.name == "Character")
        {
            StartCoroutine(FadeToLevel());
        }
    }

    private IEnumerator FadeToLevel()
    {
        audioSource.clip = LevelCompletedAudioClip;
        audioSource.Play();
        // Wait for the screen to fade out.
        yield return StartCoroutine(m_VRCameraFade.BeginFadeOut(true));

        // Load the main menu by itself.
        SceneManager.LoadScene(LevelName, LoadSceneMode.Single);
    }
}
