using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit_Destroyer : MonoBehaviour
{
     public GameObject player;

     private void Update() {
         transform.position = new Vector3(
             transform.position.x, player.transform.position.y -5, transform.position.z
         );
     }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Fruit")){
            Destroy(other.gameObject);
        }
    }

}
