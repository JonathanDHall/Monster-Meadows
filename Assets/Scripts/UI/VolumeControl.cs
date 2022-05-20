using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private string _volumeParameter = "Master";
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private float _multiplier = 30;
    [SerializeField] private Toggle _toggle;
    private bool _dissableToggleEvent;

    void Awake()
    {
        _slider.onValueChanged.AddListener(HandleSliderValueChanged);
        _toggle.onValueChanged.AddListener(HandleToggleValueChanged);
    }

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(_volumeParameter, _slider.value);

        int temp = PlayerPrefs.GetInt("mute", 0);
        _toggle.isOn = temp > 0;
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(HandleSliderValueChanged);
        _toggle.onValueChanged.AddListener(HandleToggleValueChanged);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _slider.value);
        int temp = 0;
        if (_toggle.isOn)
            temp = 1;
        PlayerPrefs.SetInt("mute", temp);
    }

    private void HandleSliderValueChanged(float Value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(Value) * _multiplier);
        //_dissableToggleEvent = true;
        //_toggle.isOn = _slider.value > _slider.minValue;
        //_dissableToggleEvent = false;
    }

    private void HandleToggleValueChanged (bool EnableSound)
    {
        if (_dissableToggleEvent)
            return;

        switch (EnableSound)
        {
            case false:
                _slider.interactable = true;
                _slider.value = 0.8f;
                break;
            case true:
                _slider.interactable = false;
                _slider.value = _slider.minValue;
                break;
        }
    }
}