using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBlockers : MonoBehaviour
{
    void Start()
    {
        GameObject viewBlockers = transform.Find("ViewBlockers").gameObject;

        viewBlockers.SetActive(true);
    }
}
