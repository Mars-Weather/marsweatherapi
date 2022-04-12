# Sol

**Route** : `/api/sol/`

## Get all

Returns a list of all Sols.

**URL** : `/api/sol/`

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

B: There is data in the database; a list containing Sols is returned.

```json
{
    "$id": "1",
    "$values": [
        {
            "$id": "2",
            "id": 1,
            "start": "2011-01-27T12:02:41",
            "end": "2011-01-28T12:42:16",
            "season": "winter",
            "solNumber": 100,
            "wind": {
                "$id": "3",
                "id": 1,
                "average": 3.165,
                "minimum": 5.712,
                "maximum": 14.807,
                "mostCommonDirection": "NNW",
                "solId": 1
            },
            "pressure": {
                "$id": "4",
                "id": 1,
                "average": 773.3485,
                "minimum": 745.2713,
                "maximum": 773.3485,
                "solId": 1
            },
            "temperature": {
                "$id": "5",
                "id": 1,
                "average": -148.618,
                "minimum": -50.991,
                "maximum": -24.596,
                "solId": 1
            }
        },
        {...}
    ]
}
```

## Get one by id

Returns a specific Sol by id.

**URL** : `/api/sol/{id}`

**Method** : `GET`

**Auth required** : No

### Success Response

**Condition** : Sol with the given id exists.

**HTTP status code** : `200 OK`

**Content example** :

Returns the requested Sol.

```json
{
    "$id": "1",
    "$values": [
        {
            "$id": "2",
            "id": 1,
            "start": "2011-01-27T12:02:41",
            "end": "2011-01-28T12:42:16",
            "season": "winter",
            "solNumber": 100,
            "wind": {
                "$id": "3",
                "id": 1,
                "average": 3.165,
                "minimum": 5.712,
                "maximum": 14.807,
                "mostCommonDirection": "NNW",
                "solId": 1
            },
            "pressure": {
                "$id": "4",
                "id": 1,
                "average": 773.3485,
                "minimum": 745.2713,
                "maximum": 773.3485,
                "solId": 1
            },
            "temperature": {
                "$id": "5",
                "id": 1,
                "average": -148.618,
                "minimum": -50.991,
                "maximum": -24.596,
                "solId": 1
            }
        }
    ]
}
```

### Error Response

**Condition** : Sol with the given id does not exist.

**HTTP status code** : `404 Not Found`

## Get one by Sol number

Returns a specific Sol by Sol number.

**URL** : `/api/sol/solnumber/{id}`

**Method** : `GET`

**Auth required** : No

### Success Response

**Condition** : Sol with the given Sol number exists.

**HTTP status code** : `200 OK`

**Content example** :

Returns the requested Sol.

```json
{
    "$id": "1",
    "$values": [
        {
            "$id": "2",
            "id": 11,
            "start": "2012-04-06T13:36:51",
            "end": "2012-04-07T14:16:26",
            "season": "spring",
            "solNumber": 110,
            "wind": {
                "$id": "3",
                "id": 11,
                "average": 6.612,
                "minimum": 1.238,
                "maximum": 17.547,
                "mostCommonDirection": "S",
                "solId": 11
            },
            "pressure": {
                "$id": "4",
                "id": 11,
                "average": 773.5701,
                "minimum": 756.7232,
                "maximum": 773.5701,
                "solId": 11
            },
            "temperature": {
                "$id": "5",
                "id": 11,
                "average": 4.528,
                "minimum": -83.552,
                "maximum": -0.681,
                "solId": 11
            }
        }
    ]
}
```

### Error Response

**Condition** : Sol with the given id does not exist.

**HTTP status code** : `404 Not Found`

## Post

Adds a new Sol. Non-mandatory attributes left out from the request body default to 0 (or in case of a date, to 0001-01-01T00:00:00). Mandadory attributes left out from the request body or attributes of the wrong type cause an error.

**URL** : `/api/sol/`

**Method** : `POST`

**Auth required** : Yes

**Request body example** :

```json
{
    "Wind": {
        "Average": 530.6,
        "Minimum": 2220.3,
        "Maximum": 8740.9,
        "mostCommonDirection": "SW"
    },
    "Temperature": {
        "Average": 130.6,
        "Minimum": 5120.3,
        "Maximum": 240.9
    },
    "Pressure": {
        "Average": 50.6,
        "Minimum": 50.3,
        "Maximum": 50.9
    },
    "Start": "2030-02-12T08:43:34Z",
    "End": "2030-02-13T09:23:09Z",
    "Season": "Winter",
    "SolNumber": 5000
}
```

### Success Response KESKEN

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

Modifies an existing Sol by id. Non-mandatory attributes left out from the request body default to 0 (or in case of a date, to 0001-01-01T00:00:00). Mandadory attributes left out from the request body or attributes of the wrong type cause an error.

**URL** : `/api/sol/{id}`

**Method** : `PUT`

**Auth required** : Yes

**Request body example** : Sent to `/api/sol/276`

```json
{
    "Id": 276,
    "Wind": {
        "Average": 530.6,
        "Minimum": 2220.3,
        "Maximum": 8740.9,
        "mostCommonDirection": "SW"
    },
    "Temperature": {
        "Average": 130.6,
        "Minimum": 5120.3,
        "Maximum": 240.9
    },
    "Pressure": {
        "Average": 50.6,
        "Minimum": 50.3,
        "Maximum": 50.9
    },
    "Start": "2025-02-12T08:43:34Z",
    "End": "2025-02-13T09:23:09Z",
    "Season": "Winter",
    "SolNumber": 2000
}
```

### Success Response

**Condition** : Sol with the given id exists, modified successfully.

**HTTP status code** : `204 No Content`

**Content example** :

```json
{
    "$id": "1",
    "id": 276,
    "wind": {
        "$id": "2",
        "id": 0,
        "average": 530.6,
        "minimum": 2220.3,
        "maximum": 8740.9,
        "mostCommonDirection": "SW",
        "solId": 0
    },
    "temperature": {
        "$id": "3",
        "id": 0,
        "average": 130.6,
        "minimum": 5120.3,
        "maximum": 240.9,
        "solId": 0
    },
    "pressure": {
        "$id": "4",
        "id": 0,
        "average": 50.6,
        "minimum": 50.3,
        "maximum": 50.9,
        "solId": 0
    },
    "start": "2025-02-12T08:43:34Z",
    "end": "2025-02-13T09:23:09Z",
    "season": "Modified season",
    "solNumber": 2000
}
```

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

**Auth required** : Yes

### Success Response

**Condition** : Sol with the given id exists, deletion successful.

**HTTP status code** : `204 No Content`

### Error Response

**Condition** : Sol with the given id does not exist.

**HTTP status code** : `404 Not Found`
