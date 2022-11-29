using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private float timer = 0f;
    [SerializeField] float speed;
    private float xVal = -1;
    // private bool hostility = false;
    void Start() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update() {
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

    // IEnumerator Rest() {
    //     anim.SetFloat("runSpeed", 0f);
    //     yield return new WaitForSeconds(1f);
    // }
    private void Flip() {
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
