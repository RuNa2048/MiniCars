using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : BaseInput
{
	public override Vector2 GenerateInput()
	{
		return new Vector2 {
			x = Input.GetAxis(GameConstants.k_AxisNameHorizontal),
			y = Input.GetAxis(GameConstants.k_AxisNameVertical)
		};
	}
}
