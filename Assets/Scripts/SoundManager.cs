using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    public class SoundManager : MonoBehaviour
    {
        AudioSource audioSource;
        bool stillFading = false;

        // Ustawianie �r�d�a d�wi�k�w oraz dodanie obiektu do listy DontDestroyOnLoad
        private void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }

            DontDestroyOnLoad(gameObject);
        }

        // Zmienna wywo�uj�ca pojedy�czy efekt d�wi�kowy (jeszcze nic szczeg�lnego nie robi, ale na pewno pr�dzej czy p�niej b�dzie trzeba do tego doda� dodatkowe efekty)
        public void PlaySingleSound(AudioClip singleSound)
        {
            audioSource.PlayOneShot(singleSound);
        }

        // Metoda, kt�ra zmienia muzyk� w t�a, za pomoc� enumeratora
        public void ChangeBackgroundMusic(AudioClip backgroundClip)
        {
            StartCoroutine(FadeMusic(2.5f, 0, 0, backgroundClip));
            StartCoroutine(FadeMusic(2.5f, 1, 2.5f, null));
        }

        // Enumerator odpowiadaj�cy za wyciszenie/zg�o�nienie muzyki oraz opcjonaln� zmian� utworu przy podaniu trzeciego parametru
        public IEnumerator FadeMusic(float duration, float targetVolume, float waitForSeconds, AudioClip clipToChange)
        {
            // Sprawdzanie czy poprzednie wywo�anie enumeratora zosta�o zako�czone
            if (stillFading)
            {
                yield return new WaitForSeconds(waitForSeconds);
            }

            stillFading = true;
            float currentTime = 0;
            float start = audioSource.volume;

            // Stopniowa zmiana g�o�no�ci muzyki
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }

            // Opcjonalna zmiana klipu muzycznego
            if (clipToChange != null)
            {
                audioSource.clip = clipToChange;
                audioSource.Play();
            }

            stillFading = false;
            yield break;
        }
    }
}
