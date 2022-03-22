using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPhysics : MonoBehaviour
{
	public LineRenderer lineRenderer;
	[Range(3, 50)]
	public int lineSegmentCount = 30;
	private List<Vector3> _linePoints = new List<Vector3>();

	// Updates the line renderer to a new trajectory.
    	public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
	{
		// Velocity per frame is direction times force (forceVector) divided by mass times time per frame.
		Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;

		// Scaling flight duration with physics calculation didn't work. Made large enough for long trajectories.
		float FlightDuration = 70;

		// Time interval at which to set each point in the line.
		float stepTime = FlightDuration / lineSegmentCount;

		// Clear previous list. 
		_linePoints.Clear();

		for (int i = 0; i < lineSegmentCount; i++)
		{
			// Change in time.
			float stepTimePassed = stepTime * i;

			// Each point's change is a result of velocity and time passed, with gravity also acting on the y-axis.
			Vector3 MovementVector = new Vector3(
				velocity.x * stepTimePassed,
				// 0.0002 should be 0.5 according to physics, but didn't work for some reason.
				velocity.y * stepTimePassed + 0.0002f * Physics.gravity.y * stepTimePassed * stepTimePassed,
				velocity.z * stepTimePassed
			);

			// The actual points are a combination of the starting point and change over time.
			_linePoints.Add(MovementVector + startingPoint);
		}
		// Change the line renderer position count to fit our list.
		lineRenderer.positionCount = _linePoints.Count;
		// lineRenderer.SetPositions() accepts an array.
		lineRenderer.SetPositions(_linePoints.ToArray());
	}

	// To hide the line, simply remove the positions in lineRenderer by lowering their count to 0.
	public void HideLine()
	{
		lineRenderer.positionCount = 0;
	}
}
