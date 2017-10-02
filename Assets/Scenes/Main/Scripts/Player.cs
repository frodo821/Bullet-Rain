using System.Collections;
using System.Collections.Generic;
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
    public bool highscoreMarked = false;

    int count = 0;
    bool levelup = false;
    float prevScoreAdded = 0f;
    int leastLevelup = 0;
    bool isAlive = true;

    // Use this for initialization
    void Start () {
        maxHitPoint = (int)(maxHitPoint * (1 + (float)PlayerStats.level / 20));
        hitPoint = maxHitPoint;
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
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(fire) && Time.time - prevFire > fireInterval)
        {
            Instantiate(
                bullet,
                transform.position,
                Quaternion.Euler(0f, 0f, 135f - getAngle()))
                .GetComponent<Bullet>()
                .parent = transform;
            prevFire = Time.time;
        }
        Move();
        if (hitPoint <= 0 && isAlive)
        {
            SpriteRenderer r = gameObject.GetComponent<SpriteRenderer>();
            Color c = r.color;
            r.color = new Color(c.r, c.g, c.b, 0);
            Time.timeScale = 0;
            isAlive = false;
            UIControler.SendMessage("OnFinishedGame", gameObject);
            PlayerStats.ApplayEarnedExp(score / 10);
            PlayerStats.AddCash(score);
        }
        CalcScore();
        hpBar.value = (float)hitPoint / maxHitPoint * 100;
        if(levelup)
        {
            maxHitPoint += 100;
            hitPoint += 100;
            levelup = false;
        }
    }

    void OnGUI()
    {
        if (!isAlive)
        {
            GUI.Label(new Rect(
                main.pixelWidth / 2 - 250,
                main.pixelHeight / 2 - 120,
                500, 240),
                "You're Destoryed!\nYour Result: "
                + score
                + (highscoreMarked? "\nYou're Marked High Score!" : string.Empty),
                gstyle);
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
    }

    void OnEnemyKilled()
    {
        score += 10 * (int)Title.mode;
        if(hitPoint < maxHitPoint) hitPoint += 1 * (int)Title.mode;
    }

    void OnKilledBoss()
    {
        score += 50 * (int)Title.mode;
        pharse++;
        BulletStyle.pharse = pharse;
        if (hitPoint <= maxHitPoint - 20 * (int)Title.mode) {
            hitPoint += 20 * (int)Title.mode;
        }
        else
        {
            hitPoint = maxHitPoint;
        }
    }
}
