using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SH.Character;
using SH.Managers;

public class IntroManager : MonoBehaviour
{
    [SerializeField] float delay = 16;
    [SerializeField] GameObject motylek;
    [SerializeField] AudioClip introMusic;
    [SerializeField] AudioClip gameMusic;
    SoundManager soundManager;
    GameObject playerObj;
    SH.Character.CharacterController player;
    bool started = false;
    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        //playerObj = GameObject.FindGameObjectWithTag("Player");
        //player = playerObj.GetComponent<SH.Character.CharacterController>();
    }

    private void Update()
    {
        if (!started)
        {
            started = true;
            StartCoroutine(RunIntro());
        }
    }

    IEnumerator RunIntro()
    {
       // soundManager.ChangeBackgroundMusic(introMusic);
        //player.PauseMovement();
        //playerObj.SetActive(false);
        yield return new WaitForSeconds(delay);
        //soundManager.ChangeBackgroundMusic(gameMusic);
        motylek.SetActive(true);
        //playerObj.transform.position = new Vector3(playerObj.transform.position.x, 2, playerObj.transform.parent.position.z);
        //player.ResumeMovement();
        //playerObj.SetActive(true);
    }
}
