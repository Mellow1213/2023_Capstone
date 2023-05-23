/// <summary>
/// 
/// Haunted Baby Doll
/// 
/// This is a simple script JUST for showcase purposes (test scene). It coordinates some sounds based on variable iterations and pre-made transitions (animController demo).
/// 
/// NOTE> I do not give support for this script. Feel free to tweak and use it as a base for your own sounds/transitions.
/// 
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class HauntedBabySound : MonoBehaviour {
	
	[SerializeField]AudioClip[] sounds;

	private AudioSource _audioSource;
	private AudioSource _music;
	private int iterations = 0;


	void Awake(){

		_audioSource = GetComponent <AudioSource> ();

		try {
			_music = GameObject.Find ("Floor").GetComponent <AudioSource> ();
		}catch{

			_music = null;
			Debug.Log ("No music found (Floor GameObject deactivated?). Hide mesh renderer instead.");
		}
	}

	// Use this for initialization
	void Start () {

		StartCoroutine (_mecanimSound());

	}
	

	private IEnumerator _mecanimSound(){

		int headCount = 0;

		Animator thisAnim = GetComponent<Animator> ();

		//anim hashes
		int headMovState = Animator.StringToHash("Bug");
		int headBugState = Animator.StringToHash ("HeadBug");


		while (true) {

			switch (iterations){

			case (int)iterationsName.contorsionSound : 

				//First sound to be applied
				if (thisAnim.GetCurrentAnimatorStateInfo (0).shortNameHash == headMovState && !GetComponent<AudioSource>().isPlaying) {

					
					_audioSource.clip = sounds [(int)iterationsName.contorsionSound];
					GetComponent<AudioSource> ().Play ();

					
					yield return StartCoroutine (__nextIteration());

				}

				break;

			case (int)iterationsName.demonicSound : 

				//Second sound to be applied
				yield return new WaitForSeconds (1.2f);

				if (!GetComponent<AudioSource>().isPlaying){

					_audioSource.clip = sounds [(int)iterationsName.demonicSound];
					GetComponent<AudioSource> ().Play ();

					try {

						_music.volume = 0.2f;

					}catch{}

					yield return StartCoroutine (__nextIteration());

				}
					
				break;

			case (int)iterationsName.headBug:
				
				//Third
				if (thisAnim.GetCurrentAnimatorStateInfo (0).shortNameHash == headBugState && !GetComponent<AudioSource>().isPlaying){

					_audioSource.clip = sounds [(int)iterationsName.contorsionSound];
			
					GetComponent<AudioSource> ().Play ();

					yield return new WaitForSeconds (.5f);

					headCount++;
					

				}
				break;

			}


			//Reset
			if (headCount == 3){
				
				iterations = 0;
				headCount = 0;

			}

			//Music control
			try {

				if (thisAnim.GetCurrentAnimatorStateInfo (0).shortNameHash == headMovState) {

					_music.volume = .2f;
				} else {
					_music.volume = .75f;
				}

			}catch {}

			yield return null;
		}

	}



	private IEnumerator __nextIteration(){

		++iterations;
		yield return null;

	}
		

	private enum iterationsName{

		contorsionSound, demonicSound, headBug
	}
		

	
}
