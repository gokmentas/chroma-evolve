using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{

    public Transform player;
    public Transform background;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.position.y >= transform.position.y){
            transform.position = new Vector3(
                transform.position.x,
                player.position.y,
                transform.position.z);
        }

        if(transform.position.y >= background.position.y + 12.8f){
            background.position = new Vector3(
                background.position.x,
                transform.position.y + 12.8f,
                background.position.z
            );
        }

    }

} // class
