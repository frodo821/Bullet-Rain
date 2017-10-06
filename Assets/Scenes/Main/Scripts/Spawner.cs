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
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time - prev > interval)
        {
            if (Time.time - prevBoss > bossInterval && boss != null)
            {
                Instantiate(
                    boss,
                    new Vector3(worldLimitMax.x + 1f, 0f),
                    Quaternion.AngleAxis(0, Vector3.forward))
                    .GetComponent<Transform>().parent = globalRoot.transform;
                prevBoss = Time.time;
                return;
            }
            Instantiate(
                enemy,
                new Vector3(worldLimitMax.x + 1f, Random.Range(-2, 2)),
                Quaternion.AngleAxis(0, Vector3.forward))
                    .GetComponent<Transform>().parent = globalRoot.transform;
            prev = Time.time;
        }
    }

    protected override void OnFinishedGame(GameObject sender)
    {
        Destroy(this);
    }
}
