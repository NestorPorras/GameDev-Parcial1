using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;

    private GameObject leCanvas;

    // Start is called before the first frame update
    void Start(){
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update(){
        if(GameObject.FindGameObjectsWithTag("Enemy").Length < 1 && leCanvas != null)
            leCanvas.SetActive(true);
    }

    private void Awake(){
        if (GameObject.FindObjectsOfType<GameManager>().Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(this);
    }

    public void startGame(){
        SceneManager.LoadScene(1);
    }

    public void startMenu(){
        Debug.Log("aló", this.gameObject);
        SceneManager.LoadScene(0);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("GamePerSe"))
            createEntities();
    }


    public void createEntities()
    {
        float maxY, maxX, minY, minX;

        Vector2 maxLimits = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.6f));
        Vector2 minLimits = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.45f));

        //This is why it's necessary to set the biggest object at the first position.
        float biggestObjSizeX = enemies[0].gameObject.GetComponent<SpriteRenderer>().bounds.size.x;

        leCanvas = GameObject.Find("Canvas");

        leCanvas.SetActive(false);

        maxY = maxLimits.y;
        maxX = maxLimits.x - biggestObjSizeX / 2;

        minY = minLimits.y;
        minX = minLimits.x + biggestObjSizeX / 2;

        foreach (GameObject enemy in enemies)
        {
            for (int indx = 0; indx < Random.Range(1, 4); indx++)
            {
                Instantiate(enemy, new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity);
            }
        }
    }
}
