using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static AudioClip Shot1, Shot2, Powerup1, Powerup2, Powerup3, Death3, EnemyCollision, EnemyDeath1;
	static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        Shot1 = Resources.Load<AudioClip> ("Shot1");
        Shot2 = Resources.Load<AudioClip> ("Shot2");
		Powerup1 = Resources.Load<AudioClip> ("Powerup1");
        Powerup2 = Resources.Load<AudioClip> ("Powerup2");
        Powerup3 = Resources.Load<AudioClip> ("Powerup3");
        Death3 = Resources.Load<AudioClip> ("Death3");
        EnemyCollision = Resources.Load<AudioClip> ("EnemyCollision");
        EnemyDeath1 = Resources.Load<AudioClip> ("EnemyDeath1");
		audioSrc = GetComponent<AudioSource> ();
    }

    // Update is called once per frame
    void Update()
    {
	}
    public static void PlaySound (string clip)
	{
		switch(clip)
		{
			case "Shot1":
				audioSrc.PlayOneShot(Shot1);
				break;
			case "Shot2":
				audioSrc.PlayOneShot(Shot2);
				break;
			case "Powerup1":
				audioSrc.PlayOneShot(Powerup1);
				break;
			case "Powerup2":
				audioSrc.PlayOneShot(Powerup2);
				break;
			case "Powerup3":
				audioSrc.PlayOneShot(Powerup3);
				break;
			case "Death3":
				audioSrc.PlayOneShot(Death3);
				break;
			case "EnemyCollision":
				audioSrc.PlayOneShot(EnemyCollision);
				break;
			case "EnemyDeath1":
				audioSrc.PlayOneShot(EnemyDeath1);
				break;
		}
	 } 
    
}
