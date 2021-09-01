using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] int dirChanger;
    [SerializeField] bool goesToTheLeft;
    [SerializeField] Sprite emptyHeart;

    [SerializeField] GameObject[] lifes;

    private float maxX, minX;
    private int currentLife;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector2 maxLimitse = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 minLimitse = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));

        float objSizeX = (GetComponent<SpriteRenderer>()).bounds.size.x;

        maxX = maxLimitse.x - objSizeX / 2; 
        minX = minLimitse.x + objSizeX / 2;

        currentLife = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (goesToTheLeft) {
            transform.Translate(new Vector2(-1 * Time.deltaTime * speed, 0));
            goesToTheLeft = transform.position.x > minX;

            if (Random.Range(0, 500) > dirChanger)
                goesToTheLeft = false;
        }
        else {
            transform.Translate(new Vector2(1 * Time.deltaTime * speed, 0));
            goesToTheLeft = transform.position.x > maxX;

            if (Random.Range(0, 500) > dirChanger)
                goesToTheLeft = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet")) {
            if (currentLife == lifes.Length-1)
                Destroy(this.gameObject);
            else{
                lifes[currentLife].gameObject.GetComponent<SpriteRenderer>().sprite = emptyHeart;
                currentLife++;
            }
            
        }
            
    }
}
