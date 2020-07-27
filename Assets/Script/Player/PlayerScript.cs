using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] protected float movementSpeed = 4f;
    public PlayerInputs controls = null;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected bool faceRight = false;


    private void Awake()
    {
        controls = new PlayerInputs();
        controls.Player.Enable();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        ActionAssigner();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    //assigns methods to every action in the mapping
    protected void ActionAssigner()
    {
        //controls.Player.Jump.performed += ctx => Jump();
        //controls.Player.Jump.canceled += ctx => JumpEnd();
        //controls.Player.Shoot.performed += ctx => Shoot();
        //controls.Player.Pick.performed += ctx => Pick();
        //controls.Player.Special.performed += ctx => Special();
        //controls.Player.Menu.performed += ctx => Menu();
        //controls.Player.Slow.performed += ctx => Slow();
    }

    //controlls movement of the character
    protected void Move()
    {
        var movement = controls.Player.Move.ReadValue<float>();
        if (movement >= 0.2f || movement <= -0.2f)
        {
            transform.Translate(new Vector2(movement, 0) * Time.deltaTime * movementSpeed);
            SpriteFlip(movement);
        }
    }

    //flips the sprite when you change direction
    protected void SpriteFlip(float movement)
    {
        if (movement != 0)
        {
            var currentScale = transform.localScale;
            if (currentScale.x > 0)
            {
                faceRight = true;
            }
            else if (currentScale.x < 0)
            {
                faceRight = false;
            }
            currentScale.x = Mathf.Abs(currentScale.x);
            currentScale.x = Mathf.Abs(currentScale.y);
            transform.localScale = new Vector2(-Mathf.Sign(movement), 1) * currentScale;
        }
    }
}
