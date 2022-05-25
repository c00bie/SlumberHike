using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SH.Managers
{
    public class SoundManager : MonoBehaviour
    {
        AudioSource audioSource;
        bool stillFading = false;

        // Ustawianie Ÿród³a dŸwiêków oraz dodanie obiektu do listy DontDestroyOnLoad
        private void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }

            DontDestroyOnLoad(gameObject);
        }

        // Zmienna wywo³uj¹ca pojedyñczy efekt dŸwiêkowy (jeszcze nic szczególnego nie robi, ale na pewno prêdzej czy póŸniej bêdzie trzeba do tego dodaæ dodatkowe efekty)
        public void PlaySingleSound(AudioClip singleSound)
        {
            audioSource.PlayOneShot(singleSound);
        }

        // Metoda, która zmienia muzykê w t³a, za pomoc¹ enumeratora
        public void ChangeBackgroundMusic(AudioClip backgroundClip)
        {
            StartCoroutine(FadeMusic(2.5f, 0, 0, backgroundClip));
            StartCoroutine(FadeMusic(2.5f, 1, 2.5f, null));
        }

        // Enumerator odpowiadaj¹cy za wyciszenie/zg³oœnienie muzyki oraz opcjonaln¹ zmianê utworu przy podaniu trzeciego parametru
        public IEnumerator FadeMusic(float duration, float targetVolume, float waitForSeconds, AudioClip clipToChange)
        {
            // Sprawdzanie czy poprzednie wywo³anie enumeratora zosta³o zakoñczone
            if (stillFading)
            {
                yield return new WaitForSeconds(waitForSeconds);
            }

            stillFading = true;
            float currentTime = 0;
            float start = audioSource.volume;

            // Stopniowa zmiana g³oœnoœci muzyki
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
