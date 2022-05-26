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

        public IEnumerator StartAttack(BossVentsController ctrl)
        {
            animator.SetBool("Shake", true);
            yield return new WaitForSeconds(ctrl.ventShakeDuration);
            animator.SetBool("Shake", false);
            yield return new WaitForSeconds(1 / 60 * 5f);
            float f = ceiling ? -1 : 1;
            GameObject hand = Instantiate(handPrefab, transform.position - new Vector3(0, ctrl.handStartY * f, 0), Quaternion.AngleAxis(ceiling ? 0 : 180, Vector3.forward));
            BossHand bh = hand.GetComponent<BossHand>();
            bh.end = new Vector3(transform.position.x, ctrl.handEndY * f, transform.position.z);
            bh.controller = ctrl;
            bh.enabled = true;
        }
    }
}
