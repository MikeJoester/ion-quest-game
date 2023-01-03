using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TextBox : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] TextMeshProUGUI textComponent;
    [SerializeField] string[] lines;
    private float textDelay = 0.04f;
    // static int CharPhase = 0;
    [SerializeField] GameObject DialogueScreen;
    [SerializeField] TextMeshProUGUI objectName;
    private static bool ready2Activated = true;
    private GameObject player;

    // [Header("Character")]
    // [SerializeField] Image Character;
    // [SerializeField] Image charBorder;
    // [SerializeField] Sprite[] spriteArray;
    // [SerializeField] int AppearIndex;

    private int index;

    void Awake() {
        if (DialogueScreen.name == "FirstText" && ready2Activated == false) {
            DialogueScreen.SetActive(false);
        }

        if (objectName != null) {
            objectName.enabled = true;
            objectName.text = PlayerPrefs.GetString("name");
        }
    }

    void Start()
    {
        textComponent.text = string.Empty;
        player = GameObject.Find("Player");
        // charBorder.enabled = true;
        // Character.enabled = true;
        player.GetComponent<PlayerController>().setInteract = true;
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // if (CharPhase == AppearIndex) {
        //     charBorder.enabled = true;
        //     Character.enabled = true;
        // }
        
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.F)) {
            
            if (textComponent.text == lines[index]) {
                NextLine();

                // Character.sprite = spriteArray[CharPhase - AppearIndex];
            }
            else {
                StopAllCoroutines();
                textComponent.text = lines[index];
                // CharPhase += 1;
            }
        }
    }

    void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textDelay);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else {
            if (DialogueScreen.name == "FirstText") {
                ready2Activated = false;
            }

            DialogueScreen.SetActive(false);
            player.GetComponent<PlayerController>().setInteract = false;
        }
    }
}
