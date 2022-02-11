## REST

**Registrering**

För att lösa kravet med registrering används i mitt API två olika åtkomstpunkter för de olika användarna som ska finnas.
För att en admin ska registrerea sig så används här en DTO som ärver från interface:t ```IUserRegister```. Samma Dto mappas även ihop 
med domänklasssen ```User.cs```. ```UserRegisterDto.cs``` skickas sedan in som parameter till min åtkomstpunkt och skickar därmed in nödvändig data 
som behövs för att en admin ska kunna registrera sig korrekt.  Till åtkomstpunkten används även crud funktioner från ```UserRepositories``` där den ena mnetoden
kollar ifall en användare med samma namn och lösenord finns och den andra skapar helt eneklt den nya användaren. I metoden ```Register()``` har jag också gjort så att en när en 
användare från denna åtkomnstpunkt registreras får den användarewn automatiskt rollen som admin. Den nyskapade användaren returneras sedan som ett anonymt objekt och i response-bodyn
visas uppgiferna för den nyskapade användaren, dock så har lösenordet valts att exkludera i responsen. 

För att en filmstudioska kunna registrera sig används samma tillvägagångs sätt som för en admin. Den enda skillnaden är att jag använder en ny Dto som representerar informationen 
för en filmstudio istället. ```RegisterFilmStudioDto.cs``` ärver från ```IRegisterFilmstudio.cs``` som mappas ihop med ```FilmStudio.cs```. Här används också samma crud funktioner som för
en admin användare fast istället får en ny filmstudio som registrerar sig rollen som "filmstudio" och i åtkomstpunkten returneras ett anonymt objekt som uppfyller interface:t ```IFilmStudio.cs```. 

Anledningen till att det används så många Dto's är för att jag inte vill exponera mina domänklasser/enititer till mina åtkomstpunkter. Det ger mig också en viss flexibilitet där jag kan
välja vilka properties som ska finnas med och synas i mitt api. 

**Autentisering**

Till autentiseringen så har jag gjort så att samma åtkomstpunkt används för att kunna autentisera både en filmstudio och en admin. Här används en ```userDto``` som skickas in i parameterna för 
kontrollen, och för att kunna få med både users och filmstudios så ärver den även i från ```FilmStudioDto``` och en mappning sker mellan entiteten ```User``` och klassen ```userDto```. 
I åtkomstpunkten används sedan en metod som skapar en ny claim åt användaren och en JWT token genereras och sedan returneras användaren o bodyn tillsammans med nyckeln som kan använas för
att logga in som antingen admin eller filmstudio. 

Den dto som används till denna åtkomstpunkt används för att rätt information ska visas i bodyn för anropet, som i detta fallet är ett användarnamn och ett lösenord. 

**Filmer**

För att skapa en ny film och lägga till den i databasen finns åtkomstpunkten ```create```. Här används ```CreateFilmDto``` som ärver ifrån ```ICreateFilm``` som innehåller den nödvändiga datan som
behövs för att lägga till en ny film. Denna dto skickas in i parametern för åtkomstpunkten sedan sker en mappning mellan enititen ```Film``` och dto'n. Därfer returneras sedan Dto'n som inneåller den informationen som
användaren lagt till om filmen. 

Min åtkomstpunkt för att kunna hämta alla filmer använder sig av ```this.User```. Detta för att olika information ska visas beroende på vilken användare det är som är inne på api:et. Här har jag helt enkelt satt så att
om den nuvarande användaren har rollen som "admin" så används en ```LINQ SELECT statement``` som konverterar eniteten film till en ny FilmDto. Här i väljer jag sedan vilken information om filmen om ska visas. En admin får se all den information som en film innehåller. Samma metod används för om den nuvarande användaren är en filmstudio och om användaren är oautentiserad fast här har det valts att begränsas hur mycket information om en film 
som ska visas.

För att kunna gämta en specifik film så tar min åtkomstpunkt ```GetFilm()```emot en int som en parameter där Int representerar ett Id på den filmen som eftersöks. Här används en crud funktion som söker upp denfilmen som har det Id som användaren matar in så attfilmen kan hittas. Sedan används i åtkomstpunkten en vanlig if sats som kollar vilken användare somär inloggad. Om en anväändare är autentiserad som antingen admin eller filmstudio så
visas all information som finns i entiteten Film. Men om användaren är oautentiserad så visas i svaret allting föruom listan av FilmcCopies. Även här används ```This.User``` för att kolla vem som är inloggad.  
