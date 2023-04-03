using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource mainMenuSound;
    [SerializeField] private AudioSource gameSceneSound;

    private AudioSource fishCatchSoundEf;
    private AudioSource wormDropSoundEf;

    public HookCollisions hookCollisions;
    private void Start()
    {
        fishCatchSoundEf = hookCollisions.fishCatchSound;
        wormDropSoundEf = hookCollisions.wormDropSound;


    }
    public void MuteToggleBG(bool mutedBG)
    {
        if (mutedBG)
        {
            mainMenuSound.volume = 0f;
            gameSceneSound.volume = 0f;
        }
        else
        {
            mainMenuSound.volume = 0.25f;
            gameSceneSound.volume = 0.25f;
        }
    }

    public void MuteToggleEF(bool mutedEF)
    {
        if (mutedEF)
        {
            fishCatchSoundEf.volume = 0f;
            wormDropSoundEf.volume = 0f;
        }
        else
        {
            fishCatchSoundEf.volume = 0.2f;
            wormDropSoundEf.volume = 0.2f;

        }
    }

}
