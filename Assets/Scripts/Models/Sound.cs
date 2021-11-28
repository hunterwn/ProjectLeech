using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {
  public string name;
  public AudioClip clip;
  
  [Range(0f, 1f)]
  public float volume = 1f;
  [Range(0.1f, 3f)]
  public float pitch = 1f;
  public bool loop = false;

  [HideInInspector]
  public AudioSource source;

  public void setSource(AudioSource source) {
    this.source = source;
    this.source.clip = this.clip;
    this.source.volume = this.volume;
    this.source.pitch = this.pitch;
    this.source.loop = this.loop;
  }
}
