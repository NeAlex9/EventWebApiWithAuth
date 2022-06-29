# Events Web App

<h3> Summary </h3>
 
Чтобы создать базу данных надо запустить script в папке EventsWebApi/EventDB.sql.

url для Swagger https://localhost:55059/swagger/index.html

Пользователи: 

	1) id: "a4a30721-0002-4eaf-a703-2c1512b36b82"
	   login: "Alex"
	   password: "12345678"
	   role: "speaker"

	2) id: "c7264d46-02a6-4c88-bbc7-798f595d4999"
	   login: "Nikita"
	   password: "12345678"
	   role: "organizer"

	3) id: "0bbd898b-3bcf-4717-841f-1af0bef6f036" 
	   login: "Dan"
	   password: "12345678"
	   role: "organizer", "speaker"

Логин и пароль можно использовать для авторизации url https://localhost:7164/api/Authentication/login

Получить все события может пользователь с ролью speaker url https://localhost:7164/api/Event . То есть нужно залогиниться под под ролью "speaker". 

Получить событие по id может пользователь с ролью organizer url https://localhost:7164/api/Event/{id} .  То есть нужно залогиниться под под ролью "organizer". 

На остальные операции нет ограничений

Для регистрации пользователя необходимо ввести название роли url https://localhost:7164/api/Authentication/register


Роли: 

	1) id: "1"
	   role: "organizer"
	  
	2) id: "2"
	   role: "speaker"


Для создания события необязательно, но можно ввести id организатора и/или id спикера.
