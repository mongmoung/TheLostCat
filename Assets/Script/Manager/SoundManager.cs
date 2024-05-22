using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : SingleTon<SoundManager>
{

    public AudioSource bgmSound;
    public AudioClip[] bgmlist;

    protected override void Awake()
    {
        base.Awake();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgmlist.Length; i++)
        {
            if (arg0.name == bgmlist[i].name)
                BGMSoundPlay(bgmlist[i]);

        }
    }

    public void SFXPlay(string sfxName, AudioClip clip, bool isLoop = false)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(go, clip.length);

    }

    public void BGMSoundPlay(AudioClip clip)

    {
        bgmSound.clip = clip;
        bgmSound.loop = true;
        bgmSound.volume = 0.1f;
        bgmSound.Play();
    }
}
