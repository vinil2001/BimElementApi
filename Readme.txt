# Simple BIM Element API

This is a minimal ASP.NET Core Web API for managing BIM elements (like walls, doors, etc.) with in-memory storage. 
Designed as a take-home assignment to demonstrate clean API design, validation, and routing.

Features:
- Add, get, list, and update BIM elements
- In-memory storage (no database required)
- Automatic status assignment based on progress:
  - `0%` → **NotStarted**
  - `1–99%` → **InProgress**
  - `100%` → **Completed**
- Basic validation and error handling
- Swagger UI for testing

Run the App:
1. Clone the repo
2. Open terminal in the project directory
3. Run:
```bash
dotnet run

Swagger UI:
http://localhost:[yourPort]/swagger

---

Example API Usage (bash):
	Add Element
		curl -X POST "http://localhost:5242/api/elements" \
  		-H "Content-Type: application/json" \
  		-d '{"ifcGuid": "wall-123", "elementType": "Wall", "progressPercent": 0}'
	Update Progress
		curl -X PUT "http://localhost:5242/api/elements/wall-123/progress" \
  		-H "Content-Type: application/json" \
  		-d '{"progressPercent": 75}'
	Get Element
		curl http://localhost:5242/api/elements/wall-123
	Get All Elements
		curl http://localhost:5242/api/elements

---


Notes:
 - No database required — all data is lost when the app stops.

 - Duplicate IfcGuid entries return a 409 Conflict.

  -ProgressPercent must be between 0 and 100.
