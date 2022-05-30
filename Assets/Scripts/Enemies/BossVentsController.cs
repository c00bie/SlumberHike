using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Enemy
{
    [System.Serializable]
    public struct BossVentInfo
    {
        public BossVent[] vents;
    }

    public class BossVentsController : MonoBehaviour
    {
        [SerializeField]
        float initialDelay = 3f;
        [SerializeField]
        private float delay = 4f;
        [SerializeField]
        private BossVentInfo[] queue = new BossVentInfo[0];
        [SerializeField]
        private GameObject door;

        [Min(0)]
        public float handMoveDuration = .5f;
        [Min(1)]
        public int lives = 3;
        [Min(0)]
        public float ventShakeDuration = 1f;
        public float handStartY = 5f;
        public float handEndY = 2f;

        void Start()
        {
            if (door != null)
                door.SetActive(false);
            StartCoroutine(StartFight());
        }

        IEnumerator StartFight()
        {
            yield return new WaitForSeconds(initialDelay);
            foreach (var item in queue)
            {
                for (int i = 0; i < item.vents.Length; i++)
                {
                    if (i == item.vents.Length - 1)
                        yield return item.vents[i].StartAttack(this);
                    else
                        StartCoroutine(item.vents[i].StartAttack(this));
                }
                yield return new WaitForSeconds(delay);
            }
            if (door != null)
                door.SetActive(true);
        }
    }
}
