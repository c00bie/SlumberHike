using SH.Travel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    public class MommyManager : MonoBehaviour
    {
        [SerializeField]
        private Sprite left;
        [SerializeField]
        private Sprite right;
        [SerializeField]
        private Sprite def;
        [SerializeField]
        private int targetCount = 5;
        private SpriteRenderer srenderer;
        private bool dir = false;
        private int count = 0;

        [SerializeField]
        private Interactions.Interaction[] onCompleted = new Interactions.Interaction[0];

        void OnEnable()
        {
            if (srenderer == null)
                srenderer = GetComponent<SpriteRenderer>();
            dir = !dir;
            srenderer.sprite = dir ? left : right;
        }

        void OnDisable()
        {
            srenderer.sprite = def;
            if (++count == targetCount)
            {
                StartCoroutine(Completed());
            }
        }

        IEnumerator Completed()
        {
            foreach (var i in onCompleted)
            {
                if (i.IsAsync)
                    yield return i.DoActionAsync();
                else
                    i.DoAction();
            }
        }
    }
}