using UnityEngine;

public class Spawner : MyScriptBase {
    public float interval = 5f;
    public int enemiesDestroyed = 0;
    public int killToNext = 10;
    public bool bossPresent = false;
    public float prev;
    public GameObject globalRoot;
    public int threshold = 10;
    public GameObject boss = null;
    public GameObject hormingBoss = null;
    public GameObject enemy = null;
    int mode;
    int prevPhase;

    // Use this for initialization
    void Start () {
        mode = (int)Title.mode;
        globalRoot = GameObject.Find("GlobalRoot");
        prev = Time.time - interval;
        enemiesDestroyed = 0;
        GetWorldLimit();
        if(Title.mode == 0)
        {
            Title.mode = GameMode.Easy;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time - prev > interval)
        {
            Enemy e;
            if (enemiesDestroyed >= killToNext & !bossPresent)
            {
                if (boss != null && (mode > (int)GameMode.Hard || Player.phase < threshold))
                {
                    e = Instantiate(
                        boss,
                        new Vector3(worldLimitMax.x + 1f, 0f),
                        Quaternion.AngleAxis(0, Vector3.forward))
                        .GetComponent<Enemy>();
                    e.transform.parent = globalRoot.transform;
                    e.fireInterval = 4f / mode;
                    e.hitPoint += Player.phase * mode * 25;
                    e.spawn = this;
                    enemiesDestroyed = 0;
                    bossPresent = true;
                    killToNext++;
                    return;
                }
                if(hormingBoss != null)
                {
                    e = Instantiate(
                        hormingBoss,
                        new Vector3(worldLimitMax.x + 1f, 0f),
                        Quaternion.AngleAxis(0, Vector3.forward))
                        .GetComponent<Enemy>();
                    e.transform.parent = globalRoot.transform;
                    e.fireInterval = 4f / mode;
                    e.hitPoint += Player.phase * mode * 25;
                    e.spawn = this;
                    enemiesDestroyed = 0;
                    bossPresent = true;
                    killToNext++;
                    return;
                }
            }
            e =Instantiate(
                enemy,
                new Vector3(worldLimitMax.x + 1f, Random.Range(-2, 2)),
                Quaternion.AngleAxis(0, Vector3.forward))
                    .GetComponent<Enemy>();
            e.transform.parent = globalRoot.transform;
            e.fireInterval = 1f / mode;
            e.hitPoint += Player.phase * mode * 5;
            e.spawn = this;
            prev = Time.time;
        }
    }

    protected override void OnFinishedGame(GameObject sender)
    {
        Destroy(this);
    }
}
