using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource; 
    public AudioSource soundEffectSource;


    public AudioClip clickSound;
    public AudioClip hitEnemySound;
    public AudioClip attackedSound;
    public AudioClip deathSound;
    public AudioClip successSound;


    public Slider musicSlider;
    public Slider soundEffectSlider;

    private void Start()
    {
        instance = this;

        float musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float soundEffectVolume = PlayerPrefs.GetFloat("SoundEffectVolume", 1f);

        SetMusicVolume(musicVolume);
        SetSoundEffectVolume(soundEffectVolume);

        musicSlider.value = musicVolume;
        soundEffectSlider.value = soundEffectVolume;

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundEffectSlider.onValueChanged.AddListener(SetSoundEffectVolume);
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectSource.volume = volume;
        PlayerPrefs.SetFloat("SoundEffectVolume", volume);
    }

    public void PlayClickSound()
    {
        soundEffectSource.PlayOneShot(clickSound);
    }

    public void PlayHitEnemySound()
    {
        soundEffectSource.PlayOneShot(hitEnemySound);
    }

    public void PlayAttackedSound()
    {
        soundEffectSource.PlayOneShot(attackedSound);
    }

    public void PlayDeathSound()
    {
        soundEffectSource.PlayOneShot(deathSound);
    }

    public void PlayCongratulationsSound()
    {
        soundEffectSource.PlayOneShot(successSound);
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        soundEffectSource.PlayOneShot(clip);
    }
}