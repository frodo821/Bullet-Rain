using UnityEngine;

/// <summary>
/// The base class of bullets.
/// </summary>
public class Bullet : MyScriptBase
{
    /// <summary>
    /// bullet speed
    /// </summary>
    public float speed = 1f;
    /// <summary>
    /// How distance should be bullet destroyed?  
    /// </summary>
    public float destoryDistance = 10f;
    /// <summary>
    /// If this ejected by a enemy, this variable cotains pearent instance.
    /// </summary>
    public Enemy eparent = null;
    /// <summary>
    /// parent transform
    /// </summary>
    public Transform parent = null;
    /// <summary>
    /// The basic damage
    /// </summary>
    public int baseDamage;
    /// <summary>
    /// What value does this damage increase by phase?
    /// </summary>
    public int damageMultiplier = 1;
    /// <summary>
    /// game phase
    /// </summary>
    public int phase = 0;
    
	void Start () {
        init();
	}
    /// <summary>
    /// Initialize this component.
    /// </summary>
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
    /// <summary>
    /// What do this do by the time, the game has finished? 
    /// </summary>
    /// <param name="sender">who send this message?</param>
    protected override void OnFinishedGame(GameObject sender)
    {
        //Destroy(this);
    }
    /// <summary>
    /// Defines the motion of this on every tick.
    /// </summary>
    protected virtual void Move()
    {
        transform.Translate(transform.TransformDirection(Vector2.up) * speed * Time.deltaTime);
    }
}
