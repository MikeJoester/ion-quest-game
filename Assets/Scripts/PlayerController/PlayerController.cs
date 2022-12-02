using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] GameObject dust;   
    private Rigidbody2D rb;
    private Animator animator;
    private SwordController swordController;

    public string startPoint;
    public static bool isDashing = false;

    // private Vector2 movement;
    private static bool playerExists;
    private bool isRight = true;
    private float xVal;
    private float yVal;
    private bool attacking = false;
    
     

    void Start() {
        if (!playerExists) {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            Destroy(gameObject);
        }
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        swordController = FindObjectOfType<SwordController>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update() {
        if (!attacking) {
            xVal = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
            yVal = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;
        } else {
            xVal = 0;
            yVal = 0;
        }

        if (Input.GetKey("z")) {isDashing = true;} else {isDashing = false;}

        if ((xVal != 0 || yVal != 0) && isDashing) {
            moveSpeed = 6.5f;
            StaminaController.instance.UseStamina(1f);
        }

        if (!isDashing) {
            moveSpeed = 3.5f;
        }

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
        if ((Input.GetKeyDown("x")) && (attacking == false)) {
            attacking = true;
            StartCoroutine(AtkDelay());
        }
    }

    IEnumerator AtkDelay() {
        //Starts Attack
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.3f);
        swordController.Attack();
        yield return new WaitForSeconds(0.3f);

        //Ends attack
        animator.SetBool("isAttacking", false);
        attacking = false;
        swordController.stopAttack();
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
