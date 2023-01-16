using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    bool isHit=false;
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isHit)
        {
            collision.gameObject.GetComponent<Hero>().ReceiveDamage();
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 4f, ForceMode2D.Impulse);
        }
    }
    public void Not()
    {
        isHit = true;
        Destroy(gameObject);
    }
/*
    public IEnumerator Death()
    {
        health--;
        if (health<=0)
        {
            if (drop!=null)
            {
                Instantiate(drop, transform.position, Quaternion.identity);
            }
            isHit=true;
            GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;
            GetComponent<Collider2D>().enabled=false;
            transform.GetChild(0).GetComponent<Collider2D>().enabled=false;
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }
    public void startDeath()
    {
        StartCoroutine(Death());
    }*/
}