using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{

    [SerializeField] Transform _lightsHolder;
    [SerializeField] Light2D _dayLight;
    [SerializeField] Gradient _dayLightGradient;
    [SerializeField] Light2D _nightLight;
    [SerializeField] Gradient _nightLightGradient;

    [SerializeField] float dayLength = 300f; // 10 minutes per day
    private float timeOfDay;

    [SerializeField] int _tick = 300;
    [SerializeField] private int _second;
    [SerializeField] private int _minute;
    [SerializeField] private int _hour;

    [SerializeField] private int _day;
    [SerializeField] private int _month;
    [SerializeField] private int _year;

    [SerializeField] private DateTime _currentDate;

    private float _elapsedTime = 0f;
    private bool _isSimulating = true;

    [SerializeField] private float _realTime;
    [SerializeField] private float _ratio;
    [SerializeField] private float _timer = 0f;



    public DateTime getCurrentDate()
    {
        return _currentDate;
    }


    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateLighting(0);
        _realTime = Time.deltaTime;
        _ratio = _realTime / 1f;
        _currentDate = new DateTime(_year, _month, _day);
    }

    // Update is called once per frame
    void Update()
    {

        // Accumulate real-world time
        _timer += Time.deltaTime;

        // Update in-game time every second
        if (_timer >= 1f)
        {

            clock();
            _timer -= 1f; // Reset timer for next second
        }

        timeOfDay += Time.deltaTime / _tick; // Increment time
        timeOfDay %= 1; // Keep ratio between 0 and 1
        updateLighting(timeOfDay);
        if (!_isSimulating) return;

    }


    private void OnEnable()
    {
        EventBus.GetCurrentDate += getCurrentDate;

    }

    private void OnDisable()
    {
        EventBus.GetCurrentDate -= getCurrentDate;


    }


    public void updateLighting(float ratio)
    {
        _dayLight.color = _dayLightGradient.Evaluate(ratio);
        _nightLight.color = _nightLightGradient.Evaluate(ratio);

        _lightsHolder.rotation = Quaternion.Euler(0f, 0f, 360f * ratio);
    }


    public void clock()
    {
        _second += _tick;
        if (_second > 59)
        {
            _minute += (_second / 60) % 60;
            // Debug.Log(_second);
            // Debug.Log((_second / 60) % 60);
            _second = 0;

        }
        if (_minute > 59)
        {
            _minute = 0;
            _hour += 1;
        }
        if (_hour > 23)
        {
            _hour = 0;
            _day++;
            _currentDate = _currentDate.AddDays(1);
            EventBus.UpdateAllCrops?.Invoke(_currentDate);
        }
        if (_day > 30)
        {
            _day = 0;
            _month++;
        }
        if (_month > 12)
        {
            _month = 0;
            _year++;
        }

    }

}
