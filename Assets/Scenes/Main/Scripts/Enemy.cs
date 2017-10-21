using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BulletStyle))]
public class Enemy : MyScriptBase
{
    public GameObject bullet = null;
    public Spawner spawn = null;
    public float speed = 0.2f;
    public int hitPoint = 10;
    private int maxHp;
    public bool isBoss = false;
    public float border = 0f;
    public float leastTouchDamageAdded = 0f;
    public float touchDamageInterval = 1f;
    public int touchDamageRatio = 2; 
    public float prev = 0f;
    bool isDead = false;
    public float fireInterval;
    public GameObject player;
    int bulletCount = 0;
    public Slider hpBar = null;
    BulletStyle style;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        style = gameObject.GetComponent<BulletStyle>();
        GetWorldLimit();
        maxHp = hitPoint;
        touchDamageInterval /= (int)Title.mode;
	}
	
    void FireBullet(int c)
    {
        style.FireBullet(bullet, c);
    }

	// Update is called once per frame
	void Update () {
        if(Time.time - prev > fireInterval)
        {
            FireBullet(bulletCount);
            prev = Time.time;
            bulletCount++;
        }
        if(hitPoint <= 0)
        {
            player.SendMessage(isBoss ? "OnKilledBoss" : "OnEnemyKilled");
            if (!spawn.bossPresent)
            {
                spawn.enemiesDestroyed++;
            }
            if (isBoss)
            {
                spawn.bossPresent = false;
            }
            isDead = true;
        }
        if (transform.position.x < worldLimitMin.x - 0.6f)
        {
            isDead = true;
        }
        if (!isBoss)
        {
            transform.position = new Vector3(
                transform.position.x - speed * Time.deltaTime,
                Mathf.Sin(transform.position.x / 2) * 2);
        }
        else if(transform.position.x > border)
        {
            transform.position = new Vector3(
                transform.position.x - speed * Time.deltaTime, 0f);
        }
        if (isDead)
        {
            DestroyImmediate(gameObject);
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
        }
	}

    void OnCollisionStay2D(Collision2D col)
    {
        if(Time.time - leastTouchDamageAdded > touchDamageInterval)
        {
            col.gameObject.SendMessage("OnDamaged", touchDamageRatio, SendMessageOptions.DontRequireReceiver);
            leastTouchDamageAdded = Time.time;
        }
    }

    void OnDamaged(int d = 1)
    {
        hitPoint -= d;
        if (hpBar != null)
        {
            hpBar.value = (float)hitPoint * 100 / maxHp;
        }
    }

    protected override void OnFinishedGame(GameObject sender)
    {
        //Destroy(this);
    }
}
