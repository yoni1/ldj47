using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class MuteKey : MonoBehaviour
    {
        public static bool isMuted = false;
        private static float unMutedVolume = 0;

        void RefreshVolume()
        {
            if (isMuted)
            {
                if (AudioListener.volume > 0)
                    unMutedVolume = AudioListener.volume;
                AudioListener.volume = 0;
            }
            else
            {
                AudioListener.volume = unMutedVolume;
            }
        }

        // Use this for initialization
        void Start()
        {
            if (isMuted)
            {
                RefreshVolume();
            }
            else
            {
                unMutedVolume = AudioListener.volume;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                isMuted = !isMuted;
                RefreshVolume();
            }
        }
    }
}