using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTransitionCanvas : MonoBehaviour
{
    public Animator transitionPanel;
    public Animator logoTransition;

    Action videoCallback;

    void Start()
    {
        //transform.parent = Camera.main.transform;
        transitionPanel.gameObject.SetActive(false);
    }

    public void VideoFinishTransition(Action callback, float transitionTime = 5)
    {
        transitionPanel.gameObject.SetActive(true);
        //transitionPanel.transform.position = Vector3.zero + new Vector3(0,0,-5);
        
        transitionPanel.SetTrigger("Play");
        logoTransition.SetTrigger("Play");

        if(callback != null)
            videoCallback = callback;

        StartCoroutine(VideoTransition(transitionTime));
    }

    private IEnumerator VideoTransition(float transitionTime)
    {
        yield return new WaitForSeconds(transitionTime);

        transitionPanel.gameObject.SetActive(false);

        if (videoCallback != null)
            videoCallback();
    }
}
