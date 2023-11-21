PodCastPipocaAgilApi  🚀


# Configurando o Ambiente de Desenvolvimento
Seja bem-vindo ao projeto! Siga as etapas abaixo para configurar seu ambiente de desenvolvimento e começar a contribuir.

### Requisitos Prévios
.NET Core SDK:<br>
Certifique-se de ter o .NET Core SDK instalado em seu sistema.<br>
Configuração do Entity Framework Core<br>
Instale a Ferramenta dotnet-ef:
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
