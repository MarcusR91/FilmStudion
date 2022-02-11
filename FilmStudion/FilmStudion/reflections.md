## REST

**Registrering**

För att lösa kravet med registrering används i mitt API två olika åtkomstpunkter för de olika användarna som ska finnas.
För att en admin ska registrerea sig så används här en DTO som ärver från interface:t ```IUserRegister```. Samma Dto mappas även ihop 
med domänklasssen ```User.cs```. ```UserRegisterDto.cs``` skickas sedan in som parameter till min åtkomstpunkt och skickar därmed in nödvändig data 
som behövs för att en admin ska kunna registrera sig korrekt.  Till åtkomstpunkten används även crud funktioner från ```UserRepositories``` där den ena metoden
kollar ifall en användare med samma namn och lösenord finns och den andra skapar helt eneklt den nya användaren. I metoden ```Register()``` har jag också gjort så att en när en 
användare från denna åtkomnstpunkt registreras får den användaren automatiskt rollen som admin. Den nyskapade användaren returneras sedan som ett anonymt objekt och i response-bodyn
visas uppgifterna för den nyskapade användaren, dock så har lösenordet valts att exkluderas i responsen. 

För att en filmstudio ska kunna registrera sig används samma tillvägagångssätt som för en admin. Den enda skillnaden är att jag använder en ny Dto som representerar informationen 
för en filmstudio istället. ```RegisterFilmStudioDto.cs``` ärver från ```IRegisterFilmstudio.cs``` som mappas ihop med ```FilmStudio.cs```. Här används också samma crud funktioner som för
en admin användare fast istället får en ny filmstudio som registrerar sig rollen som "filmstudio" och i åtkomstpunkten returneras ett anonymt objekt som uppfyller interface:t ```IFilmStudio.cs```. 

Anledningen till att det används så många Dto's är för att jag inte vill exponera mina domänklasser/enititer till mina åtkomstpunkter. Det ger mig också en viss flexibilitet där jag kan
välja vilka properties som ska finnas med och synas i mitt api. 

**Autentisering**

Till autentiseringen så har jag gjort så att samma åtkomstpunkt används för att kunna autentisera både en filmstudio och en admin. Här används en ```userDto``` som skickas in i parameterna för 
kontrollen, och för att kunna få med både users och filmstudios så ärver den även i från ```FilmStudioDto``` och en mappning sker mellan entiteten ```User``` och klassen ```userDto```. 
I åtkomstpunkten används sedan en metod som skapar en ny claim åt användaren och en JWT token genereras och sedan returneras användaren o bodyn tillsammans med nyckeln som kan användas för
att logga in som antingen admin eller filmstudio. 

Den dto som används till denna åtkomstpunkt används för att rätt information ska visas i bodyn för anropet, som i detta fallet är ett användarnamn och ett lösenord. 

**Filmer**

För att skapa en ny film och lägga till den i databasen finns åtkomstpunkten ```create```. Här används ```CreateFilmDto``` som ärver ifrån ```ICreateFilm``` som innehåller den nödvändiga datan som
behövs för att lägga till en ny film. Denna dto skickas in i parametern för åtkomstpunkten sedan sker en mappning mellan enititen ```Film``` och dto'n. Därefter returneras sedan Dto'n som inneåller den informationen som
användaren lagt till om filmen. 

Min åtkomstpunkt för att kunna hämta alla filmer använder sig av ```this.User```. Detta för att olika information ska visas beroende på vilken användare det är som är inne på api:et. Här har jag helt enkelt satt så att
om den nuvarande användaren har rollen som "admin" så används en ```LINQ SELECT statement``` som konverterar entiteten film till en ny FilmDto. Här i väljer jag sedan vilken information om filmen som ska visas. En admin får se all den information som en film innehåller. Samma metod används för om den nuvarande användaren är en filmstudio och om användaren är oautentiserad fast här har det valts att begränsas hur mycket information om en film 
som ska visas.

För att kunna hämta en specifik film så tar min åtkomstpunkt ```GetFilm()``` emot en int som en parameter där Int representerar ett Id på den filmen som eftersöks. Här används en crud funktion som söker upp den filmen som har det Id som användaren matar in så att filmen kan hittas. Sedan används i åtkomstpunkten en vanlig if sats som kollar vilken användare som är inloggad. Om en användare är autentiserad som antingen admin eller filmstudio så visas all information som finns i entiteten Film. Men om användaren är oautentiserad så visas i svaret allting föruom listan av FilmcCopies. Även här används ```This.User``` för att kolla vem som är inloggad. 

