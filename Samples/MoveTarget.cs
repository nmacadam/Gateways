// Gateways, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;

namespace Gateways
{
	public class MoveTarget : MonoBehaviour
	{
		[SerializeField] private Vector3 _offset = Vector3.down;

		public void SetPosition(Vector3 position)
		{
			transform.position = position + _offset;
		}
	}
}