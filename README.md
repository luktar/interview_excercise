# Additional requirements

Some requirements were missing. I've added them as follow:

1. There can be limited amount of files and the size of files is not too big
2. Necessary files are: File1.pdf, File2.pdf and File3.pdf. The message will be sent after all files will have been uploaded.

# Mock data structure

Mock database has been filled with following data:

```
- user_1
  - customer_session_1
  - customer_session_2
  - customer_session_3
- user_2
  - customer_session_4
  - customer_session_5
```

Business user can have multiple customers assigned.
- `user_x` - represents business user
- `customer_session_x` - represents customer's account / session

# How to use the project

### Build and run

Use following commands on the root level of the project:

```
docker compose build
docker compose up
```

The webpage will be available at `http://localhost:8080/swagger/index.html`.

### Create new session

Create new session `customer_session_6` for user `user_1` by using POST method in `api\CustomerSession` controller.

Body:

```
{
  "customerSessionId": "user_1",
  "businessUserName": "customer_session_6"
}
```

### Upload files

Create 3 files on your drive: `File1.pdf`, `File2.pdf` and `File3.pdf`.

Use POST method `api/Upload/{customerSessionId}`, insert `customer_session_6` as a session id and upload `File1.pdf` only.

Optional:
Check session status by using GET method `api/CustomerSession/{customerSessionId}`. Use `customer_session_6` to check the relevant data.

### Upload required files and send the message

Use POST method `api/Upload/{customerSessionId}`, insert `customer_session_6` as a session id and upload 2 remaining files: `File2.pdf` and `File3.pdf`.

You can see in console the information that message has been sent:

```
filesuploaderapi-1  | Sending email:
filesuploaderapi-1  | Required files successfully uploaded for customer session ID 'customer_session_6'
```

### Check session status

Use POST method `api/Upload/{customerSessionId}` with `customer_session_6` as query param to check the relevant data.

# Limitations

Mocked database is integrated with the process (in memory) and it's breaking the idea behind microservices. 

This project can be improved by:

- adding SQL / NoSQL database
- adding DTO + Automapper instead returning entities directly from the repository / database
- JWT / OAuth authorization for business users
- Messaging can be done by separated service with the RabbitMQ queue in between or directly by SNS
- logging should be done by Log4Net or NLog + Centry / DataDog / CloudWatch
- validation for API models
- global exception handling
- save files in remote drive e.g. S3
- use K8s instead of docker-compose
- swagger should be available only in the development mode