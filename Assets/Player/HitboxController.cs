using System.Collections;
using System.Collections.Generic;
using UnityEngine;

  public class HitboxController : MonoBehaviour {

    private GameObject bone;
    private List<SphereCollider> hitboxes;

    void Start() {
        hitboxes = new List<SphereCollider>();
    }
    public void CreateHitbox(string boneName, float size)
    {
        bone = GameObject.Find(boneName);
        SphereCollider hitbox = bone.AddComponent<SphereCollider>();
        hitboxes.Add(hitbox);
        hitbox.isTrigger = true;
        hitbox.radius = size / 1000;
    }

    public void ClearHitboxes()
    {
        foreach(SphereCollider hitbox in hitboxes)
        {
            hitboxes.Remove(hitbox);
            Destroy(hitbox);
        }
    }
  }