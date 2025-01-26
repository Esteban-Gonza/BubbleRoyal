using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRigid;

    private SpikeSpawner spawner;
    private bool touchedTheGround;

    void Start()
    {
        spawner = FindObjectOfType<SpikeSpawner>();
        touchedTheGround = false;
    }

    void Update()
    {
        if (transform.position.y < -6f)
        {
            gameObject.SetActive(false);
            touchedTheGround = true;
            spawner.RecycleSpike(this.gameObject, playerRigid);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayersMovement>().isPlayer1 ) { 
            
                GameManager.Instance.ShowWinPanel("PLAYER 2");
            }
            else
            {
                GameManager.Instance.ShowWinPanel("PLAYER 1");
            }
        }
    }
}