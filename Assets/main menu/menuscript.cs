using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class menuscript : MonoBehaviour {

    public Canvas quitMenu;
    public Button startText;
    public Button exitText;

    // Use this for initialization
    void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = quitMenu.GetComponent<Button>();
        exitText = quitMenu.GetComponent<Button>();
        quitMenu.enabled = false;

    }

    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        exitText.enabled = false;

    }


    public void noPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        exitText.enabled = true;

    }

    public void StartLevel(string name)
    {
        Application.LoadLevel (name);
    }

    public void ExitGame()
    {
        Application.Quit ();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
