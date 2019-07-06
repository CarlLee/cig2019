using UnityEngine;
using System.Collections;
using System;

namespace Fluent
{
    public delegate void GameActionDoneDelegate(GameAction gameAction);
    public class GameAction : MonoBehaviour
    {
        private GameActionDoneDelegate DoneEvent;
        [HideInInspector]
        public GameActionInitiator Initiator;

        public GameAction()
        {
        }

        protected void ActionDone()
        {
            if (DoneEvent == null)
            {
                Debug.LogError("Done event not set on GameAction: " + GetType().Name, this);
                return;
            }
            DoneEvent(this);
        }

        public void SetDoneEvent(GameActionDoneDelegate done)
        {
            DoneEvent = done;
        }

        public virtual void Execute()
        {
        }

        public virtual bool CanExecuteWhileActive
        {
            get
            {
                return false;
            }
        }

        public virtual string Description()
        {
            return "";
        }
    }

}
