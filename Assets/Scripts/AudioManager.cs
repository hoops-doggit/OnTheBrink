using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClips { diamondPickup, hamsterDeath }

public class AudioManager : MonoBehaviour
{


    [Header("Audio Clips")]
    [SerializeField] AudioClip[] diamondPickup;
    [SerializeField] AudioClip[] hamsterDeath;

    [Header("Music")]
    public AudioClip gameMusic;

    [Header("Audio Sources")]
    public AudioSource[] audioSources;

    private int GetNonPlayingSource()
    {
        for(int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                return i;
            }
            else
            {
                return 0;
            }
        }
        return 0;
    }

    public void PlaySound(AudioClips _ac)
    {
        int i = 0;
        int j = GetNonPlayingSource();
        switch (_ac)
        {
            case AudioClips.diamondPickup:
                i = Random.Range(0, diamondPickup.Length);
                audioSources[j].PlayOneShot(diamondPickup[i]);
                break;
            case AudioClips.hamsterDeath:
                i = Random.Range(0, diamondPickup.Length);
                audioSources[j].PlayOneShot(hamsterDeath[i]);
                break;
        }
    }


}
