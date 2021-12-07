using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class HitboxController : MonoBehaviour {
    private Player player;
    private GameObject bone;
    private List<Hitbox> hitboxes;

    void Start() {
        this.hitboxes = new List<Hitbox>();
        this.player = GetComponent<Player>();
    }
    public void CreateHitbox(string boneName, float size)
    {
        this.bone = GameObject.Find(boneName);
        Hitbox hitbox = bone.GetComponent<Hitbox>();
        if(hitbox == null)
        {
            hitbox = bone.AddComponent<Hitbox>();
            hitbox.size = size;
        } else {
            hitbox.enabled = true;
        }
        hitbox.player = this.player;
        this.hitboxes.Add(hitbox);
    }

    public void ClearHitboxes()
    {
        foreach(Hitbox hitbox in hitboxes)
        {
            hitbox.enabled = false;
        }
        this.hitboxes.Clear();
    }
  }