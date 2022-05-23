using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Enemy
{
    public class BossVent : MonoBehaviour
    {
        [SerializeField]
        private GameObject handPrefab;
        [SerializeField]
        private bool ceiling = false;

        private Animator animator;
        
        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        void Update()
        {

        }

        public IEnumerator StartAttack(float handDuration)
        {
            animator.SetBool("Shake", true);
            yield return new WaitForSeconds(1f);
            animator.SetBool("Shake", false);
            yield return new WaitForSeconds(1 / 60 * 5f);
            GameObject hand = Instantiate(handPrefab, transform.position - new Vector3(0, ceiling ? -10 : 10, 0), Quaternion.AngleAxis(ceiling ? 0 : 180, Vector3.forward));
            BossHand bh = hand.GetComponent<BossHand>();
            bh.end = new Vector3(transform.position.x, 0, transform.position.z);
            bh.duration = handDuration;
            bh.enabled = true;
        }
    }
}
