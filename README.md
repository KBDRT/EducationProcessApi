# Education Process

WebAPI для оценки учебных уроков руководством образовательного учреждения.

Основной стек: C#, ASP.NET Core, Entity Framework Core, PostreSQL.

Проект реализован в виде монолита с использованием Clean Architecture (с разделением на 4 слоя).

Дополнительные технологии:
- Валидация (FluentValidation)
- Кеширование (Memory cache и Distributed cache Redis)
- Использование подхода CQRS (MediatR)
- Логирование (Seriloq)
- Стандартная аутентификация и авторизация с помощью JWT токена
- Тестирование (XUnit и Moq)
- Работа с файлами (Minio S3)
- Использование Docker compose для объединения всех компонентов

Приложение представлено в виде REST API, реализующее CRUD операции/работу с файлами с необходимыми сущностями (Учителя, группы, уроки и т.д.)

В Frontend реализована только главная страница.
