using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STController : MonoBehaviour
{
    public GameObject thisObj;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            thisObj.SetActive(false);
        }
    }
}
