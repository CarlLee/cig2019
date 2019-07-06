using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

namespace Fluent
{
    [FluentNodeHandler(typeof(WriteHandler), true)]
    public class WriteNode : FluentNode
    {
        public static float DefaultSecondsToPause = 1.5f;
        public float SecondsToPause;
        public bool WaitForButtonPress = false;

        public TextMeshProUGUI TextUIElement;

        /// <summary>
        /// The text of this option
        /// </summary>
        public FluentString Text { get; set; }

        /// <summary>
        /// Tell this Write to wait for a button press
        /// </summary>
        /// <returns></returns>
        public WriteNode WaitForButton()
        {
            WaitForButtonPress = true;
            return this;
        }

        public override string StringInEditor()
        {
            return "<b>Write: </b>" + Text;
        }

        public WriteNode(GameObject gameObject, float secondsToPause, FluentString fluentString)
            : base(gameObject)
        {
            SecondsToPause = secondsToPause;
            Text = fluentString;
        }

        public WriteNode(GameObject gameObject, TextMeshProUGUI textUIElement, float secondsToPause, FluentString fluentString)
            : this(gameObject, secondsToPause, fluentString)
        {
            TextUIElement = textUIElement;
        }

    }

    public partial class FluentScript
    {
        protected WriteNode Write(FluentString fluentString)
        {
            return new WriteNode(gameObject, WriteNode.DefaultSecondsToPause, fluentString);
        }

        protected WriteNode Write(float secondsToPause, FluentString fluentString)
        {
            return new WriteNode(gameObject, secondsToPause, fluentString);
        }

        protected WriteNode Write(TextMeshProUGUI textUIElement, FluentString fluentString)
        {
            return new WriteNode(gameObject, textUIElement, WriteNode.DefaultSecondsToPause, fluentString);
        }

        protected WriteNode Write(TextMeshProUGUI textUIElement, float secondsToPause, FluentString fluentString)
        {
            return new WriteNode(gameObject, textUIElement, secondsToPause, fluentString);
        }
    }

}
