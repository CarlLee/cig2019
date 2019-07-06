using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Fluent;

namespace Fluent
{
    [FluentNodeHandler(typeof(SoundHandler))]
    public class SoundNode : FluentNode
    {
        public string ResourceName;
        public SoundNode(GameObject gameObject, string resourceName) 
            :base(gameObject)
        {
            ResourceName = resourceName;
        }

        public override string StringInEditor()
        {
            return "<b>Sound: </b>" + ResourceName;
        }

        public override void Interrupt()
        {
            ((SoundHandler)GetHandler()).Interrupt(this);
        }

    }

    public partial class FluentScript : MonoBehaviour
    {
        protected SoundNode Sound(string resourceName)
        {
            return new SoundNode(gameObject, resourceName);
        }
    }

}

