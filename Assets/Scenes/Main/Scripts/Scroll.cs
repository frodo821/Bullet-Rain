using UnityEngine;

public class Scroll : MonoBehaviour {
    public float speed = 2f;
    public float startX = 17.98f;
    public float endX = -18;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 pos = transform.position;
        pos.x -= speed * Time.deltaTime;
        transform.position = pos;
        if(transform.position.x < endX)
        {
            pos.x = startX;
            transform.position = pos;
        }
	}

    protected void OnFinishedGame(GameObject sender)
    {
        Destroy(this);
    }
}
