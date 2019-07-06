using UnityEngine;
using System.Collections.Generic;

namespace Fluent
{

    [FluentNodeHandler(typeof(YellHandler), true)]
    public class YellNode : FluentNode
    {
        public static float DefaultSecondsToPause = 1.5f;

        public bool IsBillboard { get; set; }

        /// <summary>
        /// Fluent method to set this yell's canvas to always face the camera
        /// </summary>
        /// <returns></returns>
        public YellNode Billboard()
        {
            IsBillboard = true;
            return this;
        }

        public GameObject TalkingObject = null;

        /// <summary>
        /// How long should we pause after showing the yell
        /// </summary>
        public float SecondsToPause = DefaultSecondsToPause;


        /// <summary>
        /// Yell a list of sentences
        /// </summary>
        public FluentString[] Sentences { get; set; }

        public YellNode(GameObject gameObject) 
            : base(gameObject)
        {
        }

        public YellNode(GameObject gameObject, params FluentString[] text)
            : base(gameObject)
        {
            Sentences = text;
        }

        public YellNode(GameObject gameObject, float secondsToPause, params FluentString[] fluentString) 
            : base(gameObject)
        {
            Sentences = fluentString;
            SecondsToPause = secondsToPause;
        }

        public override void BeforeExecute()
        {
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override string StringInEditor()
        {
            string yeller = null;
            if (TalkingObject != null)
                yeller = TalkingObject.name;
            else
                yeller = "This";

            if (!string.IsNullOrEmpty(yeller))
                return "<b>Yell <color=#ffffffff>[" + yeller + "]</color>: </b>" + FluentString.Join("+", Sentences);

            return "<b>Yell: </b>" + FluentString.Join("+", Sentences);
        }

        public override FluentNodeHandler GetHandler()
        {
            GameObject objectDoingTalking = TalkingObject;
            if (objectDoingTalking == null)
                objectDoingTalking = GameObject;

            YellHandler talkingObjectHandler = objectDoingTalking.GetComponentInChildren<YellHandler>();

            // If we found the handler return it
            if (talkingObjectHandler != null)
                return talkingObjectHandler;

            // Try to find the YellCanvas object
            Canvas[] canvases = objectDoingTalking.GetComponentsInChildren<Canvas>(true);

            if (canvases.Length == 0)
            {
                Debug.LogError("You have to add either explicitly add a 'YellResponseHandler' component to " + TalkingObject.name + " or add a canvas as a child for Yelling to work ", TalkingObject);
                return null;
            }

            if (canvases.Length > 1)
                Debug.LogWarning("You have more than one canvas as a child of an object that wants to yell, please explicity add the YellResponseHandler and define the canvas to use", TalkingObject);

            GameObject yellUI = canvases[0].gameObject;

            // Create a repsonse handler with the canvas
            YellHandler handler = objectDoingTalking.AddComponent<YellHandler>();
            handler.YellUI = yellUI;

            return handler;
        }
    }

    public partial class FluentScript : MonoBehaviour
    {
        protected YellNode Yell(params FluentString[] fluentString)
        {
            return new YellNode(gameObject, fluentString);
        }

        protected YellNode Yell(float secondsToPause, params FluentString[] fluentString)
        {
            return new YellNode(gameObject, secondsToPause, fluentString);
        }

        protected YellNode Yell(GameObject talkObject, float secondsToPause, params FluentString[] fluentString)
        {
            return new YellNode(gameObject) { TalkingObject = talkObject, Sentences = fluentString, SecondsToPause = secondsToPause };
        }

        protected YellNode Yell(GameObject talkObject, params FluentString[] fluentString)
        {
            return new YellNode(gameObject) { TalkingObject = talkObject, Sentences = fluentString, SecondsToPause = YellNode.DefaultSecondsToPause };
        }

    }

}
