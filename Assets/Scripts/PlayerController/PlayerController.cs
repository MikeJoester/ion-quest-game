using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] GameObject dust;   
    private Rigidbody2D rb;
    private Animator animator;

    public string startPoint;

    // private Vector2 movement;
    private bool isRight = true;
    private float xVal;
    private float yVal;
    private bool attacking = false;
     

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    // public string startP {
    //     get { return startP; }
    // }

    // public Vector2 playerMovement {
    //     get { return movement; }
    // }

    // Update is called once per frame
    void Update() {
        xVal = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
        yVal = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;

        //Flip function
        if (xVal > 0 && !isRight) {
            Flip();
        }
        if (xVal < 0 && isRight) {
            Flip();
        }
        if (xVal == 0) {
            animator.SetFloat("Speed", Mathf.Abs(yVal));
        } else animator.SetFloat("Speed", Mathf.Abs(xVal));

        //Move Function
        var pos = new Vector2(transform.position.x + xVal, transform.position.y + yVal);
        transform.position = Vector3.Lerp(pos, transform.position, 0.1f);

        //Generate dust particles every x frame
        if (xVal != 0 || yVal != 0) {
            if (Time.frameCount % 20 == 0) {
                GameObject instance = (GameObject)Instantiate(dust, transform.position, Quaternion.identity);
                Destroy(instance, 0.8f);
            }
        }

        //Attack trigger
        if ((Input.GetKeyDown("z")) && (attacking == false)) {
            attacking = true;
            StartCoroutine(AtkDelay());
        }
    }

    IEnumerator AtkDelay() {
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.5f);
        attacking = false;
        animator.SetBool("isAttacking", false);
        yield return new WaitForSeconds(0.5f);
    }

    private void Flip() {
        Vector2 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        isRight = !isRight;
    }

    public void Fade(bool fading) {
        animator.SetBool("isEnter", fading);
    }
}
