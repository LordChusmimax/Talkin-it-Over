using Cinemachine;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [HideInInspector] public int playerIndex;


    private CapsuleCollider2D playerCollider;
    private PlayerScript playerScript;
    private Weapon weapon;
    private Rigidbody2D rb;
    private Animator animator;

    [HideInInspector] public CinemachineTargetGroup cmTargerGroup;
    [HideInInspector] public GameObject head;

    void Start()
    {
        playerScript = GetComponent<PlayerScript>();
        animator = GetComponentInChildren<Animator>();
        weapon = playerScript.Weapon;
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ammunition" && !playerScript.Dead)
        {
            //Tomamos la información de la bala y notificamos al sistema de rondas
            BulletScript bullet = collision.gameObject.GetComponent<BulletScript>();
            ScoreData.addKill(bullet.idPlayer);

            playerScript.Die(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.tag == "NonLethal" && !playerScript.Dead)
        {
            playerScript.Stun(3);
        }
        else if (collider.tag == "Lethal" && !playerScript.Dead)
        {
            playerScript.Die(true);
        }
        else if (collider.tag == "Scenary" && !playerScript.Dead)
        {
            playerScript.Die(false);
        }
    }

}
