<h1 align="center">Проект по летней практике</h1>
<h2>Запуск проекта</h2>
Запустите контейнер образа PostgreSQl:

```docker run --name psql -e POSTGRES_PASSWORD=mysecretpassword -d postgres```

Изменить строку подключения в соответствии с контейнером.

<h2>Функциональные требования</h2>

<h3>Регистрация и авторизация пользователей</h3>

Возможность регистрации новых пользователей

Возможность авторизации зарегистрированных пользователей

Проверка уникальности электронной почты при регистрации

Подтверждение электронной почты пользователя

<h3>Управление тестами</h3>

Возможность просмотра доступных тестов

Возможность создания собственных тестов

Просмотр результата пройденного теста

<h3>Создание тестов</h3>

Указание наименования тестирования

Указание описания тестирования

Возможность указания даты завершения доступа к тестированию или
времени на прохождение теста

<h3>Поддержка различных типов вопросов</h3>

Вопросы с множественным выбором

Вопросы с единственно верным вариантом
