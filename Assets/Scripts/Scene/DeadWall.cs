using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class DeadWall : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Collider2D>().enabled = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Restart();
    }

    public void Restart()
    {
        string scene = SceneManager.GetActiveScene().name;
        GameLevelManager.Instance.LoadSceneGame(scene);
        //SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
    }
}
