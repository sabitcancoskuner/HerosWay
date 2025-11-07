using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public string parameter;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private float multiplier;

    public void SliderValue(float _value)
    {
        mixer.SetFloat(parameter, Mathf.Log10(_value) * multiplier);
    }
}
