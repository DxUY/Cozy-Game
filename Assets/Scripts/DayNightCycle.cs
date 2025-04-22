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

    [SerializeField] int _tick =1;
    [SerializeField]  private int _second;
    [SerializeField]  private int _minute;
    [SerializeField] private int _hour;

    private float _elapsedTime = 0f;
    private bool _isSimulating = true;

    [SerializeField] private float _realTime;
    [SerializeField] private float _ratio;
    [SerializeField] private float _targetSeconds;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateLighting(0);
        _realTime = Time.deltaTime;
        _ratio = _realTime / 1f;
        _targetSeconds = Mathf.FloorToInt(288 * _ratio);
    }

    // Update is called once per frame
    void Update()
    {
        timeOfDay += Time.deltaTime / dayLength; // Increment time
        timeOfDay %= 1; // Keep ratio between 0 and 1
        updateLighting(timeOfDay);
        if (!_isSimulating) return;

        // Simulate 288 seconds in 1 real-world second
        _elapsedTime += Time.deltaTime;
        float ratio = _elapsedTime / 1f; // 1 second duration
        int targetSeconds = Mathf.FloorToInt(288 * ratio); // Total seconds to reach

        // Reset clock to 00:00:00 and advance to targetSeconds
        _second = 0;
        _minute = 0;
        _hour = 0;

        // Advance clock to targetSeconds
        while (targetSeconds > 0)
        {
            clock();
            targetSeconds -= _tick;
        }

        // Stop simulation at 00:04:48 (288 seconds)
        if (_elapsedTime >= 1f)
        {
            _isSimulating = false;
            _second = 48; // Force exact end time (00:04:48)
            _minute = 4;
            _hour = 0;
        }
    }

    public void updateLighting(float ratio)
    {
        _dayLight.color = _dayLightGradient.Evaluate(ratio);
        _nightLight.color = _nightLightGradient.Evaluate(ratio);

        _lightsHolder.rotation = Quaternion.Euler(0f, 0f, 360f* ratio);
    }
    
    
    public void clock()
    {
        _second += _tick;
        if (_second > 59)
        {
            _second = 0;
            _minute +=1;
        }
        if (_minute > 59)
        {
            _minute = 0;
            _hour += 1;
        }
        if (_hour > 23)
        {
            _hour = 0;
        }
    }
}
