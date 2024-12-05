using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    //This prevents the AudioManager from duplicating (1)
    public static AudioManager instance;

    void Awake()
    {
        //This prevents the AudioManager from duplicating (2)
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //This allows music to flow between scenes
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        
        Play("Music");
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        if (s.randomizePitch)
        {
            float variation = UnityEngine.Random.Range(-s.pitchVariation, s.pitchVariation);
            s.source.pitch = s.pitch + variation;
        }
        
        s.source.Play();
    }
}
