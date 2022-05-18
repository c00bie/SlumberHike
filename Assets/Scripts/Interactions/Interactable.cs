using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using SH.Dialogs;

namespace SH.Interactions
{
    [RequireComponent(typeof(Collider2D))]
    public class Interactable : MonoBehaviour
    {
        [SerializeField]
        [InspectorName("Dialog Settings")]
        private DialogParser dialogParser = new DialogParser();
        [SerializeField]
        private List<Interaction> interactionsBeforeDialogs = new List<Interaction>();
        [SerializeField]
        private List<Interaction> interactionsAfterDialogs = new List<Interaction>();
        [SerializeField]
        private bool autoplay = false;

        bool started = false;
        bool canStart = false;
        SH.Character.CharacterController player;
        NewInput input;

        private void Awake()
        {
            StartCoroutine(dialogParser.ParseDialogs());
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<SH.Character.CharacterController>();
            input = new NewInput();
            input.Actions.Grab.Enable();
        }

        private void Update()
        {
            if (!started && canStart && input.Actions.Grab.IsPressed())
            {
                StartCoroutine(StartActions());
            }
        }

        private IEnumerator StartActions()
        {
            started = true;
            foreach (Interaction interaction in interactionsBeforeDialogs)
            {
                if (interaction.IsAsync)
                    yield return interaction.DoActionAsync();
                else
                    interaction.DoAction();
            }
            yield return dialogParser.ProcessDialogs();
            foreach (Interaction interaction in interactionsAfterDialogs)
            {
                if (interaction.IsAsync)
                    yield return interaction.DoActionAsync();
                else
                    interaction.DoAction();
            }
            started = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!started && collision.CompareTag("Player"))
            {
                if (autoplay)
                    StartCoroutine(StartActions());
                else
                    canStart = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
                canStart = false;
        }
    }
}