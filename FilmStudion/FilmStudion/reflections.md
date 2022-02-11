## REST

**Registrering**

För att lösa kravet med registrering används i mitt API två olika åtkomstpunkter för de olika användarna som ska finnas.
För att en admin ska registrerea sig så används här en DTO som ärver från interface:t ```IUserRegister```. Samma Dto mappas även ihop 
med domänklasssen ```User.cs```. ```UserRegisterDto.cs``` skickas sedan in som parameter till min åtkomstpunkt och skickar dörmed in nödvändig data 
som behövs för att en admin ska kunna registrera sig korrekt.  Till åtkomstpunkten används även crud funktioner från ```UserRepositories``` där den ena mnetoden
kollar ifall en användare med samma namn och lösenord finns och den andra skapar helt eneklt den nya användaren. I metoden ```Register()``` har jag också gjort så att en när en 
användare från denna åtkomnstpunkt registreras får den användarewn automatiskt rollen som admin. Den nyskapade användaren returneras sedan som ett anonymt objekt och i response-bodyn
visas uppgiferna för den nyskapade användaren, dock så har lösenordet valts att exkludera i responsen. 

