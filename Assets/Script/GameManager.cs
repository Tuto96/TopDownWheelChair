using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    private GameObject menuPanel;
    private GameObject menuButton;

	// Use this for initialization
	void Start () {
        // Set Local UI Variables
        menuPanel = GameObject.Find("MenuPanel");
        menuButton = GameObject.Find("MenuButton");
        menuPanel.SetActive(false);
    }
	
    public void OnMenu()
    {
        menuPanel.SetActive(true);
        menuButton.SetActive(false);
    }
}
