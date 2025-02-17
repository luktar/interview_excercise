# Additional requirements

Some requirements were missing. I've added them as follow:

1. There can be limited amount of files and the size of files is not too big
2. Necessary files are: File1.pdf, File2.pdf and File3.pdf. The message will be sent after all files will have been uploaded.

# Mock data structure

Mock database has been filled with following data:

- user_1
  - customer_session_1
  - customer_session_2
  - customer_session_3
- user_2
  - customer_session_4
  - customer_session_5

Business user can have multiple customers assigned.
- user_x - represents business user
- customer_session_x - represents customer's account / session

# How to use the project

### Create new session



# Limitations

Mocked database is integrated with the process (in memory) and it's breaking the idea behind microservices. 

This model can be improved by:

- adding SQL / NoSQL database
- adding DTO + Automapper instead returning entities directly from the repository / database
- JWT / OAuth authorization for business users
- Messaging can be done by separated service with the RabbitMQ queue in between or directly by SNS
- logging should be done by Log4Net or NLog + Centry / DataDog / CloudWatch
- validation for API models
- global exception handling
- save files in remote drive e.g. S3
- use K8s instead of docker-compose