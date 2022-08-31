using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MusicChanger : MonoBehaviour
{
    [SerializeField] private AudioClip[] music;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject Plate;
    [SerializeField] private int previousTrack = 2;
    [SerializeField] private int randomNumber = -1;
    private AudioSource mixer;

    private void Start()
    {
        mixer = GetComponent<AudioSource>();
        mixer.clip = music[Random.Range(0, music.Length)];
        mixer.Play();
        ShowName();
        StartCoroutine(NextPlaying());
    }

    private void ShowName()
    {
        Plate.SetActive(true);
        text.text = mixer.clip.name;
        StartCoroutine(HidingName());
    }


    IEnumerator NextPlaying()
    {
        mixer.Play();
        yield return new WaitForSeconds(mixer.clip.length);
        while (randomNumber != previousTrack) 
        { 
            randomNumber = Random.Range(0, music.Length);
        }
        mixer.clip = music[randomNumber];
        previousTrack = randomNumber;
        mixer.Play();
        ShowName();
    }
    IEnumerator HidingName()
    {
        yield return new WaitForSeconds(3);
        Plate.SetActive(false);
    }
}
