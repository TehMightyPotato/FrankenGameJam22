using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Extensions;
using UnityEngine.Events;

namespace Shooting
{
    public class PlayerShooting : MonoBehaviour
    {
        private ControlScheme _controlScheme;
        [SerializeField] private ProjectilesHandler projectilesHandler;
        [SerializeField] private Transform spawnPosition;
        [SerializeField] private float shootingVelocity;
        [SerializeField] private LineRenderer lineRenderer;

        public MusicManager MusicManager;
        public FMODUnity.EventReference ShotEvent;

        private int _selectedRow;
        private Vector2 _mousePosition;
        private Camera _mainCam;
        
        private void Start()
        {
            _controlScheme = new ControlScheme();
            _mainCam = CameraExtensions.GetMainCamForActiveScene();
            SubscribeInputEvents();
            lineRenderer.SetPosition(0,spawnPosition.position);
            _controlScheme.Enable();
        }

        private void SubscribeInputEvents()
        {
            _controlScheme.Gameplay.Fire1.performed += FireSelected;
            _controlScheme.Gameplay.Fire2.performed += FireSelected;
            _controlScheme.Gameplay.Fire3.performed += FireSelected;
            _controlScheme.Gameplay.SelectRow1.performed += OnSelectRow1Onperformed;
            _controlScheme.Gameplay.SelectRow2.performed += OnSelectRow2Onperformed;
            _controlScheme.Gameplay.SelectRow3.performed += OnSelectRow3Onperformed;
            _controlScheme.Gameplay.Mouse.performed += HandleMousePosition;
        }

        private void OnSelectRow3Onperformed(InputAction.CallbackContext _)
        {
            SelectRow(2);
        }

        private void OnSelectRow2Onperformed(InputAction.CallbackContext _)
        {
            SelectRow(1);
        }

        private void OnSelectRow1Onperformed(InputAction.CallbackContext _)
        {
            SelectRow(0);
        }

        private void UnsubscribeInputEvents()
        {
            _controlScheme.Gameplay.Fire1.performed -= FireSelected;
            _controlScheme.Gameplay.Fire2.performed -= FireSelected;
            _controlScheme.Gameplay.Fire3.performed -= FireSelected;
            _controlScheme.Gameplay.SelectRow1.performed -= OnSelectRow1Onperformed;
            _controlScheme.Gameplay.SelectRow2.performed -= OnSelectRow2Onperformed;
            _controlScheme.Gameplay.SelectRow3.performed -= OnSelectRow3Onperformed;
            _controlScheme.Gameplay.Mouse.performed -= HandleMousePosition;
        }
        

        private void HandleMousePosition(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            _mousePosition = context.ReadValue<Vector2>();
            var laserVector = _mainCam.MousePositionToWorldPosition(_mousePosition);
            laserVector.z = 10;
            laserVector *= 20;
            lineRenderer.SetPosition(1,laserVector);
        }
        
        private void FireSelected(InputAction.CallbackContext context)
        {   
            if(!context.performed) return;
            switch (context.action.name)
            {
                case "Fire1":
                    Fire(projectilesHandler.GetSelectedPrefab(_selectedRow, 0));
                    break;
                case "Fire2":
                    Fire(projectilesHandler.GetSelectedPrefab(_selectedRow, 1));
                    break;
                case "Fire3":
                    Fire(projectilesHandler.GetSelectedPrefab(_selectedRow, 2));
                    break;
            }
        }

        private void Fire(GameObject prefab)
        {
            var obj = Instantiate(prefab, spawnPosition.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce(CalcForceVector());

            MusicManager.PlayEvent(ShotEvent);
        }

        private Vector3 CalcForceVector()
        {
            var direction = _mainCam.MousePositionToWorldPosition(_mousePosition);
            direction.z = 10;
            return direction * shootingVelocity;
        }

        private void SelectRow(int index)
        {
            _selectedRow = index;
            projectilesHandler.RowChanged(index);
        }

        private void OnDisable()
        {
            UnsubscribeInputEvents();
        }
    }
}
