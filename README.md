# LiveLib

## Сущности:
- User(Reader) - профиль пользователя.

- BookEditors - название редакции, описание.
- Book - название, автор, дата выпуска, кол-во страниц, isbn, описание, averageRaiting.
- Review(отзыв) - userId, оценка, комментарий, рекомендут/не рекомендует
- Author - фио, биография.
- Genres - название жанра, возрастное ограничение.

- UserCollection - userId, collectionId, публичная/приватная.
- Collection - userId, название колекции. 
- BookCollection - collectionId, bookId.

## эндпоинты:
- post /auth/login
- post /auth/register
- post /auth/refresh

- get /api/users

- get /api/users/{id}/profile

- get /api/users/{id}/reviews - получить отзывы пользователя
- post /api/users/{id}/reviews - создать новый отзыв (book_id, comment)

- get /api/users/{id}/collections - получить коллекции пользователя
- post /api/users/{id}/collections - добавить коллекцию (название коллекции)

# для админа
- /api/books - посмотреть, добавить, все книги.
- /api/books/{id} - удалить, изменить книгу.

- /api/authors - посмотреть, добавить, всех авторов.
- /api/authors/{id} - удалить, изменить автора.
