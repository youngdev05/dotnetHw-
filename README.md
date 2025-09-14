# ASP.NET Core Web API — Управление пользователями

## Структура проекта

```text
UserApi
├── Controllers
│   └── UsersController.cs   — контроллер с 4 методами
├── Models
│   └── User.cs              — модель пользователя
└── Program.cs               — настройка Swagger
```

## Эндпоинты

- **POST** `/Users/login` — авторизация  
- **POST** `/Users` — создать пользователя  
- **PUT** `/Users/{id}` — обновить пользователя  
- **DELETE** `/Users/{id}` — удалить пользователя  

## Тестовый пользователь

```json
{
  "userId": 1,
  "username": "user",
  "password": "user"
}
