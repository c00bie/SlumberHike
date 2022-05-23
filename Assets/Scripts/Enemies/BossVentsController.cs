using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Enemy
{
    public class BossVentsController : MonoBehaviour
    {
        [SerializeField]
        private float delay = 4f;
        [SerializeField]
        private BossVent[] queue = new BossVent[0];
        [SerializeField]
        private float handMoveDuration = 1f;

        void Awake()
        {
            StartCoroutine(StartFight());
        }

        void Update()
        {

        }

        IEnumerator StartFight()
        {
            yield return new WaitForSeconds(delay);
            foreach (var item in queue)
            {
                yield return item.StartAttack(handMoveDuration);
                yield return new WaitForSeconds(delay);
            }
        }
    }
}
