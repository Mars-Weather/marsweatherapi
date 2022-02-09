# Sol

**Route** : `/api/sol/`

## Get

### Get all

Returns a list of all Sols.

**URL** : `/api/sol/`

**Method** : `GET`

**Auth required** : NO

### Success Response

**Code** : `200 OK`

**Content example** :

A: There is no data in the database; an empty list is returned.
```json
{
    []
}
```

B: There is data in the database; a list containing Sols is returned.

```json
{
xxx
}
```

### Get one

Returns a specific Sol by id.

**URL** : `/api/sol/{id}`

**Method** : `GET`

**Auth required** : NO

### Success Response

**Code** : `200 OK`

**Content example** :

Returns the requested Sol.

```json
{
xxx
}
```

### Error Response

**Condition** : Sol with the given id does not exist.

**HTTP status code** : `404 Not Found`

## Post

Adds a new Sol.

**URL** : `/api/sol/`

**Method** : `POST`

**Auth required** : YES

**Request body example** :

```json
{
xxx
}
```

### Success Response

**Code** : `201 Created`

**Content example** :

Returns the created Sol.

```json
{
xxx
}
```

### Error Response

**Condition** : Type error in the request body, e.g. an attribute is a Boolean when a String was expected.

**HTTP status code** : `400 Bad Request`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-673d978370b7ff12f36c40b18faab7b9-bd87bfbdff0ce12e-00",
    "errors": {
        "$.Season": [
            "The JSON value could not be converted to System.String. Path: $.Season | LineNumber: 3 | BytePositionInLine: 18."
        ]
    }
}
```

## Put

Modifies an existing Sol by id.

**URL** : `/api/sol/{id}`

**Method** : `PUT`

**Auth required** : YES

**Request body example** :

```json
{
xxx
}
```

### Success Response

**Code** : `204 No Content`

### Error Response

**Condition** : Type error in the request body, e.g. an attribute is a Boolean when a String was expected.

**HTTP status code** : `400 Bad Request`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-673d978370b7ff12f36c40b18faab7b9-bd87bfbdff0ce12e-00",
    "errors": {
        "$.Season": [
            "The JSON value could not be converted to System.String. Path: $.Season | LineNumber: 3 | BytePositionInLine: 18."
        ]
    }
}
```

**Condition** : Missing attribute in the request body.

**HTTP status code** : `400 Bad Request`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "Bad Request",
    "status": 400,
    "traceId": "00-b08f3e8ce2e3fb20bfef4490f1097331-70424ffbe7db0635-00"
}
```

## Delete

Deletes a specific Sol by id.

**URL** : `/api/sol/{id}`

**Method** : `DELETE`

**Auth required** : No

### Success Response

**Condition** : Sol with the given id exists.

**HTTP status code** : `204 No Content`

### Error Response

**Condition** : Sol with the given id does not exist.

**HTTP status code** : `404 Not Found`
