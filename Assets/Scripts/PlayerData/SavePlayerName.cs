using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SavePlayerName : MonoBehaviour
{
    public TMP_InputField textBox;

    public void onClickSave() {
        PlayerPrefs.DeleteAll(); //reset playerprefs

        PlayerPrefs.SetString("name", textBox.text);
        Debug.Log($"Hello {PlayerPrefs.GetString("name")}");
    }
}
