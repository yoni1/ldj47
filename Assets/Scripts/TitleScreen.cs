using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public AudioClip startGameSound;
    private bool startingGame;
    public float waitAfterSpace;

    public IEnumerator PlayGame()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();
        audio.PlayOneShot(startGameSound);
        yield return new WaitForSeconds(waitAfterSpace);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !startingGame)
        {
            startingGame = true;
            StartCoroutine(PlayGame());
        }
    }
}
