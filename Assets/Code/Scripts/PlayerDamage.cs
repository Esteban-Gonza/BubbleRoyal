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

                GameManager.Instance.ShowWinPanel("PLAYER 1");
            }
            else
            {
                GameManager.Instance.ShowWinPanel("PLAYER 2");
            }
        }
    }

    public void OnPlayerDead()
    {
        
    }

    private IEnumerator DeadFlow(string player)
    {
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.ShowWinPanel(player);
    }
    

}
