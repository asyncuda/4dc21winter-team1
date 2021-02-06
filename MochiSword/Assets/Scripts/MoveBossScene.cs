using Library.Scene;
using UnityEngine;

public class MoveBossScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneMover.MoveAsync(Scenes.Boss).Forget();
        }
    }
}
