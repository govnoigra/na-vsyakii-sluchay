using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

//для автоматического перехода из сцены с интро в сцену с меню
public class VideoController : MonoBehaviour
{
    public VideoPlayer vid; 


    void Start() { vid.loopPointReached += CheckOver; }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        Application.LoadLevel(1);
    }

}
