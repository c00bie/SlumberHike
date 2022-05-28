using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SH.Managers 
{
    public class IntercomManager : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] buttons = new Sprite[12];
        [SerializeField]
        private Sprite[] digits = new Sprite[4];
        [SerializeField]
        private Sprite clear;
        [SerializeField]
        private Image intercom;
        [SerializeField]
        private Image panel;
        [SerializeField]
        private float glowTime = .5f;
        [SerializeField]
        private Interactions.Interaction[] onSuccess = new Interactions.Interaction[0];

        string code = "";

        void Start()
        {
            intercom.sprite = clear;
            panel.sprite = digits[0];
        }

        public void NumberClicked(int number)
        {
            StartCoroutine(ShowNumber(number));
            if (number == -1)
                code = "";
            else if (number == -2)
            {
                if (code == "666")
                {
                    StartCoroutine(Success());
                    return;
                }
                else
                    code = "";
            }
            else if (code.Length < 3)
                code += number.ToString();
            panel.sprite = digits[code.Length];
        }

        private IEnumerator Success()
        {
            foreach (var i in onSuccess)
            {
                if (i.IsAsync)
                    yield return i.DoActionAsync();
                else
                    i.DoAction();
            }
        }

        private IEnumerator ShowNumber(int number)
        {
            int num = number - 1;
            if (number == 0)
                num = 10;
            else if (number == -1)
                num = 9;
            else if (number == -2)
                num = 11;
            intercom.sprite = buttons[num];
            yield return new WaitForSecondsRealtime(glowTime);
            intercom.sprite = clear;
        }
    }
}