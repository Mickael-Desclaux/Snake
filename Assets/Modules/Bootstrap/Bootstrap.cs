using System;
using Modules.Controls;
using UnityEngine;

namespace Modules.Bootstrap
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ControlsManager _controlsManagerPrefab;

        private void Awake()
        {
            if (_controlsManagerPrefab == null)
            {
                throw new NotImplementedException();
            }
        }

        private void Start()
        {
            Instantiate(_controlsManagerPrefab);
        }
    }
}
