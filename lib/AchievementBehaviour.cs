using UnityEngine;

namespace UltraAchievements_Lib;

public class AchievementBehaviour : MonoBehaviour
{
    private float _endTimer = 0;
    private float _startTimer = 0;
    private float _animTimer;
    private bool _endTimerStarted = false;
    private bool _startTimerStarted = false;
    private RectTransform _rect;
    
    private void Start()    
    {
        _rect = GetComponent<RectTransform>();
        _rect.anchoredPosition = new Vector2(1170f,-414f);
        _startTimerStarted = true;
    }
    private void Update()
    {
        if(_startTimerStarted)
        {
            _startTimer += Time.deltaTime;
            _animTimer += Time.deltaTime * 1.35f;
            if (_animTimer >= 1) _animTimer = 1;
            _rect.anchoredPosition = new Vector2(Mathf.Lerp(1170f, 760f, _animTimer),-414f);
            if(_startTimer >= 5)
            {
                _animTimer = 0;
                _endTimerStarted = true;
                _startTimerStarted = false;
            }
        }
        if(_endTimerStarted)
        {
            _endTimer += Time.deltaTime;
            _animTimer += Time.deltaTime * 1.35f;
            if (_animTimer >= 1) _animTimer = 1;
            _rect.anchoredPosition = new Vector2(Mathf.Lerp(780f, 1170f, _animTimer), -414f);
            if (_endTimer >= 5)
            {
                _endTimerStarted = false; 
                Destroy(this.transform.parent);
            }
        }
    }
    
}