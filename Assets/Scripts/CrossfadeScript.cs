using UnityEngine;
using UnityEngine.UI;


public class CrossfadeScript : MonoBehaviour
{
    //Set FadeTrigger Script
    public Animator anim;
    private Image fadeImage;


    public void Awake()
    {
        fadeImage = GetComponent<Image>();
        //I want to do fadeImage.Color.alpha = 1f;
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1f);

    }
    public void FadeOut()
    {
        anim.SetTrigger("FadeTrigger");
    }
}
