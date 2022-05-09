# Pressure

**Route** : `/api/pressure/`

**Authorization** : GET requests do not require authorization. All other requests require an admin API key as a request parametre; otherwise they return the HTTP status code `401 Unauthorized`.

## Get all

Returns all the Pressures in the database.

**URL** : `/api/pressure/`

**Method** : `GET`

**Auth required** : No

### Success Response

**HTTP status code** : `200 OK`

**Content example** :

A: There is no data in the database; an empty list is returned.

```json
{
    "$id": "1",
    "$values": []
}
```

B: There is data in the database; a list containing Pressures is returned.

```json
{
    "$id": "1",
    "$values": [
        {
            "$id": "2",
            "id": 3,
            "average": 761.1285,
            "minimum": 756.1563,
            "maximum": 761.1285,
            "solId": 3
        },
        {
            "$id": "3",
            "id": 4,
            "average": 770.7514,
            "minimum": 747.8796,
            "maximum": 770.7514,
            "solId": 4
        },
        {
            "$id": "4",
            "id": 5,
            "average": 752.0035,
            "minimum": 749.0634,
            "maximum": 752.0035,
            "solId": 5
        },
        {
            "$id": "5",
            "id": 6,
            "average": 774.9528,
            "minimum": 746.2127,
            "maximum": 774.9528,
            "solId": 6
        },
        {
            "$id": "6",
            "id": 7,
            "average": 775.4021,
            "minimum": 741.2166,
            "maximum": 775.4021,
            "solId": 7
        },
        {
            "$id": "7",
            "id": 8,
            "average": 766.8261,
            "minimum": 758.8106,
            "maximum": 766.8261,
            "solId": 8
        },
        {...}
    ]
}
```

## Get pressure by id

Returns a specific pressure by id.

**URL** : `/api/pressure/{id}`

**Method** : `GET`

**Auth required** : No

### Success Response

**Condition** : Pressure with the given id exists.

**HTTP status code** : `200 OK`

**Content example** :

Returns the requested Pressure.

```json
{
    "$id": "1",
    "$values": [
        {
            "$id": "2",
            "id": 3,
            "average": 761.1285,
            "minimum": 756.1563,
            "maximum": 761.1285,
            "solId": 3
        }
    ]
}
```

### Error Response

**Condition** : Pressure with the given id does not exist.

**HTTP status code** : `404 Not Found`

## Post

Adds a new Pressure. Mandadory attributes left out from the request body or attributes of the wrong type cause an error.

**URL** : `/api/pressure/`

**Method** : `POST`

**Auth required** : Yes

**Request body example** :

```json
{    
    "average": 761.1285,
    "minimum": 756.1563,
    "maximum": 761.1285,
    "solId": 3    
}
```

### Success Response

**Condition** : Pressure was created successfully.

**HTTP status code** : `201 Created`

**Content example** :

Returns the created Pressure.

```json
{
    "$id": "1",
    "id": 41,
    "average": 761.1285,
    "minimum": 756.1563,
    "maximum": 761.1285,
    "solId": 3
}
```

### Error Response

A:

**Condition** : Type error in the request body, e.g. an attribute is a String when an Integer was expected.

**HTTP status code** : `400 Bad Request`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-958858bdca24d69850c33588fae23b77-60ef2feea4f5458f-00",
    "errors": {
        "$.solId": [
            "The JSON value could not be converted to System.Int32. Path: $.solId | LineNumber: 5 | BytePositionInLine: 18."
        ]
    }
}
```

## Put

Modifies an existing Pressure by id. Mandadory attributes left out from the request body or attributes of the wrong type cause an error.

**URL** : `/api/pressure/{id}`

**Method** : `PUT`

**Auth required** : Yes

**Request body example** : Sent to `/api/pressure/42`

```json
{
    "id": 42,
    "average": 761.1234,
    "minimum": 756.5678,
    "maximum": 761.9876,
    "solId": 3    
}
```

### Success Response

**Condition** : Pressure with the given id exists, modified successfully.

**HTTP status code** : `204 No Content`

**Content example** :

```json
{
    "$id": "1",
    "$values": [
        {
            "$id": "2",
            "id": 42,
            "average": 761.1234,
            "minimum": 756.5678,
            "maximum": 761.9876,
            "solId": 3
        }
    ]
}
```

### Error Response

A:

**Condition** : Pressure with the given id does not exist.

**HTTP status code** : `404 Not Found`

B:

**Condition** : Type error in the request body, e.g. an attribute is missing.

**HTTP status code** : `400 Bad Request`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "Bad Request",
    "status": 400,
    "traceId": "00-4739a94b899e7bdc14f6a9c55e9790b3-e94c1942445ccbb1-00"
}
```

## Delete

Deletes a specific Pressure by id.

**URL** : `/api/pressure/{id}`

**Method** : `DELETE`

**Auth required** : Yes

### Success Response

**Condition** : Pressure with the given id exists, deletion successful.

**HTTP status code** : `204 No Content`

### Error Response

**Condition** : Pressure with the given id does not exist.

**HTTP status code** : `404 Not Found`
