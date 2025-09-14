ASP.NET Core Web API - Управление пользователями

/UserApi
  /Controllers
    UsersController.cs     - контроллер с 4 методами
  /Models
    User.cs               - модель пользователя
  Program.cs              - настройка Swagger

POST /Users/login - авторизация
POST /Users - создать пользователя
PUT /Users/{id} - обновить пользователя
DELETE /Users/{id} - удалить пользователя

тестовый юзер:
user id = 1,
username = user,
pasword = user
