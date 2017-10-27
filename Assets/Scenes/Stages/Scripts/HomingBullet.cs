using System;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet: Bullet
{
    private GameObject target = null;
    public string targetTag = "Player";
    public List<string> excludeTags = new List<string>();
    public float startHoming;
    public float endHorming;
    public float drag = 5f;

    protected override void init()
    {
        base.init();
        target = GameObject.FindGameObjectWithTag(targetTag);
        startHoming = Time.time + 0.5f;
        endHorming = Time.time + 5.5f;
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == targetTag)
        {
            col.gameObject.SendMessage("OnDamaged", (baseDamage + Player.phase) * damageMultiplier);
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
        if(endHorming < Time.time && target != null)
        {
            target = null;
            transform.rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.forward);
        }
        if (startHoming < Time.time && target != null) {
            Vector2 pos = transform.position;
            Vector2 tpos;
            try
            {
                tpos = target.transform.position;
                float tx = tpos.x - pos.x;
                float ty = tpos.y - pos.y;
                float dir;
                try
                {
                    dir = Mathf.Atan2(ty, tx) * Mathf.Rad2Deg + 240;
                }
                catch (DivideByZeroException)
                {
                    dir = 0;
                }
                transform.rotation = Quaternion.AngleAxis(dir / 2, Vector3.forward);
            }
            catch (NullReferenceException) {
                target = null;
                transform.rotation = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.forward);
            }
            catch (MissingReferenceException) { }
        }
        transform.Translate(transform.TransformDirection(Vector2.up) * speed * Time.deltaTime);
    }

    protected override void OnFinishedGame(GameObject sender)
    {
    }
}
