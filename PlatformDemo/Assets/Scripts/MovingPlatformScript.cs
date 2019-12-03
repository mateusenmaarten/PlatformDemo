using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    [SerializeField]
    public Transform pointA, pointB;
    private float _speed = 1f;
    [SerializeField]
    private bool _switch = false;

    // Start is called before the first frame update
    void Start()
    {

        pointA.position = (Vector2)pointA.transform.position;
        pointB.position = (Vector2)pointB.transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_switch == false)
        {
            //move to point B
            transform.position = Vector2.MoveTowards(transform.position, pointB.position, _speed * Time.deltaTime);
        }
        else if (_switch == true)
        {
            //move to point A
            transform.position = Vector2.MoveTowards(transform.position, pointA.position, _speed * Time.deltaTime);
        }

        if (transform.position == pointB.position)
        {
            _switch = true;
        }
        else if (transform.position == pointA.position)
        {
            _switch = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.parent = this.transform;
        }
    }
    public void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }

}
