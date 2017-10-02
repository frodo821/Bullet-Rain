using System.Reflection;
using UnityEngine;

public class BulletStyle : MonoBehaviour {
    public Style bulletStyle = Style.Circular6;
    public MethodInfo fire;
    public static int pharse;

    void Start()
    {
        fire = typeof(BulletStyle).GetMethod(bulletStyle.ToString(),
            BindingFlags.NonPublic | BindingFlags.Instance);
    }

    public void FireBullet(GameObject b, int f = 0)
    {
        fire.Invoke(gameObject.GetComponent<BulletStyle>(), new object[] { b, f });
    }

    void Circular6(GameObject b, int f)
    {
        Bullet bc = Instantiate(b, transform.position, Quaternion.AngleAxis(7.5f + f * 18, Vector3.forward))
            .GetComponent<Bullet>();
        bc.parent = transform;
        bc.pharse = pharse;
        bc = Instantiate(b, transform.position, Quaternion.AngleAxis(37.5f + f * 18, Vector3.forward))
            .GetComponent<Bullet>();
        bc.parent = transform;
        bc.pharse = pharse;
        bc = Instantiate(b, transform.position, Quaternion.AngleAxis(67.5f + f * 18, Vector3.forward))
            .GetComponent<Bullet>();
        bc.parent = transform;
        bc.pharse = pharse;
        bc = Instantiate(b, transform.position, Quaternion.AngleAxis(97.5f + f * 18, Vector3.forward))
            .GetComponent<Bullet>();
        bc.parent = transform;
        bc.pharse = pharse;
        bc = Instantiate(b, transform.position, Quaternion.AngleAxis(127.5f + f * 18, Vector3.forward))
            .GetComponent<Bullet>();
        bc.parent = transform;
        bc.pharse = pharse;
        bc = Instantiate(b, transform.position, Quaternion.AngleAxis(157.5f + f * 18, Vector3.forward))
            .GetComponent<Bullet>();
        bc.parent = transform;
        bc.pharse = pharse;
    }

    void Forward3(GameObject b, int f)
    {
        for (var i = 0; i < 20 + (int)Title.mode; i++)
        {
            Bullet bc = Instantiate(
                b, transform.position - new Vector3(1f, 0f),
                Quaternion.AngleAxis(
                    45f + i * 18 + Random.Range(-15, 15), Vector3.forward))
                .GetComponent<Bullet>();
            bc.pharse = pharse;
            bc.parent = transform;
            
            bc = Instantiate(
                b, transform.position - new Vector3(1f, 2f),
                Quaternion.AngleAxis(
                    45f + i * 18 + Random.Range(-15, 15), Vector3.forward))
                .GetComponent<Bullet>();
            bc.pharse = pharse;
            bc.parent = transform;

            bc = Instantiate(
                b, transform.position - new Vector3(1f, -2f),
                Quaternion.AngleAxis(
                    45f + i * 18 + Random.Range(-15, 15), Vector3.forward))
                .GetComponent<Bullet>();
            bc.pharse = pharse;
            bc.parent = transform;
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
