PodCastPipocaAgilApi  🚀


# Configurando o Ambiente de Desenvolvimento
Seja bem-vindo ao projeto! Siga as etapas abaixo para configurar seu ambiente de desenvolvimento e começar a contribuir.

### Requisitos Prévios
.NET Core SDK:<br>
Certifique-se de ter o .NET Core SDK instalado em seu sistema.<br>
Configuração do Entity Framework Core<br>
Instale a Ferramenta dotnet-ef:


### Crie um projeto digitando o seguinte comando:
```sh
    dotnet new webapi --name  PodCastPipocaAgilApi
```

### Execute os seguintes comandos para instalar e atualizar a ferramenta dotnet-ef:
```sh
    dotnet tool install --global dotnet-ef
    dotnet tool update --global dotnet-ef
```

### Execute os seguintes comandos para adicionar os pacotes necessários do Entity Framework Core:
```sh
    dotnet add package Microsoft.EntityFrameworkCore
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.EntityFrameworkCore.SqlServer
    dotnet add package Microsoft.EntityFrameworkCore.Tools
```

### Execute o seguinte comando para instalar o pacote do SQL Server e ADO.NET:
```sh
    dotnet add package Microsoft.Data.SqlClient --version 5.2.0-preview3.23201.1
```


### Gerenciamento de Migrações com Entity Framework Core

### [Migrations](https://learn.microsoft.com/pt-br/ef/core/cli/dotnet#dotnet-ef-migrations-list)


```sh
    dotnet ef migrations add Start --context DataContext
    dotnet ef database update --context DataContext
    dotnet ef database instead. --context DataContext

    dotnet ef migrations remove  --context DataContext

    dotnet ef dbcontext info --context DataContext
    dotnet ef dbcontext list --context DataContext
    dotnet ef dbcontext script

    select @@version;
    8.0.23

    DROP TABLE "__EFMigraçõesHistórico"
    delete from __EFMigraçõesHistórico;
```

### Documentação da API

### Configuração do Swagger

A configuração do Swagger foi realizada da seguinte forma:

- **Título:** PodquestPipocaWebApi
- **Versão:** v1
- **Descrição:** Funcionalidade 'Trilha de Conhecimento' no Podcast Pipoca Ágil. Explore vídeos sobre Metodologias Ágeis, Inovação, Gerenciamento de Projetos, e mais. Acesse trilhas personalizadas, e receba recomendações gratuitamente.
- **Termos de Serviço:** [Termos de Serviço](https://meusite.com)
- **Contato:** Victor Sérgio, [meusite.com](https://meusite.com)
- **Licença:** Podquest Pipoca Agil, [Detalhes da Licença](https://meusite.com)