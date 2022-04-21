# Sol

**Route** : `/api/sol/`

## Get all

Returns all the Sols in the database.

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

## Get the last seven Sols

Returns the last seven Sols. Always returns a list of seven items; if there are less than seven Sols in the database, the list contains null values in their place.

**URL** : `/api/sol/solweek`

**Method** : `GET`

**Auth required** : No

### Success Response

**HTTP status code** : `200 OK`

**Content example** :

```json
[
    {
        "$id": "1",
        "id": 315,
        "start": "2035-02-12T08:43:34",
        "end": "2035-02-13T09:23:09",
        "season": "Winter",
        "solNumber": 4002,
        "wind": {
            "$id": "2",
            "id": 314,
            "average": 530.6,
            "minimum": 2220.3,
            "maximum": 8740.9,
            "mostCommonDirection": "SW",
            "solId": 315
        },
        "pressure": {
            "$id": "3",
            "id": 315,
            "average": 50.6,
            "minimum": 50.3,
            "maximum": 50.9,
            "solId": 315
        },
        "temperature": {
            "$id": "4",
            "id": 314,
            "average": 130.6,
            "minimum": 5120.3,
            "maximum": 240.9,
            "solId": 315
        }
    },
    null,
    {...}
]
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

## Get Sols by date range

Returns the Sols that fall within the requested date range. The start and end dates are mandatory and given as request parametres in UTC format (the time is optional; if left out, it defaults to 00:00:00). For example, the following request returns all the Sols from the year 2011:

api/sol/date?start=2011-01-01T00:00:00&end=2011-12-31T23:59:59

**URL** : `/api/sol/date`

**Method** : `GET`

**Auth required** : No

### Success Response

**Condition** : Both the start and end date are given, and both are valid as DateTime type.

**HTTP status code** : `200 OK`

**Content example** :

A: No Sols match the parametres; an empty list is returned

```json
{
    "$id": "1",
    "$values": []
}
```

B: There are Sols that match the parametres; a list containing the matching Sols is returned.
```json
{
    "$id": "1",
    "$values": [
        {
            "$id": "2",
            "id": 277,
            "start": "2011-01-27T21:27:05.425",
            "end": "2011-01-28T22:06:40.425",
            "season": "winter",
            "solNumber": 101,
            "wind": {
                "$id": "3",
                "id": 276,
                "average": 8.675,
                "minimum": 4.453,
                "maximum": 15.203,
                "mostCommonDirection": "WSW",
                "solId": 277
            },
            "pressure": {
                "$id": "4",
                "id": 277,
                "average": 752.6169,
                "minimum": 753.4258,
                "maximum": 752.6169,
                "solId": 277
            },
            "temperature": {
                "$id": "5",
                "id": 276,
                "average": -101.598,
                "minimum": -76.022,
                "maximum": -14.041,
                "solId": 277
            }
        },
        {
            "$id": "6",
            "id": 278,
            "start": "2011-01-03T21:52:52.293",
            "end": "2011-01-04T22:32:27.293",
            "season": "winter",
            "solNumber": 102,
            "wind": {
                "$id": "7",
                "id": 277,
                "average": 17.441,
                "minimum": 8.088,
                "maximum": 13.044,
                "mostCommonDirection": "SE",
                "solId": 278
            },
            "pressure": {
                "$id": "8",
                "id": 278,
                "average": 761.1285,
                "minimum": 756.1563,
                "maximum": 761.1285,
                "solId": 278
            },
            "temperature": {
                "$id": "9",
                "id": 277,
                "average": -82.155,
                "minimum": -149.209,
                "maximum": -27.772,
                "solId": 278
            }
        },
        {...}
    ]
}
```

### Error Response

A: 

**Condition** : The start or end parameter (or both) are missing.

**HTTP status code** : `400 Bad Request`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-9314353ec67b14145bd430e2c7ad5692-7daf1f25369917c4-00",
    "errors": {
        "end": [
            "The end field is required."
        ],
        "start": [
            "The start field is required."
        ]
    }
}
```

B: 

**Condition** : The start or end parametres are not of a valid type, e.g. a string instead of a date. For example, the following request will return an error:

api/sol/date?start=abc&end=xyz

**HTTP status code** : `400 Bad Request`

**Content example** :

```json
{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "00-c62b4ff23a0db2a617bf138120891949-16d7842e276eeec6-00",
    "errors": {
        "end": [
            "The value 'xyz' is not valid."
        ],
        "start": [
            "The value 'abc' is not valid."
        ]
    }
}
```

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
    "Start": "2035-02-12T08:43:34Z",
    "End": "2035-02-13T09:23:09Z",
    "Season": "Winter",
    "SolNumber": 4000
}
```

### Success Response

**Condition** : Sol was created successfully.

**HTTP status code** : `201 Created`

**Content example** :

Returns the created Sol.

```json
{
    "$id": "1",
    "id": 312,
    "wind": {
        "$id": "2",
        "id": 311,
        "average": 530.6,
        "minimum": 2220.3,
        "maximum": 8740.9,
        "mostCommonDirection": "SW",
        "solId": 312
    },
    "temperature": {
        "$id": "3",
        "id": 311,
        "average": 130.6,
        "minimum": 5120.3,
        "maximum": 240.9,
        "solId": 312
    },
    "pressure": {
        "$id": "4",
        "id": 312,
        "average": 50.6,
        "minimum": 50.3,
        "maximum": 50.9,
        "solId": 312
    },
    "start": "2035-02-12T08:43:34Z",
    "end": "2035-02-13T09:23:09Z",
    "season": "Winter",
    "solNumber": 4000
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
