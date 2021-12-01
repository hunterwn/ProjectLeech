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
        bone = transform.Find(boneName).gameObject;
        SphereCollider hitbox = bone.GetComponent<SphereCollider>();
        if(hitbox == null)
        {
            hitbox = bone.AddComponent<SphereCollider>();
        } else {
            hitbox.enabled = true;
        }
        hitboxes.Add(hitbox);
        hitbox.isTrigger = true;
        hitbox.radius = size / 1000;
        hitbox.tag = "hitbox";
    }

    public void ClearHitboxes()
    {
        foreach(SphereCollider hitbox in hitboxes)
        {
            //hitboxes.Remove(hitbox);
            hitbox.enabled = false;
            hitbox.radius = 0.0f;
        }

        hitboxes.Clear();
    }
  }