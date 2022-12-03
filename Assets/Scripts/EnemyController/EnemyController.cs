using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private float timer = 0f;
    private float xVal = -1;
    private float distance;
    private bool isRight = false;
    private GameObject player;
    private HealthManager playerHealth;

    [SerializeField] float speed;
    [SerializeField] int life;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerHealth = FindObjectOfType<HealthManager>();
        player = GameObject.Find("Player");
    }

    void Update() {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 4) {
            float deltaX = player.transform.position.x - this.transform.position.x;
            anim.SetFloat("runSpeed", Mathf.Abs(deltaX));
            if (deltaX < 0 && isRight)
                Flip ();
            if (deltaX > 0 && !isRight)
                Flip ();

            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        } else {
            if (xVal < 0 && isRight)
                Flip ();
            if (xVal > 0 && !isRight)
                Flip ();
            IdleMoving();
        }
    }

    private void IdleMoving() {
        timer += Time.deltaTime;
        anim.SetFloat("runSpeed", Mathf.Abs(xVal));
        if (timer >= 2f) {
            // StartCoroutine(Rest());
            xVal = xVal * -1;
            Flip();
            timer = 0f;
        }
        body.velocity = new Vector2(xVal * speed, body.velocity.y);
    }

    private void Flip() {
        isRight = !isRight;
        Vector2 currentScale = this.gameObject.transform.localScale;
        currentScale.x *= -1;
        this.gameObject.transform.localScale = currentScale;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Weapon") {
            StartCoroutine(HitAnim());
            life -= 1;
            if (life == 0) {
                Destroy(this.gameObject);
            }
        }

        if (collider.gameObject.tag == "Player") {
            playerHealth.TakeDmg();
        }
    }

    IEnumerator HitAnim() {
        anim.SetBool("isHit", true);
        yield return new WaitForSeconds(0.05f);
        anim.SetBool("isHit", false);
    }
}
