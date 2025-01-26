using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Score")]
    private int score;

    [SerializeField] private PlayersMovement player;

    [Header("Sound")]
    [SerializeField] private AudioClip damageSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player")) 
        {
            //Debug.LogWarning("colision")
            SoundManager.Instance.PlaySound(damageSound);
            //score++;
            //player.UpdateScore(score);

            if (collision.gameObject.GetComponent<PlayersMovement>().isPlayer1)
            {

                GameManager.Instance.ShowWinPanel("PLAYER 1");
            }
            else
            {
                GameManager.Instance.ShowWinPanel("PLAYER 2");
            }
        }
    }

    
}
