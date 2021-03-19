
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [Space(10)]
    public Slider sfxSlider;
    public Slider gameSlider;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("GameVolume", volume);
        

    }
    public void SetSfxVolume(float volume)
    {
        audioMixer.SetFloat("SfxVolume", volume);

    }
    private void Start()
    {
        //SetSfxVolume(PlayerPrefs.GetFloat("SfxVolume",0));
        //SetVolume(PlayerPrefs.GetFloat("GameVolume",0));
        gameSlider.value = PlayerPrefs.GetFloat("GameVolume",0);
        sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", 0);
    }

    private void OnDisable()
    {
        float musicVolume=0f;
        float sfxVolume=0f;

        audioMixer.GetFloat("SfxVolume", out sfxVolume);
        audioMixer.GetFloat("GameVolume", out musicVolume);

        PlayerPrefs.SetFloat("GameVolume", musicVolume);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
        PlayerPrefs.Save();

    }
}