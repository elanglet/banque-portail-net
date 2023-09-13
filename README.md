-- Installation des outils (si nécessaire) 

    -> dotnet tool install --global dotnet-ef

-- Initialisation du projet

    -> dotnet new webapp
    
-- Ajout de dépendances

    -> dotnet add package Microsoft.EntityFrameworkCore
    -> dotnet add package Microsoft.EntityFrameworkCore.Tools
    -> dotnet add package Pomelo.EntityFrameworkCore.MySql
    //-> dotnet add package Microsoft.AspNetCore.Session
    //-> dotnet add package Microsoft.Extensions.Configuration.Binder

-- Génération à partir d'une base 

    -> dotnet ef dbcontext scaffold "server=127.0.0.1;uid=banque-user;pwd=banque-user;database=banque" Pomelo.EntityFrameworkCore.MySql -o Helpers -f