using UnityEngine;
using UnityEngine.UI;

public class Fishing_Manager : MonoBehaviour
{
    [SerializeField] private Slider _caughtSlider;
    [SerializeField] private Slider _tensionSlider;

    [SerializeField] private float _caughtMeter;
    [SerializeField] private float _tensionMeter;

    [SerializeField] private float _caughtIncreaseRate;
    [SerializeField] private float _tensionIncreaseRate;

    [SerializeField] private float _caughtDecreaseRate;
    [SerializeField] private float _tensionDecreaseRate;

    [SerializeField] private bool _isFishing;

    public bool isFishing
    {
        get { return _isFishing; }
        set { _isFishing = value; }
    }

    private void OnEnable()
    {
        EventBus.Fishing += startFishing;
    }

    private void OnDisable()
    {
        EventBus.Fishing -= startFishing;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _caughtMeter = _tensionMeter = 0;
        _isFishing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFishing)
        {
            if (Input.GetMouseButton(0))
            {
                _caughtMeter += _caughtIncreaseRate * Time.deltaTime;
                _tensionMeter += _tensionIncreaseRate * Time.deltaTime;
            }
            else
            {
                _caughtMeter -= _caughtDecreaseRate * Time.deltaTime;
                _tensionMeter -= _tensionDecreaseRate * Time.deltaTime;
            }

            // Giới hạn giá trị trong khoảng 0 đến 1
            _caughtMeter = Mathf.Clamp(_caughtMeter, 0f, 1f);
            _tensionMeter = Mathf.Clamp(_tensionMeter, 0f, 1f);

            // Cập nhật giá trị lên UI Sliders
            _caughtSlider.value = _caughtMeter;
            _tensionSlider.value = _tensionMeter;

            // Kiểm tra điều kiện thắng/thua
            if (_caughtMeter >= 1f)
            {
                Debug.Log("Bắt được cá!");
                isFishing = false; // Kết thúc quá trình câu cá

                // Thêm logic xử lý khi bắt được cá (ví dụ: tăng điểm)
            }
            else if (_tensionMeter >= 1f)
            {
                Debug.Log("Dây câu đứt!");
                isFishing = false; // Kết thúc quá trình câu cá

                // Thêm logic xử lý khi dây đứt (ví dụ: reset)
            }
        }
        else
        {
            _caughtSlider.gameObject.SetActive(false);
            _tensionSlider.gameObject.SetActive(false);
        }

    }

    public void startFishing()
    {
        _caughtMeter = _tensionMeter = 0;
        _isFishing = true;

    }
}
