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

**Content example**

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

**Content example**

```json
{
xxx
}
```

### Error Response

**Condition** : Sol with the given id does not exist.

**HTTP status code** : `404 Not Found`

## Post



## Put


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
