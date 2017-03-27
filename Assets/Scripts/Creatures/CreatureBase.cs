//Created by Kenneth Mak

//This is the base script which should hold the majority of all common functions of the aquatic sealife
//Creature-specific functions such as fish eating, sharks attacking, etc. can be found in their scripts

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureBase : MonoBehaviour {


	public enum CreatureState
	{
		Wander, //swimming around cage
		Chase, //going to target (food, cage)
		Attack, //hit target 
		Dead, 
	}
	public CreatureState CurrentState;

	protected Transform myTransform; //my transform
	protected GameObject target;
	protected Vector3 targetPosition; //destination transform

	public float curSpeed;
	public float curRotSpeed;

	protected float elapsedTime = 0.0f;

	protected float maxSpeed;
	protected float maxRotSpeed;

	protected bool attacking;

	protected bool flocking; //is this thing a part of a flock?
	protected GameObject flockingLeader; //Is this thing the leader?
	protected Transform FlockDisplacement; //Dispalcement from leader


	// Use this for initialization
	void Start () {
		Initialize ();	
	}
	
	// Update is called once per frame
	void Update () {
		CreatureUpdate ();
	}


	protected virtual void Initialize() {
		CurrentState = CreatureState.Wander;
		targetPosition = transform.position;
		curSpeed = 4.0f; curRotSpeed = 5.0f;
		target = transform.GetChild (0).gameObject;
		target.transform.parent = null;
		GetNewWanderTarget ();
	}


	protected virtual void CreatureUpdate() {
		switch (CurrentState) {
		case CreatureState.Wander:
			UpdateWanderState ();
			break;
		case CreatureState.Chase:
			UpdateChaseState();
			break;
		case CreatureState.Attack:
			UpdateAttackState ();
			break;
		case CreatureState.Dead:
			break;
		}

		elapsedTime += Time.deltaTime;
	}

	protected void UpdateWanderState() {
		if (target.transform.name == "Food") { //arbitrary case to initiate chase status
			SwitchCurrentStateTo (CreatureState.Chase);
		} else {
			if (elapsedTime > 10.0f) // Mathf.Abs(Vector3.Distance(targetPosition, transform.position)) <= 0.000005f
				GetNewWanderTarget ();
			MoveToward (target.transform.position);
		}
	}

	protected void UpdateChaseState(){
		MoveToward (target.transform.position);
		if (Mathf.Abs(Vector3.Distance(target.transform.position, transform.position)) <= 2.0f) { //if reached chase target, attack
			SwitchCurrentStateTo(CreatureState.Attack);
		}
		if (target.transform.name != "Food")  //no food, wander
			SwitchCurrentStateTo (CreatureState.Wander);
		
	}

	protected void UpdateAttackState(){
		if (Mathf.Abs(Vector3.Distance(target.transform.position, transform.position)) <= 2.0f) { //start lunging
			AttackTarget (target);
		}
		if (Mathf.Abs (Vector3.Distance (target.transform.position, transform.position)) <= 0.1f) //if hit food, reset the object
			GetNewWanderTarget ();
		
		if (Mathf.Abs(Vector3.Distance(target.transform.position, transform.position)) >= 2.1f && attacking) { //stop lunging
			attacking = false;
			SwitchCurrentStateTo (CreatureState.Chase);
		}
	}

	protected void GetNewWanderTarget(){
		elapsedTime = 0;
		targetPosition = new Vector3 (Random.Range (-10.0f, 10.0f), 7, Random.Range (-10.0f, 10.0f));
		target.transform.position = targetPosition;

		//randomizer between food or waypoint for target test
		if (Random.Range (0, 2) == 0)
			target.transform.name = "WayPoint";
		else
			target.transform.name = "Food";
	}

	protected void MoveToward(Vector3 targetLocation){
		Vector3 newRot = Vector3.RotateTowards 
			(transform.forward,  //direction face of which side should point to it
			targetLocation - transform.position, //direction vector
			curRotSpeed * Time.deltaTime, 0.0f); //rotatoin speed, max magnitude
		transform.rotation = Quaternion.LookRotation(newRot); //rotate to new vector
		transform.Translate (Vector3.forward * Time.deltaTime * curSpeed); //move forward
	}

	protected void AttackTarget(GameObject attackTarget){
		if(!attacking) {
			transform.LookAt (attackTarget.transform.position, Vector3.forward); //lunging, can not turn
			attacking = true; //you are now attacking
		}
		transform.Translate (Vector3.forward * Time.deltaTime * curSpeed * 5);//lunging towards food
	}

	protected void SwitchCurrentStateTo(CreatureState newState){
		CurrentState = newState;
		elapsedTime = 0.0f;
	}

}
