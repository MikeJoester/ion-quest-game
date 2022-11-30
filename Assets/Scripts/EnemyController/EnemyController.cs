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

    [SerializeField] GameObject player;
    [SerializeField] float speed;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            Debug.Log("Hit");
        }

        if (collider.gameObject.tag == "Player") {
            Debug.Log("minus HP");
        }
    }
}
