using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(ShowMenuOptionConnections))]
public abstract class MenuOption : MonoBehaviour
{
	
	[Serializable] 
	public struct NearbyOptions {
		public MenuOption up;
		public MenuOption right;
		public MenuOption down;
		public MenuOption left;
	}

	public NearbyOptions nearbyOption;

	public abstract void Select();

	public virtual MenuOption GetUpNeighbour() {
		return nearbyOption.up;
	}

	public virtual MenuOption GetDownNeighbour() {
		return nearbyOption.down;
	}

	public MenuOption GetRightNeighbour() {
		return nearbyOption.right;
	}

	public MenuOption GetLeftNeighbour() {
		return nearbyOption.left;
	} 



}
