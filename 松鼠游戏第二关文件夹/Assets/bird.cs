using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;




public class bird : MonoBehaviour{
    public int score = 0;
    public Text t;
    private Rigidbody2D rigidbody;
    private PlayMakerFSM fsm;
    public AudioClip hitmusic;
    public AudioClip scoremusic;
    public GameObject failure;
       

    void Awake()
    {
        rigidbody= GetComponent<Rigidbody2D>();
        fsm = GetComponent<PlayMakerFSM>();

    }
    public void AddForce(Vector2 force)
    {
        Vector2 velocity = rigidbody.velocity;
        velocity.y = 0;
        rigidbody.velocity = velocity;
       
        rigidbody.AddForce(force);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "piaochong" || coll.gameObject.tag == "di" || coll.gameObject.tag == "shuzhi" || coll.gameObject.tag == "niao" || coll.gameObject.tag == "niao 1" || coll.gameObject.tag == "2" || coll.gameObject.tag == "1" || coll.gameObject.tag == "3" || coll.gameObject.tag == "4")
        {
            AudioSource.PlayClipAtPoint(hitmusic, transform.position);
            fsm.SendEvent("Gameover");
            failure.SetActive(true);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("songguo"))
        {
            score++;
            Destroy(collision.gameObject);
            AudioSource.PlayClipAtPoint(scoremusic, transform.position);
        }
    }
    public void Update()
    {
    
        t.text = "Score:" + score;
    }
    public void restart()
    {
        SceneManager.LoadScene("2");
    }

}
