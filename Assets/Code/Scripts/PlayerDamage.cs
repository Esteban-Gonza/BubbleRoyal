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

    [Header("Animation")]
    public Animator animator;


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
            SoundManager.Instance.PlaySound(damageSound);


            if (collision.gameObject.GetComponent<PlayersMovement>().isPlayer1)
            {

                OnPlayerDead("PLAYER 1");

            }
            else
            {
                OnPlayerDead("PLAYER 2");

            }
        }
    }

    public void OnPlayerDead(string playerName)
    {
        animator.SetBool("IsDead", true);
        player.moveSpeed = 0;
        GameManager.Instance.ResetPlayers();
        GameManager.Instance.ShowWinPanel(playerName);
    }


    

}
