## DocumentAccessApprovalSystem
### Endpoints

| Method | Endpoint                                      | Description                                                           |
|--------|-----------------------------------------------|-----------------------------------------------------------------------|
| `GET`  | `/api/users/{id}/accessrequests`              | Returns access requests for current user.                             |
| `GET`  | `/api/users/{id}/accessrequests/pending`      | Returns pending access requests for user with approver role(2)        |
| `POST` | `/api/users/{id}/accessrequests`              | Creates access request for user.                                      |                                                
| `POST` | `/api/users/{id}/accessrequests/{id}/approve` | Approves a request. User must have approver role(2)                   |
| `POST` | `/api/users/{id}/accessrequests/{id}/reject`  | Rejects a request. User must have approver role(2)                    |


Example Request body for request creation:

>{
>    "documentId": 3,
>    "accessType": 2,
>    "status": 1
>}
>

>Request body for approving/rejecting requests is raw string.

### Structure

•	Controllers depend on services/repositories for business logic.

•	Repositories interact with MyDbContext for data persistence.

•	Entities are mapped to database tables via EF Core.

•	DTOs and AutoMapper profiles facilitate data transfer between layers.

### To add/improve

- user authentication

- improve error handling

- add duplicates check
