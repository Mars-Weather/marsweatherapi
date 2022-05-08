# Wind

**Route** : `/api/wind/`

**Authorization** : GET requests do not require authorization. All other requests require an admin API key as a request parametre; otherwise they return the HTTP status code `401 Unauthorized`.

## Get all

Returns all Wind objects in the databasee.

**URL** : `/api/wind/`

**Method** : `GET`

**Auth required** : No

### Success Response

**HTTP status code** : `200 OK`

**Content example** : 

A: There is no data in the database, an empty list is returned.

```json
{
    "$id": "1",
    "$values": []
}
```

B: There is data in the database; a list containing Wind objects is returned.

```json
{
    "$id": "1",
    "$values": [
        {
            "$id": "2",
            "id": 3,
            "average": 17.441,
            "minimum": 8.088,
            "maximum": 13.044,
            "mostCommonDirection": "SE",
            "solId": 3
        },
        {
            "$id": "3",
            "id": 4,
            "average": 17.853,
            "minimum": 2.371,
            "maximum": 14.828,
            "mostCommonDirection": "NNW",
            "solId": 4
        },
        {
            "$id": "4",
            "id": 5,
            "average": 16.634,
            "minimum": 8.755,
            "maximum": 15.377,
            "mostCommonDirection": "SSW",
            "solId": 5
        },
        {...}
    ]
```

## Get one by id

Returns a specific Wind object by id.

**URL** : `/api/wind/{id}`

**Method** : `GET`

**Auth required*** : No

### Success Response

**Condition** : A Wind object with the given id exists.

**HTTP status code** : `200 OK`

**Content example** :

Returns requested Wind object.

```json
{
    "$id": "1",
    "$values": [
        {
            "$id": "2",
            "id": 5,
            "average": 16.634,
            "minimum": 8.755,
            "maximum": 15.377,
            "mostCommonDirection": "SSW",
            "solId": 5
        }
    ]
}
```

### Error Response

**Condition** : A Wind object with the given id does not exist.

**HTTP status code**: `404 Not Found`

## Post

Adds a new Wind object. Non-mandatory attributes left out from the request body default to null. Mandadory attributes left out from the request body or attributes of the wrong type cause an error.

**URL** : `/api/wind/`

**Method** : `POST`

**Auth required** : Yes

**Request body example** :

```json
{
"average": 530.6,
"minimum": 2220.3,
"maximum": 8740.9,
"mostCommonDirection": "SW",
"solId": 3
}
```

### Success Response

**Condition** : The Wind object was created successfully.

**HTTP status code**: `201 Created`

**Content example** :

Returns the created Wind object.

```json
{
    "$id": "1",
    "id": 41,
    "average": 530.6,
    "minimum": 2220.3,
    "maximum": 8740.9,
    "mostCommonDirection": "SW",
    "solId": 3
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
    "traceId": "00-8f9372baf4ac692439b4878b60f885cc-0d05467f69790277-00",
    "errors": {
        "$.average": [
            "The JSON value could not be converted to System.Nullable`1[System.Single]. Path: $.average | LineNumber: 1 | BytePositionInLine: 15."
        ]
    }
}
```

## Put

Modifies an existing Wind object specified by id. Non-mandatory attributes left out from the request body default to 0 or null.

**URL** : `/api/wind/{id}`

**Method** : `PUT`

**Auth required** : Yes

**Request body example** : Sent to `/api/wind/134`

```json
{
"average": 530.6,
"minimum": 2220.3,
"maximum": 8740.9,
"mostCommonDirection": "WSW",
"solId": 3
}
```

### Success Response

**Condition** : A Wind object with the given id exists, modified successfully.

**HTTP status code** : `204 No Content`

A

**Condition** : A Wind object with the given id does not exist.

**HTTP status code** : `404 Not Found`

B

**Condition** : Type error in the request body, e.g. an attribute is a Boolean when a String was expected.

**HTTP status code** : `400 Bad Request`

**Content example** : 

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-82e68f42d2e5d305235fbeeab3f78377-49b4850b95b6517a-00",
    "errors": {
        "$.average": [
            "The JSON value could not be converted to System.Nullable`1[System.Single]. Path: $.average | LineNumber: 2 | BytePositionInLine: 15."
        ]
    }
}
```

C

**Condition** : Missing attribute in the request body.

**HTTP status code** : `400 Bad Request`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "Bad Request",
    "status": 400,
    "traceId": "00-70a97450ab5f1bcbea7d55b23282ada4-1a721e3d871d6553-00"
}
```

## Delete

Deletes a specific Wind object by id.

**URL** : `/api/wind/{id}`

**Method** : `DELETE`

**Auth required** : Yes

### Success Response

**Condition** : A Wind object with the given id exists, deletion successful.

**HTTP status code** : `204 No Content`

### Error Response

**Condition** : A Wind object with the given id does not exist.

**HTTP status code** : `404 Not Found`
