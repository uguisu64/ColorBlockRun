using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    private bool canJump;
    private ColorType color;
    private float differTime;
    
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        canJump = false;
        differTime = 0;

        //色の初期化
        color = ColorType.BLUE;
        PlayerColorChange();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (canJump)
            {
                rigidbody.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
                canJump = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerColorChange();
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (!canJump)
            {
                rigidbody.AddForce(Vector2.down * 2);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Ground")
        {
            canJump = true;
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            GameOver();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            GroundScript gs = collision.gameObject.GetComponent<GroundScript>();

            if (color == gs.GetColor())
            {
                differTime = 0;
            }
            else
            {
                differTime += Time.deltaTime;

                //GameOver
                if (differTime >= 0.8f)
                {
                    GameOver();
                }
            }
        }
    }

    public void PlayerColorChange()
    {
        switch (color)
        {
            case ColorType.RED:
                color = ColorType.GREEN;
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;

            case ColorType.GREEN:
                color = ColorType.BLUE;
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;

            case ColorType.BLUE:
                color = ColorType.RED;
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
