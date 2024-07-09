using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public class Tree : MonoBehaviour
    {
        private Node _root = null;

        protected void Start()
        {
            _root = GetComponent<Node>();
        }
    }
}

