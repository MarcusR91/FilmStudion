## Guide till mitt API

**Kräver admin**

PUT: Skapar och lägger till ny film

```
{
  "name": "string",
  "releaseDate": "2022-02-11T19:20:56.116Z",
  "country": "string",
  "director": "string",
  "filmCopies": [
    {
      "filmCopyId": 0,
      "filmId": 0,
      "rentedOut": true,
      "filmStudioId": 0,
      "numberOfCopies": 0
    }
  ]
}
```