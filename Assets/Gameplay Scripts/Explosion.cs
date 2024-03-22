using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject spaceship;
    public GameObject explosionPrefab;

    private Vector3 position; // Variable to store the position

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spaceship != null)
        {
            position = spaceship.transform.position; // Store the position of the spaceship
        }
        else
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Instantiate explosion prefab at the stored position
        Instantiate(explosionPrefab, position, Quaternion.identity);
        
        // Destroy this game object
        Destroy(gameObject);    
    }
}
