using UnityEngine;
public class Bullet : MyScriptBase
{
    public float speed = 1f;
    public float destoryDistance = 10f;
    public Enemy eparent = null;
    public Transform parent = null;
    public int baseDamage;
    public int damageMultiplier = 1;
    public int phase = 0;
    
	void Start () {
        init();
	}
    protected virtual void init()
    {
        gameObject.name = "Bullet " + gameObject.GetInstanceID();
        GetWorldLimit();
        try
        {
            if (parent == null)
            {
                parent = eparent.transform;
            }
            if (parent.tag == "Enemy" || parent.tag == "Boss")
            {
                baseDamage += (int)Title.mode;
            }
        }
        catch (MissingReferenceException) { parent = null; }
    }
	
	void Update () {
        Move();
        if (
            transform.position.x < worldLimitMin.x - 0.3f ||
            transform.position.x > worldLimitMax.x + 0.3f ||
            transform.position.y < worldLimitMin.y - 0.3f ||
            transform.position.y > worldLimitMax.y + 0.3f)
        {
            DestroyImmediate(gameObject);
        }
        if (parent == null) return;
        try
        {
            if (Vector2.Distance(parent.position, transform.position) > destoryDistance)
            {
                DestroyImmediate(gameObject);
            }
        }
        catch (MissingReferenceException) { parent = null; }
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        col.gameObject.SendMessage("OnDamaged", (baseDamage + phase) * damageMultiplier);
        Destroy(gameObject);
    }
    protected override void OnFinishedGame(GameObject sender)
    {
    }
    protected virtual void Move()
    {
        transform.Translate(transform.TransformDirection(Vector2.up) * speed * Time.deltaTime);
    }
}
