using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueBtn : MonoBehaviour
{
    private SaveData saveData;
    void Start()
    {
        saveData = FindObjectOfType<SaveData>();
    }

    public void Load() {
        saveData.LoadFromJson();
    }
}
