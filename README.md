# **Рекомендую ознакомиться перед просмотром**  

## Описание

#### В проекте используется:

**Версия .NET: 9.0  
Платформа: ASP.NET Core**

**Пакеты:**

1. Microsoft.AspNetCore.OpenApi
2. Swashbuckle.AspNetCore
3. Swashbuckle.AspNetCore.SwaggerUI

**Пространства имен:**

1. [Test.API](src/Test.API/) - Эндпоинты, настройки запуска
2. [Test.Classes](src/Test.Classes/) - Классы и DTO
3. [Test.Core](src/Test.Core/) - Бизнес-логика, вспомогательные классы
4. [Test.Test](src/Test.Test/) - Модульные тесты


## Шаги запуска

### Клонировать репозиторий и перейти в папку

```bash
git clone https://github.com/droptrigger/TestTask.git
cd TestTask
```

## Вариант: Консоль

#### 1. Восстановить зависимости

``` bash
dotnet restore
```

#### 2. Запустить проект

``` bash
dotnet run --project src/Test.API
```

#### 3. Приложение после запуска доступно по адресу:

```
https://localhost:7050
```

#### 4. Swagger UI:

```
https://localhost:7050/swagger
```

## Вариант: IDE

#### 1. Запустить проект [Test.API](src/Test.API/)

#### 2. Автоматически откроется браузер по адресу `https://localhost:7050/swagger`, если нет - открыть вручную


## API

Базовый URL: `https://localhost:7050/api/fields/`

### Получить все поля

```
GET /api/fields/
```

### Описание
Возвращает список всех полей.

### Ответ

- Код 200 OK  
  JSON-массив объектов `Field`.

- Код 404 Not Found  
  Если поля не найдены.

### Пример ответа

```json
[
  {
    "id": 1,
    "name": "м01",
    "size": 1402209.12,
    "location": {
      "center": { "latitude": 41.3380610642585, "longitude": 45.6962567581079 },
      "polygon": [ 
        { "latitude": 41.3346809239899, "longitude": 45.7074047669366 },
        { "latitude": 41.3414148034017, "longitude": 45.707543073278 },
        { "latitude": 41.3414148034017, "longitude": 45.6850638023809 },
        { "latitude": 41.3347304378091, "longitude": 45.6849600309502 },
        { "latitude": 41.3346809239899, "longitude": 45.7074047669366 }
      ]
    }
  }
]
```

### Получить площадь поля по ID

```
GET /api/fields/size?id={id}
```

### Параметры запроса

| Параметр | Тип | Описание           |
| -------- | --- | ------------------ |
| id       | int | Идентификатор поля |

### Описание

Возвращает площадь поля с указанным ID.

### Ответ

* Код 200 OK
  Число — площадь поля в квадратных метрах.

* Код 404 Not Found
  Если поле с таким ID не найдено.

### Пример

```
GET /api/fields/size?id=1
```

Ответ:

```
1402209.12
```


## Получить расстояние от точки до центра поля

```
GET /api/fields/distance?lat={lat}&lng={lng}&id={id}
```

### Параметры запроса

| Параметр | Тип    | Обязательный | Описание               |
| -------- | ------ | ------------ | ---------------------- |
| lat      | double | Да           | Широта исходной точки  |
| lng      | double | Да           | Долгота исходной точки |
| id       | int    | Да           | Идентификатор поля     |

### Описание

Вычисляет расстояние (в метрах) от заданной точки до центра поля с указанным ID.

### Ответ

* Код 200 OK
  Число — расстояние в метрах.

* Код 404 Not Found
  Если поле с таким ID не найдено.

### Пример

```
GET /api/fields/distance?lat=41.3346809239899&lng=45.7074047669366&id=1
```

Ответ:

```
1003.77
```


## Проверить, находится ли точка внутри какого-либо поля

```
GET /api/fields/inside?latitude={latitude}&longitude={longitude}
```

### Параметры запроса

| Параметр  | Тип    | Описание      |
| --------- | ------ | ------------- |
| latitude  | double | Широта точки  |
| longitude | double | Долгота точки |

### Описание

Проверяет, находится ли точка внутри какого-либо поля.

### Ответ

* Код 200 OK
  Если точка **не внутри ни одного поля** — возвращает `false`.
  Если внутри поля — возвращает строку с ID и именем поля, например:
  `"id: 1, name: Поле 1"`

### Пример

```
GET /api/fields/inside?latitude=41.3380610642585&longitude=45.6962567581079
```

Ответ:

```
"id: 1, name: м01"
```

или

```
false
```

## Типы объектов

### Field

| Свойство | Тип      | Описание           |
| -------- | -------- | ------------------ |
| Id       | int      | Идентификатор поля |
| Name     | string   | Название поля      |
| Size     | double   | Площадь поля (м²)  |
| Location | Location | Локация поля       |

### Location

| Свойство | Тип         | Описание              |
| -------- | ----------- | --------------------- |
| Center   | Point       | Центр поля            |
| Polygon  | List<Point> | Контур поля (полигон) |

### Point

| Свойство  | Тип    | Описание |
| --------- | ------ | -------- |
| Latitude  | double | Широта   |
| Longitude | double | Долгота  |


# Примечания

* Все координаты заданы в десятичных градусах (WGS84).
* Для всех GET-запросов параметры передаются в URL.
* При ошибках возвращаются стандартные HTTP коды ошибок.



## Тесты

#### После запуска проекта можно проверить API через файл:

* [src.http](src/Test.API/src.http)

#### Тесты проверны так же на Google Maps

| Проверка                                     | Google Maps | Программа |
| -------------------------------------------- | ----------- | --------- |
| Площадь первого поля                         | <img width="442" height="281" alt="image" src="https://github.com/user-attachments/assets/34c4908f-b008-4262-9e6c-bca87364a9e6" /> | <img width="443" height="320" alt="image" src="https://github.com/user-attachments/assets/05d3a917-b148-4ff6-a2d3-686b7c755078" /> |
| Расстояние от центра до левого верхного края | <img width="725" height="439" alt="image" src="https://github.com/user-attachments/assets/9a69259a-898f-4145-97b8-50dde47fe1ef" /> | <img width="782" height="308" alt="image" src="https://github.com/user-attachments/assets/458b8110-5cde-4fe6-9542-627ba85eb311" /> |
| Принадлежность точки                         | <img width="596" height="537" alt="image" src="https://github.com/user-attachments/assets/3157f8ce-da6d-45e6-93c1-622c42c2a1f7" /> | <img width="663" height="300" alt="image" src="https://github.com/user-attachments/assets/2ef807dc-23f3-49b1-94e6-22ee1ad11fe3" /> |