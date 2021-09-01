using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] int firePSec;
    [SerializeField] int timeBetFires;
    [SerializeField] bool open;
    [SerializeField] GameObject bullet;

    [SerializeField] GameObject firefire;

    private float timeBurning;

    private float maxY, maxX, minY, minX;

    BoxCollider2D leCollider;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 maxLimits = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 minLimits = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
        float objSizeX = (GetComponent<SpriteRenderer>()).bounds.size.x;
        float objSizeY = (GetComponent<SpriteRenderer>()).bounds.size.y;

        maxY = maxLimits.y - objSizeY/2;
        maxX = maxLimits.x - objSizeX/2;

        minY = minLimits.y + objSizeY/2;
        minX = minLimits.x + objSizeX/2;

        leCollider = firefire.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!open)
            transform.Translate(new Vector2(Input.GetAxis("Horizontal") * Time.deltaTime * speed, Input.GetAxis("Vertical") * Time.deltaTime * speed));
        else
            transform.Translate(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        float fixedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float fixedY = Mathf.Clamp(transform.position.y, minY, maxY);

        transform.position = new Vector2(fixedX, fixedY);

        if (Input.GetKeyDown(KeyCode.Z) && Time.time - timeBurning > timeBetFires){
            timeBurning = Time.time;
        }

        if(Time.time - timeBurning < 1){
            if(firefire.gameObject.transform.localScale.y < 12)
                firefire.gameObject.transform.localScale += new Vector3(0, Time.deltaTime * firePSec, 0);
        }else if(firefire.gameObject.transform.localScale.y > 3)
            firefire.gameObject.transform.localScale -= new Vector3(0, Time.deltaTime * firePSec, 0);

        if (Input.GetKeyDown(KeyCode.Space) && !GameObject.Find("Bullet(Clone)")){
            Instantiate(bullet, transform.position, transform.rotation);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D collision){
        Destroy(this.gameObject);
    }
}
