using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClosePopUp : ButtonAction
{
    public override void OnAction()
    {
        Destroy(transform.parent.gameObject);
    }
}
