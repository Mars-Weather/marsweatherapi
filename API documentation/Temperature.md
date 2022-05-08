# Temperature

**Route** : `/api/Temperature/`

## Get all

Returns a list of all Temperatures.

**URL** : `/api/Temperature/`

**Method** : `GET`

**Auth required** : NO

### Success Response

**HTTP status code** : `200 OK`

**Content example** :

A: There is no data in the database; an empty list is returned.

```json
    []
```

B: There is data in the database; a list containing Temperatures is returned.

```json
[
    {
        "$id": "2",
        "id": 1,
        "average": -148.618,
        "minimum": -50.991,
        "maximum": -24.596,
        "solId": 1
    },
    {
        "$id": "3",
        "id": 3,
        "average": -82.155,
        "minimum": -149.209,
        "maximum": -27.772,
        "solId": 3
    }
]
```
## Get one by id

Returns a specific Temperature by id.

**URL** : `/api/Temperature/{id}`

**Method** : `GET`

**Auth required** : NO

### Success Response

**Condition** : Temperature with the given id exists.

**HTTP status code** : `200 OK`

**Content example** :

Returns requested Temperature.

```json
    {
        "$id": "2",
        "id": 1,
        "average": -148.618,
        "minimum": -50.991,
        "maximum": -24.596,
        "solId": 1                                
    }
```
### Error Response

**Condition** : Temperature with the given id does not exist.

**HTTP status code** : `404 Not Found`

## Post

Adds a new Temperature. Mandadory attributes left out from the request body cause an error.

**URL** : `/api/Temperature/`

**Method** : `POST`

**Auth required** : YES

**Request body example** :

```json
    {
        "id": 36,
        "average": -93.478,
        "minimum": -87.446,
        "maximum": -32.809
    }
```
### Success Response

**Condition** : Temperature was created successfully.

**HTTP status code** : `201 Created`

**Content example** :

Returns created Temperature.

```json
    {
        "$id": "36",
        "id": 36,
        "average": -93.478,
        "minimum": -87.446,
        "maximum": -32.809,
        "solId": 36                                
    }
```
### Error Response

A

**Condition** : Type error in the request body, e.g. an attribute is a String when a double value was expected.

**HTTP status code** : `400 Bad Request`

**Content example** :

```json
    {
        "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
        "title": "One or more validation errors occurred.",
        "status": 400,
        "traceId": "00-673d978370b7ff12f36c40b18faab7b9-bd87bfbdff0ce12e-00",
        "errors": {
            "$.average": [
                "The JSON value could not be converted to System.ToDouble. Path: $.average | LineNumber: 3 | BytePositionInLine: 18."
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
        "average": [
            "The average field is required."
        ],
        "solId": [
            "The solId field is required."
        ]
    }
}
```
## Put

Modifies an existing Temperature by id.

**URL** : `/api/Temperature/{id}`

**Method** : `PUT`

**Auth required** : YES

**Request body example** :

```json
    {
        "id": 36,
        "average": -90.5,
        "minimum": -75.6,
        "maximum": -30.9
    }
```

### Success Response

**Condition** : Temperature with the given id exists, modified successfully.

**HTTP status code** : `204 No Content`

### Error Response

A

**Condition** : Temperature with the given id does not exist.

**HTTP status code** : `404 Not Found`

B

**Condition** : Type error in the request body, e.g. an attribute is a String when a double value was expected.

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
            "The JSON value could not be converted to System.ToDouble. Path: $.average | LineNumber: 3 | BytePositionInLine: 18."
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
    "traceId": "00-5afe1bdb408bf72390180e4a7a352f5c-14ff4cd1983bdb06-00"
}
```

## Delete

Deletes a specific Temperature by id.

**URL** : `/api/Temperature/{id}`

**Method** : `DELETE`

**Auth required** : No

### Success Response

**Condition** : Temperature with the given id exists, deletion successful.

**HTTP status code** : `204 No Content`

### Error Response

**Condition** : Temperature with the given id does not exist.

**HTTP status code** : `404 Not Found`
