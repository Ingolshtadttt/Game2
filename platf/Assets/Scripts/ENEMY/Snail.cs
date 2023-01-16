/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : MonoBehaviour
{
    float playerX;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerX=gameObject.FindGameObjectWithTag("Player").transform.position.x;
        transform.Translate(Vector2.left*speed*Time.deltaTime);

        if (playerX<transform.position.x)
            transform.eulerAngles=new Vector3(0,0,0);
        else if (playerX<transform.position.x)
            transform.eulerAngles=new Vector3(0,180,0);
    }
}
*/