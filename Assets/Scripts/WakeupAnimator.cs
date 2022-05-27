using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeupAnimator : MonoBehaviour
{
    [SerializeField]
    float delay = .5f;
    [SerializeField]
    private Animator playerAnimator;
    void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        playerAnimator?.SetBool("Wakeup", true);
        StartCoroutine(DoAnimation());
    }

    IEnumerator DoAnimation()
    {
        yield return new WaitForSeconds(delay);
        playerAnimator?.SetBool("Wakeup", false);
    }
}
