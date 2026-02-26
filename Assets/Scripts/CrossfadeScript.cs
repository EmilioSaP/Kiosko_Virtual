using UnityEngine;

public class CrossfadeScript : MonoBehaviour
{
    //Set FadeTrigger Script
    public Animator anim;

    public void FadeOut()
    {
        anim.SetTrigger("FadeOut");
    }
}
