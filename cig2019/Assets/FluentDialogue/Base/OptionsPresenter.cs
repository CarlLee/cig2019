using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using TMPro;

namespace Fluent
{
    public delegate void SelectNodeOptionDelegate(OptionNode optionNode);

    [AddComponentMenu("Fluent/Options Presenter")]
    public class OptionsPresenter : MonoBehaviour
    {

        public void Start()
        {
            // It is easier to build the UI with options in the options panel
            // Here we clear those options
            ClearOptions();
        }

        /// <summary>
        /// Public
        /// </summary>
        public GameObject DialogUI = null;
        public GameObject OptionItemUI = null;
        public GameObject OptionsPanel;

        private OptionsNode currentOptionsNode;

        public void SetupOptions(OptionsNode optionsNode)
        {
            // Store the OptionsNode
            currentOptionsNode = optionsNode;

            OptionNode firstItem = null;
            foreach (FluentNode fluentNode in optionsNode.Children)
            {
                // The optionsNode children are treated sequentially until we find an option, 
                // after that all nodes should be Options
                if (!(fluentNode is OptionNode))
                    continue;

                OptionNode option = fluentNode as OptionNode;

                // Check if we should show this option
                if ((option.OnlyDisplayOnce && option.HasBeenChosen) || (option.ShouldShow != null && !option.ShouldShow()))
                    continue;

                // Create the options prefab 
                UnityEngine.Object dialogOptionItemPrefab = OptionItemUI;

                if (dialogOptionItemPrefab == null)
                {
                    Debug.LogError("Looks like you are trying to add options: You need define a unity ui prefab that will represent your dialog option", this);
                    return;
                }

                GameObject dialogOptionItem = (GameObject)Instantiate(dialogOptionItemPrefab, new Vector3(0, 0, 0), Quaternion.identity);

                if (OptionsPanel == null)
                {
                    Debug.LogError("You need to define where the options are added to, set the OptionsPanel on " + this.GetType().Name, this);
                    return;
                }

                dialogOptionItem.transform.SetParent(OptionsPanel.transform, false);

                TextMeshProUGUI dialogOptionItemText = dialogOptionItem.GetComponentInChildren<TextMeshProUGUI>();
                if (dialogOptionItemText == null)
                {
                    Debug.LogError("Please add a Unity Text component to your Dialog Item prefab ('" + dialogOptionItem.name + "'");
                    return;
                }

                // Set the text to the dialog option's text
                dialogOptionItemText.text = option.OptionText;

                // If there is a button component on the dialog option we connect the on click add listener
                if (dialogOptionItem.GetComponentInChildren<Button>() != null)
                {
                    dialogOptionItem.GetComponentInChildren<Button>().onClick.AddListener(() =>
                    {
                        SelectOption(option);
                    });
                }
                else
                {
                    Debug.LogError("You need to have a Button component somewhere in your OptionItemUI prefab", dialogOptionItem);
                }
                
                // If its the first item, then give it focus
                if (firstItem == null)
                {
                    EventSystem.current.SetSelectedGameObject(dialogOptionItem);
                    firstItem = option;
                }
            }
        }

        public void End()
        {
            // Find the highest level Options node
            FluentNode highestOptionsNode = currentOptionsNode;
            while(highestOptionsNode.Parent is OptionsNode || highestOptionsNode.Parent is OptionNode)
                highestOptionsNode = highestOptionsNode.Parent;

            // Hide
            Hide();

            // Call done on it
            highestOptionsNode.Done();
        }

        void SelectOption(OptionNode optionNode)
        {
            // I assume selecting an option will clear the options
            // but I can see instances where it shouldn't work that way ...
            ClearOptions();

            optionNode.SetDoneCallback(OptionCompleted);
            optionNode.Execute();
        }

        private void OptionCompleted(FluentNode fluentNode)
        {
            OptionNode completedOptionNode = fluentNode as OptionNode;

            // Check to see if the selected option wants us to go back
            if (completedOptionNode.GoesBack)
            {
                currentOptionsNode.Done();
                return;
            }

            // The completed option does not go back, just redisplay these options
            currentOptionsNode.Execute();
        }

        public void ClearOptions()
        {
            if (DialogUI == null)
                return;

            if (OptionsPanel == null)
            {
                // Turns out people want to use the OptionsPresenter without options
                return;
            }

            while (OptionsPanel.transform.childCount > 0)
            {
                Transform child = OptionsPanel.transform.GetChild(0);
                child.SetParent(null); 
                GameObject.Destroy(child.gameObject);

                //todo disconnect button listeners ?
            }
        }

        public virtual void Show()
        {
            DialogUI.SetActive(true);
        }

        public virtual void Hide()
        {
            DialogUI.SetActive(false);
        }

    }

}
