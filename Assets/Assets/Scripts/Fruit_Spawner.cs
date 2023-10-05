using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_Spawner : MonoBehaviour
{

    public GameObject[] fruits;

    public float speed = 5;
    public float minX;
    public float maxX;
    public float interval;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", interval, interval);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void Spawn(){
        GameObject newFruit = Instantiate(fruits[Random.Range(0, 2)]);

        newFruit.transform.position = new Vector2(
            Random.Range(minX, maxX),
            transform.position.y
        );
    }

}
