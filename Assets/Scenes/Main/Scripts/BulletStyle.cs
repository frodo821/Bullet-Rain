using System.Reflection;
using UnityEngine;

public class BulletStyle : MonoBehaviour {
    public Style bulletStyle = Style.Circular6;
    public GameObject globalRoot;
    public MethodInfo fire;
    Enemy enemy;

    void Start()
    {
        globalRoot = transform.parent.gameObject;
        enemy = GetComponent<Enemy>();
        fire = typeof(BulletStyle).GetMethod(bulletStyle.ToString(),
            BindingFlags.NonPublic | BindingFlags.Instance);
    }

    void OnDestroy()
    {
        fire = null;
    }

    public void FireBullet(GameObject b, int f = 0)
    {
        fire.Invoke(gameObject.GetComponent<BulletStyle>(), new object[] { b, f });
    }

    void Circular6(GameObject b, int f)
    {
        GameObject g = Instantiate(b, transform.position, Quaternion.AngleAxis(7.5f + f * 18, Vector3.forward));
        Bullet bc = g.GetComponent<Bullet>();
        if(bc == null)
            bc = g.GetComponent<HomingBullet>();
        bc.eparent = enemy;
        bc.phase = Player.phase;
        bc.transform.parent = globalRoot.transform;

        g = Instantiate(b, transform.position, Quaternion.AngleAxis(37.5f + f * 18, Vector3.forward));
        bc = g.GetComponent<Bullet>();
        if (bc == null)
            bc = g.GetComponent<HomingBullet>();
        bc.eparent = enemy;
        bc.phase = Player.phase;
        bc.transform.parent = globalRoot.transform;

        g = Instantiate(b, transform.position, Quaternion.AngleAxis(67.5f + f * 18, Vector3.forward));
        bc = g.GetComponent<Bullet>();
        if (bc == null)
            bc = g.GetComponent<HomingBullet>();
        bc.eparent = enemy;
        bc.phase = Player.phase;
        bc.transform.parent = globalRoot.transform;

        g = Instantiate(b, transform.position, Quaternion.AngleAxis(97.5f + f * 18, Vector3.forward));
        bc = g.GetComponent<Bullet>();
        if (bc == null)
            bc = g.GetComponent<HomingBullet>();
        bc.eparent = enemy;
        bc.phase = Player.phase;
        bc.transform.parent = globalRoot.transform;

        g = Instantiate(b, transform.position, Quaternion.AngleAxis(127.5f + f * 18, Vector3.forward));
        bc = g.GetComponent<Bullet>();
        if (bc == null)
            bc = g.GetComponent<HomingBullet>();
        bc.eparent = enemy;
        bc.phase = Player.phase;
        bc.transform.parent = globalRoot.transform;

        g = Instantiate(b, transform.position, Quaternion.AngleAxis(157.5f + f * 18, Vector3.forward));
        bc = g.GetComponent<Bullet>();
        if (bc == null)
            bc = g.GetComponent<HomingBullet>();
        bc.eparent = enemy;
        bc.phase = Player.phase;
        bc.transform.parent = globalRoot.transform;
    }

    void Forward3(GameObject b, int f)
    {
        for (var i = 0; i < 20 + (int)Title.mode; i++)
        {
            GameObject g = Instantiate(
                b, transform.position - new Vector3(1f, 0f),
                Quaternion.AngleAxis(
                    45f + i * 18 + Random.Range(-15, 15), Vector3.forward));
            Bullet bc = g.GetComponent<Bullet>();
            if (bc == null)
                bc = g.GetComponent<HomingBullet>();
            bc.phase = Player.phase;
            bc.eparent = enemy;
            bc.transform.parent = globalRoot.transform;

            g = Instantiate(
                b, transform.position - new Vector3(1f, 2f),
                Quaternion.AngleAxis(
                    45f + i * 18 + Random.Range(-15, 15), Vector3.forward));
            bc = g.GetComponent<Bullet>();
            if (bc == null)
                bc = g.GetComponent<HomingBullet>();
            bc.phase = Player.phase;
            bc.eparent = enemy;
            bc.transform.parent = globalRoot.transform;

            g = Instantiate(
                b, transform.position - new Vector3(1f, -2f),
                Quaternion.AngleAxis(
                    45f + i * 18 + Random.Range(-15, 15), Vector3.forward));
            bc = g.GetComponent<Bullet>();
            if (bc == null)
                bc = g.GetComponent<HomingBullet>();
            bc.phase = Player.phase;
            bc.eparent = enemy;
            bc.transform.parent = globalRoot.transform;
        }
    }

    void NoBullet(GameObject b, int f) { }
}

public enum Style
{
    Circular6,
    Forward3,
    NoBullet
}
