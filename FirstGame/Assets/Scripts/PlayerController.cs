using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D bird;
	private bool facingLeft;
    public float speed;
	public Text countText;
	public Text winText;
	private int pickupCount;

	private bool isMoving;
	private Animator anim;

    void Start()
    {
        bird = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		pickupCount = 0;
		facingLeft = true;
		winText.text = "";
		countText.text = "Collected: " + pickupCount.ToString ();
    }

	void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        if( moveHorizontal < 0 && !facingLeft )
        {
            Flip();
        }

        if( moveHorizontal > 0 && facingLeft )
        {
            Flip();
        }

        bird.AddForce(movement * speed);

		if (moveHorizontal != 0 || moveVertical != 0) {
			isMoving = true;
		} else {
			isMoving = false;
		}

		anim.SetBool ("isMoving", isMoving);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if( other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
			pickupCount = pickupCount + 1;
			countText.text = "Collected: " + pickupCount.ToString ();

			if (pickupCount >= 9) {
				winText.text = "You Win!";
			}
        }
    }

    void Flip()
    {
        facingLeft = !facingLeft;

        Vector2 tempScale = transform.localScale;
        tempScale.x = -1 * transform.localScale.x;
        transform.localScale = tempScale;
    }
}
