using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MyScriptBase
{
    public GameObject bullet = null;
    public float speed = 0.2f;
    public KeyCode fire = KeyCode.Space;
    public KeyCode forward = KeyCode.D;
    public KeyCode backward = KeyCode.A;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public int hitPoint;
    public int score = 0;
    public float prevFire = 0;
    public float fireInterval = 0.1f;
    public GameObject UIControler;
    public GUIStyle gstyle;
    public GUIStyle defstyle;
    public int maxHitPoint = 100;
    public Rect[] GUIRect = { new Rect() };
    public int pharse = 0;
    public Slider hpBar;
    public Animator anim;
    public Spawner spawner;
    public bool simulateOnDamage;
    public SpriteRenderer fadeout;
    public bool highscoreMarked = false;

    int count = 0;
    bool levelup = false;
    float prevScoreAdded = 0f;
    int leastLevelup = 0;
    bool isAlive = true;
    bool showDiedMenu = false;
    
    void Start () {
        maxHitPoint = (int)(maxHitPoint * (1 + (float)PlayerStats.level / 20));
        hitPoint = maxHitPoint;
        anim = GetComponent<Animator>();
        Time.timeScale = 1f;
        GetWorldLimit();
	}
	
    float getAngle()
    {
        var angleOrign = (count % 6) * 5;
        var angleFlag = Mathf.Floor(count / 6) % 2 == 1;
        count++;
        return angleFlag ? angleOrign : -angleOrign;
    }
    
    void Move()
    {
        if (Input.GetKey(forward) && transform.position.x < worldLimitMax.x - 5.6f)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(backward) && transform.position.x > worldLimitMin.x + 0.6f)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(up) && transform.position.y < worldLimitMax.y - 0.6f)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(down) && transform.position.y > worldLimitMin.y + 0.6f)
        {
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
    }
    
	void Update () {
        if (isAlive)
        {
            if (Input.GetKey(fire) && Time.time - prevFire > fireInterval)
            {
                Bullet b = Instantiate(
                    bullet,
                    transform.position,
                    Quaternion.Euler(0f, 0f, 135f - getAngle()))
                    .GetComponent<Bullet>();
                b.parent = transform;
                b.transform.parent = transform.parent;
                prevFire = Time.time;
            }
            Move();
            if (hitPoint <= 0)
            {
                isAlive = false;
                StartCoroutine("onDied");
                PlayerStats.ApplayEarnedExp(score / 10);
                PlayerStats.AddCash(score);
            }
            CalcScore();
            hpBar.value = (float)hitPoint / maxHitPoint * 100;
            if (simulateOnDamage)
            {
                OnDamaged(0);
                simulateOnDamage = false;
            }
            if (levelup)
            {
                maxHitPoint += 100;
                hitPoint += hitPoint * 100 / maxHitPoint;
                levelup = false;
            }
        }
    }

    IEnumerator onDied()
    {
        spawner.globalRoot.BroadcastMessage("OnFinishedGame", gameObject, SendMessageOptions.DontRequireReceiver);
        SpriteRenderer r = gameObject.GetComponent<SpriteRenderer>();
        GetComponent<Animator>().SetTrigger("onDied");
        Color fade = fadeout.color;
        Color c = r.color;
        yield return new WaitForSeconds(1f);
        while (true)
        {
            fade.a += 0.01f;
            c.a -= 0.01f;
            r.color = c;
            fadeout.color = fade;
            yield return new WaitForSeconds(0.01f);
            if (c.a <= 0) break;
        }
        UIControler.SendMessage("OnShowUI", gameObject);
        showDiedMenu = true;
    }

    void OnGUI()
    {
        if (!isAlive)
        {
            if (showDiedMenu)
            {
                GUI.Label(new Rect(
                    main.pixelWidth / 2 - 250,
                    main.pixelHeight / 2 - 120,
                    500, 240),
                    "You're Destoryed!\nYour Result: "
                    + score
                    + (highscoreMarked ? "\nYou're Marked High Score!" : string.Empty),
                    gstyle);
            }
        }
        else
        {
            GUI.Label(GUIRect[0], "HP: " + hitPoint + "/" + maxHitPoint, defstyle);
            GUI.Label(GUIRect[1], "Score: " + score, defstyle);
            GUI.Label(GUIRect[2], "Phase: " + (pharse + 1), defstyle);
        }
    }

    void CalcScore()
    {
        if (Mathf.FloorToInt(score / (100 * (int)Title.mode)) > leastLevelup)
        {
            levelup = true;
            leastLevelup = Mathf.FloorToInt(score / (100 * (int)Title.mode));
        }
        if (Time.time - prevScoreAdded > 5f)
        {
            score++;
            prevScoreAdded = Time.time;
        }
    }

    void OnDamaged(int d = 5)
    {
        hitPoint -= d;
        anim.SetTrigger("onDamage");
    }

    void OnEnemyKilled()
    {
        score += 10 * (int)Title.mode;
        int health = (int)Title.mode / 2;
        health = health <= 0 ? 1 : health;
        if (hitPoint < maxHitPoint - health)
            hitPoint += health;
    }

    void OnKilledBoss()
    {
        score += 50 * (int)Title.mode;
        pharse++;
        BulletStyle.pharse = pharse;
        if (hitPoint <= maxHitPoint - 10 * (int)Title.mode) {
            hitPoint += 10 * (int)Title.mode;
        }
        else
        {
            hitPoint = maxHitPoint;
        }
    }

    override protected void OnFinishedGame(GameObject sender)
    {

    }
}
