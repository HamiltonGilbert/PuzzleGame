using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cat : MonoBehaviour
{
    public int speed = 10;
    // Start is called before the first frame update
    void Update()
    {
        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        transform.position += Movement * speed * Time.deltaTime;
    }
}
