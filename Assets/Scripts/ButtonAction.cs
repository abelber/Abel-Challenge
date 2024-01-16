using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    public Action callback;

    void Start()
    {
        callback = OnAction;    
    }

    public virtual void OnAction()
    {

    }
}
