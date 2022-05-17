using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    public class SoundManager : MonoBehaviour
    {
        AudioSource audioSource;

        private void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }

            DontDestroyOnLoad(transform.gameObject);
        }

        public void PlaySingleSound(AudioClip singleSound)
        {
            audioSource.PlayOneShot(singleSound);
        }

        //Sorry za komentarze, jeszcze to skoñczê w najbli¿szym czasie
        public void ChangeBackgroundMusic(AudioClip backgroundClip)
        {
            audioSource.Stop();
            //StartCoroutine(FadeBackgroundMusic(2.5f));
            audioSource.clip = backgroundClip;
            //StartCoroutine(ReturnBackgroundMusic(2.5f));
            audioSource.Play();
        }

        /**
        public IEnumerator FadeBackgroundMusic(float duration)
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 0, currentTime / duration);
                yield return null;
            }

            audioSource.Stop();
            yield break;
        }

        public IEnumerator ReturnBackgroundMusic(float duration)
        {
            float currentTime = 0;
            float start = audioSource.volume;

            audioSource.Play();
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 1, currentTime / duration);
                yield return null;
            }
            yield break;
        }
        */
    }
}
