using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerMuteKey : MonoBehaviour
{
    private VideoPlayer vp;

    // Start is called before the first frame update
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
        vp.SetDirectAudioMute(0, MuteKey.isMuted);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            vp.SetDirectAudioMute(0, !vp.GetDirectAudioMute(0));
        }
    }
}
