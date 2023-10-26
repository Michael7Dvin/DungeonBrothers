using UnityEngine;
using UnityEngine.UI;

namespace _Project.CodeBase.UI.HealthBar
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _fillerPartBar;

        public void UpdateHealthBar(float value) => 
            _fillerPartBar.fillAmount = value;

        public void Destroy() =>
            Destroy(this);
    }
}