WebShop API — Простой интернет-магазин на ASP.NET Core

WebShop API — это backend для онлайн-магазина, реализованный на ASP.NET Core 8 с использованием чистой архитектуры (Controllers → Services → Repository/Entity Framework).  
Проект демонстрирует базовую функциональность e-commerce: управление товарами, корзиной и заказами.

Создан как pet-проект для собеседования на позиции .NET Developer (Junior).

Основные возможности

- CRUD для товаров (Product)
- Добавление/удаление/изменение количества товаров в корзине
- Создание заказа из корзины с уменьшением остатков на складе
- Отмена заказа → возврат товаров на склад
- Мягкое удаление товаров (IsActive)
- Передача цены на момент покупки в заказ (защита от изменения цены)
- DTO для ответов (не экспонируем модели БД напрямую)
- Валидация входных данных через DataAnnotations
- Асинхронные операции с EF Core

Технологический стек

- Backend: ASP.NET Core 8 (Web API)
- ORM: Entity Framework Core
- База данных: SQLite (для простоты локального запуска)
- DI: встроенный Microsoft.Extensions.DependencyInjection
- Swagger: Swashbuckle.AspNetCore для документации API
- Архитектура: Layered (Controllers, Services, Models, DTO)
- Дополнительно: async/await, DataAnnotations validation, Enums для статусов

 Как запустить проект локально

 Требования

- .NET 8 SDK
- Git
