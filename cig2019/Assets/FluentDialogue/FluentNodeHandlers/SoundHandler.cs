using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace Fluent
{
    [AddComponentMenu("")]
    public class SoundHandler : FluentNodeHandler
    {
        List<GameObject> SoundPlayers = new List<GameObject>();
        SoundNode soundNode;

        public override void HandleFluentNode(FluentNode fluentNode)
        {
            soundNode = (SoundNode)fluentNode;

            // Create the sound player
            GameObject soundPlayerGameObject = SoundPlayer.CreateSoundPlayer(soundNode);
            SoundPlayer soundPlayer = soundPlayerGameObject.GetComponent<SoundPlayer>();
            soundPlayer.DonePlaying += soundPlayer_DonePlaying;

            // Make the game object the parent
            soundPlayer.transform.parent = soundNode.GameObject.transform;

            SoundPlayers.Add(soundPlayerGameObject);

        }

        void soundPlayer_DonePlaying(SoundPlayer soundPlayer)
        {
            FinishSoundPlayer(soundPlayer);
        }

        private void FinishSoundPlayer(SoundPlayer soundPlayer)
        {
            SoundPlayers.Remove(soundPlayer.gameObject);
            Destroy(soundPlayer.gameObject);
            soundPlayer.soundNode.Done();
        }

        public override void Interrupt(FluentNode fluentNode)
        {
            GameObject fluentPlayer = SoundPlayers.Find(x => x.GetComponent<SoundPlayer>().soundNode == fluentNode);

            SoundPlayer sp = fluentPlayer.GetComponent<SoundPlayer>();
            sp.StopPlaying();
            FinishSoundPlayer(sp);
        }


    }
}
