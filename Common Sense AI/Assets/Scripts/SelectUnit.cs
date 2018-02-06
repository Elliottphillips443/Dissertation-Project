/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SelectUnit : MonoBehaviour {

	public GameObject selectedunit;
	public List<GameObject> selectedunits = new List<GameObject>();
	RaycastHit hit;
	private Vector3 MouseDownPoint, CurrentDownPoint;
	private Vector3 MouseDownPoint2, CurrentDownPoint2;
	public bool IsDragging;
	private float BoxWidth, BoxHeight, BoxLeft, BoxTop;
	private Vector2 BoxStart, BoxFinish;
	public List<GameObject> UnitsOnScreenSpace = new List<GameObject>();
	public List<GameObject> UnitInDrag = new List<GameObject>();
	public float distance;
	public List <GameObject> markers;
	public List<Vector3>[] path;
	//struct target{
	//	public Vector3 pos ; 
	//	public bool isTaken ;
	//	public void setIsTaken (bool value){
	//		isTaken = value;
	//	}
	//	public bool getIsTaken() {
	//		return isTaken ;
	//	}
	//};

	// is edditing
	void OnGUI()
	{
		if(IsDragging) 
		{
			GUI.Box(new Rect(BoxLeft, BoxTop, BoxWidth, BoxHeight ), "");
		}
	}
	void LateUpdate()
	{
		UnitInDrag.Clear ();
		if (IsDragging && UnitsOnScreenSpace.Count > 0)
		{
			selectedunit = null;
			for (int i = 0; i < UnitsOnScreenSpace.Count; i++) 
			{
				GameObject UnitObj = UnitsOnScreenSpace [i] as GameObject;
				Unit PosScript = UnitObj.transform.GetComponent<Unit> ();
				GameObject selectmarker = UnitObj.transform.Find ("Marker").gameObject;
				if (!UnitInDrag.Contains (UnitObj)) 
				{
					if (UnitWithInDrag(PosScript.ScreenPos)) 
					{
						selectmarker.SetActive(true);
						UnitInDrag.Add(UnitObj);
					}

					else 
					{
						if (!UnitInDrag.Contains (UnitObj)) 
						{
							selectmarker.SetActive(false);

						}
					}
				}
			}
		}
	}
	// Use this for initialization


	// Update is called once per frame
	void Update () 
	{

		if (selectedunits.Count > 0) 
		{
			if (Input.GetMouseButtonDown(1)) 
			{
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
					MouseDownPoint2 = hit.point;

					GameObject prefab = Resources.Load("Marker") as GameObject;  
					markers = new List<GameObject>();
					for (int i = 0; i < selectedunits.Count; i++) {
						GameObject marker = Instantiate (prefab, MouseDownPoint2, Quaternion.identity);
						markers.Add(marker);
					}

				}
			}
			if (Input.GetMouseButton (1)) {
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
					
					CurrentDownPoint2 = hit.point;
					Vector3 direction = CurrentDownPoint2 - MouseDownPoint2;
					Vector3 step = direction / selectedunits.Count;
					step.y = 0;
					Debug.Log (step);
				//	for (int i = 0; i < (markers.Count / 2); i++) {

				//		markers [i].transform.position = MouseDownPoint2 + ((step * i) * 2);
				//		markers [i].transform.rotation = Quaternion.LookRotation (direction);
				//	}
				//	for (int j = 6; j < (markers.Count); j++) {

				//		markers [j].transform.position = markers [j % 6].transform.position + (markers [j % 6].transform.right * 4 ); //egg;
				//		markers [j].transform.rotation = Quaternion.LookRotation (direction);
				//	}
					for (int i = 0; i < (markers.Count); i++) 
					{
						if (markers.Count > 6) {
							if (i < (markers.Count / 2)) {
								markers [i].transform.position = MouseDownPoint2 + ((step * i)*2);
								markers [i].transform.rotation = Quaternion.LookRotation (direction);
							}
							if (i >= (markers.Count / 2)) {
								markers [i].transform.position = markers [i % (markers.Count / 2)].transform.position + (markers [i % (markers.Count / 2)].transform.right * 4);
								markers [i].transform.rotation = Quaternion.LookRotation (direction);
							}
						} else {
							markers [i].transform.position = MouseDownPoint2 + ((step * i));
							markers [i].transform.rotation = Quaternion.LookRotation (direction);
						}
					}


				}
			}

					}
			

			if (Input.GetMouseButtonUp (1)) 
			{
			List<Vector3> path = new List<Vector3> ();
				for (int i = 0; i < markers.Count; i++)
				{
				path.Add (new Vector3(markers[i].transform.position.x,markers[i].transform.position.y,markers[i].transform.position.z));
					Destroy (markers [i]);
				}

				 
				if (hit.transform.tag == "Floor") 
				{

					Vector3 direction = CurrentDownPoint2 - MouseDownPoint2;
					Vector3 step = direction / selectedunits.Count;
				


				if (direction.x > 0) {
					selectedunits.Sort (delegate(GameObject a, GameObject b) {
						return Vector3.Distance (MouseDownPoint2, a.transform.position).CompareTo (Vector3.Distance (MouseDownPoint2, b.transform.position));	

					});
				}
				else if (direction.x < 0) {
					selectedunits.Sort (delegate(GameObject a, GameObject b) {
						return Vector3.Distance (CurrentDownPoint2, b.transform.position).CompareTo (Vector3.Distance (CurrentDownPoint2, a.transform.position));	


					});
				}
					//for (int i = 0; i < selectedunits.Count; i++) {
						//Debug.Log (MouseDownPoint2 + (step * i));


						//selectedunits [i].GetComponent<NavMeshAgent> ().SetDestination (MouseDownPoint2 + (step * i));
					//}
				for (int i = 0; i < (selectedunits.Count); i++) {

					selectedunits [i].GetComponent<NavMeshAgent> ().SetDestination (path[i]);
				}
				//path.Clear();
					}
				}
			//}
				
			

		 
		if (selectedunit != null){
			if (Input.GetMouseButton (1))
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 100)) {
					if (hit.transform.tag == "Floor") {
					{
						selectedunit.GetComponent<NavMeshAgent> ().SetDestination (hit.point);
					}
				}
			}
		}
			
		if (Input.GetMouseButton(0))
		{
			for (int i = 0; i < markers.Count; i++)
			{
				Destroy (markers [i]);
			}
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				if(hit.transform.tag != "SelectableUnit")
				{
					if(CheckIfMouseIsDragging())
					{
						IsDragging = true;
					}
				}
			}
		}
		if(Input.GetMouseButtonUp(0))
		{
			PutUnitsFromDragIntoSelectedUnits();
			IsDragging = false;
		}
		if (selectedunit == null)
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
				{
					if (hit.transform.tag == "SelectableUnit")
					{
						selectedunit = hit.transform.gameObject;
						selectedunit.transform.Find("Marker").gameObject.SetActive(true);
						for (int i = 0; i < selectedunits.Count; i++)
						{
							selectedunits[i].transform.Find("Marker").gameObject.SetActive(false);
						}
						selectedunits.Clear();
					}
					if(hit.transform.tag == "Floor")
					{
						for (int i = 0; i < selectedunits.Count; i++)
						{
							selectedunits[i].transform.Find("Marker").gameObject.SetActive(false);

						}
						selectedunits.Clear();
					}
				}
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
				{
					if (hit.transform.tag == "SelectableUnit")
					{
						selectedunit.transform.Find("Marker").gameObject.SetActive(false);
						selectedunit = null;
						selectedunit = hit.transform.gameObject;
						selectedunit.transform.Find("Marker").gameObject.SetActive(true);

					}
					if (hit.transform.tag == "Floor")
					{
						selectedunit.transform.Find("Marker").gameObject.SetActive(false);
						selectedunit = null;
					}
				}
			}
		}
		if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			{
				if (hit.transform.tag == "SelectableUnit")
				{
					if(selectedunit != null)
					{
						selectedunits.Add(selectedunit);
						selectedunit = null;
					}
					selectedunits.Add(hit.transform.gameObject);
					for (int i = 0; i < selectedunits.Count; i++)
					{
						selectedunits[i].transform.Find("Marker").gameObject.SetActive(true);
					}

				}
			}
		}
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
			CurrentDownPoint = hit.point;
		if(Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
				MouseDownPoint = hit.point;
		}
		if(IsDragging)
		{
			BoxWidth = Camera.main.WorldToScreenPoint(MouseDownPoint).x - Camera.main.WorldToScreenPoint(CurrentDownPoint).x;
			BoxHeight = Camera.main.WorldToScreenPoint(MouseDownPoint).y - Camera.main.WorldToScreenPoint(CurrentDownPoint).y;
			BoxLeft = Input.mousePosition.x;
			BoxTop = (Screen.height - Input.mousePosition.y) - BoxHeight;

			if(BoxWidth > 0f && BoxHeight < 0f)
			{
				BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
			}
			else if (BoxWidth > 0f && BoxHeight > 0f)
			{
				BoxStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y + BoxHeight);
			}
			else if (BoxWidth < 0f && BoxHeight < 0f)
			{
				BoxStart = new Vector2(Input.mousePosition.x + BoxWidth, Input.mousePosition.y);
			}
			else if (BoxWidth < 0f && BoxHeight > 0f)
			{
				BoxStart = new Vector2(Input.mousePosition.x + BoxWidth, Input.mousePosition.y + BoxHeight);
			}
			BoxFinish = new Vector2(BoxStart.x + Unsigned(BoxWidth), BoxStart.y - Unsigned(BoxHeight));
		}
	}
				

	float Unsigned(float val)
	{
		if (val < 0f)
			val *= -1;
		
		return val;

	}
	private bool CheckIfMouseIsDragging()
	{
		if (CurrentDownPoint.x - 2 >= MouseDownPoint.x || CurrentDownPoint.y - 2 >= MouseDownPoint.y || CurrentDownPoint.z - 2 >= MouseDownPoint.z ||
			CurrentDownPoint.x < MouseDownPoint.x - 2 || CurrentDownPoint.y < MouseDownPoint.y - 2 || CurrentDownPoint.z < MouseDownPoint.z - 2)
				return true;

		else
			
				return false;

	}

	public bool UnitWithinScreenSpace(Vector2 UnitScreenPos)
	{
		if ((UnitScreenPos.x < Screen.width && UnitScreenPos.y < Screen.height) && (UnitScreenPos.x > 0f && UnitScreenPos.y > 0f))

			return true;
		
		else

		return false;
		
	}

	public bool UnitWithInDrag(Vector2 UnitScreenPos)
	{
		if ((UnitScreenPos.x > BoxStart.x && UnitScreenPos.y < BoxStart.y) && (UnitScreenPos.x < BoxFinish.x && UnitScreenPos.y > BoxFinish.y))

			return true;
		
		else 

			return false;
		
	}
	public void PutUnitsFromDragIntoSelectedUnits()
	{
		if (UnitInDrag.Count > 0) 
		{
			for (int i = 0; i < UnitInDrag.Count; i++) 
			{
				if (!selectedunits.Contains (UnitInDrag[i]))
				{
					selectedunits.Add(UnitInDrag[i]);

				}

			//Debug.Log(direction.x);

		}
		UnitInDrag.Clear();
	}
}
}
*/