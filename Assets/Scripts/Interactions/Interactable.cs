using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using SH.Dialogs;

namespace SH.Interactions
{
    public enum InteractablePlaySchedule
    {
        OnStart,
        OnAwake,
        OnEnable,
        None
    }
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
        [SerializeField]
        private float debounce = .5f;
        [SerializeField]
        private bool playOnce = false;
        [SerializeField]
        private InteractablePlaySchedule playOn = InteractablePlaySchedule.None;
        [SerializeField]
        private bool glowWhenNear = false;
        [SerializeField]
        private SpriteRenderer glowTarget = null;

        bool started = false;
        bool _canStart = false;
        bool canStart
        {
            get => _canStart;
            set
            {
                _canStart = value;
                if (glowTarget != null)
                {
                    if (glowWhenNear)
                        glowTarget.material.SetInteger("_Glow", value ? 1 : 0);
                    Debug.Log(glowTarget.material.GetInteger("_Glow"));
                }
            }
        }
        SH.Character.CharacterController player;
        NewInput input;
        bool played = false;

        private void Awake()
        {
            StartCoroutine(dialogParser.ParseDialogs());
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<SH.Character.CharacterController>();
            input = new NewInput();
            input.Actions.Grab.Enable();
            if (playOn == InteractablePlaySchedule.OnAwake)
                StartCoroutine(StartActions());
        }

        private void OnEnable()
        {
            if (playOn == InteractablePlaySchedule.OnEnable)
                StartCoroutine(StartActions());
        }

        private void Start()
        {
            if (glowTarget is null)
                glowTarget = GetComponent<SpriteRenderer>();
            if (playOn == InteractablePlaySchedule.OnStart)
                StartCoroutine(StartActions());
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
            if (playOnce && played && !DialogParser.IsRunning)
                yield break;
            played = true;
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
            yield return new WaitForSecondsRealtime(debounce);
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