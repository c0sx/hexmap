using UnityEngine;

namespace Unit
{
    [RequireComponent(typeof(MeshRenderer))]
    public class PawnUnit : MonoBehaviour, Unit
    {
        private MeshRenderer _mesh;
        private int _distance;
        private int _direction;
        private Color _notSelected;
        private Color _selected;

        private void Awake()
        {
            _mesh = GetComponent<MeshRenderer>();
        }

        public void Init(int direction, int distance, Player player)
        {
            _notSelected = player.Primary;
            _selected = player.Selected;
            _direction = direction;
            _distance = distance;
        }
        
        public void Select()
        {
            _mesh.material.color = _selected;
        }
        
        public void Deselect()
        {
            _mesh.material.color = _notSelected;
        }
    }
}