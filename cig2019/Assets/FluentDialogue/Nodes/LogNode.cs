using UnityEngine;
using System.Collections;
using Fluent;
using System;

namespace Fluent
{

    public class LogNode : FluentNode
    {
        public string StringToPrint { get; set; }

        public LogNode(string stringToPrint, GameObject gameObject) : base(gameObject)
        {
            StringToPrint = stringToPrint;
        }

        public override void Execute()
        {
            Debug.Log(StringToPrint, GameObject);
            DoneDelegate(this);
        }

        public override void BeforeExecute()
        {
        }

        public override string StringInEditor()
        {
            return "<b>Log:</b> " + StringToPrint;
        }

    }

    public partial class FluentScript : MonoBehaviour
    {
        public FluentNode Log(string stringToPrint)
        {            
            return new LogNode(stringToPrint, gameObject);
        }
    }

}
