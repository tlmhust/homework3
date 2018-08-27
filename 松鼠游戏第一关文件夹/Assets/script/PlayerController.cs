using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    public int i = 0;//实现连跳的循环变量
    public int j = 0;//向下降的循环变量
    public const int JumpTimes = 2;//连跳次数
    public GameObject SuccedImage;
    public GameObject FailureImage;
    public Text ScoreText;
    public Text HpText;
    public int Score;
    public GameObject CaremaPosition;
    public Vector2 playerForword = Vector2.right;
    public float h;//h是在unity里面表示的一个参数,按下a后h小于0,按下d后h大于0,结合转向代码实现转向操作
    public int Hp = 1;
    //插入音乐
    public AudioClip JumpMusic;
    public AudioClip ScoreMusic;
    public AudioClip EnemyMusic;
    private void Update()
    {
        CaremaPosition.transform.position = transform.position;//让屏幕中心保持在主角松鼠上
        ScoreText.text = "Score:" + Score;
        HpText.text = "Hp:" + Hp;
        #region(实现连跳)
        if (i == JumpTimes && this.GetComponent<Rigidbody2D>().velocity.magnitude == 0)
        {
            i = 0;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            if(i==0||i==1)
            {
                this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1500);//向上方加力
                i++;
                AudioSource.PlayClipAtPoint(JumpMusic, transform.position);//播放跳跃音乐
            }
        }
        if (j == 1 && this.GetComponent<Rigidbody2D>().velocity.magnitude == 0)//一旦进行二连跳之后只有速度为0才能继续跳跃,保证不存在更多连跳
        {
            j = 0;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            if (j == 0)
            {
                this.GetComponent<Rigidbody2D>().AddForce(Vector2.down * 1000);//像下方加力
                j++;
            }
        }
        #endregion
        #region(实现ad移动操作)
        h = Input.GetAxis("Horizontal");
        if (h < 0)
        {
            transform.rotation = Quaternion.Euler(Vector3.up * 180f);
        }
        else if (h > 0)
        { 
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        transform.Translate(Vector2.right * Mathf.Abs(h) * 0.2f);
        #endregion
    }
    private void OnTriggerEnter2D(Collider2D collision)//物体发生碰撞
    {
        switch(collision.tag)//根据碰撞的物体不同后续操作不同
        {
            case "score":
                 Score++;
                Destroy(collision.gameObject);
                AudioSource.PlayClipAtPoint(ScoreMusic, transform.position);//激活得分音效
                break;
            case "enemy":
                if (Hp == 1)
                {
                    FailureImage.SetActive(true);
                    this.GetComponent<PlayerController>().enabled = false;//在游戏胜利或失败后让组件失活
                }
                else
                {
                    Hp--;
                    Destroy(collision.gameObject);
                }
                AudioSource.PlayClipAtPoint(EnemyMusic, transform.position);//碰到敌人音效
                break;
            case "HP":
                Hp++;
                Destroy(collision.gameObject);
                break;
            case "Dead"://无论剩余多少命都会直接死亡
                if (Score >= 20)
                {
                    SuccedImage.SetActive(true);
                    this.GetComponent<PlayerController>().enabled = false;
                }
                else
                {
                    FailureImage.SetActive(true);
                    this.GetComponent<PlayerController>().enabled = false;
                }
                break;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene("game");//重新开始按钮
    }
    public void Exit()
    {
        Application.Quit();//退出游戏按钮
    }
}