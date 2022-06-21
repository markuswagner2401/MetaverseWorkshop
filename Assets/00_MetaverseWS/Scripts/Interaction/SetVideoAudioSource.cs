using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SetVideoAudioSource : MonoBehaviour
{
    VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.audioOutputMode =  VideoAudioOutputMode.AudioSource;

        videoPlayer.SetTargetAudioSource(0, GetComponent<AudioSource>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
