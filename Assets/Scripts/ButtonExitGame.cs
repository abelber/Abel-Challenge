using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonExitGame : ButtonAction
{
    public override void OnAction()
    {
#if UNITY_EDITOR
        Destroy(transform.parent.gameObject);
#endif
        Application.Quit();
    }
}
