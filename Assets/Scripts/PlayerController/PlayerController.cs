using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private float moveSpeed = 3f;
    private int attackStat = 2;
    [SerializeField] GameObject dust;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject deadAlert;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] SwordController swordController;
    [SerializeField] float damageDelay;
    private Rigidbody2D rb;
    private Animator animator;
    

    public string startPoint;
    public static bool isDashing = false;
    private int totalMoney = 0;

    // private Vector2 movement;
    private static bool playerExists;
    private static bool UIExists;
    private bool isRight = true;
    private float xVal;
    private float yVal;
    private bool isInteract = false;
    public bool attacking = false;
    
    public int playerMoney {
        get {return totalMoney;}
        set {totalMoney = value;}
    }

    public bool setInteract {
        get {return isInteract;}
        set {isInteract = value;}
    }

    public int getAtk {
        get {return attackStat;}
        set {attackStat = value;}
    }
    
    void Awake() {
        if (!playerExists) {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            Destroy(gameObject);
        }
        if (!UIExists) {
            UIExists = true;
            DontDestroyOnLoad(gameUI);
        }
        else {
            Destroy(gameUI);
        }
    }

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        // swordController = FindObjectOfType<SwordController>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    void Update() {
        moneyText.text = totalMoney.ToString();
        if (attacking || isInteract) {
            xVal = 0;
            yVal = 0;
        } else {
            xVal = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
            yVal = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;
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

    public IEnumerator PlayerDead() {
        animator.SetTrigger("Dead");
        yield return new WaitForSeconds(1.5f);
        deadAlert.SetActive(true);
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

    public IEnumerator isHitAnim() {
        Collider2D collidebox = this.GetComponent<Collider2D>();
        animator.SetTrigger("isHit");
        collidebox.enabled = false;
        yield return new WaitForSeconds(damageDelay);
        collidebox.enabled = true;
    }
}
