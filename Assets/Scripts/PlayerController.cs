﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {
    public Interactable focus;
    public LayerMask movementMask;
    private Camera cam;
    private PlayerMotor motor;
    
    
    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                motor.MoveToPoint(hit.point);
                
                RemoveFocus();
            }
        }
        
        if (Input.GetMouseButtonDown(1)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable) {
                    SetFocus(interactable);
                }
                // if so set it as our focus
            }
        }
    }

    private void RemoveFocus() {
        if (focus != null) {
            focus.OnUnFocused();
        }

        focus = null;
        motor.StopFollowTarget();
    }

    private void SetFocus(Interactable newFocus) {
        if (newFocus != focus) {
            if (focus != null) {
                focus.OnUnFocused();
            }

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }
}
