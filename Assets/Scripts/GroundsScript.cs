using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundsScript : MonoBehaviour
{
    private float differ;

    private float speed;

    private float createPos;

    public GameObject ground;

    public GameObject[] enemies;

    void Start()
    {
        differ = 0;
        speed = 3;
        createPos = 0;

        //初期床生成
        MakeNewGround(ColorType.RED);
        MakeNewGround((ColorType)Random.Range(0, 3));
        MakeNewGround((ColorType)Random.Range(0, 3));

        //敵生成開始
        StartCoroutine(MakeEnemy());

        //定期的に床のスピードを上げる
        StartCoroutine(SpeedUp());
    }


    void Update()
    {
        gameObject.transform.position -= Vector3.right * speed * Time.deltaTime;
        differ += speed * Time.deltaTime;

        if (differ >= 12.8f)
        {
            differ = 0;
            MakeNewGround((ColorType)Random.Range(0, 3));
        }
    }

    public void MakeNewGround(ColorType i)
    {
        GameObject newGround = Instantiate(ground);
        newGround.GetComponent<GroundScript>().SetColor(i);
        newGround.transform.parent = transform;
        newGround.transform.localPosition = new Vector3(createPos, 0, 0);

        createPos += 12.8f;
    }

    public IEnumerator SpeedUp()
    {
        while(true)
        {
            speed += 0.5f;

            yield return new WaitForSeconds(5);
        }
    }

    public IEnumerator MakeEnemy()
    {
        while (true)
        {
            GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Count())]);
            enemy.transform.parent = transform;

            yield return new WaitForSeconds(Random.Range(10.0f / speed, 15.0f / speed));
        }
    }
}
