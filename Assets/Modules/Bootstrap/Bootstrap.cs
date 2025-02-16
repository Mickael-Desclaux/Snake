using System;
using Modules.Controls;
using UnityEngine;

namespace Modules.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ControlsManager _controlsManagerPrefab;
        [SerializeField] private GameManager _gameManagerPrefab;

        private void Start()
        {
            Instantiate(_controlsManagerPrefab);
            Instantiate(_gameManagerPrefab);
        }
    }
}
