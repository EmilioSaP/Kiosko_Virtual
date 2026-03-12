using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PinsToggle : MonoBehaviour
{
    //Set FadeTrigger Script
    public List<GameObject> pins; 


    public void EnablePins()
    {
        foreach (GameObject obj in pins)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

    public void DisablePins()
    {
        foreach (GameObject obj in pins)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

}
