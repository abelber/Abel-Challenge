using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    public TextMeshProUGUI title;

    public void Init(string message)
    {
        SetMessage(message);
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    private void SetMessage(string message)
    {
        if(!string.IsNullOrWhiteSpace(message))
        {
            title.text = message;
        }
    }
}
