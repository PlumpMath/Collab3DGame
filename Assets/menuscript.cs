using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour {

    public Canvas quitMenu;
    public Canvas howMenu;
    public Button startText;
    public Button optionsText;
    public Button exitText;
    public Button howText;

    // Use this for initialization
    void Start () {
        //set up buttons
        quitMenu = quitMenu.GetComponent<Canvas>();
        startText = quitMenu.GetComponent<Button>();
        optionsText = quitMenu.GetComponent<Button>();
        exitText = quitMenu.GetComponent<Button>();
        howText = quitMenu.GetComponent<Button>();
        quitMenu.enabled = false;
        howMenu.enabled = false;

    }

    //show exit menu
    public void ExitPress()
    {
        quitMenu.enabled = true;
        startText.enabled = false;
        optionsText.enabled = false;
        exitText.enabled = false;
        howText.enabled = false;

    }

    //close exit menu
    public void noPress()
    {
        quitMenu.enabled = false;
        startText.enabled = true;
        optionsText.enabled = true;
        exitText.enabled = true;
        howText.enabled = true;

    }

    public void HowPress()
    {
        howMenu.enabled = true;
        startText.enabled = false;
        optionsText.enabled = false;
        exitText.enabled = false;
        howText.enabled = false;

    }

    //close exit menu
    public void notPress()
    {
        howMenu.enabled = false;
        startText.enabled = true;
        optionsText.enabled = true;
        exitText.enabled = true;
        howText.enabled = true;

    }

    public void StartLevel(string name)
    {
        //Application.LoadLevel (name);
		SceneManager.LoadScene (name);
    }

    public void optionsLevel(string name)
    {
        //Application.LoadLevel (name);
        SceneManager.LoadScene(name);
    }
    //quit the game
    public void ExitGame()
    {
        Application.Quit ();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
