# Sol

**Route** : `/api/sol/`

## Get all

Returns a list of all Sols.

**URL** : `/api/sol/`

**Method** : `GET`

**Auth required** : NO

### Success Response

**HTTP status code** : `200 OK`

**Content example** :

A: There is no data in the database; an empty list is returned.
```json
{
    []
}
```

B: There is data in the database; a list containing Sols is returned.

```json
[
    {
        "id": 1,
        "wind": null,
        "temperature": null,
        "pressure": null,
        "start": "2022-08-20T08:43:34Z",
        "end": "2022-08-21T09:23:09Z",
        "season": "Spring",
        "solNumber": 1
    },
    {
        "id": 2,
        "wind": null,
        "temperature": null,
        "pressure": null,
        "start": "2022-08-20T08:43:34Z",
        "end": "2022-08-21T09:23:09Z",
        "season": "Spring",
        "solNumber": 2
    }
]
```

## Get one by id

Returns a specific Sol by id.

**URL** : `/api/sol/{id}`

**Method** : `GET`

**Auth required** : NO

### Success Response

**Condition** : Sol with the given id exists.

**HTTP status code** : `200 OK`

**Content example** :

Returns the requested Sol.

```json
{
    "id": 1,
    "wind": null,
    "temperature": null,
    "pressure": null,
    "start": "2022-08-20T08:43:34Z",
    "end": "2022-08-21T09:23:09Z",
    "season": "Spring",
    "solNumber": 1
}
```

### Error Response

**Condition** : Sol with the given id does not exist.

**HTTP status code** : `404 Not Found`

## Post

Adds a new Sol. Non-mandatory attributes left out from the request body default to 0 (or in case of a date, to 0001-01-01T00:00:00). Mandadory attributes left out from the request body cause an error.

**URL** : `/api/sol/`

**Method** : `POST`

**Auth required** : YES

**Request body example** :

```json
{
    "Wind": {
        "Average": 10.6,
        "Minimum": 10.3,
        "Maximum": 10.9,
        "mostCommonDirection": "N"
    },
    "Temperature": {
        "Average": 130.6,
        "Minimum": 10.3,
        "Maximum": 10.9
    },
    "Pressure": {
        "Average": 10.6,
        "Minimum": 20.3,
        "Maximum": 40.9
    },
    "Start": "2022-08-20T08:43:34Z",
    "End": "2022-08-21T09:23:09Z",
    "Season": "Autumn",
    "SolNumber": 2
}
```

### Success Response

**Condition** : Sol was created successfully.

**HTTP status code** : `201 Created`

**Content example** :

Returns the created Sol.

```json
{
    "id": 2,
    "wind": {
        "id": 2,
        "average": 10.6,
        "minimum": 10.3,
        "maximum": 10.9,
        "mostCommonDirection": "N"
    },
    "temperature": {
        "id": 2,
        "average": 130.6,
        "minimum": 10.3,
        "maximum": 10.9,
        "sol_Id": 0
    },
    "pressure": {
        "id": 2,
        "average": 10.6,
        "minimum": 20.3,
        "maximum": 40.9,
        "sol_Id": 0
    },
    "start": "2022-08-20T08:43:34Z",
    "end": "2022-08-21T09:23:09Z",
    "season": "Autumn",
    "solNumber": 2
}
```

### Error Response

A

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

B

**Condition** : Mandatory attribute(s) missing from the request body.

**HTTP status code** : `400 Bad Request`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-07564e8edea7239a66e4073f3be314ec-78a5131a10535ea2-00",
    "errors": {
        "Wind": [
            "The Wind field is required."
        ],
        "Pressure": [
            "The Pressure field is required."
        ],
        "Temperature": [
            "The Temperature field is required."
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
    "Id": 2,
    "Wind": {
        "Average": 130.6,
        "Minimum": 120.3,
        "Maximum": 140.9,
        "mostCommonDirection": "SW",
        "Sol_id" : 2
    },
    "Temperature": {
        "Average": 130.6,
        "Minimum": 120.3,
        "Maximum": 140.9,
        "Sol_id" : 2
    },
    "Pressure": {
        "Average": 130.6,
        "Minimum": 120.3,
        "Maximum": 140.9,
        "Sol_id" : 2
    },
    "Start": "2022-08-20T08:43:34Z",
    "End": "2022-08-21T09:23:09Z",
    "Season": "Winter",
    "SolNumber": 2
}
```

### Success Response

**Condition** : Sol with the given id exists, modified successfully.

**HTTP status code** : `204 No Content`

### Error Response

A

**Condition** : Sol with the given id does not exist.

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
    "traceId": "00-673d978370b7ff12f36c40b18faab7b9-bd87bfbdff0ce12e-00",
    "errors": {
        "$.Season": [
            "The JSON value could not be converted to System.String. Path: $.Season | LineNumber: 3 | BytePositionInLine: 18."
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
    "traceId": "00-b08f3e8ce2e3fb20bfef4490f1097331-70424ffbe7db0635-00"
}
```

## Delete

Deletes a specific Sol by id.

**URL** : `/api/sol/{id}`

**Method** : `DELETE`

**Auth required** : No

### Success Response

**Condition** : Sol with the given id exists, deletion successful.

**HTTP status code** : `204 No Content`

### Error Response

**Condition** : Sol with the given id does not exist.

**HTTP status code** : `404 Not Found`
