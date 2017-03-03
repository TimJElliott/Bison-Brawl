using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToPixelGrid : MonoBehaviour {

	public Transform SpriteObject;
	public int PixelsPerUnit;
	public Vector3 Offset;
	public bool SuppressWarnings;		//Give the user the option to supress warnings in case they want to assign this value at runtime and not generate spam

	public void Update () {

		//Make sure the user/dev has provided an object
		if (!SpriteObject) {
			if (!SuppressWarnings)
				Debug.LogWarning ("Please assign a valid object to \"SpriteObject\"", this);
			return;
		}

		//Make sure the user/dev uses a child gameObject as the SpriteObject.
		if (SpriteObject.transform.parent != transform) {
			
			if (!SuppressWarnings)
				if (SpriteObject == transform) {
					Debug.LogError ("You cannot assign this object to itself. Please assign a child to the \"SpriteObject\" variable", this);
				} else {
					Debug.LogError (SpriteObject.gameObject + " is not a child of this object. Please assign a child to the \"SpriteObject\" variable", this);
				}

			SpriteObject = null;
			return;
		}

		//Make sure the user/dev provides a number of pixels per unit (some values are divided by the PPU var so it is essential to show a warning
		if (PixelsPerUnit <= 0) {

			if (!SuppressWarnings)
				Debug.LogWarning ("Please specify a valid amount of pixels per unit to use for snapping. Current value: " + PixelsPerUnit);

			return;

		}

		//If all failsafes have passed, proceed to set the new position of the SpriteObject using the SnapVectorToPixel method
		SpriteObject.transform.position = SnapVectorToPixel (transform.position, PixelsPerUnit) + ((Offset * PixelsPerUnit) / Mathf.Pow (PixelsPerUnit, 2)); //Divide by 512 for some uknown reason. It works though ¯\_(ツ)_/¯---- Turns out you're supposed to divide by the pixelsperunit squared. that's why i got 512 but 10 for using 10 ppi

	}

	private Vector3 SnapVectorToPixel (Vector3 vectorToSnap, int pixelsPerUnit) {

		//Multiply the vector by the number of pixels, round to nearest whole number, and divide by pixels per unit to receive the snapped value

		Vector3 vec = vectorToSnap* PixelsPerUnit;

		vec = new Vector3 (Mathf.Round (vec.x)/pixelsPerUnit, Mathf.Round (vec.y)/pixelsPerUnit, Mathf.Round (vec.z)/pixelsPerUnit);

		return vec;
		
	}

}
