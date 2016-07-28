using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D bird;
	private CharacterController birdControl;
	private bool facingLeft;
    public float speed;
	public Text countText;
	public Text winText;
	private int pickupCount;

	private bool dead;
	private bool isMoving;
	private Animator anim;

    void Start()
    {
        bird = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();

		pickupCount = 0;
		facingLeft = true;
		dead = false;
		winText.text = "";
		countText.text = "Collected: " + pickupCount.ToString ();
    }

	void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

		if (dead) {
			moveVertical = 0;
			moveHorizontal = 0;
		}

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
		anim.SetBool ("dead", dead);
			
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

		if (other.gameObject.CompareTag ("Bomb")) {
			other.gameObject.SetActive (false);

			winText.text = "You Lose!";
			dead = true;
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
