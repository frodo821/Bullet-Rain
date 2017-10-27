using System;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class SelectView : MonoBehaviour {
    
    [Serializable]
    public class WorldContainer
    {
        [SerializeField]
        public Button selector;
        [SerializeField]
        public ScrollRect stages;
    }
    public List<WorldContainer> containers = new List<WorldContainer>();
    public WorldContainer enable = null;
	void Start () {
        enable = containers.ToArray()[0];
        enable.selector.interactable = false;
        enable.stages.gameObject.SetActive(true);
	}

    public void OnClick(int i)
    {
        var c = containers.ToArray()[i - 1];
        c.selector.interactable = false;
        c.stages.gameObject.SetActive(true);
        enable.stages.gameObject.SetActive(false);
        enable.selector.interactable = true;
        enable = c;
    }
}

