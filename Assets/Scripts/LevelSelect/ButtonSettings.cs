using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonSettings : MonoBehaviour {
	public static int releasedLevelStatic = 1;
	public int releasedLevel;
	public string nextLevel;


	void Awake()
	{
		if (PlayerPrefs.HasKey ("Level")) {
			releasedLevelStatic = PlayerPrefs.GetInt ("Level", releasedLevelStatic);

		}
	}

	public void ButtonNextLevel()
	{
		SceneManager.LoadScene (nextLevel);
		if(releasedLevelStatic <= releasedLevel){
			releasedLevelStatic = releasedLevel;
			PlayerPrefs.SetInt ("Level", releasedLevelStatic);

	}
   }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(nextLevel);
        }
    }

    public void ButtonMenu()
	{
		SceneManager.LoadScene ("LevelSelect");
	}
}
