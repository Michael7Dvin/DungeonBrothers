using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.HealthBar
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _fillerPartBar;

        public void UpdateHealthBar(float value) => 
            _fillerPartBar.fillAmount = value;
    }
}