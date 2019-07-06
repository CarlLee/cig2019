using UnityEngine;
using System.Collections;
using Fluent;
using System;

namespace Fluent
{
    [FluentNodeHandler(typeof(PauseFluentNodeHandler))]
    public class PauseNode : FluentNode
    {
        public float Seconds;
        public PauseNode(float seconds, GameObject gameObject) : base(gameObject)
        {
            Seconds = seconds;
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "<b>Pause: </b>" + Seconds;
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode Pause(float seconds)
        {
            return new PauseNode(seconds, gameObject);
        }
    }

}