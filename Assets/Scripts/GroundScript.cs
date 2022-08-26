using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    private ColorType color;

    private void Update()
    {
        if (transform.position.x <= -16)
        {
            Destroy(gameObject);
        }
    }

    public void SetColor(ColorType c)
    {
        color = c;
        switch (c)
        {
            case ColorType.RED:
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                break;

            case ColorType.GREEN:
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                break;

            case ColorType.BLUE:
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
        }
    }

    public ColorType GetColor()
    {
        return color;
    }
}
