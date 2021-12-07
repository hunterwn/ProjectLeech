using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
  public static AudioManager instance;
  
  [SerializeField]
  private Sound[] sounds;
  [SerializeField]
  AudioMixerGroup mixerGroup;

  private void Awake() {
    if (instance == null) {
      instance = this;
    }
    else {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);

    foreach (Sound sound in sounds) {
      AudioSource source = gameObject.AddComponent<AudioSource>();
      source.outputAudioMixerGroup = mixerGroup;
      sound.setSource(source);
    }
  }

  private void Start() {
    play("theme");
  }

  public void play(string name) {
    Sound sound = Array.Find(sounds, sound => sound.name == name);
    sound.source.Play();
  }

  public void stop(string name) {
    Sound sound = Array.Find(sounds, sound => sound.name == name);
    sound.source.Stop();
  }
}
