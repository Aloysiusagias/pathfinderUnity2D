using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gotolvl2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        } else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(3);
        } else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            SceneManager.LoadScene(4);
        }
    }
}
