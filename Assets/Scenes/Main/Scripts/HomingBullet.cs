using System.Collections.Generic;
using UnityEngine;

public class HomingBullet: Bullet
{
    private GameObject target = null;
    public string targetTag = "Player";
    public List<string> excludeTags = new List<string>();
    public float startHoming;
    public float endHorming;
    Rigidbody2D rb;

    protected override void init()
    {
        base.init();
        target = GameObject.FindGameObjectWithTag(targetTag);
        startHoming = Time.time + 0.5f;
        endHorming = Time.time + 5.5f;
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == targetTag)
        {
            col.gameObject.SendMessage("OnDamaged", (baseDamage + pharse) * damageMultiplier);
        }
        if (excludeTags.Contains(col.gameObject.tag)) return;
        Destroy(gameObject);
    }

    float GetLen(Vector3 vec)
    {
        return Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
    }
    
    protected override void Move()
    {
        if(startHoming > Time.time || endHorming < Time.time)
        {
            if (GetLen(rb.velocity) < 10f)
            {
                transform.Translate(
                    transform.TransformDirection(Vector2.up) * (speed / 10) * Time.deltaTime);
            }
            return;
        }
        Vector2 pos = transform.position;
        Vector2 tpos = target.transform.position;
        float tx = tpos.x - pos.x;
        float ty = tpos.y - pos.y;
        Vector2 direction;
        /*float dx = -0.1f, dy = 0.0f;
        if (pos.x < tpos.x)
        {
            dx = -0.1f;
        }
        else if (pos.x > tpos.x)
        {
            dx = 0.1f;
        }
        if (pos.y < tpos.y)
        {
            dy = 0.1f;
        }
        else if (pos.y > tpos.y)
        {
            dy = -0.1f;
        }*/
        direction = new Vector3(tx, ty);
        rb.velocity = direction.normalized * speed * Time.deltaTime * rb.mass * 8;
    }
}
