using UnityEngine;
using System.Collections.Generic;

public class DetectedManager : MonoBehaviour
{
    [SerializeField] private MessengePanel _messengePanel;
    [SerializeField] private Chest _chest;
    [SerializeField] private InputManager _inputManager;

    private Camera _camera;
    private float _interactionDistance = 3.0f;
    private List<Item> _inventoryItems = new();
    private Item _currentItem;

    private void Start()
    {
        _camera = Camera.main;
        _inputManager.TakeInputCanceled += OnTakeInput;
        _inputManager.DropInputCanceled += OnPutInput;
    }

    private void Update()
    {
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            if (hit.collider.TryGetComponent(out Item hitItem))
            {
                _currentItem = hitItem;
                _messengePanel.Take();
                Debug.Log($"Наведён на предмет: {hitItem.Type}");
            }

            if (hit.collider.TryGetComponent(out Chest chest))
            {
                _messengePanel.Put();
            }
        }
        else
        {
            _messengePanel.HideMessengePanel();
            _currentItem = null;
        }
    }

    private void OnTakeInput()
    {
        if (_currentItem != null)
        {
            _currentItem.PickUp(_inventoryItems);
        }
    }

    private void OnPutInput()
    {
        if (_inventoryItems != null)
        {
            for (int i = _inventoryItems.Count - 1; i >= 0; i--)
            {
                _inventoryItems[i].Drop(_inventoryItems, _chest.Items);
            }
        }
    }

    private void OnDisable()
    {
        _inputManager.TakeInputCanceled -= OnTakeInput;
        _inputManager.DropInputCanceled -= OnPutInput;
    }

}
