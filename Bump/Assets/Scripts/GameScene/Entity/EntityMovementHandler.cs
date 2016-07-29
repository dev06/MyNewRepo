﻿using UnityEngine;
using System.Collections;
using System;
public class EntityMovementHandler : MonoBehaviour {

	protected Rigidbody2D rg2d;
	protected float _boostCoolDown = 1.5f;
	protected float _boostCounter = 0.0f;
	protected bool _boosted = false;
	protected PolygonCollider2D pCol2D;
	protected CustomAnimator _animator;


	void Start () {
		Init();
	}

	protected void Init() {
		try {
			rg2d = GetComponent<Rigidbody2D>();
			pCol2D = transform.FindChild("BumperCollider").GetComponent<PolygonCollider2D>();
			_animator = GetComponent<CustomAnimator>();

		} catch (Exception e) {
			Debug.LogError("One of the component is not found : Source EntityMovementHandler ");
		}
	}

	// Update is called once per frame
	void Update () {

	}

	protected void Move() {

	}

	protected void Boost(Vector2 movement, float boost, out bool isBoosting)
	{
		if (_boosted == false && (movement != Vector2.zero || (Mathf.Abs(rg2d.velocity.x) >= 2 || Mathf.Abs(rg2d.velocity.y) >= 2)))
		{
			Vector2 force = movement * boost;
			rg2d.AddForce(force);

			_boosted = true;
			isBoosting = _boosted;
		} else {

			isBoosting = false;
		}
	}

	protected void Boost(Vector2 movement, float boost)
	{
		if (_boosted == false && (movement != Vector2.zero || (Mathf.Abs(rg2d.velocity.x) >= 2 || Mathf.Abs(rg2d.velocity.y) >= 2)))
		{
			Vector2 force = movement * boost;
			rg2d.AddForce(force);
			_boosted = true;
		}
	}

	protected void CanBoost()
	{
		if (_boosted)
		{
			_boostCounter += Time.deltaTime;
			if (_boostCounter > _boostCoolDown)
			{
				_boostCounter = 0;
				_boosted = false;
			}
		}
	}

	protected void AddExternalObject(GameObject obj, Vector3 position, Quaternion rotation)
	{
		GameObject ring = Instantiate(obj, position, rotation) as GameObject;
	}

	protected void AnimateBumper(int rate) {
		_animator.AnimateBumper(rate);
	}

	protected void FaceFoward(Transform transform, Vector2 movement) {
		float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	protected void AdjustColliderOffset(float index)
	{
		Vector2 offset = new Vector2(-.45f, 0);
		offset.x = (.45f * index) / (_animator.sprites.Length - 1);
		pCol2D.offset = offset;
	}

	protected bool isMoving(bool useVelocity, Vector2 movement) {
		if (useVelocity)
			return rg2d.velocity.x != 0 && rg2d.velocity.y != 0;
		else
			return movement.x != 0 || movement.y != 0;
	}


}
