using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Fluent
{

    public delegate void SoundPlayerDoneDelegate(SoundPlayer soundPlayer);
    public class SoundPlayer : MonoBehaviour
    {
        public SoundNode soundNode;
        AudioSource audioSource;
        public event SoundPlayerDoneDelegate DonePlaying;

        void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            //audioSource.bypassEffects = true;
            //audioSource.bypassListenerEffects = true;
            //audioSource.bypassReverbZones = true;
            audioSource.rolloffMode = AudioRolloffMode.Custom;
            audioSource.clip = Resources.Load<AudioClip>(soundNode.ResourceName);

            if (audioSource.clip == null)
            {
                Debug.LogWarning("Could not load sound clip: " + soundNode.ResourceName);
                return;
            }

            audioSource.Play();
        }

        void Update()
        {
            if (!audioSource.isPlaying)
                DonePlaying(this);
        }

        public void StopPlaying()
        {
            audioSource.Stop();
            DonePlaying(this);
        }

        public static GameObject CreateSoundPlayer(SoundNode soundNode)
        {
            // Create the object that will be responsible for playing the sound
            GameObject go = new GameObject("Sound Handler");
            SoundPlayer soundPlayer = go.AddComponent<SoundPlayer>();
            soundPlayer.soundNode = soundNode;

            return go;
        }
    }
}
