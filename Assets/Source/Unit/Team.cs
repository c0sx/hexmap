using System;
using System.Collections.Generic;

using UnityEngine;

namespace Unit 
{
    public class Team : MonoBehaviour
    {
        [SerializeField] private List<Pawn> _pawns;
        [SerializeField] private Color _primary;
        [SerializeField] private Color _selected;

        public Color Primary => _primary;
        public Color Selected => _selected;
        public int Count => _pawns.Count;

        public Action PawnAdded;

        private void Awake()
        {
            _pawns = new List<Pawn>();
        }

        public void Add(Pawn pawn)
        {
            pawn.AssignTeam(this);
            _pawns.Add(pawn);

            PawnAdded?.Invoke();
        }
    }
}
