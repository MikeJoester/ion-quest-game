using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer enemySprite;
    private Animator anim;
    private float timer = 0f;
    private float xVal = -1;
    private float distance;
    private bool isRight = false;
    private GameObject player;
    private HealthManager playerHealth;

    [SerializeField] float speed;
    [SerializeField] int life;
    [SerializeField] float distanceLimit;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerHealth = FindObjectOfType<HealthManager>();
        enemySprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
    }

    void Update() {

        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < distanceLimit) {
            float deltaX = player.transform.position.x - this.transform.position.x;
            anim.SetFloat("runSpeed", Mathf.Abs(deltaX));
            if (deltaX < 0 && isRight)
                {Flip ();}
            if (deltaX > 0 && !isRight)
                {Flip ();}

            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        } else {
            if (xVal < 0 && isRight)
                {Flip(); Debug.Log("Flipped Left");}
            if (xVal > 0 && !isRight)
                {Flip (); Debug.Log("Flipped Right");}
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
        enemySprite.flipX = isRight;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Weapon") {
            life -= 1;
            if (life <= 0) {
                StartCoroutine(DeadAnim());
            } else {
                StartCoroutine(HitAnim());
            }
        }

        if (collider.gameObject.tag == "Player") {
            playerHealth.TakeDmg();
        }
    }

    IEnumerator DeadAnim() {
        speed = 0f;
        anim.SetTrigger("Dead");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    IEnumerator HitAnim() {
        anim.SetBool("isHit", true);
        yield return new WaitForSeconds(0.01f);
        anim.SetBool("isHit", false);
    }
}
