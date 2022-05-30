using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        AudioSource standardAudioSource;
        bool stillFading = false;
        public static bool alreadyExisting = false;

        // Ustawianie �r�d�a d�wi�k�w oraz dodanie obiektu do listy DontDestroyOnLoad
        private void Awake()
        {
            if (alreadyExisting)
            {
                Destroy(gameObject);
            }
            else
            {
                alreadyExisting = true;
            }

            if (standardAudioSource.clip != null)
            {
                standardAudioSource.Play();
            }

            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (InGameMenuManager.gameIsPaused)
            {
                standardAudioSource.Pause();
            }
            else
            {
                standardAudioSource.UnPause();
            }
        }

        // Zmienna wywo�uj�ca pojedy�czy efekt d�wi�kowy (jeszcze nic szczeg�lnego nie robi, ale na pewno pr�dzej czy p�niej b�dzie trzeba do tego doda� dodatkowe efekty)
        public void PlaySingleSound(AudioClip singleSound, float volumeScaling)
        {
            if (!InGameMenuManager.gameIsPaused)
            {
                standardAudioSource.PlayOneShot(singleSound, volumeScaling);
            }
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
            float start = standardAudioSource.volume;

            // Stopniowa zmiana g�o�no�ci muzyki
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                standardAudioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
                yield return null;
            }

            // Opcjonalna zmiana klipu muzycznego
            if (clipToChange != null)
            {
                standardAudioSource.clip = clipToChange;
                standardAudioSource.Play();
            }

            stillFading = false;
            yield break;
        }
    }
}
