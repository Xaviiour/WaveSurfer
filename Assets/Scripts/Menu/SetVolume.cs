using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private float currentVolume;
    private void OnEnable()
    {
        volumeSlider.value = AudioManager.instance.GetVolume();
    }
    public void ChangeVolume(float volume)
    {
        AudioManager.instance.SetVolume(volume);
    }
}
