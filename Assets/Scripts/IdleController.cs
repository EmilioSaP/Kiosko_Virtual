using UnityEngine;
using System.Collections;
using TMPro;

public class IdleController : MonoBehaviour
{
    public CanvasGroup idleCanvasGroup;

    public float idleTime = 10f;
    public float canvasTime = 8f;
    public float fadeDuration = 0.5f;
    public TMP_Text timerText;


    public float timer;
    private bool secondTimerRunning = false;
    private Coroutine fadeRoutine;

    private SetWorldInfo setWorldInfo;
    private ChangeScene changeScene;

    void Start()
    {
        ResetToIdleTimer();
        setWorldInfo = FindObjectOfType<SetWorldInfo>();
        changeScene = FindObjectOfType<ChangeScene>();
    }

    void Update()
{
    timer -= Time.deltaTime;

    if (!secondTimerRunning)
    {
        if (timer <= 0f)
        {
            StartSecondTimer();
        }
    }
    else
    {
        // Display floored timer
        timerText.text = Mathf.Max(0, Mathf.FloorToInt(timer)).ToString();

        if (timer <= 0f)
        {
            TriggerAction();
            ResetToIdleTimer();
        }
    }
}

    void StartSecondTimer()
    {
        secondTimerRunning = true;
        timer = canvasTime;

        FadeCanvas(1f);
    }

    void TriggerAction()
    {
        setWorldInfo.ResetWorldInfo();
        Debug.Log("Idle, changing scene");
        changeScene.GoToScene("1_Pantalla_Inicio");
    }

    public void InterruptIdleTimer()
    {
        ResetToIdleTimer();
    }

    void ResetToIdleTimer()
    {
        secondTimerRunning = false;
        timer = idleTime;

        FadeCanvas(0f);
    }

    void FadeCanvas(float targetAlpha)
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeRoutine(targetAlpha));
    }

    IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = idleCanvasGroup.alpha;
        float time = 0f;

        if (targetAlpha > 0)
            idleCanvasGroup.gameObject.SetActive(true);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            idleCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            yield return null;
        }

        idleCanvasGroup.alpha = targetAlpha;

        if (targetAlpha == 0)
            idleCanvasGroup.gameObject.SetActive(false);
    }
}
