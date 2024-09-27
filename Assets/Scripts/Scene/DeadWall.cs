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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Restart();
    }

    public void Restart()
    {
        string scene = SceneManager.GetActiveScene().name;
        GameManager.Instance.LoadScene(scene);
    }
}
