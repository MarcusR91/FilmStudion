## Guide till mitt API

Starta i VsCode: Dotnet watch run

Länk: https://localhost:5001/swagger/index.html

Om du vill använda swagger UI för att testa mitt api

**Kräver admin**

PUT: Skapar och lägger till ny film
/api/Film/create

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

PATCH: Ändrar vald property till en film
/api/Film/{filmId}

```
[
  {
    "path": "Name", // Vilken propertysom ska ändras, ex "Name"
    "op": "Replace", // Anvnds replace för att byta nuvarande Name till ett annat
    "Value": "Sagan om ringen" // Det nya värdet som propertyn får
  }
]
```

PUT: Ändrar information till specifik film
/api/Film/{filmId}

Denna behöver du skriva in Id både för filmen och filmcopyId för att slippa dubletter. Använd denna för att ändra tillgängliga exemplar.

```
{
  "name": "string",
  "releaseDate": "2022-02-11T19:29:39.354Z",
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

**Kräver ej admin**

GET: /api/Film/{id}

Hämta specifik film

GET: /api/Film/GetAllFilms

 Använd för att hämta alla filmer.


**FilmStudios**

POST: /api/FilmStudio/register
Registrera en filmstudio.

```
{
  "city": "string",
  "name": "string",
  "password": "string"
}
```

GET:/api/FilmStudio/Getstudios

Hämtar alla studios.

GET: /api/FilmStudio/{studioId}

Hämtar specifik filmstudio

**Users**

POST: /api/Users/Authenticate

```
{
    // Om du voll logga in som studio
  "studioName": "string",
  "studioPassword": "string"

    // Om du vill logga in som admin
  "userName": "string",
  "userPassword": "string"
}
```

POST: /api/Users/register

Registrera dig som admin

```
{
  "userName": "string",
  "password": "string"
}
```

GET: /api/Users/Getusers

Om du vill hämta d eadmin användare som finns
