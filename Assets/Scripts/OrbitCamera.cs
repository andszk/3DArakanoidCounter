using UnityEngine;
public class OrbitCamera : MonoBehaviour
{
	[SerializeField, Range(1f, 25f)]
	float distance = 15f;

	[SerializeField, Range(150f, 1500f)]
	public float rotationSpeed = 100f;

	[SerializeField]
	public float minRotationVertical = 0f;

	[SerializeField]
	public float maxRotationVertical = 90f;

	private Vector3 target = new Vector3(0, 0, 0);
	private Vector2 orbitAngles = new Vector2(45f, 0f);
	private const float e = 0.001f;

	void LateUpdate()
	{
		float y = -Input.GetAxis("CameraVertical");			
		float z = Input.GetAxis("CameraHorizontal");
		if (y < -e || y > e || z < -e || z > e)
		{
			Vector2 input = new Vector2(2*y, z);
			orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
			orbitAngles.x = Mathf.Clamp(orbitAngles.x, minRotationVertical, maxRotationVertical);
			Quaternion lookRotation = Quaternion.Euler(orbitAngles);
			Vector3 lookDirection = lookRotation * Vector3.forward;
			Vector3 lookPosition = target - lookDirection * distance;
			transform.SetPositionAndRotation(lookPosition, lookRotation);
		}
	}
}