Liknande tillvägagångssätt som ovan har använts för åtkomsten där en autentiserad admin kan uppdatera en film med ny information. Här har jag dock gett min åtkomstpunkt en ```Authorize``` med rollen som admin för att ingen annan ska kunna uppdatera en film. 

**Filmstudio**

Åtkomstpunkten för att hämta filmstudios används samma metoder som åtkomstpunkten för att hämta alla filmer där jag först kollar vilken användare det är som är inloggad. En autentiserad användare får mer information om en
studio än vad en oautentiserad användare får. 

Åtkomstpunkten för att hämta en specifik filmstudio händer det inte så mycket nytt. Precis som åtkomstpunkten för att hämta en specifik film skickas här in en int som parameter och som representerar det id på den
filmstudion som användaren söker efter. Sedan används if satser som kollar vilken användare som är inloggad. En admin får all den information som finns för en filmstudio. Sedan så har jag en if sats som kollar ifall den
filmstudion som är inloggad har samma id som den filmstudio som eftersöks får denne studion fram all information. Om ovanstående inte stämmer så får den studio som skickar anropet begränsad information. Detta för att inte
alla studios ska kunna se allting om alla andra. 

Till dessa åtkomstpunkter används även crud funktioner från mitt repository som hämtar data från databasen. 

## Implementation

Jag använder mig utav fyra stycken domänklasser: ```Film.cs```, ```FilmStudio.cs```, ```FiilmCopy.cs``` och ```User.cs```. Dessa klasser är de som är mina entiteter och används för att lagras i databasen. Varje entitet har även sina interfaces som uppfylls av sina repektive enititetsklasser. I dessa så lagras den nödvändiga datan som behövs för varje entitet. Exempelvis ```UserName``` i entiten ```User```. 

För de synliga resurserna som visas i api:et har det valts att användas Dto's där mappning sker mellan en Dto och sin respektive entitet. Exempelvis min ```UserDto``` innehåller bara ```UserName``` och ```Password```. Då denna resurs används för autentisering av en användare så behöver man inte mer än dessa två properties för att en registrering ska kunna göras. Mappningen mellan ```UserDto``` och ```User```gör så att den datan som skrivs in i anropet sedan sparas i entiteten som i sin tur lagras i databasen. 

Entitet ```FilmStudio``` har resurserna ```FilmStudioDto``` , som används för att lagra UserName och PassWord av en filmstudio, ```GetFilmStudioDto``` som ser likadan ut som entiteten eftersom att denna används för att hämta alla studios. ```GetFilmStudioLimitedDto``` som används för att hämta mer begränsad information om en filmstudio. Här har egenskaperna ```City``` och ```RentedFilmCopies``` exkluderats då denna dto används för att visa information till en användare som inte är autentiserad. Sedan har vi ```RegisterFilmStudioDto``` som bara innehåller egenskaperna ```City```, ```Name``` och ```password``` då denna används för registrering av en 
filmstudio. 

Entiten ```Film``` har de synliga resurserna: ```FilmDto``` som har alla properties från entiteten. Denna används för att kunna returnera filmerna i GET anrop. ```CreateFilmDto``` har även den samma properties som sin entitet. Denna används i skapandet av en film. ```UpdateFilmDto``` har även den samma properties, inklusive listan av filmcopies. Denna används för att en admin ska kunna ändra och uppdatera en existerande film samt ändra hur många exemplar som finns för filmen. 

Entitetn ```User``` har två stycken synliga resurser: ```UserDto```, denna har bara egenskaperna UserName och Password och används vid autentisering av en användare. ```UserRegisterDto``` har också bara dessa två egenskaper och används vid registrering av en användare.


## Säkerhet

Som säkerhetsåtgärder har jag använt mig mycket av autentiseringen. Mina åtkomstpunkter för att skapa ny film, uppdatera en existerande film och ändra antalet tillgängliga kopior av en film så krävs en administratörsroll. Detta görs med hjälp av ```[Authorize(Role = "admin")]```. Försöker en icke autentiserad användare göra detta så får de felmeddelandet "401".

Core Identity används för att säkerställa vilken användare som för tillfället är inloggad med hjälp av ```IsInRole()``` metoden på de åtkomstpunkter där en viss autentisering behövs, exempelvis åtkomstpunkten för att hämta en viss filmstudio. På detta sättet ser jag till så att inte fel information visas för fel användare.  

Dto klasserna har också använts flitigt för att begränsa vilken data som visas för användaren. Jag skapar olika Dto's för flera av mina entiteter för att på ett bra sätt kunna begränsa det som användaren ser. 

Min klient använder en enkel fetch funktion där jag hämtar alla filmer i mitt api och skriver ut dem på webbläsaren.
