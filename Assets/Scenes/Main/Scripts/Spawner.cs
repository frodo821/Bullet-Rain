using UnityEngine;

public class Spawner : MyScriptBase {
    public float interval = 5f;
    public float bossInterval = 60f;
    public float prev;
    public float prevBoss;
    public GameObject globalRoot;
    public GameObject boss = null;
    public GameObject enemy = null;

    // Use this for initialization
    void Start () {
        globalRoot = GameObject.Find("GlobalRoot");
        prev = Time.time - interval;
        prevBoss = Time.time;
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
            if (Time.time - prevBoss > bossInterval && boss != null)
            {
                e = Instantiate(
                    boss,
                    new Vector3(worldLimitMax.x + 1f, 0f),
                    Quaternion.AngleAxis(0, Vector3.forward))
                    .GetComponent<Enemy>();
                e.transform.parent = globalRoot.transform;
                e.fireInterval = 4f / (int)Title.mode;
                e.hitPoint += BulletStyle.pharse * 2;
                prevBoss = Time.time;
                return;
            }
            e =Instantiate(
                enemy,
                new Vector3(worldLimitMax.x + 1f, Random.Range(-2, 2)),
                Quaternion.AngleAxis(0, Vector3.forward))
                    .GetComponent<Enemy>();
            e.transform.parent = globalRoot.transform;
            e.fireInterval = 1f / (int)Title.mode;
            e.hitPoint += BulletStyle.pharse * 2;
            prev = Time.time;
        }
    }

    protected override void OnFinishedGame(GameObject sender)
    {
        Destroy(this);
    }
}
