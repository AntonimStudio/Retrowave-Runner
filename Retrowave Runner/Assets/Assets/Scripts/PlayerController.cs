using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  
/// <summary>
/// Потом убрать  то, что сверху
/// </summary>

public class PlayerController : MonoBehaviour
{
    private int lineToMove = 1;
    [SerializeField] private float speed;
    [SerializeField] private float lineDistance;

    void Update()
    {
        SwipeMove();
    }

    private void SwipeMove()
    {
        if (SwipeController.swipeRight && lineToMove < 2) { lineToMove++; }

        if (SwipeController.swipeLeft && lineToMove > 0) { lineToMove--; }

        Vector3 targetPosition = transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("AAAAAAAAAAAAA");
        if (collision.gameObject.GetComponent<Enemy>())
        {
            Destroy(gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
