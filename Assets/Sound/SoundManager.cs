using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Mixer")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider voiceSlider;

    public AudioSource sfxSource;
    public AudioSource musicSource;
    public AudioSource extramusicSource;
    public AudioSource voiceSource;
    public AudioClip[] SFXList;
    public AudioClip[] MusicList;
    public AudioClip[] VoiceList;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        Debug.Log(volume);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetVoiceVolume()
    {
        float volume = voiceSlider.value;
        audioMixer.SetFloat("VoiceVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20);
        PlayerPrefs.SetFloat("VoiceVolume", volume);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            PlayerPrefs.SetFloat("MasterVolume", 0.6f);
            PlayerPrefs.SetFloat("MusicVolume", 1f);
            PlayerPrefs.SetFloat("SFXVolume", 1f);
            PlayerPrefs.SetFloat("VoiceVolume", 1f);
            PlayerPrefs.Save();
        }
        float master = PlayerPrefs.GetFloat("MasterVolume");
        float music = PlayerPrefs.GetFloat("MusicVolume");
        float sfx = PlayerPrefs.GetFloat("SFXVolume");
        float voice = PlayerPrefs.GetFloat("VoiceVolume");

        float debugCheck;
        audioMixer.GetFloat("MusicVolume", out debugCheck);

        Debug.Log("Loaded Music Volume: " + debugCheck);

        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(master, 0.0001f, 1f)) * 20);
        masterSlider.value = master;

        audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(music, 0.0001f, 1f)) * 20);
        musicSlider.value = music;

        audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(sfx, 0.0001f, 1f)) * 20);
        sfxSlider.value = sfx;

        audioMixer.SetFloat("VoiceVolume", Mathf.Log10(Mathf.Clamp(sfx, 0.0001f, 1f)) * 20);
        voiceSlider.value = voice;

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
        SetVoiceVolume();
    }

    private void Start()
    {
        StartCoroutine(DelayedLoadVolume());
    }

    private IEnumerator DelayedLoadVolume()
    {
        yield return null; // wait 1 frame
        LoadVolumeSettings();
        yield return null; // wait 1 frame
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
       sfxSource.PlayOneShot(GetClipByName(SFXList, name));
    }

    public void PlayVoiceClip(string name)
    {
        voiceSource.clip = GetClipByName(VoiceList, name);
        voiceSource.Play();
    }

    public void PlayThisVoiceClip(AudioClip chosenclip)
    {
        voiceSource.clip = chosenclip;
        voiceSource.Play();
    }

    public void PlayMusic(string name)
    {
        musicSource.clip = GetClipByName(MusicList, name);  // Assign your AudioClip
        musicSource.loop = true;            // Enable looping
        musicSource.Play();
    }

    public void SwitchMusic(bool pause, string name)
    {
        if (pause)
        {
            musicSource.Pause();
            extramusicSource.clip = GetClipByName(MusicList, name);
            extramusicSource.Play();
        }
        else
        {
            extramusicSource.Stop();
            musicSource.UnPause();
        }
    }

    public AudioClip GetClipByName(AudioClip[] clips, string clipName)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip != null && clip.name == clipName)
            {
                return clip;
            }
        }

        Debug.LogWarning($"Clip with name '{clipName}' not found.");
        return clips[0];
    }

}