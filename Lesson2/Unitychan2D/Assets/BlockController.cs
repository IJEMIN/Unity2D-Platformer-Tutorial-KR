using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    public LayerMask whatIsPlayer;

    public AudioClip hitClip;

    public bool canBreak;

    public GameObject brokenPrefab;

    private BoxCollider2D m_boxCollider2D;

    void Start()
    {
        m_boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector2 pos = transform.position;
            Vector2 groundCheck = new Vector2(pos.x, pos.y - transform.lossyScale.y);

            Vector2 groundArea = new Vector2(
                m_boxCollider2D.size.x * transform.lossyScale.y * 0.45f,
                0.05f);

            bool isCollide = Physics2D.OverlapArea
                (groundCheck + groundArea, groundCheck - groundArea, whatIsPlayer);

            if(isCollide)
            {
                if (canBreak)
                {
                    GameObject brokenBlock =
                        (GameObject)Instantiate(brokenPrefab, transform.position, transform.rotation);

                    brokenBlock.transform.localScale = transform.localScale;
                    Destroy(gameObject);
                }
                else
                {
                    SoundEffectPlayer.GetInstance().PlaySFX(hitClip);
                }
            }
        }

    }
}
