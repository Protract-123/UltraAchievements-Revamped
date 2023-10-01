using UnityEngine;

namespace UltraAchievements_Revamped;

public class AchievementBehaviour : MonoBehaviour
{
    private float endTimer = 0;
    private float startTimer = 0;
    private float animTimer;
    private bool endTimerStarted = false;
    private bool startTimerStarted = false;
    private RectTransform rect;
    
    private void Start()    
    {
        rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(1150f,-445f);
        startTimerStarted = true;
    }
    private void Update()
    {
        if(startTimerStarted)
        {
            startTimer += Time.deltaTime;
            animTimer += Time.deltaTime * 1.35f;
            if (animTimer >= 1) animTimer = 1;
            rect.anchoredPosition = new Vector2(Mathf.Lerp(1150f, 780f, animTimer),-445f);
            if(startTimer >= 5)
            {
                animTimer = 0;
                endTimerStarted = true;
                startTimerStarted = false;
            }
        }
        if(endTimerStarted)
        {
            endTimer += Time.deltaTime;
            animTimer += Time.deltaTime * 1.35f;
            if (animTimer >= 1) animTimer = 1;
            rect.anchoredPosition = new Vector2(Mathf.Lerp(780f, 1150f, animTimer), -445f);
            if (endTimer >= 5)
            {
                endTimerStarted = false; 
            }
        }
    }
}