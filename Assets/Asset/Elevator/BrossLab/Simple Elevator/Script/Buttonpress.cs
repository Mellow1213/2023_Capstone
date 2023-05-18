using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleElevator {

public class Buttonpress : MonoBehaviour
{
	public GameObject SwitchExt;
	public GameObject SwitchInt;
	private Collider[] ButtonsInt; // Colliders Interior Buttons
	private Collider[] ButtonsExt; // Colliders Exterior Buttons
	private MeshRenderer[] RendsInt; 
	private MeshRenderer[] RendsExt;
	public Material RedEmission; // Material Buttons Emission
	public Material Transparent; // Material Buttons Transparent
	
	// UI Panels
	public Text FloorExt;
	public GameObject UpExt = null;
	public GameObject DownExt = null;
	
	public Text FloorInt;
	public GameObject UpInt = null;
	public GameObject DownInt = null;
	
	// Clips Audio
	[SerializeField] private AudioClip OpenClip = null;
	[SerializeField] private AudioClip ButtonClip = null;

	// Initial Floor
    public int FloorElevator;
	public float FloorChara;
	private float ActualFloor = 0;
		
	// Animator and AudioSourse Elevator
	private Animator ElevatorAnimator = null;
	private AudioSource ElevatorSound = null;
	
	// Bools
	private bool ClickUpDown = false;
	private bool CallStandby = false;

   // Start is called before the first frame update
    void Start()
    {
		ActualFloor = FloorElevator;
		FloorExt.text = "" + ActualFloor.ToString("f0");
		FloorInt.text = "" + ActualFloor.ToString("f0");
        ElevatorAnimator = this.GetComponent<Animator>();
		ElevatorSound = this.GetComponent<AudioSource>();
		
		ButtonsExt = SwitchExt.GetComponentsInChildren<Collider>();
		ButtonsInt = SwitchInt.GetComponentsInChildren<Collider>();
		RendsExt = SwitchExt.GetComponentsInChildren<MeshRenderer>();
		RendsInt = SwitchInt.GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {	
	
	// RAYCAST EVENT
	
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Mouse Position Ray
		//Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0)); // Center Position Ray
		RaycastHit hit;
		
		// Button Exterior Up Down
		
		if (ButtonsExt[0].Raycast(ray,out hit,10))
			{	
				if (Input.GetMouseButtonUp(0))
					{
					RendsExt[1].material = RedEmission;
					CallElevator();
					}	
			}
			
		if (ButtonsExt[1].Raycast(ray,out hit,10))
			{	
				if (Input.GetMouseButtonUp(0))
					{
					RendsExt[2].material = RedEmission;
					CallElevator();
					}	
			}
			
		// Button Interior Number Open Close
		
		if (ButtonsInt[0].Raycast(ray,out hit,10)) // Button 1
			{	
				if (Input.GetMouseButtonUp(0))
					{
					FloorElevator=1;
					if (ActualFloor!=FloorElevator)
					{
					RendsInt[0].material = RedEmission;
					UpDownElevator();
					DisabledCollider();	
					}
					
					}	
			}
			
		if (ButtonsInt[1].Raycast(ray,out hit,10)) // Button 2
			{	
				if (Input.GetMouseButtonUp(0))
					{
					FloorElevator=2;
					
					if (ActualFloor!=FloorElevator)
					{
					RendsInt[1].material = RedEmission;
					UpDownElevator();
					DisabledCollider();	
					}
					
					}	
			}	
			
		if (ButtonsInt[2].Raycast(ray,out hit,10)) // Button 3
			{	
				if (Input.GetMouseButtonUp(0))
					{
					FloorElevator=3;
					if (ActualFloor!=FloorElevator)
					{
					RendsInt[2].material = RedEmission;
					UpDownElevator();
					DisabledCollider();	
					}
					}	
			}
			
		if (ButtonsInt[3].Raycast(ray,out hit,10)) // Button 4
			{	
				if (Input.GetMouseButtonUp(0))
					{
					FloorElevator=4;
					if (ActualFloor!=FloorElevator)
					{
					RendsInt[3].material = RedEmission;
					UpDownElevator();
					DisabledCollider();	
					}
					}	
			}	
			
		if (ButtonsInt[4].Raycast(ray,out hit,10)) // Button 5
			{	
				if (Input.GetMouseButtonUp(0))
					{
					FloorElevator=5;
					if (ActualFloor!=FloorElevator)
					{
					RendsInt[4].material = RedEmission;
					UpDownElevator();
					DisabledCollider();	
					}
					}	
			}
			
		if (ButtonsInt[5].Raycast(ray,out hit,10)) // Button 6
			{	
				if (Input.GetMouseButtonUp(0))
					{
					FloorElevator=6;
					if (ActualFloor!=FloorElevator)
					{
					RendsInt[5].material = RedEmission;
					UpDownElevator();
					DisabledCollider();	
					}
					}	
			}	
			
		if (ButtonsInt[6].Raycast(ray,out hit,10)) // Button 7
			{	
				if (Input.GetMouseButtonUp(0))
					{
					FloorElevator=7;
					if (ActualFloor!=FloorElevator)
					{
					RendsInt[6].material = RedEmission;
					UpDownElevator();
					DisabledCollider();	
					}
					}	
			}
			
		if (ButtonsInt[7].Raycast(ray,out hit,10)) // Button 8
			{	
				if (Input.GetMouseButtonUp(0))
					{
					FloorElevator=8;
					if (ActualFloor!=FloorElevator)
					{
					RendsInt[7].material = RedEmission;
					UpDownElevator();
					DisabledCollider();	
					}
					}	
			}
			
		if (ButtonsInt[8].Raycast(ray,out hit,10)) // Button 9
			{	
				if (Input.GetMouseButtonUp(0))
					{
					FloorElevator=9;
					if (ActualFloor!=FloorElevator)
					{
					RendsInt[8].material = RedEmission;
					UpDownElevator();
					DisabledCollider();	
					}
					}	
			}	
			
		if (ButtonsInt[9].Raycast(ray,out hit,10)) // Button 10
			{	
				if (Input.GetMouseButtonUp(0))
					{
					FloorElevator=10;
					if (ActualFloor!=FloorElevator)
					{
					RendsInt[9].material = RedEmission;
					UpDownElevator();
					DisabledCollider();	
					}
					}	
			}
			
		
	
	// Click Call Elevator	
	
		if (CallStandby)
		{
			
			
			if (FloorChara<=FloorElevator)
			{
				ActualFloor -= Time.deltaTime*0.5f;
				FloorExt.text = "" + ActualFloor.ToString("f0");
				FloorInt.text = "" + ActualFloor.ToString("f0");
				DownExt.SetActive(true);
				
				if ((0f)<(ActualFloor-FloorChara) && (ActualFloor-FloorChara) < (0.49f))
				{
				ElevatorSound.PlayOneShot(OpenClip, 1f);
				ActualFloor=FloorChara;
				ElevatorAnimator.Play("Open");	
				RendsExt[1].material = Transparent;
				RendsExt[2].material = Transparent;
				DownExt.SetActive(false);
				CallStandby = false;
				}	
			}	
			
			if (FloorChara>=FloorElevator)
			{
				ActualFloor += Time.deltaTime*0.5f;
				FloorExt.text = "" + ActualFloor.ToString("f0");
				FloorInt.text = "" + ActualFloor.ToString("f0");
				UpExt.SetActive(true);
				
				if ((0f)<(FloorChara-ActualFloor) && (FloorChara-ActualFloor) < (0.49f))
				{
				ElevatorSound.PlayOneShot(OpenClip, 1f);
				ActualFloor=FloorChara;
				ElevatorAnimator.Play("Open"); 
				RendsExt[1].material = Transparent;
				RendsExt[2].material = Transparent;
				UpExt.SetActive(false);
				CallStandby = false;
				}	
			}	
		}
		
	// Click Up or Down Elevator	
		
		if (ClickUpDown)
		{
			
			
			if (FloorChara<=FloorElevator)
			{
				ActualFloor += Time.deltaTime*0.4f;
				FloorExt.text = "" + ActualFloor.ToString("f0");
				FloorInt.text = "" + ActualFloor.ToString("f0");
				UpInt.SetActive(true);
				
				if ((0f)<(ActualFloor-FloorElevator) && (ActualFloor-FloorElevator) < (0.49f))
				{
				ElevatorSound.PlayOneShot(OpenClip, 1f);
				ActualFloor=FloorElevator;
				FloorChara=ActualFloor;
				ElevatorAnimator.Play("Open");	
				RendsInt[0].material = Transparent;
				RendsInt[1].material = Transparent;	
				RendsInt[2].material = Transparent;
				RendsInt[3].material = Transparent;	
				RendsInt[4].material = Transparent;
				RendsInt[5].material = Transparent;				
				RendsInt[6].material = Transparent;
				RendsInt[7].material = Transparent;		
				RendsInt[8].material = Transparent;
				RendsInt[9].material = Transparent;
				RendsExt[1].material = Transparent;
				RendsExt[2].material = Transparent;				
				UpInt.SetActive(false);
				EnabledCollider();
				ClickUpDown = false;
				}	
			}	
			
			if (FloorChara>=FloorElevator)
			{
				ActualFloor -= Time.deltaTime*0.4f;
				FloorExt.text = "" + ActualFloor.ToString("f0");
				FloorInt.text = "" + ActualFloor.ToString("f0");
				DownInt.SetActive(true);
				
				if ((0f)<(FloorElevator-ActualFloor) && (FloorElevator-ActualFloor) < (0.49f))
				{
				ElevatorSound.PlayOneShot(OpenClip, 1f);
				ActualFloor=FloorElevator;
				FloorChara=ActualFloor;
				ElevatorAnimator.Play("Open"); 
				RendsInt[0].material = Transparent;
				RendsInt[1].material = Transparent;	
				RendsInt[2].material = Transparent;
				RendsInt[3].material = Transparent;	
				RendsInt[4].material = Transparent;
				RendsInt[5].material = Transparent;				
				RendsInt[6].material = Transparent;
				RendsInt[7].material = Transparent;		
				RendsInt[8].material = Transparent;
				RendsInt[9].material = Transparent;
				RendsExt[1].material = Transparent;
				RendsExt[2].material = Transparent;
				DownInt.SetActive(false);
				EnabledCollider();
				ClickUpDown = false;
				}	
			}	
		}
		
		
		
	
    }
	
	// Functions
	
	void CallElevator()
	
	{
	ElevatorSound.PlayOneShot(ButtonClip, 1f);
	
	
		if (ActualFloor!=FloorChara)
		{
		CallStandby = true;
		}
	}
	
	void UpDownElevator()
	
	{
	FloorChara=ActualFloor;	
	ElevatorSound.PlayOneShot(ButtonClip, 1f);
	ElevatorAnimator.Play("Close");
		if (ActualFloor!=FloorElevator)
		{
		ClickUpDown = true;
		} 
	}
	
	void EnabledCollider()
	{
		foreach (Collider Col in  ButtonsInt) 
		{
		Col.enabled = true;
		}
	}
	
	void DisabledCollider()
	{
		foreach (Collider Col in ButtonsInt) 
		{
		Col.enabled = false;
		}
	}
	
}
	
	}