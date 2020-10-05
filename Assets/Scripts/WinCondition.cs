using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    public float stageLength;
    public AudioClip winSound;
    public float winWait;

    private float elapsedTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator PlaySoundAndGoToNext()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();
        audio.PlayOneShot(winSound);
        yield return new WaitForSeconds(winWait);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if ((stageLength <= elapsedTime) || (Input.GetKeyDown(KeyCode.RightBracket)))
        {
            StartCoroutine(PlaySoundAndGoToNext());
        }
        else if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
