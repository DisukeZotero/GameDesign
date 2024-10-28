using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public bool isSwitched = false;
    public Image background1;
    public Image background2;

    public void SwitchImage(Sprite sprite)
    {
        if (!isSwitched)
        {
            background2.sprite = sprite;
            background2.gameObject.SetActive(true);
            background1.gameObject.SetActive(false);
        }
        else
        {
            background1.sprite = sprite;
            background1.gameObject.SetActive(true);
            background2.gameObject.SetActive(false);
        }
        isSwitched = !isSwitched;
    }

    public void SetImage(Sprite sprite)
    {
        if (!isSwitched)
        {
            background1.sprite = sprite;
            background1.gameObject.SetActive(true);
            background2.gameObject.SetActive(false);
        }
        else
        {
            background2.sprite = sprite;
            background2.gameObject.SetActive(true);
            background1.gameObject.SetActive(false);
        }
    }
}
